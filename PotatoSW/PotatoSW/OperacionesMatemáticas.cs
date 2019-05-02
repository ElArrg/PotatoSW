using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotatoSW
{
    class OperacionesMatemáticas
    {
        public double Media(List<double> datosNum)
        {
            int numeroVal = 1;
            double media = 0.0;

            foreach (double value in datosNum)
            {
                double tmpMedia = media;
                media += (value - tmpMedia) / numeroVal;
                numeroVal++;
            }

            return media;
        }

        public double Mediana(List<double> datosNum)
        {
            double mediana = datosNum[datosNum.Count / 2];

            return mediana;
        }

        public double ModaNum(List<double> datosNum)
        {
            List<Tuple<double, int>> moda = new List<Tuple<double, int>>();

            moda.Clear();

            bool encontroModa;

            foreach (double con in datosNum)
            {
                encontroModa = false;

                for (int i = 0; i < moda.Count; i++)
                    if (moda[i].Item1 == con)
                    {

                        encontroModa = true;
                        moda[i] = new Tuple<double, int>(moda[i].Item1, moda[i].Item2 + 1);

                    }

                if (!encontroModa)
                {

                    moda.Add(new Tuple<double, int>(con, 1));

                }
            }

            moda = moda.OrderBy(t => t.Item2).ToList();

            return moda.Last().Item1;
        }

        public double desviaciónEstándar(List<double> datosNum)
        {
            double media = 0.0;
            double sumatoria = 0.0;
            int numeroVal = 1;

            double dev = 0.0;

            foreach (double value in datosNum)
            {
                double tmpMedia = media;
                media += (value - tmpMedia) / numeroVal;// promedio
                sumatoria += (value - tmpMedia) * (value - media);//sumatoria
                numeroVal++;
            }

            dev = Math.Sqrt(sumatoria / (numeroVal - 2));

            return dev;
        }
    }
}
