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
    public partial class PotatinPlace : Form
    {
        public PotatinPlace()
        {
            InitializeComponent();

            PotatinTime.Enabled = true;
            PotatinTime.Interval = 10000;
        }

        private void PotatinTime_Tick(object sender, EventArgs e)
        {
            PotatinTime.Stop();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
