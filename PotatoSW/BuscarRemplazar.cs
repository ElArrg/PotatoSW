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
    public partial class BuscarRemplazar : Form
    {
        public BuscarRemplazar()
        {
            InitializeComponent();
        }

        public string datos;
        public string celda;
        public string valSel;

        public void Recibir(string data)
        {
            datos = data;
        }

        private void BuscarRemplazar_Load(object sender, EventArgs e)
        {
            string[] valores = datos.Split(',');
            for(int i = 0; i < valores.Length; i++)
            {
                comboBox1.Items.Add(valores[i]);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1 != null && !string.IsNullOrEmpty(textBox1.Text))
            {
                button1.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            celda = textBox1.Text;
            valSel = comboBox1.Text;
        }
    }
}
