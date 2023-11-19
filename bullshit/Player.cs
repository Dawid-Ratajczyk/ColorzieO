using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace bullshit
{
    internal class Player
    {
        public int position;
        public static char symbol= '☺';


        public int Movement()
        {
            ConsoleKeyInfo press;
            press = Console.ReadKey();
            string key = press.Key.ToString();
            switch (key)
            {
                case "W": return 0; 
                case "D": return 1; 
                case "S": return 2;
                case "A": return 3;
                case "X": return 4; //shoot arrow
            }
            return 5;//do nothing
        }
        public int Action() 
        {
            int move = Movement();
            if (move == 5) return -1;
            else if (move != 4) return move;
            else if (move == 4)//we shooting
            {
                ConsoleKeyInfo press;
                press = Console.ReadKey();
                string key = press.Key.ToString();
                switch (key)
                {
                    case "W": return 6; break;
                    case "D": return 7; break;
                    case "S": return 8; break;
                    case "A": return 9; break;
                }
            }
            return -1;
        }

    }
}
