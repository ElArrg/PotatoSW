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
    public partial class ModificarDominio : Form
    {
        public ModificarDominio()
        {
            InitializeComponent();
        }

        public string datos;

        private void ModificarDominio_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = datos;
        }

        public void Recibir(string data)
        {
            datos = data;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            datos = richTextBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
