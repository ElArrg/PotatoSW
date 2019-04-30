using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Text.RegularExpressions;

namespace PotatoSW
{
    public partial class WorkPlace : Form
    {
        FileParser fileParser;

        public WorkPlace()
        {
            fileParser = new FileParser();
            InitializeComponent();
        }

        private void SalirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CargarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "CSV Files (*.csv)|*.csv|DATA Files (*.data)|*.data";
                openDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    fileParser.FilePath = openDialog.FileName;
                    fileParser.LoadFile();
                    datasetGrid.DataSource = fileParser.ReadData();
                    datasetGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }
            }
        }
        
        private string DataToString()
        {
            string data = "";

            foreach(DataGridViewRow row in datasetGrid.Rows)
            {
                if (!row.IsNewRow)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (!cell.OwningColumn.Name.Equals("ID"))
                        {
                            data += cell.Value.ToString() + ',';
                        }
                    }
                    data = data.TrimEnd(',') + '\n';
                }
            }

            return data.Trim();
        }

        private void GuardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileParser.SaveData(fileParser.FilePath, DataToString());
        }

        private void GuardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileParser != null)
            {
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    string filePath;
                    saveDialog.CreatePrompt = true;
                    saveDialog.OverwritePrompt = true;
                    saveDialog.FileName = fileParser.Relation;
                    saveDialog.DefaultExt = "data";
                    saveDialog.Filter = "DATA Files (*.data)|*.data|CSV Files (*.csv)|*.csv";
                    filePath = (fileParser.FilePath == "") ?
                        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) :
                        fileParser.FilePath;
                    saveDialog.InitialDirectory = filePath;

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        fileParser.SaveData(saveDialog.FileName, DataToString());
                    }

                }
            }
        }
        
        private void ArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            datasetGrid.EndEdit();
        }

        void DatasetGrid_MouseClick(object sender, MouseEventArgs e)
        {
            ContextMenuStrip menu = new ContextMenuStrip();

            if (e.Button == MouseButtons.Right)
            {

                menu.Items.Add("Seleccionar").Name = "Seleccionar";

            }

            menu.Show(datasetGrid, new Point(e.X, e.Y));
        }
    }
}
