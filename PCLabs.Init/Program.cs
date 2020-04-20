using Newtonsoft.Json;
using PCLabs.Helpers;
using System.IO;

namespace PCLabs.Init
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = 500;

            double a;
            double[] D, B, Z;
            double[,] MD, MT, MZ, ME;

            a = RandomHelper.GetDouble();

            D = RandomHelper.GenerateVector(size);
            B = RandomHelper.GenerateVector(size);
            Z = RandomHelper.GenerateVector(size);

            MD = RandomHelper.GenerateMatrix(size);
            MT = RandomHelper.GenerateMatrix(size);
            MZ = RandomHelper.GenerateMatrix(size);
            ME = RandomHelper.GenerateMatrix(size);

            File.WriteAllText("a.json", JsonConvert.SerializeObject(a));
            File.WriteAllText("D.json", JsonConvert.SerializeObject(D));
            File.WriteAllText("B.json", JsonConvert.SerializeObject(B));
            File.WriteAllText("Z.json", JsonConvert.SerializeObject(Z));
            File.WriteAllText("MD.json", JsonConvert.SerializeObject(MD));
            File.WriteAllText("MT.json", JsonConvert.SerializeObject(MT));
            File.WriteAllText("MZ.json", JsonConvert.SerializeObject(MZ));
            File.WriteAllText("ME.json", JsonConvert.SerializeObject(ME));
        }
    }
}
