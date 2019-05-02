using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PotatoSW
{
    public class FileParser
    {        
        // Symbolic constants
        private const int MAX_CHARS_PER_LINE = 70;
        private const int UNSET_DATA_INDEX = -1;
        private const string METATAGS_PATTERN = @"^(?<TAG_TYPE>%%|@(?:relation|attribute|missingValue|data))\ ?(?<TAG_CONTENT>.*)$";
        private const string COMMENT_TAG = "%%";
        private const string RELATION_TAG = "@relation";
        private const string ATTRIBUTE_TAG = "@attribute";
        private const string MISSING_VALUE_TAG = "@missingValue";
        private const string DATA_TAG = "@data";

        public enum FileTypes { EMPTY, CSV, METADATA };

        // Attributes
        private string filePath;
        private FileTypes fileType;
        private string comments;
        private string relation;
        private string missingValue;
        private List<Attribute> attributes = null;
        private int dataIndex;
        
        // CTOR
        public FileParser()
        {
            fileType = FileTypes.EMPTY;
            comments = "Dataset description.";
            relation = "Dataset_name";
            attributes = new List<Attribute>();
            dataIndex = UNSET_DATA_INDEX;
        }

        public FileParser(string filePath) : this() => this.FilePath = filePath;

        // Methods
        private string[] ReadFile()
        {
            string[] lines = null;

            if (File.Exists(FilePath))
            {
                lines = File.ReadAllLines(filePath, Encoding.Default);
            }

            return lines;
        }

        public void LoadFile()
        {
            Regex regex;
            List<Match> matches;
            string[] fileContent;
            Attributes.Clear();

            fileContent = ReadFile();

            if (fileContent != null)
            {
                regex = new Regex(METATAGS_PATTERN, RegexOptions.Multiline | RegexOptions.Compiled);
                matches = new List<Match>();

                for (int i = 0; i < fileContent.Length; i++)
                {
                    Match match = regex.Match(fileContent[i]);

                    if (match.Success)
                    {
                        matches.Add(match);
                    }
                }

                //matchCollection = regex.Matches(fileContent);

                if (matches.Count > 0)
                {
                    FileType = FileTypes.METADATA;
                    ParseMetadata(matches.ToArray());
                }
                else
                {
                    FileType = FileTypes.CSV;
                    ParseHeaders(fileContent[0]);
                }
            }
        }

        private void ParseMetadata(Match[] matches)
        {
            string comments = "";

            for(int i = 0; i < matches.Length; i++)
            {
                switch (matches[i].Groups["TAG_TYPE"].Value)
                {
                    case COMMENT_TAG:
                        {
                            comments += ' ' + matches[i].Groups["TAG_CONTENT"].Value;
                            break;
                        }
                    case RELATION_TAG:
                        {
                            Relation = matches[i].Groups["TAG_CONTENT"].Value;
                            break;
                        }
                    case ATTRIBUTE_TAG:
                        {
                            Attributes.Add(ParseAttribute(matches[i].Groups["TAG_CONTENT"].Value));
                            break;
                        }
                    case MISSING_VALUE_TAG:
                        {
                            MissingValue = matches[i].Groups["TAG_CONTENT"].Value;
                            break;
                        }
                    case DATA_TAG:
                        {
                            DataIndex = i + 1;
                            break;
                        }
                    default:
                        {
                            comments = string.Format("[!]Etiqueta no soportada: <TAG_TYPE>='{0}', <TAG_CONTENT>='{1}'\n", matches[i].Groups["TAG_TYPE"], matches[i].Groups["TAG_CONTENT"]) + comments;
                            break;
                        }
                }
            }

            Comments = comments.Trim();
        }

        private Attribute ParseAttribute(string attributeData)
        {
            Attribute attribute;
            string[] elements;

            elements = attributeData.Split(new char[] { ' ' }, 3);

            attribute = new Attribute(elements[0], elements[1], elements[2]);

            return attribute;
        }

        private void ParseHeaders(string headersLine)
        {
            DataIndex = 1;

            foreach(string header in headersLine.Split(','))
            {
                Attributes.Add(new Attribute(header));
            }
        }

        public DataTable ReadData()
        {
            string[] rows;
            DataTable dataTable = null;

            dataTable = new DataTable(Relation);
            rows = ReadFile();
            dataTable.Columns.Add("ID");

            // Headers as columns into table
            foreach(Attribute attribute in Attributes)
            {
                dataTable.Columns.Add(attribute.Name);
            }

            // Data values into table on each column.
            for(int i = DataIndex; i < rows.Length; i++)
            {
                DataRow dataRow = dataTable.NewRow();
                string[] values = (i.ToString() + ',' + rows[i]).Split(',');

                dataRow.ItemArray = values;
                
                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }

        public void SaveData(string filePath, string data)
        {            
            if (!FilePath.Equals(filePath))
            {
                FilePath = filePath;

                switch (Path.GetExtension(filePath))
                {
                    default:
                    case ".data":
                        {
                            FileType = FileTypes.METADATA;
                            break;
                        }
                    case ".csv":
                        {
                            FileType = FileTypes.CSV;
                            break;
                        }
                }
            }

            switch (FileType)
            {
                default:
                case FileTypes.METADATA:
                    {
                        SaveAsMetadata(filePath, data);
                        break;
                    }
                case FileTypes.CSV:
                    {
                        SaveAsCsv(filePath, data);
                        break;
                    }
            }
        }
        
        private void SaveAsMetadata(string filepath, string data)
        {
            string buffer;
            string tagFormat = "{0} {1}\n";
            string attributeFormat = "{0} {1} {2} {3}\n";
            Regex regex;
            MatchCollection matchCollection;

            using (StreamWriter streamWriter = new StreamWriter(filepath, false, Encoding.Default))
            {
                buffer = "";

                // Limits a comment line length to MAX_CHARS_PERS_LINE and wraps it into a new comment line if exceeds it.
                regex = new Regex(@"(?<TEXT>.{1," + MAX_CHARS_PER_LINE + @"})(?:\s|$)", RegexOptions.Multiline);
                matchCollection = regex.Matches(Comments.Trim('\n', ' '));

                foreach (Match match in matchCollection)
                {
                    buffer += string.Format(tagFormat, COMMENT_TAG, match.Groups["TEXT"].Value);
                }

                // Writes relation tag
                buffer += string.Format(tagFormat, RELATION_TAG, Relation);

                // Writes out all attributes as meta tag.
                foreach (Attribute attribute in Attributes)
                {
                    buffer += string.Format(attributeFormat, ATTRIBUTE_TAG, attribute.Name, attribute.DataType, attribute.Domain);
                }

                // Writes missing value as meta tag;
                buffer += string.Format(tagFormat, MISSING_VALUE_TAG, MissingValue);
                buffer += DATA_TAG + '\n';
                buffer += data;

                streamWriter.Write(buffer);
                streamWriter.Flush();
                streamWriter.Close();
            }
        }

        private void SaveAsCsv(string filepath, string data)
        {
            string buffer;

            using (StreamWriter streamWriter = new StreamWriter(filepath, false, Encoding.Default))
            {
                buffer = "";

                for (int i = 0; i < Attributes.Count; i++)
                {
                    buffer += Attributes[i].Name;
                    buffer += (i + 1 != Attributes.Count) ? ',' : '\n';
                }

                buffer += data;
                streamWriter.Write(buffer);
                streamWriter.Flush();
                streamWriter.Close();
            }
        }

        // Properties
        public string FilePath { get => filePath; set => filePath = value; }
        internal FileTypes FileType { get => fileType; set => fileType = value; }
        public string Comments { get => comments; set => comments = value; }
        public string Relation { get => relation; set => relation = value; }
        public string MissingValue { get => missingValue; set => missingValue = value; }
        internal List<Attribute> Attributes { get => attributes; set => attributes = value; }
        public int DataIndex { get => dataIndex; set => dataIndex = value; }
    }
}
