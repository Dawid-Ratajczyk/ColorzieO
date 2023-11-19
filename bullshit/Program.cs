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
        //initializatio
        static public void fontSetup()
        {
            ConsoleHelper.SetCurrentFont("Cascadia Code SemiBold", 50);
            //Console.SetWindowSize(15, 10);
            //  Console.SetWindowPosition(50, 50);
        }
        static public void cleanBuffer() { while (Console.KeyAvailable) { Console.ReadKey(true); } }
        static public void drawHud(List<string> toDraw, int roll = 6)
        {
            for (int i = 1; i <= toDraw.Count; i++)
            {
                Console.Write(toDraw[i - 1]);
                if (i % roll == 0) Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            //setup
            fontSetup();
            List<Arrow> arrowList = new List<Arrow>();
            List<Sperg> spergList = new List<Sperg>();
            Map mapa = new Map();
            Player player = new Player();
          
            mapa.sizeX = 21;
            mapa.sizeY = 9;
            mapa.Initialize();
          //  player.position = mapa.sizeX + (mapa.sizeX / 2);
           mapa.objPlane[mapa.sizeX + (mapa.sizeX / 2)] = 1;
           mapa.objPlane[mapa.sizeX + 1] = 6;
           //mapa.colPlane[mapa.sizeX + (mapa.sizeX / 2)];

            //map load
            /*using (StreamReader file = new StreamReader("map0.txt"))
            {
                
                mapa.sizeX= file.Read()-48;
                mapa.sizeY = file.Read()-48;
                mapa.Initialize();
                file.Read();
                file.Read();


                for (int i = 0; i < mapa.size(); i++)
                {
                    int element = file.Read()-48;
                    Console.WriteLine(element+" ");
                    if (element >= 0)
                    { 
                        mapa.objPlane[i] = element;
                        if(i%mapa.sizeX == 0) Console.WriteLine();
                      //  Console.Write(element);
                    }
                 
                }

                for (int i = 0; i < mapa.size(); i++)
                {
                    int element = file.Read() - 48;
                    if (element >= 0) mapa.colPlane[i] = element;
                    
                }

                file.Close();
            }
            Thread.Sleep(2000);
            */

            for (int i=0;i<mapa.size();i++)//adding objects from map to classes
                 {
                if (mapa.objPlane[i] == 1)
                {
                    player.position = i;
                }
                if (mapa.objPlane[i]==6)
                {
                    spergList.Add(new Sperg());
                    int spergID=spergList.Count-1;
                    spergList[spergID].position = i;
                }

             }    
            //setup end


            while (true)
            {
                //render start
                cleanBuffer();
                mapa.printPlane();
                //Console.WriteLine(mapa.isOnEdge(player.position));
                //Console.WriteLine(player.position);
                //render end

                //map logic start
                
                //arrow logic
                for(int a = 0;a<arrowList.Count;a++)
                {
                    int dir = arrowList[a].direction;
                    int pos = arrowList[a].position;

                    int inFront = mapa.whatsThere(pos+dir);
                    int whatsBelow = mapa.whatsBelow(pos + dir);
                    if (inFront==6)
                    {
                        mapa.objPlane[dir + pos] = 0;
                        whatsBelow = 1;
                    }
                    else if(mapa.isMoveValid(pos,dir)&&whatsBelow!=1)
                        {
                        mapa.move(pos,pos+dir);
                        arrowList[a].position += arrowList[a].direction;
                        }
                    if(whatsBelow==1||mapa.isInsideMap(dir+pos)==false)
                        {
                        arrowList.RemoveAt(a);
                        mapa.replace(pos, 0);
                        break;
                        }
                }
                //arrow logic end

                //sperg logic start
                for (int s = 0; s < spergList.Count; s++)
                {
                    
                    int pos = spergList[s].position;
                    int dir = mapa.convertToMovement(spergList[s].move());
                    int inFront = mapa.objPlane[dir + pos];
                    if (Symbol.back(mapa.objPlane[pos])!=Sperg.symbol)
                    {
                        spergList.RemoveAt(s);
                    }
                    else if (mapa.isMoveValid(pos,dir) && mapa.isSpotEmpty(pos+dir))
                    {
                        mapa.move(pos, pos + dir);
                        spergList[s].position += dir;
                    }
                }
                //sperg logic end 
                //draw hud start
                List<string> hudELements = new List<string>() {"Position: " ,player.position.ToString()," Arrows: ",arrowList.Count.ToString()," Spergs:",spergList.Count.ToString() };
                drawHud(hudELements,6);
                //draw hud end
                //player action

                int playerAction = player.Action();//player action
                int move = 0;
                if(playerAction<4)move = mapa.convertToMovement(playerAction);//player move
                if (playerAction > 5)//player shot
                {
                    int direction = playerAction - 6;
                    int placement = player.position + mapa.convertToMovement(direction);
                    if (mapa.whatsBelow(placement)!=0 && mapa.whatsBelow(placement) != 1&& mapa.whatsThere(placement)==0)
                    {
                        arrowList.Add(new Arrow());
                        int arrowID = arrowList.Count - 1;
                        arrowList[arrowID].position = placement;
                        arrowList[arrowID].direction = mapa.convertToMovement(direction);
                        mapa.objPlane[placement] = direction + 2;
                    }
                  
                }
                //player action end

                //move = mapa.convertToMovement(player.Movement());
                if (mapa.isMoveValid(player.position, move) && mapa.isSpotEmpty(player.position + move))
                {
                    mapa.move(player.position, player.position + move);
                    player.position += move;
                }
                //map logic end
              
                //loop end start
                Thread.Sleep(200);
                Console.Clear();
                //loop end end
            }

        }
    }
}
