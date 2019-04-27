using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotatoSW
{
    class AnálisisEstadístico
    {
        public double Pearson(List<double> c1, List<double> c2)
        {
            double sumaC1 = 0.0;
            double promedioC1 = 0.0;

            double sumaC2 = 0.0;
            double promedioC2 = 0.0;

            double M = 0.0;
            double S = 0.0;
            int k = 1;

            double M1 = 0.0;
            double S1 = 0.0;
            int k1 = 1;


            double d1 = 0.0;
            double d2 = 0.0;

            double sumatoria = 0.0;

            for (int i = 0; i < c1.Count; i++)
            {
                sumaC1 = sumaC1 + c1[i];
            }

            promedioC1 = sumaC1 / c1.Count();

            for (int i = 0; i < c2.Count; i++)
            {
                sumaC2 = sumaC2 + c1[i];
            }

            promedioC2 = sumaC2 / c2.Count();

            foreach (double value in c1)
            {
                double tmpM = M;
                M += (value - tmpM) / k;// promedio
                S += (value - tmpM) * (value - M);//sumatoria
                k++;
            }

            d1 = Math.Sqrt(S / (k - 2));

            foreach (double value in c2)
            {
                double tmpM1 = M1;
                M1 += (value - tmpM1) / k1;// promedio
                S1 += (value - tmpM1) * (value - M1);//sumatoria
                k1++;
            }

            d2 = Math.Sqrt(S1 / (k1 - 2));

            for (int i = 0; i < c1.Count; i++)
            {
                sumatoria = sumatoria + ((c1[i] - M) * (c2[i] - M1));
            }

            double total = sumatoria / ((c1.Count) * (d1 * d2));

            return total;
        }

        public double Tschuprow(List<string> x, List<string> y, List<string> c1, List<string> c2)
        {
            int columnas = y.Count;
            int filas = x.Count;

            int[] resultadoC = new int[columnas];
            int[] resultadoF = new int[filas];

            int[,] resultado = new int[filas, columnas];
            int p = 0;
            int q = 0;
            int total = 0;
            double[,] frecuencia = new double[filas, columnas];
            double tchi = 0;
            double resultadoT = 0;

            double[,] chi = new double[filas, columnas];

            for (int a = 0; a < filas; a++)
            {
                for (int b = 0; b < columnas; b++)
                {
                    for (int c = 0; c < c1.Count; c++)
                    {
                        if (x[a] == c1[c] && y[b] == c2[c])
                        {
                            resultado[a, b]++;
                        }
                    }
                }
            }

            for (int a = 0; a < filas; a++)
            {
                for (int b = 0; b < columnas; b++)
                {
                    resultadoC[b] = resultadoC[b] + resultado[a, b];
                    resultadoF[a] = resultadoF[a] + resultado[a, b];
                }
            }

            for (int i = 0; i < columnas; i++)
            {
                p += resultadoC[i];
            }

            for (int i = 0; i < filas; i++)
            {
                q += resultadoF[i];
            }

            if (p != q)
            {
                resultadoT = 0;
            }
            else
                total = q;

            for (int a = 0; a < filas; a++)
            {
                for (int b = 0; b < columnas; b++)
                {
                    frecuencia[a, b] = (double)(resultadoF[a] * resultadoC[b]) / total;
                }
            }

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    chi[i, j] = (Math.Pow((resultado[i, j] - frecuencia[i, j]), 2)) / frecuencia[i, j];
                }
            }

            for (int i = 0; i < filas; i++)
            {
                for (int n = 0; n < columnas; n++)
                {
                    tchi += chi[i, n];
                }
            }

            resultadoT = Math.Sqrt((tchi) / (total * (Math.Sqrt((columnas - 1) * (filas - 1)))));

            return resultadoT;
        }
    }
}
