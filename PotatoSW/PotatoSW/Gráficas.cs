using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Windows.Forms.DataVisualization.Charting;

namespace PotatoSW
{
    public partial class Gráficas : Form
    {
        public Gráficas()
        {
            InitializeComponent();
        }

        List<double> BP = new List<double>();
        List<Tuple<string, int>> frecuenciaPalabras = new List<Tuple<string, int>>();

        string nombreC;

        private void Gráficas_Load(object sender, EventArgs e)
        {
            if (BP.Count > 0)
            {
                BP.Sort();

                double mediana = BP[BP.Count / 2];
                double q1 = BP[BP.Count / 4];
                double q3 = BP[3 * BP.Count / 4];
                double valorMax = BP.Last();
                double valorMin = BP.First();

                int numVal = 1;
                double media = 0;


                foreach (double value in BP)
                {
                    double tmpMedia = media;
                    media += (value - tmpMedia) / numVal;
                    numVal++;
                }

                chart1.Series[0].ChartType = SeriesChartType.BoxPlot;

                chart1.Series["Series1"].Points.AddXY(1, valorMin, valorMax, q1, q3, media, mediana);

                var chart1_POINTINDEX = chart1.Series["Series1"].Points.AddXY(1, valorMin, valorMax, q1, q3, media, mediana);

                chart1.Series["Series1"].Points[chart1_POINTINDEX].ToolTip = "Descripcion: " + nombreC + System.Environment.NewLine + "ValorMin: " + valorMin + System.Environment.NewLine + "ValorMax: " + valorMax + System.Environment.NewLine + "Q1: " + q1 + System.Environment.NewLine + "Q3: " + q3 + System.Environment.NewLine + "Media: " + media + System.Environment.NewLine + "Mediana: " + mediana;

            }
            else
            {

                chart1.Palette = ChartColorPalette.Pastel;

                for (int i = 0; i < frecuenciaPalabras.Count; i++)
                {
                    Series serie = chart1.Series.Add(frecuenciaPalabras[i].Item1.ToString());


                    serie.Label = frecuenciaPalabras[i].Item2.ToString();

                    serie.Points.Add(frecuenciaPalabras[i].Item2);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void valoresBP(List<double> numeros, string NC)
        {
            BP.Clear();
            nombreC = string.Empty;

            BP = numeros;
            nombreC = NC;
        }

        public void valoresFrecuencia(List<Tuple<string, int>> palabras)
        {
            frecuenciaPalabras.Clear();

            frecuenciaPalabras = palabras;
        }
    }
}
