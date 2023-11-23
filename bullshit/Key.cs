using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Windows.Input;

namespace bullshit
{

    internal class Key
    {
        public static bool Pressed(string Button)
        {
            char c = Button[0];

            if (Console.KeyAvailable)
                if (Console.ReadKey(false).Key == ConsoleKey.C)
                {
                return true;
                }
        return false;
        }
    }



    }

