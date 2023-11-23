using System;
using System.Collections.Generic;
using System.Text;

namespace bullshit
{
    internal class Symbol
    {
        //private List<string> iList = new List<string>(){0,1};
        //0 empty/1 player/
        private static List<string> sList = new List<string>()//colors of objects 
        {
        " ", Player.symbol.ToString(),//0 empty 1 player 
        Arrow.arrows[0], Arrow.arrows[1], Arrow.arrows[2], Arrow.arrows[3], //2-5 arrows
        Sperg.symbol//6 sperg
        };
        private static List<int> cList = new List<int>()//colors of objects
        {
        0, 1,//0 empty 1 player 
        0, 0, 0, 0, //2-5 arrows
        4//6 sperg
        };
        public static string Back(int i)
        {
            return sList[i];
        }
        public static int Color(int i)
        {
            return cList[i];
        }
    }
}
