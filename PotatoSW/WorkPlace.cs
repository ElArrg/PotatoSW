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
        int valoresV;

        // Variable que se activa al realizar un cambio.
        bool modificacion = false;

        // Variable que se activa al encontrar celda con valores incorrectos.
        bool valoresVacios = false;

        // Variable que almacena, los tipos de columna.
        List<string> tipoColumna; 

        // Variables que almacena datos acerca de la columna seleccionada.
        public int indiceColumna;
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

        List<string> moda = new List<string>();
        double min;
        double max;
        string[,] valoresFila;
        int cantidadInstancias;

        List<int> indicesColumnas = new List<int>();
        List<string> myList = new List<string>();
        public List<string> resultado;

        // Expreciones regulares que permiten identificar si un valor es numerico o string.
        Regex Valnum = new Regex(@"[0-9]{1,9}(\.[0-9]{0,2})?$");
        Regex Valletra = new Regex(@"[a-zA-ZñÑ\s]");
        Regex valEnteros = new Regex(@"^[0-9]*$");
        Regex valDecimales = new Regex(@"^-?\d+(\.\d+)?$");

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
                string palabras = "";

                openDialog.Filter = "CSV Files (*.csv)|*.csv|DATA Files (*.data)|*.data";
                openDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                List<string> contadorP = new List<string>();

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    fileParser.FilePath = openDialog.FileName;
                    fileParser.LoadFile();
                    datasetGrid.DataSource = fileParser.ReadData();
                    datasetGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;


                    tipoColumna = new List<string>();

                    VerificarDatos();

                    resultado = new List<string>();

                    for (int numberOfColumns = 0; numberOfColumns < this.datasetGrid.Columns.Count; numberOfColumns++)
                    {
                        int enteros = 0;
                        int decimales = 0;

                        if (tipoColumna[numberOfColumns] == "Numerico")
                        {
                            for (int numberOfCells = 0; numberOfCells < this.datasetGrid.Rows.Count - 1; numberOfCells++)
                            {
                                if (valEnteros.IsMatch(datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Value.ToString()))
                                {
                                    enteros++;
                                }
                                else if (valDecimales.IsMatch(datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Value.ToString()))
                                {
                                    decimales++;
                                }
                            }

                            if (enteros > decimales)
                            {
                                resultado.Add("Enteros");
                            }
                            else
                            {
                                resultado.Add("Decimales");
                            }
                        }
                        if (tipoColumna[numberOfColumns] == "Categorico")
                        {
                            indiceColumna = numberOfColumns;
                            LlenarListaCat();

                            bool encontroColumna1;

                            foreach (string con in moda)
                            {
                                encontroColumna1 = false;

                                for (int i = 0; i < contadorP.Count; i++)
                                    if (contadorP[i] == con)
                                    {

                                        encontroColumna1 = true;
                                        contadorP[i] = contadorP[i];

                                    }

                                if (!encontroColumna1)
                                {

                                    contadorP.Add(con);

                                }
                            }
                            for (int i = 0; i < contadorP.Count; i++)
                            {
                                if (i == contadorP.Count - 1)
                                {
                                    palabras += contadorP[i];
                                }
                                else
                                {
                                    palabras += contadorP[i] + ",";
                                }

                            }

                            resultado.Add(palabras);
                        }
                        moda.Clear();
                        contadorP.Clear();
                        palabras = string.Empty;
                    }

                    análisisToolStripMenuItem.Enabled = true;

                    VerificarDominios();

                    nombreR.Visible = true;
                    nombreR.Text = Path.GetFileName(openDialog.FileName);

                    this.datasetGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    this.datasetGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                    datasetGrid.MouseClick += new MouseEventHandler(DatasetGrid_MouseClick);
                    datasetGrid.CellDoubleClick += DatasetGrid_CellDoubleClick;
                    datasetGrid.CellEndEdit += DatasetGrid_CellEndEdit;

                    limpiezaToolStripMenuItem.Enabled = true;
                    modificacion = false;
                }
            }
        }

        // Funcion que manda los valores al metodo para sobreescribir los datos.
        private void GuardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (datasetGrid.Rows.Count > 0)
            {
                fileParser.SaveData(fileParser.FilePath, DataToString());
                modificacion = false;
            }
        }

        // Funcion que manda los valores al metodo para guardar como.
        private void GuardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (datasetGrid.Rows.Count > 0)
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
                    modificacion = false;
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
                menu.Items.Add("Seleccionar conjunto de datos(Univariable)").Name = "conjunto";
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
                menu.Items.Add("-");
                menu.Items.Add("Llenar datos vacios").Name = "Datos vacios";
                menu.Items.Add("Corregir outliers").Name = "Outliers";
                menu.Items.Add("-");
                menu.Items.Add("Seleccionar conjunto de datos(Normalización)").Name = "Normalización";
                menu.Items.Add("-");
                menu.Items.Add("Buscar y remplazar").Name = "buscar";
                menu.Items.Add("-");
                menu.Items.Add("Errores Tipograficos").Name = "errores";

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
                    ModificarDominio();
                    VerificarDominios();
                    break;
                case "editar":
                    indiceColumna = datasetGrid.CurrentCell.ColumnIndex;
                    nombreColumna = datasetGrid.Columns[indiceColumna].Name;
                    ModificarNombreC();
                    break;
                case "Agregar atributo":
                    AgregarColumna();
                    break;
                case "Eliminar atributo":
                    if (MessageBox.Show(new Form() { TopMost = true }, "¿Eliminar atributo?", "Eliminacion de atributo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        tipoColumna.RemoveAt(datasetGrid.CurrentCell.ColumnIndex);
                        resultado.RemoveAt(datasetGrid.CurrentCell.ColumnIndex);
                        fileParser.Attributes.RemoveAt(datasetGrid.CurrentCell.ColumnIndex);
                        datasetGrid.Columns.RemoveAt(datasetGrid.CurrentCell.ColumnIndex);
                        modificacion = true;
                        VerificarDatos();
                        VerificarDominios();
                        
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
                    VerificarDominios();
                    modificacion = true;
                    break;
                case "Eliminar instancia":
                    if (MessageBox.Show(new Form() { TopMost = true }, "¿Eliminar instancia?", "Eliminacion de instancia", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        datasetGrid.Rows.RemoveAt(datasetGrid.CurrentRow.Index);
                        modificacion = true;
                        VerificarDatos();
                        VerificarDominios();
                    }
                    break;
                case "Datos vacios":
                    indiceColumna = datasetGrid.CurrentCell.ColumnIndex;
                    boxPlot.Clear();
                    moda.Clear();
                    LlenarDatosCol();
                    VerificarDominios();
                    break;
                case "Outliers":
                    indiceColumna = datasetGrid.CurrentCell.ColumnIndex;
                    OutliersCol();
                    break;
                case "Normalización":
                    indiceColumna = datasetGrid.CurrentCell.ColumnIndex;
                    if (tipoColumna[indiceColumna] == "Numerico")
                    {
                        boxPlot.Clear();
                        LlenarListaNum();
                    }
                    else
                    {
                        MessageBox.Show(new Form() { TopMost = true }, "TIPO DE COLUMNA NO VALIDA");
                    }

                    break;
                case "buscar":
                    indiceColumna = datasetGrid.CurrentCell.ColumnIndex;
                    if (tipoColumna[indiceColumna] == "Categorico")
                    {
                        BR();
                    }
                    else
                    {
                        BRN();
                    }
                    VerificarDominios();
                    break;
                case "errores":
                    indiceColumna = datasetGrid.CurrentCell.ColumnIndex;
                    if (tipoColumna[indiceColumna] == "Categorico")
                    {
                        levenshtein();
                        VerificarDominios();
                    }
                    else
                    {
                        MessageBox.Show(new Form() { TopMost = true }, "TIPO DE COLUMNA NO VALIDA");
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

        // ----------------------------------------------- FUNCIONES DE LIMPIEZA ------------------------------------------------------

        private void llenarValoresFaltantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LlenarTodo();
            VerificarDatos();
        }

        private void detecciónYCorrecciónDeOutliersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            allOutliers();
        }

        private void minMaxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            min = Double.Parse(datasetGrid.Rows[0].Cells[indiceColumna].Value.ToString());
            max = Double.Parse(datasetGrid.Rows[0].Cells[indiceColumna].Value.ToString());

            for (int i = 0; i < datasetGrid.RowCount - 1; i++)
            {
                if (boxPlot[i] > max)

                    max = boxPlot[i];


                else if (boxPlot[i] < min)

                    min = boxPlot[i];
            }

            for (int numberOfCells = 0; numberOfCells < this.datasetGrid.Rows.Count - 1; numberOfCells++)
            {
                datasetGrid.Rows[numberOfCells].Cells[indiceColumna].Value = string.Format("{0:n5}", ((Double.Parse(datasetGrid.Rows[numberOfCells].Cells[indiceColumna].Value.ToString()) - min) / (max - min)) * (1 - (-1)) + (-1));
            }
        }

        private void zscoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var media = (Double.Parse(string.Format("{0:n5}", (operaciones.Media(boxPlot)))));
            var desviacion = (Double.Parse(string.Format("{0:n5}", operaciones.desviaciónEstándar(boxPlot))));

            for (int numberOfCells = 0; numberOfCells < this.datasetGrid.Rows.Count - 1; numberOfCells++)
            {
                datasetGrid.Rows[numberOfCells].Cells[indiceColumna].Value = string.Format("{0:n5}", (Double.Parse(datasetGrid.Rows[numberOfCells].Cells[indiceColumna].Value.ToString()) - media) / desviacion);
            }

        }

        private void zscoreDesviaciónMediaAbsolutaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double media = 0;
            media = (Double.Parse(string.Format("{0:n5}", (operaciones.Media(boxPlot)))));
            double sumatoria = 0;
            double desviacion = 0;

            for (int numberOfCells = 0; numberOfCells < this.datasetGrid.Rows.Count - 1; numberOfCells++)
            {
                sumatoria = sumatoria + Math.Abs((Double.Parse(datasetGrid.Rows[numberOfCells].Cells[indiceColumna].Value.ToString()) - media));
            }

            desviacion = sumatoria / (datasetGrid.Rows.Count - 1);

            for (int numberOfCells = 0; numberOfCells < this.datasetGrid.Rows.Count - 1; numberOfCells++)
            {
                datasetGrid.Rows[numberOfCells].Cells[indiceColumna].Value = string.Format("{0:n5}", (Double.Parse(datasetGrid.Rows[numberOfCells].Cells[indiceColumna].Value.ToString()) - media) / desviacion);
            }
        }

        private void conRemplazoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TamañoMuestra tamañoMuestra = new TamañoMuestra();
            DialogResult respuesta = tamañoMuestra.ShowDialog();

            if (respuesta == DialogResult.OK)
            {
                Random rnd = new Random();
                SaveFileDialog sfd = new SaveFileDialog();

                sfd.Filter = "CSV Files (*.csv)|*.csv";
                valoresFila = new string[datasetGrid.Columns.Count, datasetGrid.Rows.Count - 1];

                cantidadInstancias = (tamañoMuestra.tamaño * (this.datasetGrid.Rows.Count - 1)) / 100;

                for (int numberOfColumns = 0; numberOfColumns < this.datasetGrid.Columns.Count; numberOfColumns++)
                {
                    indiceColumna = numberOfColumns;
                    for (int numberOfCells = 0; numberOfCells < cantidadInstancias; numberOfCells++)
                    {
                        if (tipoColumna[numberOfColumns] == "Numerico")
                        {
                            boxPlot.Clear();
                            LlenarListaNum();
                            int indice = rnd.Next(boxPlot.Count);
                            valoresFila[numberOfColumns, numberOfCells] = boxPlot[indice].ToString();

                        }
                        if (tipoColumna[numberOfColumns] == "Categorico")
                        {
                            moda.Clear();
                            LlenarListaCat();
                            int index = rnd.Next(moda.Count);
                            valoresFila[numberOfColumns, numberOfCells] = moda[index];
                        }
                    }
                }
                if (MessageBox.Show(new Form() { TopMost = true }, "¿Guardar Muestra?", "Muestra", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        SaveAsDataCSV(sfd.FileName);
                    }
                }
            }
        }

        private void sinnRemplazoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TamañoMuestra tamañoMuestra = new TamañoMuestra();
            DialogResult respuesta = tamañoMuestra.ShowDialog();

            if (respuesta == DialogResult.OK)
            {
                Random rnd = new Random();
                List<int> valoresIe = new List<int>();
                SaveFileDialog sfd = new SaveFileDialog();

                sfd.Filter = "CSV Files (*.csv)|*.csv";
                valoresFila = new string[datasetGrid.Columns.Count, datasetGrid.Rows.Count - 1];

                cantidadInstancias = (tamañoMuestra.tamaño * (this.datasetGrid.Rows.Count - 1)) / 100;

                while (valoresIe.Count < cantidadInstancias)
                {

                    int posible = rnd.Next(this.datasetGrid.Rows.Count - 1);

                    if (!valoresIe.Contains(posible))
                    {
                        valoresIe.Add(posible);
                    }
                }

                for (int numberOfColumns = 0; numberOfColumns < this.datasetGrid.Columns.Count; numberOfColumns++)
                {
                    indiceColumna = numberOfColumns;
                    boxPlot.Clear();
                    moda.Clear();

                    for (int numberOfCells = 0; numberOfCells < cantidadInstancias; numberOfCells++)
                    {
                        if (tipoColumna[numberOfColumns] == "Numerico")
                        {
                            LlenarListaNum();
                            valoresFila[numberOfColumns, numberOfCells] = boxPlot[valoresIe[numberOfCells]].ToString();
                        }
                        if (tipoColumna[numberOfColumns] == "Categorico")
                        {
                            LlenarListaCat();
                            valoresFila[numberOfColumns, numberOfCells] = moda[valoresIe[numberOfCells]];
                        }
                    }
                }
                if (MessageBox.Show(new Form() { TopMost = true }, "¿Guardar Muestra?", "Muestra", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        SaveAsDataCSV(sfd.FileName);
                    }
                }
            }
        }

        private void BR()
        {
            BuscarRemplazar buscarRemplazar = new BuscarRemplazar();
            string celda = datasetGrid.CurrentCell.Value.ToString();
            string valorB = "";
            string valorR = "";
            buscarRemplazar.Recibir(resultado[indiceColumna]);
            DialogResult respuesta = buscarRemplazar.ShowDialog();

            if (respuesta == DialogResult.OK)
            {
                valorB = buscarRemplazar.celda;
                valorR = buscarRemplazar.valSel;
            }

            for (int numberOfCells = 0; numberOfCells < this.datasetGrid.Rows.Count - 1; numberOfCells++)
            {
                if (datasetGrid.Rows[numberOfCells].Cells[indiceColumna].Value.ToString() == valorB)
                {
                    datasetGrid.Rows[numberOfCells].Cells[indiceColumna].Value = valorR;
                    modificacion = true;
                }
            }
        }

        private void levenshtein()
        {
            string[] valores = resultado[indiceColumna].Split(',');
            string valor = datasetGrid.Rows[datasetGrid.CurrentRow.Index].Cells[indiceColumna].Value.ToString();
            int[] minimos = new int[valores.Length];

            for (int i = 0; i < valores.Length; i++)
            {
                minimos[i] = Compute(valor, valores[i]);
            }

            int min = minimos[0];

            for (int i = 0; i < minimos.Length; i++)
            {
                if (minimos[i] < min)
                {
                    min = i;
                }
            }

            for (int numberOfColumns = 0; numberOfColumns < this.datasetGrid.Columns.Count; numberOfColumns++)
            {
                for (int numberOfCells = 0; numberOfCells < this.datasetGrid.Rows.Count - 1; numberOfCells++)
                {
                    if (datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Value.ToString() == valor)
                    {
                        datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Value = valores[min];
                    }
                }
            }
        }

        // ----------------------------------------------- EVENTOS ------------------------------------------------------

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

            VerificarDominios();
        }

        // Funcion que verifica si los datos son correctos y actualiza la informacion de la izquierda.
        private void VerificarDatos()
        {
            for (int numberOfColumns = 0; numberOfColumns < this.datasetGrid.Columns.Count; numberOfColumns++)
            {
                int valorNumerico = 0;
                int valorLetra = 0;

                for (int numberOfCells = 0; numberOfCells < this.datasetGrid.Rows.Count - 1; numberOfCells++)
                {
                    
                    var valorColumna = datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Value;
                    if (valorColumna != null)
                    {

                    
                        if (Valnum.IsMatch(valorColumna.ToString()))
                        {
                            valorNumerico++;
                        }
                        else if (Valletra.IsMatch(valorColumna.ToString()))
                        {
                            valorLetra++;
                        }
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

            cantidadAR.Visible = true;
            cantidadAR.Text = datasetGrid.ColumnCount.ToString();

            cantidadIR.Visible = true;
            cantidadIR.Text = ((datasetGrid.Rows.Count) - 1).ToString();
        }

        // Funcion que verifica que los datos entren en el dominio de la columna.
        private void VerificarDominios()
        {
            valoresF = 0;
            valoresT = 0;
            valoresV = 0;
            int porciento = 100;
            double proporcion = 0.0;

            análisisToolStripMenuItem.Enabled = true;

            for (int numberOfColumns = 0; numberOfColumns < this.datasetGrid.Columns.Count; numberOfColumns++)
            {
                if (tipoColumna[numberOfColumns] == "Numerico")
                {
                    for (int numberOfCells = 0; numberOfCells < this.datasetGrid.Rows.Count - 1; numberOfCells++)
                    {
                        //if (Valletra.IsMatch(datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Value.ToString()) && datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Value.ToString() != "?")
                        //{
                        //    valoresF++;
                        //    datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Style.BackColor = Color.OrangeRed;
                        //    análisisToolStripMenuItem.Enabled = false;
                        //}
                        if (resultado[numberOfColumns] == "Decimales")
                        {
                            if (!valDecimales.IsMatch(datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Value.ToString()) && datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Value.ToString() != "?")
                            {
                                valoresF++;
                                datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Style.BackColor = Color.OrangeRed;
                                análisisToolStripMenuItem.Enabled = false;
                            }
                            else
                            {
                                datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Style.BackColor = Color.White;
                            }
                        }
                        if (resultado[numberOfColumns] == "Enteros")
                        {
                            if (!valEnteros.IsMatch(datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Value.ToString()) && datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Value.ToString() != "?")
                            {
                                valoresF++;
                                datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Style.BackColor = Color.OrangeRed;
                                análisisToolStripMenuItem.Enabled = false;
                            }
                            else
                            {
                                datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Style.BackColor = Color.White;
                        }
                    }
                }
                if (tipoColumna[numberOfColumns] == "Categorico")
                {
                    if (resultado[numberOfColumns] != null)
                    {
                        myList = resultado[numberOfColumns].Split(',').ToList();
                    }

                    for (int numberOfCells = 0; numberOfCells < this.datasetGrid.Rows.Count - 1; numberOfCells++)
                    {
                        if (!myList.Contains(datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Value.ToString()) && datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Value.ToString() != "")
                        {
                            if (datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Value.ToString() == "?")
                            {
                                datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Style.BackColor = Color.OrangeRed;
                                valoresF--;
                            }
                            valoresF++;
                            datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Style.BackColor = Color.OrangeRed;
                            análisisToolStripMenuItem.Enabled = false;
                        }
                        else
                        {
                            datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Style.BackColor = Color.White;
                        }
                    }
                }
            }

            

            for (int numberOfColumns = 0; numberOfColumns < this.datasetGrid.Columns.Count; numberOfColumns++)
            {
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
                        valoresV++;
                        datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Value = "?";
                        análisisToolStripMenuItem.Enabled = false;
                    }
                }
            }

            valoresFR.Visible = true;
            valoresFR.Text = (valoresF + valoresV).ToString();

            proporcion = (double)((double.Parse(valoresFR.Text) * porciento) / valoresT);
            proporcionVR.Visible = true;
            proporcionVR.Text = Math.Round(proporcion, 2).ToString();
        }

        // ------------------------------------ METODOS USADOS POR EL ANALISIS ESTADISTICO ----------------------------------

        private void LlenarListasBi(bool column1, bool column2)
        {
            if (activarC1 && activarC2)
            {
                //if (columnaSelec1 != columnaSelec2)
                //{
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
                        MessageBox.Show(new Form() { TopMost = true }, "NO SE PUEDE REALIZAR LA OPERACION", "Advertencia");
                        pearsonToolStripMenuItem.Visible = false;
                        tschprowToolStripMenuItem.Visible = false;
                        activarC1 = false;
                        activarC2 = false;
                    }
                //}
                //else
                //{
                //    MessageBox.Show(new Form() { TopMost = true }, "SE SELECCIONO LA MISMA COLUMNA", "Seleccion de columa");
                //    activarC1 = false;
                //}
            }
        }

        private void LlenarListasUni()
        {
            if (tipoColumna[indiceColumna] == "Numerico")
            {
                boxPlot.Clear();

                nombreColumna = datasetGrid.Columns[indiceColumna].Name;

                LlenarListaNum();

                ActivarFuncionesUnivariablesNum();
                frecuenciaToolStripMenuItem.Visible = false;
            }
            else if (tipoColumna[indiceColumna] == "Categorico")
            {
                frecuencia.Clear();
                frecuenciaP.Clear();

                LlenarListaCatUni();

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
            

            DataGridViewColumnCollection columnCollection = datasetGrid.Columns;
            DataGridViewColumn lastVisibleColumn = columnCollection.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None);

            indiceNC = lastVisibleColumn.DisplayIndex;

            for (int numberOfCells = 0; numberOfCells < this.datasetGrid.Rows.Count - 1; numberOfCells++)
            {
                datasetGrid.Rows[numberOfCells].Cells[indiceNC].Value = "?";
            }

            resultado.Add("");
            fileParser.Attributes.Add(new Attribute(nuevaColumna.nombreCol, nuevaColumna.tipoCol, resultado[indiceNC]));
            modificacion = true;
            VerificarDatos();
            VerificarDominios();
            }
        }

        private void LlenarListaNum()
        {
            for (int i = 0; i < datasetGrid.RowCount - 1; i++)
            {
                if (datasetGrid.Rows[i].Cells[indiceColumna].Value.ToString() != "")
                {
                    if (datasetGrid.Rows[i].Cells[indiceColumna].Value.ToString() != "?")
                    {
                        boxPlot.Add(Double.Parse(datasetGrid.Rows[i].Cells[indiceColumna].Value.ToString()));
                    }
                }
                else
                {
                    boxPlot.Add(0);
                }
            }
        }

        private void LlenarListaCatUni()
        {
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
        }

        private void LlenarListaCat()
        {
            for (int i = 0; i < datasetGrid.RowCount - 1; i++)
            {
                if (datasetGrid.Rows[i].Cells[indiceColumna].Value.ToString() != "" && datasetGrid.Rows[i].Cells[indiceColumna].Value.ToString() != "?")
                {
                    moda.Add(datasetGrid.Rows[i].Cells[indiceColumna].Value.ToString());
                }
                else
                {
                    moda.Add("");
                }
            }
        }

        // ------------------------------------ METODOS USADOS POR LA LIMPIEZA ----------------------------------

        private void LlenarDatosCol()
        {
            if (tipoColumna[indiceColumna] == "Numerico")
            {
                LlenarListaNum();
                double mediana = operaciones.Mediana(boxPlot);

                for (int numberOfCells = 0; numberOfCells < this.datasetGrid.Rows.Count - 1; numberOfCells++)
                {
                    if (datasetGrid.Rows[numberOfCells].Cells[indiceColumna].Value.ToString() == "?")
                        datasetGrid.Rows[numberOfCells].Cells[indiceColumna].Value = mediana;
                }
            }
            else if (tipoColumna[indiceColumna] == "Categorico")
            {
                LlenarListaCat();
                string modaList = operaciones.ModaLetras(moda);

                for (int numberOfCells = 0; numberOfCells < this.datasetGrid.Rows.Count - 1; numberOfCells++)
                {
                    if (datasetGrid.Rows[numberOfCells].Cells[indiceColumna].Value.ToString() == "?")
                        datasetGrid.Rows[numberOfCells].Cells[indiceColumna].Value = modaList;
                }
            }
        }

        private void LlenarTodo()
        {
            for (int numberOfColumns = 0; numberOfColumns < this.datasetGrid.Columns.Count; numberOfColumns++)
            {
                indiceColumna = numberOfColumns;

                if (tipoColumna[numberOfColumns] == "Numerico")
                {
                    boxPlot.Clear();
                    LlenarListaNum();
                    double mediana = operaciones.Mediana(boxPlot);

                    for (int numberOfCells = 0; numberOfCells < this.datasetGrid.Rows.Count - 1; numberOfCells++)
                    {
                        if (datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Value.ToString() == "?")
                            datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Value = mediana;
                    }
                }
                if (tipoColumna[numberOfColumns] == "Categorico")
                {
                    moda.Clear();
                    LlenarListaCat();
                    string modaList = operaciones.ModaLetras(moda);

                    for (int numberOfCells = 0; numberOfCells < this.datasetGrid.Rows.Count - 1; numberOfCells++)
                    {
                        if (datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Value.ToString() == "?")
                            datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Value = modaList;
                    }
                }
            }
            VerificarDominios();
        }

        private void OutliersCol()
        {
            boxPlot.Clear();
            LlenarListaNum();

            double mediana = operaciones.Mediana(boxPlot);
            double desv = operaciones.desviaciónEstándar(boxPlot);

            double outlierMayor = mediana + (desv * 3);
            double outlierMenor = mediana - (desv * 3);

            double pOutlierMayor = mediana + (desv * 1.5);
            double pOutlierMenor = mediana - (desv * 1.5);

            if (tipoColumna[indiceColumna] == "Numerico")
            {
                for (int numberOfCells = 0; numberOfCells < this.datasetGrid.Rows.Count - 1; numberOfCells++)
                {
                    if (Double.Parse(datasetGrid.Rows[numberOfCells].Cells[indiceColumna].Value.ToString()) > pOutlierMayor || Double.Parse(datasetGrid.Rows[numberOfCells].Cells[indiceColumna].Value.ToString()) < pOutlierMenor)
                    {
                        datasetGrid.Rows[numberOfCells].Cells[indiceColumna].Style.BackColor = Color.Yellow;
                    }
                    if (Double.Parse(datasetGrid.Rows[numberOfCells].Cells[indiceColumna].Value.ToString()) > outlierMayor || Double.Parse(datasetGrid.Rows[numberOfCells].Cells[indiceColumna].Value.ToString()) < outlierMenor)
                    {
                        datasetGrid.Rows[numberOfCells].Cells[indiceColumna].Style.BackColor = Color.Green;
                    }
                }

                if (MessageBox.Show("¿Cambiar outliers por la mediana?", "Outliers", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int numberOfCells = 0; numberOfCells < this.datasetGrid.Rows.Count - 1; numberOfCells++)
                    {
                        if (Double.Parse(datasetGrid.Rows[numberOfCells].Cells[indiceColumna].Value.ToString()) > outlierMayor || Double.Parse(datasetGrid.Rows[numberOfCells].Cells[indiceColumna].Value.ToString()) < outlierMenor)
                        {
                            datasetGrid.Rows[numberOfCells].Cells[indiceColumna].Value = mediana;
                            datasetGrid.Rows[numberOfCells].Cells[indiceColumna].Style.BackColor = Color.White;
                        }
                    }
                }
            }
        }

        private void allOutliers()
        {
            for (int numberOfColumns = 0; numberOfColumns < this.datasetGrid.Columns.Count; numberOfColumns++)
            {
                indiceColumna = numberOfColumns;

                if (tipoColumna[indiceColumna] == "Numerico")
                {
                    boxPlot.Clear();
                    LlenarListaNum();

                    double mediana = operaciones.Mediana(boxPlot);
                    double desv = operaciones.desviaciónEstándar(boxPlot);

                    double outlierMayor = mediana + (desv * 3);
                    double outlierMenor = mediana - (desv * 3);

                    double pOutlierMayor = mediana + (desv * 1.5);
                    double pOutlierMenor = mediana - (desv * 1.5);

                    for (int numberOfCells = 0; numberOfCells < this.datasetGrid.Rows.Count - 1; numberOfCells++)
                    {
                        if (Double.Parse(datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Value.ToString()) > pOutlierMayor || Double.Parse(datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Value.ToString()) < pOutlierMenor)
                        {
                            datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Style.BackColor = Color.Yellow;
                        }
                        if (Double.Parse(datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Value.ToString()) > outlierMayor || Double.Parse(datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Value.ToString()) < outlierMenor)
                        {
                            datasetGrid.Rows[numberOfCells].Cells[numberOfColumns].Style.BackColor = Color.Green;
                        }
                    }
                }
            }
            if (MessageBox.Show("¿Cambiar outliers por la mediana?", "Outliers", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int numberOfColumns = 0; numberOfColumns < this.datasetGrid.Columns.Count; numberOfColumns++)
                {
                    indiceColumna = numberOfColumns;

                    if (tipoColumna[indiceColumna] == "Numerico")
                    {
                        boxPlot.Clear();
                        LlenarListaNum();

                        double mediana = operaciones.Mediana(boxPlot);
                        double desv = operaciones.desviaciónEstándar(boxPlot);

                        double outlierMayor = mediana + (desv * 3);
                        double outlierMenor = mediana - (desv * 3);

                        double pOutlierMayor = mediana + (desv * 1.5);
                        double pOutlierMenor = mediana - (desv * 1.5);

                        for (int numberOfCells = 0; numberOfCells < this.datasetGrid.Rows.Count - 1; numberOfCells++)
                        {
                            if (Double.Parse(datasetGrid.Rows[numberOfCells].Cells[indiceColumna].Value.ToString()) > outlierMayor || Double.Parse(datasetGrid.Rows[numberOfCells].Cells[indiceColumna].Value.ToString()) < outlierMenor)
                            {
                                datasetGrid.Rows[numberOfCells].Cells[indiceColumna].Value = mediana;
                                datasetGrid.Rows[numberOfCells].Cells[indiceColumna].Style.BackColor = Color.White;
                            }
                        }
                    }
                }

            }
        }

        private void SaveAsDataCSV(string filePath)
        {

            StringBuilder csvMemoria = new StringBuilder();

            string columnsHeader = "";

            // Ciclo que se utiliza para definir las columnas.
            for (int i = 0; i < datasetGrid.Columns.Count; i++)
            {
                if (i == datasetGrid.Columns.Count - 1)
                {
                    columnsHeader += datasetGrid.Columns[i].Name;
                }
                else
                {
                    columnsHeader += datasetGrid.Columns[i].Name + ",";
                }
            }

            csvMemoria.Append(columnsHeader + Environment.NewLine);

            for (int m = 0; m < cantidadInstancias; m++)
            {
                for (int n = 0; n < datasetGrid.Columns.Count; n++)
                {
                    // Si es la última columna no poner la coma.
                    if (n == datasetGrid.Columns.Count - 1)
                    {
                        csvMemoria.Append(valoresFila[n, m]);
                    }
                    else
                    {
                        csvMemoria.Append(valoresFila[n, m] + ",");
                    }
                }
                csvMemoria.AppendLine();
            }

            System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath, false, System.Text.Encoding.Default);
            sw.Write(csvMemoria.ToString());
            sw.Close();
        }

        private void ModificarDominio()
        {

            ModificarDominio modificarDominio = new ModificarDominio();
            modificarDominio.Recibir(resultado[indiceColumna]);
            DialogResult respuesta = modificarDominio.ShowDialog();

            if (respuesta == DialogResult.OK)
            {
                resultado[indiceColumna] = modificarDominio.datos;
            }
        }

        public static int Compute(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
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

        private void datasetGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
            {
                DataGridViewRow row = ((DataGridView)sender).Rows[i];

                if (!row.IsNewRow)
                {
                    row.HeaderCell.Value = (i + 1).ToString();
                }
            }
        }

        private void datasetGrid_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (e.Row.IsNewRow)
            {
                ((DataGridView)sender).Rows[e.Row.Index - 1].HeaderCell.Value = e.Row.Index.ToString();
            }
        }

        private void BRN()
        {
            BuscarRemplazarNum buscarRemplazarNum = new BuscarRemplazarNum();
            string valorB = "";
            string valorR = "";
            DialogResult respuesta = buscarRemplazarNum.ShowDialog();

            if (respuesta == DialogResult.OK)
            {
                valorB = buscarRemplazarNum.valBus;
                valorR = buscarRemplazarNum.valRem;
            }

            for (int numberOfCells = 0; numberOfCells < this.datasetGrid.Rows.Count - 1; numberOfCells++)
            {
                if (datasetGrid.Rows[numberOfCells].Cells[indiceColumna].Value.ToString() == valorB)
                {
                    datasetGrid.Rows[numberOfCells].Cells[indiceColumna].Value = valorR;
                    modificacion = true;
                }
            }

        }
    }
}
