namespace PotatoSW
{
    partial class PotatinPlace
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PotatinPlace));
            this.Titulo = new System.Windows.Forms.Label();
            this.Potatin = new System.Windows.Forms.PictureBox();
            this.PotatinTime = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Potatin)).BeginInit();
            this.SuspendLayout();
            // 
            // Titulo
            // 
            this.Titulo.AutoSize = true;
            this.Titulo.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Titulo.Font = new System.Drawing.Font("Maiandra GD", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Titulo.ForeColor = System.Drawing.Color.Turquoise;
            this.Titulo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Titulo.Location = new System.Drawing.Point(158, 0);
            this.Titulo.Name = "Titulo";
            this.Titulo.Size = new System.Drawing.Size(242, 57);
            this.Titulo.TabIndex = 1;
            this.Titulo.Text = "PotatoSW";
            this.Titulo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Potatin
            // 
            this.Potatin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Potatin.Image = global::PotatoSW.Properties.Resources.Potatoe;
            this.Potatin.Location = new System.Drawing.Point(0, 0);
            this.Potatin.Name = "Potatin";
            this.Potatin.Size = new System.Drawing.Size(567, 361);
            this.Potatin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Potatin.TabIndex = 0;
            this.Potatin.TabStop = false;
            // 
            // PotatinTime
            // 
            this.PotatinTime.Interval = 300;
            this.PotatinTime.Tick += new System.EventHandler(this.PotatinTime_Tick);
            // 
            // PotatinPlace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(567, 361);
            this.Controls.Add(this.Titulo);
            this.Controls.Add(this.Potatin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PotatinPlace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.TransparencyKey = System.Drawing.SystemColors.AppWorkspace;
            ((System.ComponentModel.ISupportInitialize)(this.Potatin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Potatin;
        private System.Windows.Forms.Label Titulo;
        private System.Windows.Forms.Timer PotatinTime;
    }
}

