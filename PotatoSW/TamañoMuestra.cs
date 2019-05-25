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
    public partial class TamañoMuestra : Form
    {
        public TamañoMuestra()
        {
            InitializeComponent();
        }

        public int tamaño;

        private void regresarB_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label2.Text = trackBar1.Value.ToString();
        }

        private void crearB_Click(object sender, EventArgs e)
        {
            tamaño = int.Parse(label2.Text);
        }
    }
}
