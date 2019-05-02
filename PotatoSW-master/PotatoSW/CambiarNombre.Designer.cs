namespace PotatoSW
{
    partial class CambiarNombre
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CambiarNombre));
            this.nuevoNombreB = new System.Windows.Forms.Button();
            this.botonRegresar = new System.Windows.Forms.Button();
            this.nuevoNombre = new System.Windows.Forms.TextBox();
            this.nuevoNombreT = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // nuevoNombreB
            // 
            this.nuevoNombreB.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.nuevoNombreB.Location = new System.Drawing.Point(12, 129);
            this.nuevoNombreB.Name = "nuevoNombreB";
            this.nuevoNombreB.Size = new System.Drawing.Size(75, 23);
            this.nuevoNombreB.TabIndex = 0;
            this.nuevoNombreB.Text = "Cambiar";
            this.nuevoNombreB.UseVisualStyleBackColor = true;
            this.nuevoNombreB.Click += new System.EventHandler(this.nuevoNombreB_Click);
            // 
            // botonRegresar
            // 
            this.botonRegresar.Location = new System.Drawing.Point(135, 129);
            this.botonRegresar.Name = "botonRegresar";
            this.botonRegresar.Size = new System.Drawing.Size(75, 23);
            this.botonRegresar.TabIndex = 1;
            this.botonRegresar.Text = "Regresar";
            this.botonRegresar.UseVisualStyleBackColor = true;
            this.botonRegresar.Click += new System.EventHandler(this.botonRegresar_Click);
            // 
            // nuevoNombre
            // 
            this.nuevoNombre.Location = new System.Drawing.Point(30, 79);
            this.nuevoNombre.Name = "nuevoNombre";
            this.nuevoNombre.Size = new System.Drawing.Size(151, 20);
            this.nuevoNombre.TabIndex = 2;
            // 
            // nuevoNombreT
            // 
            this.nuevoNombreT.AutoSize = true;
            this.nuevoNombreT.Location = new System.Drawing.Point(27, 50);
            this.nuevoNombreT.Name = "nuevoNombreT";
            this.nuevoNombreT.Size = new System.Drawing.Size(165, 13);
            this.nuevoNombreT.TabIndex = 3;
            this.nuevoNombreT.Text = "INGRESE EL NUEVO NOMBRE:";
            // 
            // CambiarNombre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(222, 164);
            this.ControlBox = false;
            this.Controls.Add(this.nuevoNombreT);
            this.Controls.Add(this.nuevoNombre);
            this.Controls.Add(this.botonRegresar);
            this.Controls.Add(this.nuevoNombreB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CambiarNombre";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CambiarNombre";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button nuevoNombreB;
        private System.Windows.Forms.Button botonRegresar;
        private System.Windows.Forms.TextBox nuevoNombre;
        private System.Windows.Forms.Label nuevoNombreT;
    }
}