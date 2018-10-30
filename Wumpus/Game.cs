using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus
{
    class Game
    {
        int wins = 0;
        int losses = 0;
        int currentRoom = 1; // the index of the room the player is in.
        Room[] rooms = new Room[21]; // the rooms in the caves 

        public Game()
        {
            SetupRooms();
            ResetGame();
        }

        public void Run()
        {

            //TODO: Put your name here
            Console.WriteLine("Welcome to Hunt the Wumpus.");
            Console.WriteLine("C# version by Christopher Garcia.");
            Console.WriteLine("");

            while (true)
            {
                //TODO Write the main game loop using the flowchart in the PDF
                rooms[currentRoom].Print();
                Console.WriteLine("Move, Shoot, or quit? (m/s/q)");
                char ch = ReadChar();

                if (ch == 'q')
                {
                    break;

                }
                else if (ch == 'd')
                {
                    DumpGame();

                }

                int n = ReadInt();

                if (IsNeighbor(n) == false)
                {
                    Console.WriteLine("invalid room");
                    continue;
                }

                if (ch == 'm')
                {
                    Move(n);

                }
                else if (ch == 's')
                {
                    Shoot(n);

                }
            }
        }

        /// <summary>
        /// Initializes the room connections
        /// </summary>
        /// <param name="room"></param>
        /// <param name="neighbor1"></param>
        /// <param name="neighbor2"></param>
        /// <param name="neighbor3"></param>
        public void SetNeighbors(int room, int neighbor1, int neighbor2, int neighbor3)
        {
            rooms[room].neighbors[0] = rooms[neighbor1];
            rooms[room].neighbors[1] = rooms[neighbor2];
            rooms[room].neighbors[2] = rooms[neighbor3];
        }

        /// <summary>
        /// Creates the names and initial connections
        /// </summary>
        public void SetupRooms()
        {
            for (int i = 0; i < 21; i++)
            {
                rooms[i] = new Room();
                rooms[i].name = i.ToString(); ;
            }

            SetNeighbors(1, 2, 5, 8);
            SetNeighbors(2, 1, 3, 10);
            SetNeighbors(3, 2, 4, 12);
            SetNeighbors(4, 3, 5, 14);
            SetNeighbors(5, 1, 4, 6);
            SetNeighbors(6, 5, 7, 15);
            SetNeighbors(7, 6, 8, 17);
            SetNeighbors(8, 1, 7, 9);
            SetNeighbors(9, 8, 10, 18);
            SetNeighbors(10, 2, 9, 11);
            SetNeighbors(11, 10, 12, 19);
            SetNeighbors(12, 3, 11, 13);
            SetNeighbors(13, 12, 14, 20);
            SetNeighbors(14, 4, 13, 15);
            SetNeighbors(15, 6, 14, 16);
            SetNeighbors(16, 15, 17, 20);
            SetNeighbors(17, 7, 16, 18);
            SetNeighbors(18, 9, 17, 19);
            SetNeighbors(19, 11, 18, 20);
            SetNeighbors(20, 13, 16, 19);

        }
        /// <summary>
        /// Clears all the rooms
        /// Then repositions the bats,pits,wumpus,and player
        /// </summary>
        void ResetGame()
        {
            Random rand = new Random();
            int r;

            //TODO - Use the comments below as a guide to setup the game

            //Use a for loop to call the Reset function on all the rooms

            for (int i = 0; i < rooms.Length; i++)
            {
                rooms[i].Reset();

            }

            //Pick a random number 1 - 20 and set that room's hasWumpus variable to true
            r = rand.Next(1, 20);
            rooms[r].hasWumpus = true;

            //Pick a random number 1 - 20 and set that room's hasPit variable to true
            r = rand.Next(1, 20);
            rooms[r].hasPit = true;
            //Pick (another) random number 1 - 20 and set that room's hasPit variable to true
            r = rand.Next(1, 20);
            rooms[r].hasPit = true;

            //Pick a random number 1 - 20 and set that room's hasBat flag to true
            r = rand.Next(1, 20);
            rooms[r].hasBats = true;

            //Pick (another) random number 1 - 20 and set that room's hasBat flag to true
            r = rand.Next(1, 20);
            rooms[r].hasBats = true;

            //Use a do-while loop to place the player. Loop as 
            //as the current room has a pit or a Wumpus
           do
            {
                r = rand.Next(1, 20);
                r = currentRoom;



            } while (rooms[currentRoom].hasPit || rooms[currentRoom].hasWumpus);
            


        }


        /// <summary>
        /// Reads a char from the console
        /// If not char is entered, ' ' is returned
        /// </summary>
        /// <returns></returns>
        char ReadChar()
        {
            string s = Console.ReadLine();
            if (s.Length > 0)
            {
                return s[0];
            }
            return ' ';
        }

        /// <summary>
        /// Reads an integer from the console
        /// If the input is not a valid it, -1 is returned
        /// </summary>
        /// <returns></returns>
        int ReadInt()
        {
            Console.WriteLine("Which room?");

            string s = Console.ReadLine();
            try
            {
                return Convert.ToInt32(s);
            }
            catch 
            {
                return -1;
            }
        }

        /// <summary>
        /// Moves the player into newRoom
        /// </summary>
        /// <param name="newRoom"></param>
        void Move(int newRoom)
        {
            //TODO Complete this function using the flowchart
            //in the PDF

            currentRoom = newRoom;
            if(rooms[newRoom].hasWumpus)
            {
                Console.WriteLine("Oh no! You have wandered in the lair of the lurking wumpus");
                Console.WriteLine("***YOU HAVE DIED***");
                losses++;
                PlayAgain();

            }
            else if(rooms[newRoom].hasPit)
            {
                Console.WriteLine("AAAAAAAAAAAAaaaaaaaaaaaa............");
                Console.WriteLine("*THUD*");
                Console.WriteLine("You have fallen into a pit and died");
                losses++;
                PlayAgain();

            }
            else if (rooms[newRoom].hasBats)
            {
                Console.WriteLine("*FLAP* *FLAP* *FLAP* Bats fly you elsewhere in the caves...");
                Random randomNumbers = new Random();
                int move = randomNumbers.Next(1, 20);
                Move(move);

            }



        }

        void Shoot(int r)
        {
            //TODO Complete this function based on the flowchart in the PDF

            if (IsNeighbor(r))
            {
                if(rooms[r].hasWumpus)
                {
                    Console.WriteLine("Thwack! Groan .. *Thud*");
                    Console.WriteLine("You have slain the mighty Wumpus!");
                }
                else
                {
                    Console.WriteLine("Clunk...Your single arrow lands harmless lessy in a nearby cave");
                    Console.WriteLine("Before you can retrieve it, the enraged Wumpus devours you whole");
                    Console.WriteLine("*** YOU HAVE DIED ***");
                    losses++;
                }
            }

            PlayAgain();
  
        }


        /// <summary>
        /// Returns true if room r is a neighbor of the current room
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        bool IsNeighbor(int r)
        {

            for (int i=0; i < 3; i++)
            {
                int val  = Convert.ToInt32(rooms[currentRoom].neighbors[i].name);

                if (val == r)
                    return true;
            }

            return false;
        }
            
        /// <summary>
        /// This function dumps the rooms to the console
        /// for testing/debugging
        /// </summary>
        void DumpGame()
        {
            for (int i = 1; i < rooms.Length; i++)
            {
                Console.Write("Room " + i + ":");
                if (rooms[i].hasBats)
                    Console.Write(" bats");
                if (rooms[i].hasPit)
                    Console.Write(" pit");
                if (rooms[i].hasWumpus)
                    Console.Write(" wumpus");
                Console.WriteLine("");
            }
        }

        /// <summary>
        /// Asks the player 
        /// </summary>
        void PlayAgain()
        {
            //TODO - ask the player to play again
            //if the answer is 'y' call ResetGame()
            //otherwise, call Environment.Exit(0);

            Console.WriteLine("Wins: " + wins + "| Losses: " + losses);
            Console.WriteLine("Play Again? (y/n)");
            ReadChar();

            if (ReadChar() == 'y')
            {
                ResetGame();
              
            }
            else
                Environment.Exit(0);


        }

    }
}
