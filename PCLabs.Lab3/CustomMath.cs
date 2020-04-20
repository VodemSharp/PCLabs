using PCLabs.Helpers;
using System.Collections.Generic;
using System.Threading;

namespace PCLabs.Lab3
{
    public static partial class CustomMath
    {
        private static readonly int tCount = 4;

        #region Vector

        public static double[] Add(double[] va, double[] vb)
        {
            double[] vc = new double[va.Length];

            var events = new List<ManualResetEvent>();

            for (int tI = 0; tI < tCount; tI++)
            {
                var resetEvent = new ManualResetEvent(false);
                ThreadPool.QueueUserWorkItem((object tI) =>
                {
                    for (int i = (int)tI * va.Length / tCount; i < ((int)tI + 1) * va.Length / tCount; i++)
                    {
                        vc[i] = HelperMath.KahanSum(va[i], vb[i]);
                    }

                    resetEvent.Set();
                }, tI);

                events.Add(resetEvent);
            }

            WaitHandle.WaitAll(events.ToArray());

            return vc;
        }

        public static double[] Mult(double[] va, double a)
        {
            double[] vb = new double[va.Length];

            var events = new List<ManualResetEvent>();

            for (int tI = 0; tI < tCount; tI++)
            {
                var resetEvent = new ManualResetEvent(false);
                ThreadPool.QueueUserWorkItem((object tI) =>
                {
                    for (int i = (int)tI * va.Length / tCount; i < ((int)tI + 1) * va.Length / tCount; i++)
                    {
                        vb[i] = va[i] * a;
                    }

                    resetEvent.Set();
                }, tI);

                events.Add(resetEvent);
            }

            WaitHandle.WaitAll(events.ToArray());

            return vb;
        }

        #endregion

        #region Matrix

        public static double[,] Mult(double[,] ma, double[,] mb)
        {
            int rows = ma.GetLength(0);
            int cols = mb.GetLength(1);

            double[,] mc = new double[rows, cols];

            var events = new List<ManualResetEvent>();

            for (int tI = 0; tI < tCount; tI++)
            {
                var resetEvent = new ManualResetEvent(false);
                ThreadPool.QueueUserWorkItem((object tI) =>
                {
                    for (int i = (int)tI * rows / tCount; i < ((int)tI + 1) * rows / tCount; i++)
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

                    resetEvent.Set();
                }, tI);

                events.Add(resetEvent);
            }

            WaitHandle.WaitAll(events.ToArray());

            return mc;
        }

        public static double[] Mult(double[,] ma, double[] va)
        {
            int rows = ma.GetLength(0);
            int cols = ma.GetLength(1);

            double[] vb = new double[rows];

            var events = new List<ManualResetEvent>();

            for (int tI = 0; tI < tCount; tI++)
            {
                var resetEvent = new ManualResetEvent(false);
                ThreadPool.QueueUserWorkItem((object tI) =>
                {
                    for (int i = (int)tI * rows / tCount; i < ((int)tI + 1) * rows / tCount; i++)
                    {
                        vb[i] = 0;

                        for (int j = 0; j < cols; j++)
                        {
                            vb[i] = HelperMath.KahanSum(vb[i], ma[i, j] * va[j]);
                        }
                    }

                    resetEvent.Set();
                }, tI);

                events.Add(resetEvent);
            }

            WaitHandle.WaitAll(events.ToArray());

            return vb;
        }

        public static double[,] Mult(double[,] ma, double a)
        {
            int rows = ma.GetLength(0);
            int cols = ma.GetLength(1);

            double[,] mb = new double[rows, cols];

            var events = new List<ManualResetEvent>();

            for (int tI = 0; tI < tCount; tI++)
            {
                var resetEvent = new ManualResetEvent(false);
                ThreadPool.QueueUserWorkItem((object tI) =>
                {
                    for (int i = (int)tI * rows / tCount; i < ((int)tI + 1) * rows / tCount; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            mb[i, j] = ma[i, j] * a;
                        }
                    }

                    resetEvent.Set();
                }, tI);

                events.Add(resetEvent);
            }

            WaitHandle.WaitAll(events.ToArray());

            return mb;
        }

        public static double[,] Sub(double[,] ma, double[,] mb)
        {
            int rows = ma.GetLength(0);
            int cols = ma.GetLength(1);

            double[,] mc = new double[rows, cols];

            var events = new List<ManualResetEvent>();

            for (int tI = 0; tI < tCount; tI++)
            {
                var resetEvent = new ManualResetEvent(false);
                ThreadPool.QueueUserWorkItem((object tI) =>
                {
                    for (int i = (int)tI * rows / tCount; i < ((int)tI + 1) * rows / tCount; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            mc[i, j] = HelperMath.KahanSum(ma[i, j], -mb[i, j]);
                        }
                    }

                    resetEvent.Set();
                }, tI);

                events.Add(resetEvent);
            }

            WaitHandle.WaitAll(events.ToArray());

            return mc;
        }

        #endregion
    }
}
