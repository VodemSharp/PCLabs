﻿using System.Collections.Generic;
using System.Threading;
using PCLabs.Helpers;

namespace PCLabs.Lab2
{
    public static partial class CustomMath
    {
        private static readonly int tCount = 4;

        #region Vector

        public static double[] Add(double[] va, double[] vb)
        {
            double[] vc = new double[va.Length];

            List<Thread> threads = new List<Thread> { };

            for (int tI = 0; tI < tCount; tI++)
            {
                Thread thread = new Thread(new ParameterizedThreadStart((tI) =>
                {
                    for (int i = (int)tI * va.Length / tCount; i < ((int)tI + 1) * va.Length / tCount; i++)
                    {
                        vc[i] = HelperMath.KahanSum(va[i], vb[i]);
                    }
                }));

                threads.Add(thread);
                thread.Start(tI);
            }

            threads.WaitAll();

            return vc;
        }

        public static double[] Mult(double[] va, double a)
        {
            double[] vb = new double[va.Length];

            List<Thread> threads = new List<Thread> { };

            for (int tI = 0; tI < tCount; tI++)
            {
                Thread thread = new Thread(new ParameterizedThreadStart((tI) =>
                {
                    for (int i = (int)tI * va.Length / tCount; i < ((int)tI + 1) * va.Length / tCount; i++)
                    {
                        vb[i] = va[i] * a;
                    }
                }));

                threads.Add(thread);
                thread.Start(tI);
            }

            threads.WaitAll();

            return vb;
        }

        #endregion

        #region Matrix

        public static double[,] Mult(double[,] ma, double[,] mb)
        {
            int rows = ma.GetLength(0);
            int cols = mb.GetLength(1);

            double[,] mc = new double[rows, cols];

            List<Thread> threads = new List<Thread> { };

            for (int tI = 0; tI < tCount; tI++)
            {
                Thread thread = new Thread(new ParameterizedThreadStart((tI) =>
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
                }));

                threads.Add(thread);
                thread.Start(tI);
            }

            threads.WaitAll();

            return mc;
        }

        public static double[] Mult(double[,] ma, double[] va)
        {
            int rows = ma.GetLength(0);
            int cols = ma.GetLength(1);

            double[] vb = new double[rows];

            List<Thread> threads = new List<Thread> { };

            for (int tI = 0; tI < tCount; tI++)
            {
                Thread thread = new Thread(new ParameterizedThreadStart((tI) =>
                {
                    for (int i = (int)tI * rows / tCount; i < ((int)tI + 1) * rows / tCount; i++)
                    {
                        vb[i] = 0;

                        for (int j = 0; j < cols; j++)
                        {
                            vb[i] = HelperMath.KahanSum(vb[i], ma[i, j] * va[j]);
                        }
                    }
                }));

                threads.Add(thread);
                thread.Start(tI);
            }

            threads.WaitAll();

            return vb;
        }

        public static double[,] Mult(double[,] ma, double a)
        {
            int rows = ma.GetLength(0);
            int cols = ma.GetLength(1);

            double[,] mb = new double[rows, cols];

            List<Thread> threads = new List<Thread> { };

            for (int tI = 0; tI < tCount; tI++)
            {
                Thread thread = new Thread(new ParameterizedThreadStart((tI) =>
                {
                    for (int i = (int)tI * rows / tCount; i < ((int)tI + 1) * rows / tCount; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            mb[i, j] = ma[i, j] * a;
                        }
                    }
                }));

                threads.Add(thread);
                thread.Start(tI);
            }

            threads.WaitAll();

            return mb;
        }

        public static double[,] Sub(double[,] ma, double[,] mb)
        {
            int rows = ma.GetLength(0);
            int cols = ma.GetLength(1);

            double[,] mc = new double[rows, cols];

            List<Thread> threads = new List<Thread> { };

            for (int tI = 0; tI < tCount; tI++)
            {
                Thread thread = new Thread(new ParameterizedThreadStart((tI) =>
                {
                    for (int i = (int)tI * rows / tCount; i < ((int)tI + 1) * rows / tCount; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            mc[i, j] = HelperMath.KahanSum(ma[i, j], -mb[i, j]);
                        }
                    }
                }));

                threads.Add(thread);
                thread.Start(tI);
            }

            threads.WaitAll();

            return mc;
        }

        #endregion
    }
}
