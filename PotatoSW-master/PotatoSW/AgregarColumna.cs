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
    public partial class AgregarColumna : Form
    {
        public AgregarColumna()
        {
            InitializeComponent();
        }

        public string nombreCol;
        public string tipoCol;

        private void tipoC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tipoC != null && !string.IsNullOrEmpty(nombreC.Text))
            {
                button1.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nombreCol = nombreC.Text;
            tipoCol = tipoC.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
