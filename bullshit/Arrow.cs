using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bullshit
{
    internal class Arrow
    {
        public int position,direction;
        public static new List<string> arrows = new List<string>() { "^", ">", "v", "<" };
        public static new List<int> killable = new List<int>() { 6 };
        public string currentArrow(){return arrows[direction];}
        
        public string symbol(int choice) { return arrows[choice];}
        public void move(int move){position += move;}

    }
}
