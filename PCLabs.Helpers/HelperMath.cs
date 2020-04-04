using System;

namespace PCLabs.Helpers
{
    public static partial class HelperMath
    {
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

        public static void Print(double[] va)
        {
            Console.WriteLine("Vector:");

            for (int i = 0; i < va.Length; i++)
            {
                Console.Write($"{$"{va[i]:F2}",-10}");
            }

            Console.WriteLine();
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

        public static double KahanSum(params double[] fa)
        {
            double sum = 0.0f;
            double c = 0.0f;
            foreach (double f in fa)
            {
                double y = f - c;
                double t = sum + y;
                c = (t - sum) - y;
                sum = t;
            }

            return sum;
        }
    }
}
