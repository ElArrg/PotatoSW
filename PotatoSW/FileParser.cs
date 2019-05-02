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
    /// <summary>
    /// Representa un dataset.
    /// </summary>
    public class FileParser
    {        
        // Symbolic constants
        /// <summary>
        /// Cantidad máxima de caracteres por línea para un comentario en un archivo de metadatos.
        /// </summary>
        private const int MAX_CHARS_PER_LINE = 70;
        /// <summary>
        /// Especifica el valor de indice cuando no se ha cargado un archivo de dataset.
        /// </summary>
        private const int UNSET_DATA_INDEX = -1;
        /// <summary>
        /// Expresión regular que representa el patron de las etiquetas de un archivo con formato de metadatos.
        /// </summary>
        private const string METATAGS_PATTERN = @"^(?<TAG_TYPE>%%|@(?:relation|attribute|missingValue|data))\ ?(?<TAG_CONTENT>.*)$";
        /// <summary>
        /// Etiqueta que indica un comentario en un archivo con fomrato de metadatos.
        /// </summary>
        private const string COMMENT_TAG = "%%";
        /// <summary>
        /// Etiqueta que indica el nombre del dataset en un archivo con formato de metadatos.
        /// </summary>
        private const string RELATION_TAG = "@relation";
        /// <summary>
        /// Etiqueta que indica la estructura de un atributo del dataset en un archivo con formato de metadatos.
        /// </summary>
        private const string ATTRIBUTE_TAG = "@attribute";
        /// <summary>
        /// Etiqueta que indica la cadena de texto que especifica un valor faltante en el dataset en un archivo con formato de metadatos.
        /// </summary>
        private const string MISSING_VALUE_TAG = "@missingValue";
        /// <summary>
        /// Etiqueta que indica el inicio de las instancias del dataset en la siguiente linea en un archivo con formato de metadatos.
        /// </summary>
        private const string DATA_TAG = "@data";
        /// <summary>
        /// Especifica el formato de archivo para el dataset actual.
        /// </summary>
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
        /// <summary>
        /// Lee el archivo especificado para el dataset.
        /// </summary>
        /// <returns>Las lineas del archivo.</returns>
        private string[] ReadFile()
        {
            string[] lines = null;

            if (File.Exists(FilePath))
            {
                lines = File.ReadAllLines(filePath, Encoding.Default);
            }

            return lines;
        }

        /// <summary>
        /// Carga la información que describe al dataset desde el archivo que representa.
        /// </summary>
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

        /// <summary>
        /// Obtiene los valores de las etiquetas de metadatos identificadas para el dataset.
        /// </summary>
        /// <param name="matches">Etiquetas de metadatos identificadas.</param>
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

        /// <summary>
        /// Crea un atributo.
        /// </summary>
        /// <param name="attributeData">Datos del nuevo atributo.</param>
        /// <returns>El atributo descrito.</returns>
        private Attribute ParseAttribute(string attributeData)
        {
            Attribute attribute;
            string[] elements;

            elements = attributeData.Split(new char[] { ' ' }, 3);

            attribute = new Attribute(elements[0], elements[1], elements[2]);

            return attribute;
        }

        /// <summary>
        /// Obtiene los atributos (cabeceras) del dataset.
        /// </summary>
        /// <param name="headersLine">Atributos separados por coma.</param>
        private void ParseHeaders(string headersLine)
        {
            DataIndex = 1;

            foreach(string header in headersLine.Split(','))
            {
                Attributes.Add(new Attribute(header));
            }
        }

        /// <summary>
        /// Carga desde el archivo las instancias separadas por coma del dataset.
        /// </summary>
        /// <returns>El conjunto de datos representado en forma de tabla.</returns>
        public DataTable ReadData()
        {
            string[] rows;
            DataTable dataTable = null;

            dataTable = new DataTable(Relation);
            rows = ReadFile();

            // Headers as columns into table
            foreach(Attribute attribute in Attributes)
            {
                dataTable.Columns.Add(attribute.Name);
            }

            // Data values into table on each column.
            for(int i = DataIndex; i < rows.Length; i++)
            {
                DataRow dataRow = dataTable.NewRow();

                dataRow.ItemArray = rows[i].Split(',');
                
                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }

        /// <summary>
        /// Escribe en un archivo la descripción del dataset.
        /// </summary>
        /// <param name="filePath">Ruta en disco del archivo.</param>
        /// <param name="data">Las instancias en formato CSV del dataset.</param>
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
        
        /// <summary>
        /// Escribe en un archivo con formato de metadatos la descripción del dataset.
        /// </summary>
        /// <param name="filepath">Ruta del archivo en disco.</param>
        /// <param name="data">Instancias del dataset en formato CSV.</param>
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

        /// <summary>
        /// Escribe en un archivo con formato CSV la descripción del dataset.
        /// </summary>
        /// <param name="filepath">Ruta en disco del archivo.</param>
        /// <param name="data">Instancias del dataset en formato CSV.</param>
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
        /// <summary>
        /// Ruta en disco del archivo que describe este dataset.
        /// </summary>
        public string FilePath { get => filePath; set => filePath = value; }
        /// <summary>
        /// Formato del archivo para este dataset.
        /// </summary>
        internal FileTypes FileType { get => fileType; set => fileType = value; }
        /// <summary>
        /// [Metadato] Información general del dataset. 
        /// </summary>
        public string Comments { get => comments; set => comments = value; }
        /// <summary>
        /// Nombre del dataset.
        /// </summary>
        public string Relation { get => relation; set => relation = value; }
        /// <summary>
        /// Especifica la cadena utilizada para determinar un valor faltante en el dataset.
        /// </summary>
        public string MissingValue { get => missingValue; set => missingValue = value; }
        /// <summary>
        /// Lista de atributos (cabeceras) del dataset.
        /// </summary>
        internal List<Attribute> Attributes { get => attributes; set => attributes = value; }
        /// <summary>
        /// Linea del archivo en la que inician las instancias del dataset.
        /// </summary>
        public int DataIndex { get => dataIndex; set => dataIndex = value; }
    }
}
