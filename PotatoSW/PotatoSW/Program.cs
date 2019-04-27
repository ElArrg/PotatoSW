using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PotatoSW
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            PotatinPlace pp = new PotatinPlace();
            if (pp.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new WorkPlace());
            }
        }
    }
}
