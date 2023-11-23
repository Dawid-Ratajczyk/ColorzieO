using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace bullshit
{
    internal class Map
    {
        public int sizeX, sizeY;
        public int Size() { return sizeX * sizeY; }
        public int Width() { return sizeX; }
        public int Height() { return sizeY; }
        public List<int> colPlane = new List<int>();
        public List<int> objPlane = new List<int>();
        public int horizontalSpacer, verticalSpacer;
        public static List<int> restrictedColors  = new List<int>() {1,3};
        public static List<int> restrictedObjects = new List<int>() {1,2,3,4,5,6};

        public void VSpacer() { for (int v = 0; v < verticalSpacer; v++) { Console.WriteLine(""); } }
        public void HSpacer() { for (int h = 0; h < horizontalSpacer; h++) { Console.Write("     "); } }


        public void FillPlane()
        {
            for (int i = 0; i < sizeX * sizeY; i++)
            {
                objPlane.Add(0);
                colPlane.Add(2);
            }
        }
        public void SetBGColour(int id)
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
        public void SetFGColour(int id)
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
        public bool IsInsideMap(int spot)
        {
            if (spot < 0 || spot >= Size()) return false;
            return true;
        }
        public bool IsOnEdge(int place) {
            if (place < sizeX || place % sizeX == 0 || place % sizeX == sizeX - 1 || place > (sizeX * sizeY) - sizeX) {
                return true; }
            else return false; }
        public bool IsSpotEmpty(int spot) {
            if (restrictedColors.Contains(colPlane[spot]) == true|| restrictedObjects.Contains(objPlane[spot]) == true) return false;
            
            return true;}
        public int  WhatsThere(int spot){
            if (IsInsideMap(spot)) 
            {
                return objPlane[spot];
            }      
            return 0;
     
        }
        public int WhatsBelow(int spot)
        {
            if (IsInsideMap(spot))
            {
                return colPlane[spot];
            }
            return 0;
          
        }
        public int ConvertToMovement(int direction)
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
        public void WallEdges()
            {
            for(int i = 0; i < sizeX*sizeY; i++)
            {
             if(IsOnEdge(i))   colPlane[i] = 1;
            }
            }       
        public bool IsAround(int center, int thing)
        {
            for(int i=0;i<4;i++)
            {
                if (objPlane[center+ConvertToMovement(i)]==thing) return true;
            }
            return false;
        }
        public void EmptyMap()
            {
            FillPlane();
            WallEdges();
            }
        public void InitializeSpacers()
            {
            FillPlane();
            verticalSpacer = 3;
            horizontalSpacer = 5;
            }
        public void PrintPlane(){
             VSpacer();
                for (int i = 0; i < sizeY; i++)
                {
                HSpacer();
                    for (int j = 0; j < sizeX; j++)
                    {
                    //Line += stringToLine;
                        int pointer = (i * sizeX) + j;
                        int intToLine = objPlane[pointer];
                        string stringToLine = Symbol.Back(intToLine);
                    SetFGColour(Symbol.Color(objPlane[pointer]));
                    SetBGColour(colPlane[pointer]);
                    Console.Write(stringToLine);
                    }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("");
         
                }
                Console.BackgroundColor = ConsoleColor.Black;
                }
        public void Replace(int position,int thing){objPlane[position]=thing;}
        public void Move(int pointA, int pointB)
            {
            int thing = objPlane[pointA];
            objPlane[pointA] = 0;
            objPlane[pointB]=thing;
            }
        public bool IsMoveValid(int from,int move)
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

