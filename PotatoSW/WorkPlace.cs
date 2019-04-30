﻿using System;
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
        public WorkPlace()
        {
            InitializeComponent();
        }

        // Variables que recolectan los datos de 2 columnas diferentes para utilizar el metodo de Pearson.
        List<double> datosC1Pearson = new List<double>();
        List<double> datosC2Pearson = new List<double>();

        // Variables que recolectan los datos de 2 columnas diferentes para utilizar el metodo de Tschprow.
        List<string> datosC1Tschprow = new List<string>();
        List<string> datosC2Tschprow = new List<string>();

        // Variables que almacen las diferentes palabras que se encuentran dentro de los datos.
        List<string> contadorPalabrasC1 = new List<string>();
        List<string> contadorPalabrasC2 = new List<string>();

        // Variables que almacen el indice de las columnas.
        string columnaSelec1;
        string columnaSelec2;

        // Variables utilizadas para la activacion de las celdas.
        bool activarC1 = false;
        bool activarC2 = false;

        // Variable que almacen la dirreccion y extencion del archivo leido.
        string direccionArchivo;
        string extencionArchivo;

        // Variables que almacenan la celda seleccionada para editar.
        string celdaSelect;
        string celdaEdit;
        int fila;
        int columna;

        // Variable que almacenan la cantidad de datos faltantes y totales.
        int valoresF;
        int valoresT;

        // Variable que se activa al realizar un cambio.
        bool modificacion = false;

        // Variable que se activa al encontrar celda con valores incorrectos.
        bool valoresVacios = false;

        // Variable que almacena los tipos de columna.
        List<string> tipoColumna = new List<string>();

        // Variables que almacena datos acerca de la columna seleccionada.
        int indiceColumna = 0;
        public string nombreColumna;

        // Variable de la clase que tiene Pearson y Tschprow.
        AnálisisEstadístico análisis = new AnálisisEstadístico();

        // Variables que almacenaran la frecuencia y los datos para el boxplot.
        List<string> frecuencia = new List<string>();
        List<double> boxPlot = new List<double>();

        // Variable que almacena los diferentes valores encontrados y su frecuencia. 
        public List<Tuple<string, int>> frecuenciaP = new List<Tuple<string, int>>();

        // Variable de la clase que tiene las operaciones matematicas necesarias para realizar los metodos.
        OperacionesMatemáticas operaciones = new OperacionesMatemáticas();

        // Expreciones regulares que permiten identificar si un valor es numerico o string.
        Regex Valnum = new Regex(@"[0-9]{1,9}(\.[0-9]{0,2})?$");
        Regex Valletra = new Regex(@"[a-zA-ZñÑ\s]");

        // ----------------------------------------------- FUNCIONES DEL ARCHIVO ------------------------------------------------------

        // Funcion que manda los valores al metodo para cargar archivo.
        private void cargarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = "c:\\Downloads";
            ofd.Filter = "CSV Files (*.csv)|*.csv|DATA Files (*.data)|*.data";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                direccionArchivo = ofd.FileName;
                OpenDataCSV(direccionArchivo);

                nombreR.Visible = true;
                nombreR.Text = Path.GetFileName(direccionArchivo);
            }
        }

        // Funcion que manda los valores al metodo para sobreescribir los datos.
        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CSVData.Rows.Count == 0)
            {
                return;
            }
            else
            {
                SaveDataCSV(direccionArchivo);
            }
        }

        // Funcion que manda los valores al metodo para guardar como.
        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.InitialDirectory = "c:\\Downloads";
            sfd.Filter = "CSV Files (*.csv)|*.csv|DATA Files (*.data)|*.data";

            if (CSVData.Rows.Count == 0)
            {
                return;
            }
            else if (sfd.ShowDialog() == DialogResult.OK)
            {
                SaveAsDataCSV(sfd.FileName);
            }
        }

        // Funcion para cerrar del programa.
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CSVData.Rows.Count == 0)
            {
                this.Close();
            }
            else
            {
                if (!modificacion)
                {
                    this.Close();
                }
                else
                {
                    if (MessageBox.Show("¿Guardar antes de salir del sistema?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        SaveDataCSV(direccionArchivo);
                        this.Close();
                    }
                    else
                        this.Close();
                }
            }
        }

        // Funcion para desplegar un menu en el gridview.
        private void CSVData_MouseClick(object sender, MouseEventArgs e)
        {
            ContextMenuStrip menu = new ContextMenuStrip();

            if (e.Button == MouseButtons.Right)
            {
                menu.Items.Add("Seleccionar conunto de datos(Univariable)").Name = "conjunto";
                menu.Items.Add("-");
                menu.Items.Add("Seleccionar columna 1(Bivariable)").Name = "Columna 1";
                menu.Items.Add("Seleccionar columna 2(Bivariable)").Name = "Columna 2";
                menu.Items.Add("-");
                menu.Items.Add("Agregar o modificar expresión regular").Name = "expresión regular";
                menu.Items.Add("Editar nombre de la columna").Name = "editar";
                menu.Items.Add("-");
                menu.Items.Add("Agregar atributo").Name = "Agregar atributo";
                menu.Items.Add("Eliminar atributo").Name = "Eliminar atributo";
                menu.Items.Add("-");
                menu.Items.Add("Agregar instacia").Name = "Agregar instacia";
                menu.Items.Add("Eliminar instancia").Name = "Eliminar instancia";

            }

            menu.Show(CSVData, new Point(e.X, e.Y));
            menu.ItemClicked += new ToolStripItemClickedEventHandler(menu_ClickedItem);
        }

        // Funcion para realizar operaciones con la opciones seleccionada.
        private void menu_ClickedItem(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name.ToString())
            {
                case "conjunto":
                    multiUso.Visible = false;
                    indiceColumna = CSVData.CurrentCell.ColumnIndex;
                    llenarListasUni();
                    break;
                case "Columna 1":
                    columnaSelec1 = CSVData.CurrentCell.ColumnIndex.ToString();
                    activarC1 = true;
                    llenarListasBi(activarC1, activarC2);
                    break;
                case "Columna 2":
                    columnaSelec2 = CSVData.CurrentCell.ColumnIndex.ToString();
                    activarC2 = true;
                    llenarListasBi(activarC1, activarC2);
                    break;
                case "expresión regular":
                    break;
                case "editar":
                    indiceColumna = CSVData.CurrentCell.ColumnIndex;
                    nombreColumna = CSVData.Columns[indiceColumna].Name;
                    modificarNombreC();
                    break;
                case "Agregar atributo":
                    break;
                case "Eliminar atributo":
                    if (MessageBox.Show(new Form() { TopMost = true }, "¿Eliminar atributo?", "Eliminacion de atributo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        CSVData.Columns.RemoveAt(CSVData.CurrentCell.ColumnIndex);
                        modificacion = true;
                        verificarDatos();
                    }
                    break;
                case "Agregar instacia":
                    break;
                case "Eliminar instancia":
                    if (MessageBox.Show(new Form() { TopMost = true }, "¿Eliminar instancia?", "Eliminacion de instancia", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        CSVData.Rows.RemoveAt(CSVData.CurrentRow.Index);
                        modificacion = true;
                        verificarDatos();
                    }
                    break;
            }
        }

        // ----------------------------------------------- FUNCIONES DEL ANALISIS ------------------------------------------------------

        // Funcion que muestra el resultado del metodo de Pearson.
        private void pearsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resultadoR.Text = string.Format("{0:n5}", (análisis.Pearson(datosC1Pearson, datosC2Pearson)));
        }

        // Funcion que muestra el resultado del metodo de Tschuprow.
        private void tschprowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resultadoR.Text = string.Format("{0:n5}", (análisis.Tschuprow(contadorPalabrasC1, contadorPalabrasC2, datosC1Tschprow, datosC2Tschprow)));
        }

        // Funcion que muestra el resultado del metodo de media.
        private void mediaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            multiUso.Text = "Media: " + string.Format("{0:n5}", (operaciones.Media(boxPlot)));
            multiUso.Visible = true;
        }

        // Funcion que muestra el resultado del metodo de mediana.
        private void medianaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            multiUso.Text = "Mediana: " + operaciones.Mediana(boxPlot);
            multiUso.Visible = true;
        }

        // Funcion que muestra el resultado del metodo de moda.
        private void modaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            multiUso.Text = "Moda: " + operaciones.ModaNum(boxPlot);
            multiUso.Visible = true;
        }

        // Funcion que muestra el resultado del metodo de desviacion estandar.
        private void desviaciónEstándarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            multiUso.Text = "Desviación Estándar: " + string.Format("{0:n5}", operaciones.desviaciónEstándar(boxPlot));
            multiUso.Visible = true;
        }

        // Funcion que manda los valores a la grafica de boxplot.
        private void boxPlotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gráficas graficas = new Gráficas();

            graficas.valoresBP(boxPlot, nombreColumna);
            graficas.Show();
        }

        // Funcion que manda los valores a la grafica de barras.
        private void frecuenciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gráficas graficas = new Gráficas();

            graficas.valoresFrecuencia(frecuenciaP);
            graficas.Show();
        }

        // ------------------------------- METODOS Y FUNCIONES UTILIZADAS UTILIZADOS PARA EL ARCHIVO Y GRIDVIEW -----------------------------

        // Metodo parar rellenar el gridview con los datos del documento que se esta leyendo.
        private void OpenDataCSV(string filePath)
        {
            extencionArchivo = Path.GetExtension(filePath);

            if (extencionArchivo == ".csv")
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

                verificarDatos();

                this.CSVData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                this.CSVData.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                CSVData.MouseClick += new MouseEventHandler(CSVData_MouseClick);
                CSVData.CellDoubleClick += CSVData_CellDoubleClick;
                CSVData.CellEndEdit += CSVData_CellEndEdit;
            }
            else
                MessageBox.Show("Lectura no disponible");
        }

        // Metodo para guadar los datos del gridview ya se como .csv o .data.
        private void SaveDataCSV(string filePath)
        {
            extencionArchivo = Path.GetExtension(filePath);

            if (extencionArchivo == ".csv")
            {

                StringBuilder csvMemoria = new StringBuilder();

                string columnsHeader = "";

                // Ciclo que se utiliza para definir las columnas.
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

                for (int m = 0; m < CSVData.Rows.Count - 1; m++)
                {
                    for (int n = 0; n < CSVData.Columns.Count; n++)
                    {
                        // Si es la última columna no poner la coma.
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
                System.IO.StreamWriter sw = new System.IO.StreamWriter(direccionArchivo, false, System.Text.Encoding.Default);
                sw.Write(csvMemoria.ToString());
                sw.Close();

                celdaSelect = default(string);
                celdaEdit = default(string);
            }
            else
                MessageBox.Show("Guardar no disponible");
        }

        // Metodo para guadar los datos del gridview ya se como .csv o .data.
        private void SaveAsDataCSV(string filePath)
        {
            extencionArchivo = Path.GetExtension(filePath);

            if (extencionArchivo == ".csv")
            {

                StringBuilder csvMemoria = new StringBuilder();

                string columnsHeader = "";

                // Ciclo que se utiliza para definir las columnas.
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

                for (int m = 0; m < CSVData.Rows.Count - 1; m++)
                {
                    for (int n = 0; n < CSVData.Columns.Count; n++)
                    {
                        // Si es la última columna no poner la coma.
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

                System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath, false, System.Text.Encoding.Default);
                sw.Write(csvMemoria.ToString());
                sw.Close();

                celdaSelect = default(string);
                celdaEdit = default(string);
            }
            else
                MessageBox.Show("Guardar como no disponible");
        }

        // Funcion para terminar la ediccion del gridview.
        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CSVData.IsCurrentCellInEditMode)
            {
                CSVData.EndEdit();
            }
        }

        // Funcion que empieza la ediccion de una celda al darle doble click.
        private void CSVData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            celdaSelect = CSVData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            fila = e.RowIndex;
            columna = e.ColumnIndex;
            CSVData.BeginEdit(true);
        }

        // Funcion que termina la edicion de la celda.
        private void CSVData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            celdaEdit = CSVData.Rows[fila].Cells[columna].Value.ToString();

            if (celdaEdit != celdaSelect || celdaSelect == null)
            {
                modificacion = true;
            }

            verificarDatos();
        }

        // Funcion que verifica si los datos son correctos y actualiza la informacion de la izquierda.
        private void verificarDatos()
        {
            int porciento = 100;
            double proporcion = 0.0;

            valoresF = 0;
            valoresT = 0;

            análisisToolStripMenuItem.Enabled = true;

            for (int numberOfColumns = 0; numberOfColumns < this.CSVData.Columns.Count; numberOfColumns++)
            {
                int valorNumerico = 0;
                int valorLetra = 0;

                for (int numberOfCells = 0; numberOfCells < this.CSVData.Rows.Count - 1; numberOfCells++)
                {
                    valoresT++;

                    if (string.IsNullOrEmpty(CSVData.Rows[numberOfCells].Cells[numberOfColumns].Value.ToString()) || string.IsNullOrWhiteSpace(CSVData.Rows[numberOfCells].Cells[numberOfColumns].Value.ToString()))
                    {
                        valoresVacios = true;
                    }
                    else
                        valoresVacios = false;

                    if (valoresVacios || CSVData.Rows[numberOfCells].Cells[numberOfColumns].Value.ToString() == "?")
                    {
                        CSVData.Rows[numberOfCells].Cells[numberOfColumns].Style.BackColor = Color.OrangeRed;
                        valoresF++;
                        CSVData.Rows[numberOfCells].Cells[numberOfColumns].Value = "?";
                        análisisToolStripMenuItem.Enabled = false;
                    }
                    else
                    {
                        CSVData.Rows[numberOfCells].Cells[numberOfColumns].Style.BackColor = Color.White;
                    }

                    var valorColumna = CSVData.Rows[numberOfCells].Cells[numberOfColumns].Value;

                    if (Valnum.IsMatch(valorColumna.ToString()))
                    {
                        valorNumerico++;
                    }
                    else if (Valletra.IsMatch(valorColumna.ToString()))
                    {
                        valorLetra++;
                    }
                }
                if (valorNumerico > valorLetra)
                {
                    tipoColumna.Add("Numerico");
                }
                else
                {
                    tipoColumna.Add("Categorico");
                }
                
            }

            verificarDominios();

            proporcion = ((double)(valoresF * porciento) / valoresT);

            cantidadAR.Visible = true;
            cantidadAR.Text = CSVData.ColumnCount.ToString();

            cantidadIR.Visible = true;
            cantidadIR.Text = ((CSVData.Rows.Count) - 1).ToString();

            valoresFR.Visible = true;
            valoresFR.Text = valoresF.ToString();

            proporcionVR.Visible = true;
            proporcionVR.Text = Math.Round(proporcion, 2).ToString();
        }

        // Funcion que verifica que los datos entren en el dominio de la columna.
        private void verificarDominios()
        {
            for (int numberOfColumns = 0; numberOfColumns < this.CSVData.Columns.Count; numberOfColumns++)
            {
                if (tipoColumna[numberOfColumns] == "Numerico")
                {
                    for (int numberOfCells = 0; numberOfCells < this.CSVData.Rows.Count - 1; numberOfCells++)
                    {
                        if (Valletra.IsMatch(CSVData.Rows[numberOfCells].Cells[numberOfColumns].Value.ToString()))
                        {
                            valoresF++;
                            CSVData.Rows[numberOfCells].Cells[numberOfColumns].Style.BackColor = Color.OrangeRed;
                            análisisToolStripMenuItem.Enabled = false;
                        }
                    }
                }
            }
        }

        // ------------------------------------ METODOS USADOS POR EL ANALISIS ESTADISTICO ----------------------------------

        private void llenarListasBi(bool column1, bool column2)
        {
            if (activarC1 && activarC2)
            {
                if (columnaSelec1 != columnaSelec2)
                {
                    if (tipoColumna[Int32.Parse(columnaSelec1)] == "Numerico" && tipoColumna[Int32.Parse(columnaSelec2)] == "Numerico")
                    {
                        datosC1Pearson.Clear();
                        datosC2Pearson.Clear();

                        for (int i = 0; i < CSVData.RowCount - 1; i++)
                        {
                            if (CSVData.Rows[i].Cells[Int32.Parse(columnaSelec1)].Value.ToString() != "")
                            {
                                datosC1Pearson.Add(Double.Parse(CSVData.Rows[i].Cells[Int32.Parse(columnaSelec1)].Value.ToString()));
                            }
                            else
                            {
                                datosC1Pearson.Add(0);
                            }
                        }

                        for (int i = 0; i < CSVData.RowCount - 1; i++)
                        {
                            if (CSVData.Rows[i].Cells[Int32.Parse(columnaSelec2)].Value.ToString() != "")
                            {
                                datosC2Pearson.Add(Double.Parse(CSVData.Rows[i].Cells[Int32.Parse(columnaSelec2)].Value.ToString()));
                            }
                            else
                            {
                                datosC2Pearson.Add(0);
                            }
                        }

                        tschprowToolStripMenuItem.Visible = false;
                        pearsonToolStripMenuItem.Visible = true;
                        activarC1 = false;
                        activarC2 = false;
                    }
                    else if (tipoColumna[Int32.Parse(columnaSelec1)] == "Categorico" && tipoColumna[Int32.Parse(columnaSelec2)] == "Categorico")
                    {
                        datosC1Tschprow.Clear();
                        datosC2Tschprow.Clear();

                        for (int i = 0; i < CSVData.RowCount - 1; i++)
                        {
                            if (CSVData.Rows[i].Cells[Int32.Parse(columnaSelec1)].Value.ToString() != "")
                            {
                                datosC1Tschprow.Add(CSVData.Rows[i].Cells[Int32.Parse(columnaSelec1)].Value.ToString());
                            }
                            else
                            {
                                datosC1Tschprow.Add("");
                            }
                        }

                        bool encontroColumna1;

                        foreach (string con in datosC1Tschprow)
                        {
                            encontroColumna1 = false;

                            for (int i = 0; i < contadorPalabrasC1.Count; i++)
                                if (contadorPalabrasC1[i] == con)
                                {

                                    encontroColumna1 = true;
                                    contadorPalabrasC1[i] = contadorPalabrasC1[i];

                                }

                            if (!encontroColumna1)
                            {

                                contadorPalabrasC1.Add(con);

                            }
                        }

                        for (int i = 0; i < CSVData.RowCount - 1; i++)
                        {
                            if (CSVData.Rows[i].Cells[Int32.Parse(columnaSelec2)].Value.ToString() != "")
                            {
                                datosC2Tschprow.Add(CSVData.Rows[i].Cells[Int32.Parse(columnaSelec2)].Value.ToString());
                            }
                            else
                            {
                                datosC2Tschprow.Add("");
                            }
                        }

                        bool encontroColumna2;

                        foreach (string con in datosC2Tschprow)
                        {
                            encontroColumna2 = false;

                            for (int i = 0; i < contadorPalabrasC2.Count; i++)
                                if (contadorPalabrasC2[i] == con)
                                {

                                    encontroColumna2 = true;
                                    contadorPalabrasC2[i] = contadorPalabrasC2[i];

                                }

                            if (!encontroColumna2)
                            {

                                contadorPalabrasC2.Add(con);

                            }
                        }

                        pearsonToolStripMenuItem.Visible = false;
                        tschprowToolStripMenuItem.Visible = true;
                        activarC1 = false;
                        activarC2 = false;
                    }
                    else
                    {
                        MessageBox.Show("NO SE PUEDE REALIZAR LA OPERACION", "Advertencia");
                        pearsonToolStripMenuItem.Visible = false;
                        tschprowToolStripMenuItem.Visible = false;
                        activarC1 = false;
                        activarC2 = false;
                    }
                }
                else
                {
                    MessageBox.Show("SE SELECCIONO LA MISMA COLUMNA", "Seleccion de columa 1");
                    activarC1 = false;
                }
            }
        }

        private void llenarListasUni()
        {
            if (tipoColumna[indiceColumna] == "Numerico")
            {
                boxPlot.Clear();

                nombreColumna = CSVData.Columns[indiceColumna].Name;

                for (int i = 0; i < CSVData.RowCount - 1; i++)
                {
                    if (CSVData.Rows[i].Cells[indiceColumna].Value.ToString() != "")
                    {
                        boxPlot.Add(Double.Parse(CSVData.Rows[i].Cells[indiceColumna].Value.ToString()));
                    }
                    else
                    {
                        boxPlot.Add(0);
                    }
                }

                activarFuncionesUnivariablesNum();
                frecuenciaToolStripMenuItem.Visible = false;
            }
            else if (tipoColumna[indiceColumna] == "Categorico")
            {
                frecuencia.Clear();
                frecuenciaP.Clear();

                for (int i = 0; i < CSVData.RowCount - 1; i++)
                {
                    if (CSVData.Rows[i].Cells[indiceColumna].Value.ToString() != "")
                    {
                        frecuencia.Add(CSVData.Rows[i].Cells[indiceColumna].Value.ToString());
                    }
                    else
                    {
                        frecuencia.Add("");
                    }
                }

                bool encontro;

                foreach (string con in frecuencia)
                {
                    encontro = false;

                    for (int i = 0; i < frecuenciaP.Count; i++)
                        if (frecuenciaP[i].Item1 == con)
                        {

                            encontro = true;
                            frecuenciaP[i] = new Tuple<string, int>(frecuenciaP[i].Item1, frecuenciaP[i].Item2 + 1);

                        }

                    if (!encontro)
                    {

                        frecuenciaP.Add(new Tuple<string, int>(con, 1));

                    }
                }

                desactivarFuncionesUnivariablesNum();
                frecuenciaToolStripMenuItem.Visible = true;
            }
        }

        private void modificarNombreC()
        {
            CambiarNombre nuevoNombre = new CambiarNombre();
            DialogResult respuesta = nuevoNombre.ShowDialog();

            if (respuesta == DialogResult.OK)
            {
                string nuevoNombreColumna = CSVData.Columns[indiceColumna].Name;

                CSVData.Columns[indiceColumna].HeaderText = nuevoNombre.nuevoNomb;

                if (nombreColumna != nuevoNombre.nuevoNomb)
                {
                    modificacion = true;
                }
            }
        }

        // ------------------------------------ ACTIVACION/DESACTIVACION DE BOTONES ----------------------------------

        private void desactivarFuncionesUnivariablesNum()
        {
            mediaToolStripMenuItem.Visible = false;
            medianaToolStripMenuItem.Visible = false;
            modaToolStripMenuItem.Visible = false;
            desviaciónEstándarToolStripMenuItem.Visible = false;
            boxPlotToolStripMenuItem.Visible = false;
        }

        private void activarFuncionesUnivariablesNum()
        {
            mediaToolStripMenuItem.Visible = true;
            medianaToolStripMenuItem.Visible = true;
            modaToolStripMenuItem.Visible = true;
            desviaciónEstándarToolStripMenuItem.Visible = true;
            boxPlotToolStripMenuItem.Visible = true;
        }
    }
}
