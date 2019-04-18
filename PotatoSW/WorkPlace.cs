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
        string archivo;

        public WorkPlace()
        {
            InitializeComponent();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cargarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = "c:\\Downloads";
            ofd.Filter = "CSV Files (*.csv)|*.csv|DATA Files (*.data)|*.data";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                archivo = ofd.FileName;
                OpenDataCSV(archivo);
            }
        }

        private void OpenDataCSV(string filePath)
        {
            DataTable dt = new DataTable();

            string[] lines = System.IO.File.ReadAllLines(filePath, Encoding.Default);

            if (lines.Length > 0)
            {

                string firstLine = lines[0];
                string[] headerLabels = firstLine.Split(',');

                foreach (string headerWord in headerLabels)
                {
                    dt.Columns.Add(new DataColumn(headerWord));
                }

                for (int r = 1; r < lines.Length; r++)
                {
                    string[] dataWord = lines[r].Split(',');
                    DataRow dr = dt.NewRow();
                    int columnIndex = 0;
                    foreach (string headerWord in headerLabels)
                    {
                        dr[headerWord] = dataWord[columnIndex++];
                    }

                    dt.Rows.Add(dr);
                }
            }

            if (dt.Rows.Count > 0)
            {
                CSVData.DataSource = dt;
            }

            this.CSVData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            CSVData.MouseClick += new MouseEventHandler(CSVData_MouseClick);
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.InitialDirectory = "c:\\Downloads";
            sfd.Filter = "CSV Files (*.csv)|*.csv|DATA Files (*.data)|*.data";

            if (CSVData.Rows.Count == 0)
            {
                return;
            }
            else
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                SaveAsDataCSV(sfd.FileName);
            }
        }

        private void SaveAsDataCSV(string filePath)
        {
            StringBuilder csvMemoria = new StringBuilder();

            string columnsHeader = "";

            //para los títulos de las columnas
            for (int i = 0; i < CSVData.Columns.Count; i++)
            {
                if (i == CSVData.Columns.Count - 1)
                {
                    columnsHeader += CSVData.Columns[i].Name;
                }
                else
                {
                    columnsHeader += CSVData.Columns[i].Name + ",";
                }
            }

            csvMemoria.Append(columnsHeader + Environment.NewLine);

            for (int m = 0; m < CSVData.Rows.Count; m++)
            {
                for (int n = 0; n < CSVData.Columns.Count; n++)
                {
                    //si es la última columna no poner el ,
                    if (n == CSVData.Columns.Count - 1)
                    {
                        csvMemoria.Append(CSVData.Rows[m].Cells[n].Value);
                    }
                    else
                    {
                        csvMemoria.Append(CSVData.Rows[m].Cells[n].Value + ",");
                    }
                }
                csvMemoria.AppendLine();
            }
            System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath, true, System.Text.Encoding.Default);
            sw.Write(csvMemoria.ToString());
            sw.Close();
        }

        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CSVData.EndEdit();
        }

        void CSVData_MouseClick(object sender, MouseEventArgs e)
        {
            ContextMenuStrip menu = new ContextMenuStrip();

            if (e.Button == MouseButtons.Right)
            {

                menu.Items.Add("Seleccionar").Name = "Seleccionar";

            }

            menu.Show(CSVData, new Point(e.X, e.Y));
        }
    }
}
