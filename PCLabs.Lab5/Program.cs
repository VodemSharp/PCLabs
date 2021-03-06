﻿using Newtonsoft.Json;
using PCLabs.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PCLabs.Lab5
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

            // https://blogs.msdn.microsoft.com/toub/2006/04/12/blocking-queues/
            BlockingQueue<Action> queue = new BlockingQueue<Action>();
            int countPrints = 0;

            List<Task> tasks = new List<Task> { };

            Action action1 = () =>
            {
                Thread thread = new Thread(new ThreadStart(() =>
                {
                    MA = Func1(D, B, MD, MT, MZ, ME, a);
                    queue.Enqueue(() => HelperMath.Print(MA));
                    countPrints++;
                }));

                thread.Start();
                thread.Join();
            };

            // BeginInvoke не поддерживается в .net core
            //action.BeginInvoke(action.EndInvoke, action);
            tasks.Add(Task.Run(action1));

            Action action2 = () =>
            {
                Thread thread = new Thread(new ThreadStart(() =>
                {
                    A = Func2(Z, D, MT, B);
                    queue.Enqueue(() => HelperMath.Print(A));
                    countPrints++;
                }));

                thread.Start();
                thread.Join();
            };

            // BeginInvoke не поддерживается в .net core
            //action.BeginInvoke(action2.EndInvoke, action2);
            tasks.Add(Task.Run(action2));

            Task.WaitAll(tasks.ToArray());

            if (countPrints != 0)
            {
                int count = 0;
                foreach (Action item in queue)
                {
                    item.Invoke();
            
                    if (countPrints == ++count) break;
                }
            }

            stopwatch.Stop();
            Console.WriteLine($"Lab 5: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
