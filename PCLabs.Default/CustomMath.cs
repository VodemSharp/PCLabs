using PCLabs.Helpers;
using System;

namespace PCLabs.Default
{
    public static partial class CustomMath
    {
        #region Vector

        public static double Max(double[] va)
        {
            double max = va[0];

            for (int i = 1; i < va.Length; i++)
            {
                if (va[i] > max)
                {
                    max = va[i];
                }
            }

            return max;
        }

        public static double Min(double[] va)
        {
            double min = va[0];

            for (int i = 1; i < va.Length; i++)
            {
                if (va[i] < min)
                {
                    min = va[i];
                }
            }

            return min;
        }

        public static double[] Add(double[] va, double[] vb)
        {
            double[] vc = new double[va.Length];

            for (int i = 0; i < va.Length; i++)
            {
                vc[i] = HelperMath.KahanSum(va[i], vb[i]);
            }

            return vc;
        }

        public static double[] Mult(double[] va, double a)
        {
            double[] vb = new double[va.Length];

            for (int i = 0; i < va.Length; i++)
            {
                vb[i] = va[i] * a;
            }

            return vb;
        }

        public static void Print(double[] va)
        {
            Console.WriteLine("Vector:");

            for (int i = 0; i < va.Length; i++)
            {
                Console.Write($"{$"{va[i]:F2}",-10}");
            }

            Console.WriteLine();
        }

        #endregion

        #region Matrix

        public static double[,] Mult(double[,] ma, double[,] mb)
        {
            int rows = ma.GetLength(0);
            int cols = mb.GetLength(1);

            double[,] mc = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    mc[i, j] = 0;
                    for (int k = 0; k < ma.GetLength(1); k++)
                    {
                        mc[i, j] = HelperMath.KahanSum(mc[i, j], ma[i, k] * mb[k, j]);
                    }
                }
            }

            return mc;
        }

        public static double[] Mult(double[,] ma, double[] va)
        {
            int rows = ma.GetLength(0);
            int cols = ma.GetLength(1);

            double[] vb = new double[rows];

            for (int i = 0; i < rows; i++)
            {
                vb[i] = 0;

                for (int j = 0; j < cols; j++)
                {
                    vb[i] = HelperMath.KahanSum(vb[i], ma[i, j] * va[j]);
                }
            }

            return vb;
        }

        public static double[,] Mult(double[,] ma, double a)
        {
            int rows = ma.GetLength(0);
            int cols = ma.GetLength(1);

            double[,] mb = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    mb[i, j] = ma[i, j] * a;
                }
            }

            return mb;
        }

        public static double[,] Sub(double[,] ma, double[,] mb)
        {
            int rows = ma.GetLength(0);
            int cols = ma.GetLength(1);

            double[,] mc = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    mc[i, j] = HelperMath.KahanSum(ma[i, j], -mb[i, j]);
                }
            }

            return mc;
        }

        public static void Print(double[,] ma)
        {
            Console.WriteLine("Matrix:");

            int rows = ma.GetLength(0);
            int cols = ma.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"{$"{ma[i, j]:F2}",-10}");
                }

                Console.WriteLine();
            }
        }

        #endregion
    }
}
