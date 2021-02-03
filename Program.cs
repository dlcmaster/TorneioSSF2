using TorneioSSF2.Entities;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace TorneioSSF2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Torneio.Start();
            Challenger c1 = new Challenger("Luigi", new List<int> { 1, 4, 2 }, new List<int> { 3, 2, 2 });
            Challenger c2 = new Challenger("Meta Knight", new List<int> { 5, 3, 2 }, new List<int> { 2, 3, 2 });
            Challenger c3 = new Challenger("Jigglypuff", new List<int> { 3, 3, 2 }, new List<int> { 2, 2, 2 });
            List<Challenger> l = new List<Challenger>();
            l.Add(c1);
            l.Add(c2);
            l.Add(c3);
            l.Sort();
            foreach (var c in l)
            {
                System.Console.WriteLine(c.Nome);
            }
        }
    }
}
