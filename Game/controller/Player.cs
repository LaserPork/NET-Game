using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Player
    {
        public int id { get; }
        public List<Unit> units { get; set; }


        public Player(int id)
        {
            this.id = id;
            units = new List<Unit>();
        }


        public void addUnit(Unit newUnit)
        {
            units.Add(newUnit);
        }

        /// <summary>
        /// Prints colorcoded beginning of ownership string
        /// </summary>
        public void printPlayer()
        {
            switch (id)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            Console.Write("Player "+id);

            Console.ResetColor();
        }

    }
}
