using System;

namespace PCLabs.Helpers
{
    public static class RandomHelper
    {
        private static readonly Random random = new Random();

        public static double GetDouble()
        {
            double temp;

            for (; ; )
            {
                temp = random.NextDouble() * 10;
                if (temp > 0.1 && temp < 9.9)
                {
                    break;
                }
            }

            return temp;
        }

        public static double[,] GenerateMatrix(int size)
        {
            double[,] matrix = new double[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = GetDouble();
                }
            }

            return matrix;
        }

        public static double[] GenerateVector(int size)
        {
            double[] vector = new double[size];

            for (int i = 0; i < size; i++)
            {
                vector[i] = GetDouble();
            }

            return vector;
        }

    }
}
