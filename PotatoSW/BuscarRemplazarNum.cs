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
    public partial class BuscarRemplazarNum : Form
    {
        public BuscarRemplazarNum()
        {
            InitializeComponent();
        }

        public string valBus;
        public string valRem;

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox1.Text))
            {
                button1.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            valBus = textBox1.Text;
            valRem = textBox2.Text;
        }
    }
}
