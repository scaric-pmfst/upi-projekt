using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
    class Karte
    {
        //'\u2660', '\u2663', '\u2665', '\u2666'
        //Pik, Tref, Srce, Karo
        public List<char> Boje = new List<char> { '\u2660', '\u2663', '\u2665', '\u2666' };
        public List<string> Brojevi = new List<string> { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
    }
}
