using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Dragon : Unit
    {
        public Dragon(Player owner, Field at, Pathfinder pf, TurnManager tm, Random rng):base(owner,at, pf, tm, rng)
        {
            unitName = "Dragon";
            hitPoints = 10000;
            maxHitPoints = hitPoints;
            range = 1;
            attackPriority = 10;
            movement = 25;
            flying = true;
        }

        public Dragon() : base()
        {
            unitName = "Dragon";
            hitPoints = 10000;
            maxHitPoints = hitPoints;
            range = 1;
            attackPriority = 10;
            flying = true;
        }

        public override void attack(Unit target)
        {
            throw new NotImplementedException();
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
            
            return 'D';
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

            Console.Write("Dragon on "+pf.indexToCoord(at.xPos)+" "+(at.yPos+1)+" with " + hitPoints+" hit points.");

            Console.ResetColor();
        }
    }
}
