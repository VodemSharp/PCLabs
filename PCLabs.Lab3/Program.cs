using Newtonsoft.Json;
using PCLabs.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace PCLabs.Lab3
{
    class Program
    {
        private static double[,] Func1(double[] D, double[] B, double[,] MD, double[,] MT, double[,] MZ, double[,] ME, double a)
        {
            return CustomMath.Sub(
                CustomMath.Mult(CustomMath.Mult(MD, HelperMath.Max(CustomMath.Add(D, B))), MT),
                CustomMath.Mult(CustomMath.Mult(MZ, ME), a)
                );
        }

        private static double[] Func2(double[] Z, double[] D, double[,] MT, double[] B)
        {
            return CustomMath.Add(CustomMath.Mult(MT, CustomMath.Mult(D, HelperMath.Min(Z))), B);
        }

        static void Main(string[] args)
        {
            double a;
            double[] D, B, Z, A;
            double[,] MD, MT, MZ, ME, MA;

            a = JsonConvert.DeserializeObject<double>(File.ReadAllText($"{Constants.InitPath}/a.json"));
            D = JsonConvert.DeserializeObject<double[]>(File.ReadAllText($"{Constants.InitPath}/D.json"));
            B = JsonConvert.DeserializeObject<double[]>(File.ReadAllText($"{Constants.InitPath}/B.json"));
            Z = JsonConvert.DeserializeObject<double[]>(File.ReadAllText($"{Constants.InitPath}/Z.json"));
            MD = JsonConvert.DeserializeObject<double[,]>(File.ReadAllText($"{Constants.InitPath}/MD.json"));
            MT = JsonConvert.DeserializeObject<double[,]>(File.ReadAllText($"{Constants.InitPath}/MT.json"));
            MZ = JsonConvert.DeserializeObject<double[,]>(File.ReadAllText($"{Constants.InitPath}/MZ.json"));
            ME = JsonConvert.DeserializeObject<double[,]>(File.ReadAllText($"{Constants.InitPath}/ME.json"));

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            object locker = new object { };

            var events = new List<ManualResetEvent>();
            var resetEvent = new ManualResetEvent(false);

            ThreadPool.QueueUserWorkItem((object stateInfo) =>
            {
                MA = Func1(D, B, MD, MT, MZ, ME, a);

                lock(locker)
                {
                    //HelperMath.Print(MA);
                }

                resetEvent.Set();
            });

            events.Add(resetEvent);

            var resetEvent2 = new ManualResetEvent(false);

            ThreadPool.QueueUserWorkItem((object stateInfo) =>
            {
                A = Func2(Z, D, MT, B);

                lock (locker)
                {
                    //HelperMath.Print(A);
                }

                resetEvent2.Set();
            });

            events.Add(resetEvent2);

            WaitHandle.WaitAll(events.ToArray());

            stopwatch.Stop();
            Console.WriteLine($"Lab 3: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
