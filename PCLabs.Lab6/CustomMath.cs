using System.Threading.Tasks;

namespace PCLabs.Lab6
{
    public static partial class CustomMath
    {
        #region Vector

        public static double[] Add(double[] va, double[] vb)
        {
            double[] vc = new double[va.Length];

            Parallel.For(0, va.Length, (i) =>
            {
                vc[i] = va[i] + vb[i];
            });

            return vc;
        }

        public static double[] Mult(double[] va, double a)
        {
            double[] vb = new double[va.Length];

            Parallel.For(0, va.Length, (i) =>
            {
                vb[i] = va[i] * a;
            });

            return vb;
        }

        #endregion

        #region Matrix

        public static double[,] Mult(double[,] ma, double[,] mb)
        {
            int rows = ma.GetLength(0);
            int cols = mb.GetLength(1);

            double[,] mc = new double[rows, cols];

            Parallel.For(0, rows, (i) =>
            {
                Parallel.For(0, cols, (j) =>
                {
                    mc[i, j] = 0;
                    for (int k = 0; k < ma.GetLength(1); k++)
                    {
                        mc[i, j] += ma[i, k] * mb[k, j];
                    }
                });
            });

            return mc;
        }

        public static double[] Mult(double[,] ma, double[] va)
        {
            int rows = ma.GetLength(0);
            int cols = ma.GetLength(1);

            double[] vb = new double[rows];

            Parallel.For(0, rows, (i) =>
            {
                vb[i] = 0;

                Parallel.For(0, cols, (j) =>
                {
                    vb[i] += ma[i, j] * va[j];
                });
            });

            return vb;
        }

        public static double[,] Mult(double[,] ma, double a)
        {
            int rows = ma.GetLength(0);
            int cols = ma.GetLength(1);

            double[,] mb = new double[rows, cols];

            Parallel.For(0, rows, (i) =>
            {
                Parallel.For(0, cols, (j) =>
                {
                    mb[i, j] = ma[i, j] * a;
                });
            });

            return mb;
        }

        public static double[,] Sub(double[,] ma, double[,] mb)
        {
            int rows = ma.GetLength(0);
            int cols = ma.GetLength(1);

            double[,] mc = new double[rows, cols];

            Parallel.For(0, rows, (i) =>
            {
                Parallel.For(0, cols, (j) =>
                {
                    mc[i, j] = ma[i, j] - mb[i, j];
                });
            });

            return mc;
        }

        #endregion
    }
}
