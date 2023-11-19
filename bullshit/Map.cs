using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Numerics;
using System.Text;

namespace bullshit
{
    internal class Map
    {
        public int sizeX, sizeY;
        public int size() { return sizeX * sizeY; }
        public int width() { return sizeX; }
        public int height() { return sizeY; }
        public List<int> colPlane = new List<int>();
        public List<int> objPlane = new List<int>();
        public int horizontalSpacer, verticalSpacer;
        public static List<int> restrictedColors  = new List<int>() {1,3};
        public static List<int> restrictedObjects = new List<int>() {1,2,3,4,5,6};

        public void vSpacer() { for (int v = 0; v < verticalSpacer; v++) { Console.WriteLine(""); } }
        public void hSpacer() { for (int h = 0; h < horizontalSpacer; h++) { Console.Write("     "); } }


        public void FillPlane()
        {
            for (int i = 0; i < sizeX * sizeY; i++)
            {
                objPlane.Add(0);
                colPlane.Add(2);
            }
        }
        public void setBGColour(int id)
        {
            switch (id)
            {
                case 0: Console.BackgroundColor = ConsoleColor.Black; break;
                case 1: Console.BackgroundColor = ConsoleColor.White; break;
                case 2: Console.BackgroundColor = ConsoleColor.Green; break;
                case 3: Console.BackgroundColor = ConsoleColor.Blue; break;
                case 4: Console.BackgroundColor = ConsoleColor.Red; break;
                case 5: Console.BackgroundColor = ConsoleColor.Green; break;
            }
        }
        public void setFGColour(int id)
        {
            switch (id)
            {
                case 0: Console.ForegroundColor = ConsoleColor.Black; break;
                case 1: Console.ForegroundColor = ConsoleColor.White; break;
                case 2: Console.ForegroundColor = ConsoleColor.Green; break;
                case 3: Console.ForegroundColor = ConsoleColor.Blue; break;
                case 4: Console.ForegroundColor = ConsoleColor.Red; break;
                case 5: Console.ForegroundColor = ConsoleColor.Green; break;
            }
        }
        public bool isInsideMap(int spot)
        {
            if (spot < 0 || spot >= size()) return false;
            return true;
        }
        public bool isOnEdge(int place) {
            if (place < sizeX || place % sizeX == 0 || place % sizeX == sizeX - 1 || place > (sizeX * sizeY) - sizeX) {
                return true; }
            else return false; }
        public bool isSpotEmpty(int spot) {
            if (restrictedColors.Contains(colPlane[spot]) == true|| restrictedObjects.Contains(objPlane[spot]) == true) return false;
            //if (objPlane[spot])
            return true;}
        public int  whatsThere(int spot){
            if (isInsideMap(spot)) 
            {
                return objPlane[spot];
            }      
            return 0;
     
        }
        public int  whatsBelow(int spot)
        {
            if (isInsideMap(spot))
            {
                return colPlane[spot];
            }
            return 0;
          
        }
        public int  convertToMovement(int direction)
            {
               switch(direction) 
                {
                case 0: return -sizeX;
                case 1: return 1;
                case 2: return sizeX;
                case 3: return -1;       
                }
            return 0;
            }
        public void wallEdges()
            {
            for(int i = 0; i < sizeX*sizeY; i++)
            {
             if(isOnEdge(i))   colPlane[i] = 1;
            }
            }       
        public void Initialize()
            {
            FillPlane();
            wallEdges();
            verticalSpacer = 3;
            horizontalSpacer = 5;
            }
        public void printPlane(){
             vSpacer();
                for (int i = 0; i < sizeY; i++)
                {
                hSpacer();
                    for (int j = 0; j < sizeX; j++)
                    {
                    //Line += stringToLine;
                        int pointer = (i * sizeX) + j;
                        int intToLine = objPlane[pointer];
                        string stringToLine = Symbol.back(intToLine);
                    setFGColour(Symbol.color(objPlane[pointer]));
                    setBGColour(colPlane[pointer]);
                    Console.Write(stringToLine);
                    }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("");
         
                }
                Console.BackgroundColor = ConsoleColor.Black;
                }
        public void replace(int position,int thing){objPlane[position]=thing;}
        public void move(int pointA, int pointB)
            {
            int thing = objPlane[pointA];
            objPlane[pointA] = 0;
            objPlane[pointB]=thing;
            }
        public bool isMoveValid(int from,int move)
        {
            int to = from+move;
            if (to > (sizeX * sizeY)-1) return false;
            if (to < 0) return false;
            if (to % sizeX == sizeX - 1 && move == -1) return false;
            if (to % sizeX ==  0&& move == 1) return false;
                return true;

        }

        }
    }

