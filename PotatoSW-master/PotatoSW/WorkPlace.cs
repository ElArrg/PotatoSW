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

        int indiceNC;

        // Expreciones regulares que permiten identificar si un valor es numerico o string.
        Regex Valnum = new Regex(@"[0-9]{1,9}(\.[0-9]{0,2})?$");
        Regex Valletra = new Regex(@"[a-zA-ZñÑ\s]");

        // ----------------------------------------------- FUNCIONES DEL ARCHIVO ------------------------------------------------------
        private string DataToString()
        {
            string data = "";

            foreach (DataGridViewRow row in datasetGrid.Rows)
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

        // Funcion que manda los valores al metodo para cargar archivo.
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

                nombreR.Visible = true;
                nombreR.Text = Path.GetFileName(openDialog.FileName);

                VerificarDatos();

                this.datasetGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                this.datasetGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                datasetGrid.MouseClick += new MouseEventHandler(DatasetGrid_MouseClick);
                datasetGrid.CellDoubleClick += DatasetGrid_CellDoubleClick;
                datasetGrid.CellEndEdit += DatasetGrid_CellEndEdit;
            }
        }

        // Funcion que manda los valores al metodo para sobreescribir los datos.
        private void GuardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileParser.SaveData(fileParser.FilePath, DataToString());
        }

        // Funcion que manda los valores al metodo para guardar como.
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

        // Funcion para cerrar del programa.
        private void SalirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (datasetGrid.Rows.Count == 0)
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
                        fileParser.SaveData(fileParser.FilePath, DataToString());
                        this.Close();
                    }
                    else
                        this.Close();
                }
            }
        }

        // Funcion para desplegar un menu en el gridview.
        private void DatasetGrid_MouseClick(object sender, MouseEventArgs e)
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

            menu.Show(datasetGrid, new Point(e.X, e.Y));
            menu.ItemClicked += new ToolStripItemClickedEventHandler(Menu_ClickedItem);
        }

        // Funcion para realizar operaciones con la opciones seleccionada.
        private void Menu_ClickedItem(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name.ToString())
            {
                case "conjunto":
                    multiUso.Visible = false;
                    indiceColumna = datasetGrid.CurrentCell.ColumnIndex;
                    LlenarListasUni();
                    break;
                case "Columna 1":
                    columnaSelec1 = datasetGrid.CurrentCell.ColumnIndex.ToString();
                    activarC1 = true;
                    LlenarListasBi(activarC1, activarC2);
                    break;
                case "Columna 2":
                    columnaSelec2 = datasetGrid.CurrentCell.ColumnIndex.ToString();
                    activarC2 = true;
                    LlenarListasBi(activarC1, activarC2);
                    break;
                case "expresión regular":
                    indiceColumna = datasetGrid.CurrentCell.ColumnIndex;
                    MessageBox.Show("" + tipoColumna[indiceColumna] + " " + indiceColumna + " " + datasetGrid.Columns.Count + " " + datasetGrid.Rows.Count);
                    break;
                case "editar":
                    indiceColumna = datasetGrid.CurrentCell.ColumnIndex;
                    nombreColumna = datasetGrid.Columns[indiceColumna].Name;
                    ModificarNombreC();
                    break;
                case "Agregar atributo":
                    AgregarColumna();
                    modificacion = true;
                    VerificarDatos();
                    break;
                case "Eliminar atributo":
                    if (MessageBox.Show(new Form() { TopMost = true }, "¿Eliminar atributo?", "Eliminacion de atributo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        datasetGrid.Columns.RemoveAt(datasetGrid.CurrentCell.ColumnIndex);
                        tipoColumna.RemoveAt(datasetGrid.CurrentCell.ColumnIndex);
                        modificacion = true;
                        VerificarDatos();
                    }
                    break;
                case "Agregar instacia":

                    DataTable datatable = new DataTable();
                    datatable = datasetGrid.DataSource as DataTable;
                    DataRow datarow;

                    datarow = datatable.NewRow();
                    datarow[0].ToString();
                    datatable.Rows.Add(datarow);

                    VerificarDatos();
                    modificacion = true;
                    break;
                case "Eliminar instancia":
                    if (MessageBox.Show(new Form() { TopMost = true }, "¿Eliminar instancia?", "Eliminacion de instancia", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        datasetGrid.Rows.RemoveAt(datasetGrid.CurrentRow.Index);
                        modificacion = true;
                        VerificarDatos();
                    }
                    break;
            }
        }

        // ----------------------------------------------- FUNCIONES DEL ANALISIS ------------------------------------------------------

        // Funcion que muestra el resultado del metodo de Pearson.
        private void PearsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resultadoR.Text = string.Format("{0:n5}", (análisis.Pearson(datosC1Pearson, datosC2Pearson)));
        }

        // Funcion que muestra el resultado del metodo de Tschuprow.
        private void TschprowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resultadoR.Text = string.Format("{0:n5}", (análisis.Tschuprow(contadorPalabrasC1, contadorPalabrasC2, datosC1Tschprow, datosC2Tschprow)));
        }

        // Funcion que muestra el resultado del metodo de media.
        private void MediaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            multiUso.Text = "Media: " + string.Format("{0:n5}", (operaciones.Media(boxPlot)));
            multiUso.Visible = true;
        }

        // Funcion que muestra el resultado del metodo de mediana.
        private void MedianaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            multiUso.Text = "Mediana: " + operaciones.Mediana(boxPlot);
            multiUso.Visible = true;
        }

        // Funcion que muestra el resultado del metodo de moda.
        private void ModaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            multiUso.Text = "Moda: " + operaciones.ModaNum(boxPlot);
            multiUso.Visible = true;
        }

        // Funcion que muestra el resultado del metodo de desviacion estandar.
        private void DesviaciónEstándarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            multiUso.Text = "Desviación Estándar: " + string.Format("{0:n5}", operaciones.desviaciónEstándar(boxPlot));
            multiUso.Visible = true;
        }

        // Funcion que manda los valores a la grafica de boxplot.
        private void BoxPlotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gráficas graficas = new Gráficas();

            graficas.valoresBP(boxPlot, nombreColumna);
            graficas.Show();
        }

        // Funcion que manda los valores a la grafica de barras.
        private void FrecuenciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gráficas graficas = new Gráficas();

            graficas.valoresFrecuencia(frecuenciaP);
            graficas.Show();
        }

        // Funcion para terminar la ediccion del gridview.
        private void ArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (datasetGrid.IsCurrentCellInEditMode)
            {
                datasetGrid.EndEdit();
            }
        }

        // Funcion que empieza la ediccion de una celda al darle doble click.
        private void DatasetGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            celdaSelect = datasetGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            fila = e.RowIndex;
            columna = e.ColumnIndex;
            datasetGrid.BeginEdit(true);
        }

        // Funcion que termina la edicion de la celda.
        private void DatasetGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            celdaEdit = datasetGrid.Rows[fila].Cells[columna].Value.ToString();

            if (celdaEdit != celdaSelect || celdaSelect == null)
            {
                modificacion = true;
            }

            VerificarDatos();
        }

        // Funcion que verifica si los datos son correctos y actualiza la informacion de la izquierda.
        private void VerificarDatos()
        {
            int porciento = 100;
            double proporcion = 0.0;

            valoresF = 0;
            valoresT = 0;

            análisisToolStripMenuItem.Enabled = true;

            for (int numberOfColumns = 0; numberOfColumns < this.datasetGrid.Columns.Count; numberOfColumns++)
            {
                int valorNumerico = 0;
                int valorLetra = 0;

                for (int numberOfCells = 0; numberOfCells < this.datasetGrid.Rows.Count - 1; numberOfCells++)
                {
                    valoresT++;

                    if (string.IsNullOrEmpty(datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Value.ToString()) || string.IsNullOrWhiteSpace(datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Value.ToString()))
                    {
                        valoresVacios = true;
                    }
                    else
                        valoresVacios = false;

                    if (valoresVacios || datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Value.ToString() == "?")
                    {
                        datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Style.BackColor = Color.OrangeRed;
                        valoresF++;
                        datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Value = "?";
                        análisisToolStripMenuItem.Enabled = false;
                    }
                    else
                    {
                        datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Style.BackColor = Color.White;
                    }

                    var valorColumna = datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Value;

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

            VerificarDominios();

            proporcion = ((double)(valoresF * porciento) / valoresT);

            cantidadAR.Visible = true;
            cantidadAR.Text = datasetGrid.ColumnCount.ToString();

            cantidadIR.Visible = true;
            cantidadIR.Text = ((datasetGrid.Rows.Count) - 1).ToString();

            valoresFR.Visible = true;
            valoresFR.Text = valoresF.ToString();

            proporcionVR.Visible = true;
            proporcionVR.Text = Math.Round(proporcion, 2).ToString();
        }

        // Funcion que verifica que los datos entren en el dominio de la columna.
        private void VerificarDominios()
        {
            for (int numberOfColumns = 0; numberOfColumns < this.datasetGrid.Columns.Count; numberOfColumns++)
            {
                if (tipoColumna[numberOfColumns] == "Numerico")
                {
                    for (int numberOfCells = 0; numberOfCells < this.datasetGrid.Rows.Count - 1; numberOfCells++)
                    {
                        if (Valletra.IsMatch(datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Value.ToString()))
                        {
                            valoresF++;
                            datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Style.BackColor = Color.OrangeRed;
                            análisisToolStripMenuItem.Enabled = false;
                        }
                    }
                }
            }
        }

        // ------------------------------------ METODOS USADOS POR EL ANALISIS ESTADISTICO ----------------------------------

        private void LlenarListasBi(bool column1, bool column2)
        {
            if (activarC1 && activarC2)
            {
                if (columnaSelec1 != columnaSelec2)
                {
                    if (tipoColumna[Int32.Parse(columnaSelec1)] == "Numerico" && tipoColumna[Int32.Parse(columnaSelec2)] == "Numerico")
                    {
                        datosC1Pearson.Clear();
                        datosC2Pearson.Clear();

                        for (int i = 0; i < datasetGrid.RowCount - 1; i++)
                        {
                            if (datasetGrid.Rows[i].Cells[Int32.Parse(columnaSelec1)].Value.ToString() != "")
                            {
                                datosC1Pearson.Add(Double.Parse(datasetGrid.Rows[i].Cells[Int32.Parse(columnaSelec1)].Value.ToString()));
                            }
                            else
                            {
                                datosC1Pearson.Add(0);
                            }
                        }

                        for (int i = 0; i < datasetGrid.RowCount - 1; i++)
                        {
                            if (datasetGrid.Rows[i].Cells[Int32.Parse(columnaSelec2)].Value.ToString() != "")
                            {
                                datosC2Pearson.Add(Double.Parse(datasetGrid.Rows[i].Cells[Int32.Parse(columnaSelec2)].Value.ToString()));
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

                        for (int i = 0; i < datasetGrid.RowCount - 1; i++)
                        {
                            if (datasetGrid.Rows[i].Cells[Int32.Parse(columnaSelec1)].Value.ToString() != "")
                            {
                                datosC1Tschprow.Add(datasetGrid.Rows[i].Cells[Int32.Parse(columnaSelec1)].Value.ToString());
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

                        for (int i = 0; i < datasetGrid.RowCount - 1; i++)
                        {
                            if (datasetGrid.Rows[i].Cells[Int32.Parse(columnaSelec2)].Value.ToString() != "")
                            {
                                datosC2Tschprow.Add(datasetGrid.Rows[i].Cells[Int32.Parse(columnaSelec2)].Value.ToString());
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

        private void LlenarListasUni()
        {
            if (tipoColumna[indiceColumna] == "Numerico")
            {
                boxPlot.Clear();

                nombreColumna = datasetGrid.Columns[indiceColumna].Name;

                for (int i = 0; i < datasetGrid.RowCount - 1; i++)
                {
                    if (datasetGrid.Rows[i].Cells[indiceColumna].Value.ToString() != "")
                    {
                        boxPlot.Add(Double.Parse(datasetGrid.Rows[i].Cells[indiceColumna].Value.ToString()));
                    }
                    else
                    {
                        boxPlot.Add(0);
                    }
                }

                ActivarFuncionesUnivariablesNum();
                frecuenciaToolStripMenuItem.Visible = false;
            }
            else if (tipoColumna[indiceColumna] == "Categorico")
            {
                frecuencia.Clear();
                frecuenciaP.Clear();

                for (int i = 0; i < datasetGrid.RowCount - 1; i++)
                {
                    if (datasetGrid.Rows[i].Cells[indiceColumna].Value.ToString() != "")
                    {
                        frecuencia.Add(datasetGrid.Rows[i].Cells[indiceColumna].Value.ToString());
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

                DesactivarFuncionesUnivariablesNum();
                frecuenciaToolStripMenuItem.Visible = true;
            }
        }

        private void ModificarNombreC()
        {
            CambiarNombre nuevoNombre = new CambiarNombre();
            DialogResult respuesta = nuevoNombre.ShowDialog();

            if (respuesta == DialogResult.OK)
            {
                string nuevoNombreColumna = datasetGrid.Columns[indiceColumna].Name;

                datasetGrid.Columns[indiceColumna].HeaderText = nuevoNombre.nuevoNomb;

                if (nombreColumna != nuevoNombre.nuevoNomb)
                {
                    modificacion = true;
                }
            }
        }

        private void AgregarColumna()
        {
            AgregarColumna nuevaColumna = new AgregarColumna();
            DialogResult respuesta = nuevaColumna.ShowDialog();

            DataGridViewTextBoxColumn nombre = new DataGridViewTextBoxColumn();

            nombre.HeaderText = nuevaColumna.nombreCol;

            if (respuesta == DialogResult.OK)
            {
                datasetGrid.Columns.Add(nombre);
                tipoColumna.Add(nuevaColumna.tipoCol);
            }

            DataGridViewColumnCollection columnCollection = datasetGrid.Columns;
            DataGridViewColumn lastVisibleColumn = columnCollection.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None);

            indiceNC = lastVisibleColumn.DisplayIndex;

            for (int numberOfCells = 0; numberOfCells < this.datasetGrid.Rows.Count - 1; numberOfCells++)
            {
                datasetGrid.Rows[numberOfCells].Cells[indiceNC].Value = "?";
            }
        }

        // ------------------------------------ ACTIVACION/DESACTIVACION DE BOTONES ----------------------------------

        private void DesactivarFuncionesUnivariablesNum()
        {
            mediaToolStripMenuItem.Visible = false;
            medianaToolStripMenuItem.Visible = false;
            modaToolStripMenuItem.Visible = false;
            desviaciónEstándarToolStripMenuItem.Visible = false;
            boxPlotToolStripMenuItem.Visible = false;
        }

        private void ActivarFuncionesUnivariablesNum()
        {
            mediaToolStripMenuItem.Visible = true;
            medianaToolStripMenuItem.Visible = true;
            modaToolStripMenuItem.Visible = true;
            desviaciónEstándarToolStripMenuItem.Visible = true;
            boxPlotToolStripMenuItem.Visible = true;
        }
    }
}
