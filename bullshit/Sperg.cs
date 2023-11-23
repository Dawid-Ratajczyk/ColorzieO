using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bullshit
{
    internal class Sperg
    {
        public int position;
        public static string symbol = "¤";
        public int Move()
        {
            Random rnd = new Random();
            return rnd.Next(0,4);
        }
    }
}
