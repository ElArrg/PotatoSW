namespace PotatoSW
{
    partial class AgregarColumna
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgregarColumna));
            this.nombreCT = new System.Windows.Forms.Label();
            this.nombreC = new System.Windows.Forms.TextBox();
            this.tipoC = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nombreCT
            // 
            this.nombreCT.AutoSize = true;
            this.nombreCT.Location = new System.Drawing.Point(41, 40);
            this.nombreCT.Name = "nombreCT";
            this.nombreCT.Size = new System.Drawing.Size(214, 13);
            this.nombreCT.TabIndex = 0;
            this.nombreCT.Text = "INGRESE EL NOMBRE DE LA COLUMNA:";
            // 
            // nombreC
            // 
            this.nombreC.Location = new System.Drawing.Point(67, 75);
            this.nombreC.Name = "nombreC";
            this.nombreC.Size = new System.Drawing.Size(160, 20);
            this.nombreC.TabIndex = 1;
            // 
            // tipoC
            // 
            this.tipoC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tipoC.FormattingEnabled = true;
            this.tipoC.Items.AddRange(new object[] {
            "Categorico",
            "Numerico"});
            this.tipoC.Location = new System.Drawing.Point(35, 121);
            this.tipoC.Name = "tipoC";
            this.tipoC.Size = new System.Drawing.Size(220, 21);
            this.tipoC.TabIndex = 2;
            this.tipoC.SelectedIndexChanged += new System.EventHandler(this.tipoC_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(23, 158);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 35);
            this.button1.TabIndex = 3;
            this.button1.Text = "Agregar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(175, 158);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 35);
            this.button2.TabIndex = 4;
            this.button2.Text = "Regresar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // AgregarColumna
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 205);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tipoC);
            this.Controls.Add(this.nombreC);
            this.Controls.Add(this.nombreCT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AgregarColumna";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AgregarColumna";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nombreCT;
        private System.Windows.Forms.TextBox nombreC;
        private System.Windows.Forms.ComboBox tipoC;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}