using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Archer : Unit
    {
        public Archer(Player owner, Field at, Pathfinder pf,TurnManager tm, Random rng):base(owner,at, pf,tm, rng)
        {
            unitName = "Archer";
            hitPoints = 300;
            maxHitPoints = hitPoints;
            range = 50;
            attackPriority = 50;
        }

        public Archer() : base()
        {
            unitName = "Archer";
            hitPoints = 300;
            range = 50;
            attackPriority = 50;
        }

        /// <summary>
        /// Archer has range attack. Doenst check distance
        /// </summary>
        /// <param name="target">Target of the attack</param>
        public override void attack(Unit target)
        {
            printUnit();
            int dmg = attackPower*(hitPoints/maxHitPoints) + rng.Next(attackPower);
            Console.Write("deals "+dmg+" damage to ");
            target.printUnit();
            Console.WriteLine();
            target.hitPoints = target.hitPoints - dmg;
            if (target.hitPoints <= 0)
            {
                target.kill();
            }
        }

        public override char getPrintChar()
        {
            switch (owner.id)
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
            if (tm.peekUnit().Equals(this))
            {
                return 'A';
            }
            else
            {
                return 'a';
            }
        }

        public override void printUnit()
        {
            switch (owner.id)
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

            Console.Write("Archer on "+pf.indexToCoord(at.xPos)+" "+(at.yPos+1)+" with " + hitPoints+" hit points ");

            Console.ResetColor();
        }
    }
}
