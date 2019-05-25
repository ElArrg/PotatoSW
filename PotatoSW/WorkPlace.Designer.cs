namespace PotatoSW
{
    partial class WorkPlace
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkPlace));
            this.datasetGrid = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cargarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarComoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.análisisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.univariableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mediaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medianaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desviaciónEstándarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boxPlotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frecuenciaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bivariableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pearsonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tschprowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.limpiezaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.llenarValoresFaltantesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detecciónYCorrecciónDeOutliersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transformaciónDeDatosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minMaxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zscoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zscoreDesviaciónMediaAbsolutaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.muestreoDeDatosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conRemplazoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sinnRemplazoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resultadoT = new System.Windows.Forms.Label();
            this.resultadoR = new System.Windows.Forms.Label();
            this.nombreT = new System.Windows.Forms.Label();
            this.nombreR = new System.Windows.Forms.Label();
            this.cantidadIT = new System.Windows.Forms.Label();
            this.cantidadIR = new System.Windows.Forms.Label();
            this.informacionT = new System.Windows.Forms.Label();
            this.cantidadAT = new System.Windows.Forms.Label();
            this.cantidadAR = new System.Windows.Forms.Label();
            this.valoresFT = new System.Windows.Forms.Label();
            this.valoresFR = new System.Windows.Forms.Label();
            this.proporcionVT = new System.Windows.Forms.Label();
            this.proporcionVR = new System.Windows.Forms.Label();
            this.multiUso = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.datasetGrid)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // datasetGrid
            // 
            this.datasetGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datasetGrid.Location = new System.Drawing.Point(12, 27);
            this.datasetGrid.Name = "datasetGrid";
            this.datasetGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.datasetGrid.Size = new System.Drawing.Size(527, 411);
            this.datasetGrid.TabIndex = 0;
            this.datasetGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.datasetGrid_RowsAdded);
            this.datasetGrid.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.datasetGrid_UserAddedRow);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.análisisToolStripMenuItem,
            this.limpiezaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(924, 26);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cargarToolStripMenuItem,
            this.guardarToolStripMenuItem,
            this.guardarComoToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Font = new System.Drawing.Font("Maiandra GD", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(77, 22);
            this.archivoToolStripMenuItem.Text = "Archivo";
            this.archivoToolStripMenuItem.Click += new System.EventHandler(this.ArchivoToolStripMenuItem_Click);
            // 
            // cargarToolStripMenuItem
            // 
            this.cargarToolStripMenuItem.Name = "cargarToolStripMenuItem";
            this.cargarToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.cargarToolStripMenuItem.Text = "Cargar";
            this.cargarToolStripMenuItem.Click += new System.EventHandler(this.CargarToolStripMenuItem_Click);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.guardarToolStripMenuItem.Text = "Guardar";
            this.guardarToolStripMenuItem.Click += new System.EventHandler(this.GuardarToolStripMenuItem_Click);
            // 
            // guardarComoToolStripMenuItem
            // 
            this.guardarComoToolStripMenuItem.Name = "guardarComoToolStripMenuItem";
            this.guardarComoToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.guardarComoToolStripMenuItem.Text = "Guardar como";
            this.guardarComoToolStripMenuItem.Click += new System.EventHandler(this.GuardarComoToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.SalirToolStripMenuItem_Click);
            // 
            // análisisToolStripMenuItem
            // 
            this.análisisToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.univariableToolStripMenuItem,
            this.bivariableToolStripMenuItem});
            this.análisisToolStripMenuItem.Enabled = false;
            this.análisisToolStripMenuItem.Font = new System.Drawing.Font("Maiandra GD", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.análisisToolStripMenuItem.Name = "análisisToolStripMenuItem";
            this.análisisToolStripMenuItem.Size = new System.Drawing.Size(75, 22);
            this.análisisToolStripMenuItem.Text = "Análisis";
            // 
            // univariableToolStripMenuItem
            // 
            this.univariableToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mediaToolStripMenuItem,
            this.medianaToolStripMenuItem,
            this.modaToolStripMenuItem,
            this.desviaciónEstándarToolStripMenuItem,
            this.boxPlotToolStripMenuItem,
            this.frecuenciaToolStripMenuItem});
            this.univariableToolStripMenuItem.Name = "univariableToolStripMenuItem";
            this.univariableToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.univariableToolStripMenuItem.Text = "Univariable";
            // 
            // mediaToolStripMenuItem
            // 
            this.mediaToolStripMenuItem.Name = "mediaToolStripMenuItem";
            this.mediaToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.mediaToolStripMenuItem.Text = "Media";
            this.mediaToolStripMenuItem.Visible = false;
            this.mediaToolStripMenuItem.Click += new System.EventHandler(this.MediaToolStripMenuItem_Click);
            // 
            // medianaToolStripMenuItem
            // 
            this.medianaToolStripMenuItem.Name = "medianaToolStripMenuItem";
            this.medianaToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.medianaToolStripMenuItem.Text = "Mediana";
            this.medianaToolStripMenuItem.Visible = false;
            this.medianaToolStripMenuItem.Click += new System.EventHandler(this.MedianaToolStripMenuItem_Click);
            // 
            // modaToolStripMenuItem
            // 
            this.modaToolStripMenuItem.Name = "modaToolStripMenuItem";
            this.modaToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.modaToolStripMenuItem.Text = "Moda";
            this.modaToolStripMenuItem.Visible = false;
            this.modaToolStripMenuItem.Click += new System.EventHandler(this.ModaToolStripMenuItem_Click);
            // 
            // desviaciónEstándarToolStripMenuItem
            // 
            this.desviaciónEstándarToolStripMenuItem.Name = "desviaciónEstándarToolStripMenuItem";
            this.desviaciónEstándarToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.desviaciónEstándarToolStripMenuItem.Text = "Desviación Estándar";
            this.desviaciónEstándarToolStripMenuItem.Visible = false;
            this.desviaciónEstándarToolStripMenuItem.Click += new System.EventHandler(this.DesviaciónEstándarToolStripMenuItem_Click);
            // 
            // boxPlotToolStripMenuItem
            // 
            this.boxPlotToolStripMenuItem.Name = "boxPlotToolStripMenuItem";
            this.boxPlotToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.boxPlotToolStripMenuItem.Text = "Box Plot";
            this.boxPlotToolStripMenuItem.Visible = false;
            this.boxPlotToolStripMenuItem.Click += new System.EventHandler(this.BoxPlotToolStripMenuItem_Click);
            // 
            // frecuenciaToolStripMenuItem
            // 
            this.frecuenciaToolStripMenuItem.Name = "frecuenciaToolStripMenuItem";
            this.frecuenciaToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.frecuenciaToolStripMenuItem.Text = "Frecuencia";
            this.frecuenciaToolStripMenuItem.Visible = false;
            this.frecuenciaToolStripMenuItem.Click += new System.EventHandler(this.FrecuenciaToolStripMenuItem_Click);
            // 
            // bivariableToolStripMenuItem
            // 
            this.bivariableToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pearsonToolStripMenuItem,
            this.tschprowToolStripMenuItem});
            this.bivariableToolStripMenuItem.Name = "bivariableToolStripMenuItem";
            this.bivariableToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.bivariableToolStripMenuItem.Text = "Bivariable";
            // 
            // pearsonToolStripMenuItem
            // 
            this.pearsonToolStripMenuItem.Name = "pearsonToolStripMenuItem";
            this.pearsonToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.pearsonToolStripMenuItem.Text = "Pearson";
            this.pearsonToolStripMenuItem.Visible = false;
            this.pearsonToolStripMenuItem.Click += new System.EventHandler(this.PearsonToolStripMenuItem_Click);
            // 
            // tschprowToolStripMenuItem
            // 
            this.tschprowToolStripMenuItem.Name = "tschprowToolStripMenuItem";
            this.tschprowToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.tschprowToolStripMenuItem.Text = "Tschuprow";
            this.tschprowToolStripMenuItem.Visible = false;
            this.tschprowToolStripMenuItem.Click += new System.EventHandler(this.TschprowToolStripMenuItem_Click);
            // 
            // limpiezaToolStripMenuItem
            // 
            this.limpiezaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.llenarValoresFaltantesToolStripMenuItem,
            this.detecciónYCorrecciónDeOutliersToolStripMenuItem,
            this.transformaciónDeDatosToolStripMenuItem,
            this.muestreoDeDatosToolStripMenuItem});
            this.limpiezaToolStripMenuItem.Enabled = false;
            this.limpiezaToolStripMenuItem.Font = new System.Drawing.Font("Maiandra GD", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.limpiezaToolStripMenuItem.Name = "limpiezaToolStripMenuItem";
            this.limpiezaToolStripMenuItem.Size = new System.Drawing.Size(86, 22);
            this.limpiezaToolStripMenuItem.Text = "Limpieza";
            // 
            // llenarValoresFaltantesToolStripMenuItem
            // 
            this.llenarValoresFaltantesToolStripMenuItem.Name = "llenarValoresFaltantesToolStripMenuItem";
            this.llenarValoresFaltantesToolStripMenuItem.Size = new System.Drawing.Size(325, 22);
            this.llenarValoresFaltantesToolStripMenuItem.Text = "Llenar valores faltantes";
            this.llenarValoresFaltantesToolStripMenuItem.Click += new System.EventHandler(this.llenarValoresFaltantesToolStripMenuItem_Click);
            // 
            // detecciónYCorrecciónDeOutliersToolStripMenuItem
            // 
            this.detecciónYCorrecciónDeOutliersToolStripMenuItem.Name = "detecciónYCorrecciónDeOutliersToolStripMenuItem";
            this.detecciónYCorrecciónDeOutliersToolStripMenuItem.Size = new System.Drawing.Size(325, 22);
            this.detecciónYCorrecciónDeOutliersToolStripMenuItem.Text = "Detección y corrección de outliers";
            this.detecciónYCorrecciónDeOutliersToolStripMenuItem.Click += new System.EventHandler(this.detecciónYCorrecciónDeOutliersToolStripMenuItem_Click);
            // 
            // transformaciónDeDatosToolStripMenuItem
            // 
            this.transformaciónDeDatosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.minMaxToolStripMenuItem,
            this.zscoreToolStripMenuItem,
            this.zscoreDesviaciónMediaAbsolutaToolStripMenuItem});
            this.transformaciónDeDatosToolStripMenuItem.Name = "transformaciónDeDatosToolStripMenuItem";
            this.transformaciónDeDatosToolStripMenuItem.Size = new System.Drawing.Size(325, 22);
            this.transformaciónDeDatosToolStripMenuItem.Text = "Transformación de datos";
            // 
            // minMaxToolStripMenuItem
            // 
            this.minMaxToolStripMenuItem.Name = "minMaxToolStripMenuItem";
            this.minMaxToolStripMenuItem.Size = new System.Drawing.Size(336, 22);
            this.minMaxToolStripMenuItem.Text = "Min-Max";
            this.minMaxToolStripMenuItem.Click += new System.EventHandler(this.minMaxToolStripMenuItem_Click);
            // 
            // zscoreToolStripMenuItem
            // 
            this.zscoreToolStripMenuItem.Name = "zscoreToolStripMenuItem";
            this.zscoreToolStripMenuItem.Size = new System.Drawing.Size(336, 22);
            this.zscoreToolStripMenuItem.Text = "Z-score(Desviación estandar)";
            this.zscoreToolStripMenuItem.Click += new System.EventHandler(this.zscoreToolStripMenuItem_Click);
            // 
            // zscoreDesviaciónMediaAbsolutaToolStripMenuItem
            // 
            this.zscoreDesviaciónMediaAbsolutaToolStripMenuItem.Name = "zscoreDesviaciónMediaAbsolutaToolStripMenuItem";
            this.zscoreDesviaciónMediaAbsolutaToolStripMenuItem.Size = new System.Drawing.Size(336, 22);
            this.zscoreDesviaciónMediaAbsolutaToolStripMenuItem.Text = "Z-score(Desviación media absoluta)";
            this.zscoreDesviaciónMediaAbsolutaToolStripMenuItem.Click += new System.EventHandler(this.zscoreDesviaciónMediaAbsolutaToolStripMenuItem_Click);
            // 
            // muestreoDeDatosToolStripMenuItem
            // 
            this.muestreoDeDatosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.conRemplazoToolStripMenuItem,
            this.sinnRemplazoToolStripMenuItem});
            this.muestreoDeDatosToolStripMenuItem.Name = "muestreoDeDatosToolStripMenuItem";
            this.muestreoDeDatosToolStripMenuItem.Size = new System.Drawing.Size(325, 22);
            this.muestreoDeDatosToolStripMenuItem.Text = "Muestreo de datos";
            // 
            // conRemplazoToolStripMenuItem
            // 
            this.conRemplazoToolStripMenuItem.Name = "conRemplazoToolStripMenuItem";
            this.conRemplazoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.conRemplazoToolStripMenuItem.Text = "Con remplazo";
            this.conRemplazoToolStripMenuItem.Click += new System.EventHandler(this.conRemplazoToolStripMenuItem_Click);
            // 
            // sinnRemplazoToolStripMenuItem
            // 
            this.sinnRemplazoToolStripMenuItem.Name = "sinnRemplazoToolStripMenuItem";
            this.sinnRemplazoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sinnRemplazoToolStripMenuItem.Text = "Sin remplazo";
            this.sinnRemplazoToolStripMenuItem.Click += new System.EventHandler(this.sinnRemplazoToolStripMenuItem_Click);
            // 
            // resultadoT
            // 
            this.resultadoT.AutoSize = true;
            this.resultadoT.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultadoT.Location = new System.Drawing.Point(563, 244);
            this.resultadoT.Name = "resultadoT";
            this.resultadoT.Size = new System.Drawing.Size(81, 16);
            this.resultadoT.TabIndex = 2;
            this.resultadoT.Text = "Resultado: ";
            // 
            // resultadoR
            // 
            this.resultadoR.AutoSize = true;
            this.resultadoR.Location = new System.Drawing.Point(801, 247);
            this.resultadoR.Name = "resultadoR";
            this.resultadoR.Size = new System.Drawing.Size(13, 13);
            this.resultadoR.TabIndex = 3;
            this.resultadoR.Text = "0";
            // 
            // nombreT
            // 
            this.nombreT.AutoSize = true;
            this.nombreT.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nombreT.Location = new System.Drawing.Point(563, 80);
            this.nombreT.Name = "nombreT";
            this.nombreT.Size = new System.Drawing.Size(71, 16);
            this.nombreT.TabIndex = 4;
            this.nombreT.Text = "Nombre: ";
            // 
            // nombreR
            // 
            this.nombreR.AutoSize = true;
            this.nombreR.Location = new System.Drawing.Point(711, 83);
            this.nombreR.Name = "nombreR";
            this.nombreR.Size = new System.Drawing.Size(12, 13);
            this.nombreR.TabIndex = 5;
            this.nombreR.Text = "x";
            this.nombreR.Visible = false;
            // 
            // cantidadIT
            // 
            this.cantidadIT.AutoSize = true;
            this.cantidadIT.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cantidadIT.Location = new System.Drawing.Point(563, 108);
            this.cantidadIT.Name = "cantidadIT";
            this.cantidadIT.Size = new System.Drawing.Size(155, 16);
            this.cantidadIT.TabIndex = 6;
            this.cantidadIT.Text = "Cantidad de instacias: ";
            // 
            // cantidadIR
            // 
            this.cantidadIR.AutoSize = true;
            this.cantidadIR.Location = new System.Drawing.Point(801, 111);
            this.cantidadIR.Name = "cantidadIR";
            this.cantidadIR.Size = new System.Drawing.Size(13, 13);
            this.cantidadIR.TabIndex = 7;
            this.cantidadIR.Text = "0";
            this.cantidadIR.Visible = false;
            // 
            // informacionT
            // 
            this.informacionT.AutoSize = true;
            this.informacionT.Font = new System.Drawing.Font("Maiandra GD", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.informacionT.Location = new System.Drawing.Point(659, 46);
            this.informacionT.Name = "informacionT";
            this.informacionT.Size = new System.Drawing.Size(137, 19);
            this.informacionT.TabIndex = 8;
            this.informacionT.Text = "INFORMACIÓN";
            // 
            // cantidadAT
            // 
            this.cantidadAT.AutoSize = true;
            this.cantidadAT.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cantidadAT.Location = new System.Drawing.Point(563, 135);
            this.cantidadAT.Name = "cantidadAT";
            this.cantidadAT.Size = new System.Drawing.Size(160, 16);
            this.cantidadAT.TabIndex = 9;
            this.cantidadAT.Text = "Cantidad de atributos: ";
            // 
            // cantidadAR
            // 
            this.cantidadAR.AutoSize = true;
            this.cantidadAR.Location = new System.Drawing.Point(801, 137);
            this.cantidadAR.Name = "cantidadAR";
            this.cantidadAR.Size = new System.Drawing.Size(13, 13);
            this.cantidadAR.TabIndex = 10;
            this.cantidadAR.Text = "0";
            this.cantidadAR.Visible = false;
            // 
            // valoresFT
            // 
            this.valoresFT.AutoSize = true;
            this.valoresFT.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valoresFT.Location = new System.Drawing.Point(563, 163);
            this.valoresFT.Name = "valoresFT";
            this.valoresFT.Size = new System.Drawing.Size(119, 16);
            this.valoresFT.TabIndex = 11;
            this.valoresFT.Text = "Valores faltantes:";
            // 
            // valoresFR
            // 
            this.valoresFR.AutoSize = true;
            this.valoresFR.Location = new System.Drawing.Point(801, 165);
            this.valoresFR.Name = "valoresFR";
            this.valoresFR.Size = new System.Drawing.Size(13, 13);
            this.valoresFR.TabIndex = 12;
            this.valoresFR.Text = "0";
            this.valoresFR.Visible = false;
            // 
            // proporcionVT
            // 
            this.proporcionVT.AutoSize = true;
            this.proporcionVT.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.proporcionVT.Location = new System.Drawing.Point(563, 191);
            this.proporcionVT.Name = "proporcionVT";
            this.proporcionVT.Size = new System.Drawing.Size(223, 16);
            this.proporcionVT.TabIndex = 13;
            this.proporcionVT.Text = "Proporción de valores faltantes: ";
            this.proporcionVT.UseWaitCursor = true;
            // 
            // proporcionVR
            // 
            this.proporcionVR.AutoSize = true;
            this.proporcionVR.Location = new System.Drawing.Point(801, 193);
            this.proporcionVR.Name = "proporcionVR";
            this.proporcionVR.Size = new System.Drawing.Size(13, 13);
            this.proporcionVR.TabIndex = 14;
            this.proporcionVR.Text = "0";
            this.proporcionVR.Visible = false;
            // 
            // multiUso
            // 
            this.multiUso.AutoSize = true;
            this.multiUso.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.multiUso.Location = new System.Drawing.Point(563, 293);
            this.multiUso.Name = "multiUso";
            this.multiUso.Size = new System.Drawing.Size(16, 16);
            this.multiUso.TabIndex = 15;
            this.multiUso.Text = "x";
            this.multiUso.Visible = false;
            // 
            // WorkPlace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(924, 461);
            this.Controls.Add(this.multiUso);
            this.Controls.Add(this.proporcionVR);
            this.Controls.Add(this.proporcionVT);
            this.Controls.Add(this.valoresFR);
            this.Controls.Add(this.valoresFT);
            this.Controls.Add(this.cantidadAR);
            this.Controls.Add(this.cantidadAT);
            this.Controls.Add(this.informacionT);
            this.Controls.Add(this.cantidadIR);
            this.Controls.Add(this.cantidadIT);
            this.Controls.Add(this.nombreR);
            this.Controls.Add(this.nombreT);
            this.Controls.Add(this.resultadoR);
            this.Controls.Add(this.resultadoT);
            this.Controls.Add(this.datasetGrid);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "WorkPlace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WorkPlace";
            ((System.ComponentModel.ISupportInitialize)(this.datasetGrid)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView datasetGrid;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cargarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarComoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem análisisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem limpiezaToolStripMenuItem;
        private System.Windows.Forms.Label resultadoT;
        private System.Windows.Forms.Label resultadoR;
        private System.Windows.Forms.ToolStripMenuItem univariableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bivariableToolStripMenuItem;
        private System.Windows.Forms.Label nombreT;
        private System.Windows.Forms.Label nombreR;
        private System.Windows.Forms.Label cantidadIT;
        private System.Windows.Forms.Label cantidadIR;
        private System.Windows.Forms.Label informacionT;
        private System.Windows.Forms.Label cantidadAT;
        private System.Windows.Forms.Label cantidadAR;
        private System.Windows.Forms.Label valoresFT;
        private System.Windows.Forms.Label valoresFR;
        private System.Windows.Forms.Label proporcionVT;
        private System.Windows.Forms.Label proporcionVR;
        private System.Windows.Forms.ToolStripMenuItem pearsonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tschprowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mediaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem medianaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desviaciónEstándarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem boxPlotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frecuenciaToolStripMenuItem;
        private System.Windows.Forms.Label multiUso;
        private System.Windows.Forms.ToolStripMenuItem llenarValoresFaltantesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detecciónYCorrecciónDeOutliersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transformaciónDeDatosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minMaxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zscoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zscoreDesviaciónMediaAbsolutaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem muestreoDeDatosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem conRemplazoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sinnRemplazoToolStripMenuItem;
    }
}