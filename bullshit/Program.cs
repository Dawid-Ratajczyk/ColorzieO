using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Threading;

namespace bullshit
{


    internal class Program
    {
        public static List<Arrow> arrowList = new List<Arrow>();
        public static List<Sperg> spergList = new List<Sperg>();
        public static Map mapa = new Map();
        public static Player player = new Player();

  

        static public void FontSetup()
        {
            ConsoleHelper.SetCurrentFont("Cascadia Code SemiBold", 50);
            //Console.SetWindowSize(15, 10);
            //  Console.SetWindowPosition(50, 50);
        }
        static public void CleanBuffer() { while (Console.KeyAvailable) { Console.ReadKey(true); } }
        static public void DrawHud( int roll = 6)
        {
            List<string> toDraw = new List<string>() { "Position: ", player.position.ToString(), " Arrows: ", arrowList.Count.ToString(), " Spergs:", spergList.Count.ToString() };
            for (int i = 1; i <= toDraw.Count; i++)
            {
                Console.Write(toDraw[i - 1]);
                if (i % roll == 0) Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void ArrowLogic()
        {
            for (int a = 0; a < arrowList.Count; a++)
            {
                int dir = arrowList[a].direction;
                int pos = arrowList[a].position;

                int inFront = mapa.WhatsThere(pos + dir);
                int whatsBelow = mapa.WhatsBelow(pos + dir);
                if (inFront == 6)
                {
                    mapa.objPlane[dir + pos] = 0;
                    whatsBelow = 1;
                }
                else if (mapa.IsMoveValid(pos, dir) && whatsBelow != 1)
                {
                    mapa.Move(pos, pos + dir);
                    arrowList[a].position += arrowList[a].direction;
                }
                if (whatsBelow == 1 || mapa.IsInsideMap(dir + pos) == false)
                {
                    arrowList.RemoveAt(a);
                    mapa.Replace(pos, 0);
                    break;
                }
            }

        }
        static void SpergLogic()
        {
            for (int s = 0; s < spergList.Count; s++)
            {

                int pos = spergList[s].position;
                int dir = mapa.ConvertToMovement(spergList[s].Move());
                int inFront = mapa.objPlane[dir + pos];
                if (Symbol.Back(mapa.objPlane[pos]) != Sperg.symbol)
                {
                    spergList.RemoveAt(s);
                }
                else if(mapa.IsAround(pos,1))
                {
                    mapa.objPlane[pos] = 0;
                    player.health--;
                    //spergList.RemoveAt(s);
                }
                else if (mapa.IsMoveValid(pos, dir) && mapa.IsSpotEmpty(pos + dir))
                {
                    mapa.Move(pos, pos + dir);
                    spergList[s].position += dir;
                }
            }

        }
        static void PlayerLogic()
        {
            int playerAction = player.Action();//player action
            int move = 0;
            if (playerAction < 4) move = mapa.ConvertToMovement(playerAction);//player move
            if (playerAction > 5)//player shot
            {
                int direction = playerAction - 6;
                int placement = player.position + mapa.ConvertToMovement(direction);
                if (mapa.WhatsBelow(placement) != 0 && mapa.WhatsBelow(placement) != 1 && mapa.WhatsThere(placement) == 0)
                {
                    arrowList.Add(new Arrow());
                    int arrowID = arrowList.Count - 1;
                    arrowList[arrowID].position = placement;
                    arrowList[arrowID].direction = mapa.ConvertToMovement(direction);
                    mapa.objPlane[placement] = direction + 2;
                }

            }
            if (mapa.IsMoveValid(player.position, move) && mapa.IsSpotEmpty(player.position + move))
            {
                mapa.Move(player.position, player.position + move);
                player.position += move;
            }
        }
        static void AddObjectsToMap()
        {
            for (int i = 0; i < mapa.Size(); i++)//adding objects from map to classes
            {
                if (mapa.objPlane[i] == 1)
                {
                    player.position = i;
                }
                if (mapa.objPlane[i] == 6)
                {
                    spergList.Add(new Sperg());
                    int spergID = spergList.Count - 1;
                    spergList[spergID].position = i;
                }

            }
        }
        static void CreateEmptyMap()
        {
           mapa.sizeX = 21;
           mapa.sizeY = 9;
           mapa.EmptyMap();
           mapa.InitializeSpacers();
           mapa.objPlane[mapa.sizeX + (mapa.sizeX / 2)] = 1;
           mapa.objPlane[mapa.sizeX + 1] = 6;

        }
        static void GameSetup()
        {
            FontSetup();
            CreateEmptyMap();
            //LoadMap(1);
            AddObjectsToMap();
        }
        static void LoadMap(int id)
        {
            using (StreamReader file = new StreamReader("map"+id.ToString()+".txt"))
            {
                int X = 0;
                int Y = 0;

                X += (file.Read() - 48)*10;
                X += (file.Read() - 48);

                Y += (file.Read() - 48) * 10;
                Y += (file.Read() - 48);
                mapa.sizeX = X;
                mapa.sizeY = Y;

                mapa.InitializeSpacers();
                for (int i = 0; i < mapa.Size(); i++)
                {
                    int element = file.Read() - 48;
                    if (element >= 0) { mapa.objPlane[i] = element; }
                }
                for (int i = 0; i < mapa.Size(); i++)
                {
                    int element = file.Read() - 48;
                    if (element >= 0) mapa.colPlane[i] = element;
                }
                file.Close();
            }
        }
        static void Main(string[] args)
        {
            GameSetup();
            while (true)
            {
                CleanBuffer();
                mapa.PrintPlane(); 
                DrawHud(6);

                ArrowLogic();
                SpergLogic();
                PlayerLogic();

                Thread.Sleep(100);
                Console.Clear();
               
            }

        }
    }
}
