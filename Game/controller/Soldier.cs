using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Soldier : Unit
    {

        public Soldier(Player owner, Field at, Pathfinder pf, TurnManager tm, Random rng) : base(owner,at,pf, tm, rng)
        {

        }

        public Soldier() : base()
        {

        }

        public override void attack(Unit target)
        {
            throw new NotImplementedException();
        }

        public override char getPrintChar()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            return 'S';
        }

        public override void printUnit()
        {
            throw new NotImplementedException();
        }
    }
}
