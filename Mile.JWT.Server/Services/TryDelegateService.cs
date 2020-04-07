using JWT.Server.Models;
using JWT.Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Server.Services
{
    public class TryDelegateService : ITryDelegateService
    {
        List<Pet> Pets = new List<Pet>();

        List<Pet> PetsI = new List<Pet>();

        public TryDelegateService()
        {
            Pets.Add(new Pet { ID = "001", Name = "FairHr001" });
            Pets.Add(new Pet { ID = "002", Name = "FairHr001" });
            Pets.Add(new Pet { ID = "003", Name = "FairHr001" });
            Pets.Add(new Pet { ID = "004", Name = "FairHr001" });

            PetsI.Add(new Pet { ID = "005", Name = "FairHr001" });
            PetsI.Add(new Pet { ID = "006", Name = "FairHr002" });
            PetsI.Add(new Pet { ID = "007", Name = "FairHr001" });


        }

        public delegate string Reverse(string s);

        public string SayHello(string name)
        {
            return $"Hello {name}";
        }

        public string RunDelegate(string name)
        {
            Reverse rev = SayHello;
            return rev("Fairhr");
        }

        public Dictionary<string, Pet> ConvertToDict()
        {
            var result = Pets.ToDictionary(e => e.ID);
            return result;

        }

        public IEnumerable<Pet> AllSameCompanyPet()
        {
            File.Create("");
            using (FileStream fs = File.Create(""))
            {

            }

            var fi = new FileInfo("");
            using (StreamWriter fs = fi.CreateText())
            { 
            
            }

            var result = Pets.Union(PetsI, new DogHairLengthComparer());
            return result;
        }

        public async Task<string> RunThread()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ThreadPool threadPool = new ThreadPool();
            var resultI = await threadPool.GetThreadId();
            stopwatch.Stop();
            var ts = stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            return string.Format($"{resultI},花费时间：{elapsedTime}");
        }
    }

}
