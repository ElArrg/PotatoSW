using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PotatoSW
{
    public partial class CambiarNombre : Form
    {
        public CambiarNombre()
        {
            InitializeComponent();
        }

        // Variable que almacena el nuevo nombre de la columna;
        public string nuevoNomb;

        private void nuevoNombreB_Click(object sender, EventArgs e)
        {
            nuevoNomb = nuevoNombre.Text;
        }

        private void botonRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
