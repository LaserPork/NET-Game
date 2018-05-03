using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Wall : Field
    {

        Unit occupant = null;
        public Wall(int i, int j):base(i,j)
        {
            this.cost = 1;
            this.passable = false;
            this.neighbours = new List<Field>();
        }

        
        public override char getPrintChar()
        {
            if (passingThrough != null)
            {
                return passingThrough.getPrintChar();
            }
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                return '■';
        }
    }
}
