﻿using Game.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Game
{
    class Program
    {
        static Map map;
        static Pathfinder pf;
        static TurnManager tm;
        static Random rng;
        static void Main(string[] args)
        {
            init();
            map.printMap();
            Application.EnableVisualStyles();
            Application.Run(new Window(map,tm)); 
            /*bool running = true;
            while (running)
            {
                printTurn();
                string command = Console.ReadLine();
                if (command.Length != 0)
                {
                    switch (command[0])
                    {
                        case 'm':
                            move(command);
                            break;
                        case 'a':
                            attack(command);
                            break;
                        case 'l':
                            line(command);
                            break;
                        case 'p':
                            map.printMap();
                            break;
                        case 't':
                            tm.printQueue();
                            break;
                        case 'e':
                        case 'q':
                            running = false;
                            break;
                        default:
                            Console.WriteLine("Unrecognized command");
                            break;
                    }
                }
            }*/
           
        }

        /// <summary>
        /// Parses console command and calls proper function
        /// Wont be used in final application
        /// </summary>
        /// <param name="command">Command that is used by user</param>
        public static void move(string command)
        {
            string[] coords = command.Split();
            if (coords.Length != 3)
            {
                Console.WriteLine("Line has to have 2 coordinates without spaces");
                return;
            }

            int x1 = pf.coordToIndex(coords[1]);
            int y1;
            Field goal;

            if (x1 == -1)
            {
                Console.WriteLine("Could not parse 1 coordinate");
                return;
            }

            if (!int.TryParse(coords[2], out y1))
            {
                Console.WriteLine("Could not parse 2 coordinate");
                return;
            }
            y1--;
            try
            {
                goal = map.allFields[y1][x1];
            }
            catch
            {
                Console.WriteLine("Incorrect coordinates");
                return;
            }
            if (goal.isOccupied())
            {
                Console.WriteLine("That field is already occupied");
                return;
            }

            Unit playing = tm.peekUnit();
            Stack<Field> path = pf.findPath(playing.at, goal, playing);
            if (path == null)
            {
                Console.WriteLine("You can't get there");
                return;
            }
            Console.Clear();
            if (path.Count > playing.movement)
            {
                Console.WriteLine("Thats too far");
                return;
            }
            Field pathField = path.Pop();
            while(path.Count != 0)
            {
               pathField.passingThrough = playing;
               map.printMap();
               System.Threading.Thread.Sleep(500);
               Console.Clear();
               pathField.passingThrough = null;
               pathField = path.Pop();
            }
            playing.move(pathField);
            /*for (int i = 0; i < playing.movement; i++)
            {
                if (path.Count == 0)
                {
                    break;
                }
                playing.move(path.Pop());
                map.printMap();
                System.Threading.Thread.Sleep(500);
                Console.Clear();
                
            }
            */
            tm.popUnit();

        }

        /// <summary>
        /// Parses console command and calls proper function
        /// </summary>
        /// <param name="command">Command that is used by user</param>
        public static void attack(string command)
        {
            string[] coords = command.Split();
            if (coords.Length != 3)
            {
                Console.WriteLine("Line has to have 2 coordinates without spaces");
                return;
            }

            int x1 = pf.coordToIndex(coords[1]);
            int y1;
            Field goal;

            if (x1 == -1)
            {
                Console.WriteLine("Could not parse 1 coordinate");
                return;
            }

            if (!int.TryParse(coords[2], out y1))
            {
                Console.WriteLine("Could not parse 2 coordinate");
                return;
            }
            y1--;
            try
            {
                goal = map.allFields[y1][x1];
            }
            catch
            {
                Console.WriteLine("Incorrect coordinates");
                return;
            }

            if (!goal.isOccupied())
            {
                Console.WriteLine("There is no unit there");
                return;
            }

            Unit playing = tm.peekUnit();
            Stack<Field> path = pf.findPath(playing.at, goal, new Dragon());
            if (path == null || path.Count > playing.range)
            {
                Console.WriteLine("You can't reach that far");
                return;
            }

            playing.attack(goal.getOccupant());

            tm.popUnit();

        }

        /// <summary>
        /// Parses console command and calls proper function
        /// </summary>
        public static void printTurn()
        {
            
            Unit onTurn = tm.peekUnit();
            List<Field> aviable = pf.findAviableMoveOptionsForUnit(onTurn);
            foreach(Field f in aviable)
            {
                f.isOnPath = true;
            }
            map.printMap();
            foreach (Field f in aviable)
            {
                f.isOnPath = false;
            }
            Console.WriteLine("Its Player " + (onTurn.owner.id) + "'s turn:");
            onTurn.printUnit();
            Console.WriteLine();
            Console.WriteLine("Attack or Move unit");
        }

        /// <summary>
        /// Parses console command and calls proper function
        /// </summary>
        /// <param name="command">Command that is used by user</param>
        static void line(string command)
        {
            string[] coords = command.Split();
            if (coords.Length != 5)
            {
                Console.WriteLine("Line has to have 4 coordinates without spaces");
                return;
            }

            int x1 = pf.coordToIndex(coords[1]);
            int y1;
            int x2 = pf.coordToIndex(coords[3]);
            int y2;

            if (x1 == -1)
            {
                Console.WriteLine("Could not parse 1 coordinate");
                return;
            }

            if (!int.TryParse(coords[2], out y1))
            {
                Console.WriteLine("Could not parse 2 coordinate");
                return;
            }

            if (x2 == -1)
            {
                Console.WriteLine("Could not parse 3 coordinate");
                return;
            }

            if (!int.TryParse(coords[4], out y2))
            {
                Console.WriteLine("Could not parse 4 coordinate");
                return;
            }

            Stack<Field> path = pf.findPath(map.allFields[y1 - 1][x1], map.allFields[y2 - 1][x2], new Soldier());
            if (path == null)
            {
                Console.WriteLine("Path not found");
                return;
            }

            foreach (Field f in path)
            {
                f.isOnPath = true;
            }

            map.printMap();

            foreach (Field f in path)
            {
                f.isOnPath = false;
            }
        }
        /// <summary>
        /// Initializes data objects before first run
        /// </summary>
        static void init()
        {
            rng = new Random();
            List<Player> players = new List<Player>();
            for (int i = 0; i < 2; i++)
            {
                players.Add(new Player(i+1));
            }

            map = new Map(25, 36);
            pf = new Pathfinder(map);
            tm = new TurnManager(players);
            
            for (int i = 25; i < 36; i++)
            {
                map.changeFields(map.allFields[4][i], new Wall(4, i));
                map.changeFields(map.allFields[20][i], new Wall(20, i));
            }
            for (int i = 5; i < 20; i++)
            {
                if (i != 12)
                {
                    map.changeFields(map.allFields[i][25], new Wall(i, 25));
                }
            }
            for (int i = 0; i < 5; i++)
            {
                new Archer(players[0], map.allFields[8 + i * 2][30],pf,tm,rng);
            }

            for (int i = 0; i < 5; i++)
            {
                new Archer(players[1], map.allFields[8 + i * 2][10],pf,tm,rng);
            }

            tm.createAttackPriorityList();
        }
    }
}
