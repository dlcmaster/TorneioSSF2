using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TorneioSSF2.Entities
{
    class Challenger : IComparable
    {
        public List<int> KOs { get; set; } = new List<int>();
        public List<int> Falls { get; set; } = new List<int>();
        public string Nome { get; set; }
        public int Pont { get; set; }

        public Challenger (string nome, List<int> kos, List<int> falls)
        {
            KOs = kos;
            Falls = falls;
            Nome = nome;
            Pont = kos.Sum() - falls.Sum();
        }

        public Challenger ()
        {
            KOs = new List<int>();
            Falls = new List<int>();
            Pont = 0;
        }

        public void CalculaPont ()
        {
            Pont = KOs.Sum() - Falls.Sum();
        }

        public int CompareTo(object obj)
        {
            Challenger c = obj as Challenger;
            return Pont.CompareTo(c.Pont);
        }
    }
}
