using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    abstract class Unit
    {
        public Random rng
        {
            get;set;
        }

        public int attackPower
        {
            get;set;
        }
        public int range
        {
            get;set;
        }
        public int hitPoints
        {
            get;set;
        }
        public Pathfinder pf
        {
            get;
        }

        public TurnManager tm
        {
            get;
        }
        public Player owner
        {
            get;
        }

        public string unitName
        {
            get;set;
        }
        public Field at
        {
            get;set;
        }
        public int attackPriority
        {
            get;set;
        }

        public int movement
        {
            get;set;
        }
        public bool flying
        {
            get;set;
        }

        public void move(Field to)
        {
            to.setOccupant(at.leaveField());
            at = to;
        }
        public Unit(Player owner, Field at, Pathfinder pf, TurnManager tm)
        {
            this.tm = tm;
            this.pf = pf;
            attackPower = 200;
            range = 1;
            hitPoints = 1000;
            unitName = "unknown";
            attackPriority = 100;
            movement = 15;
            flying = false;
            at.setOccupant(this);
            this.at = at;
            this.owner = owner;
            owner.addUnit(this);
            rng = new Random();
        }

        public void kill()
        {
            printUnit();
            Console.WriteLine("dies.");
            at.leaveField();
            owner.units.Remove(this);
            tm.removeUnit(this);
        }
        public abstract void printUnit();

        public abstract void attack(Unit target);
        public abstract char getPrintChar();
    }
}
