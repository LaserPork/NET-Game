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

        public int maxHitPoints
        {
            get; set;
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
        /// <summary>
        /// Moves unit from its current field to an new empty Field
        /// </summary>
        /// <param name="to">Empty field to which you want to move this Unit</param>
        public void move(Field to)
        {
            to.setOccupant(at.leaveField());
            at = to;
        }
        /// <summary>
        /// Creates instance of Unit and sets all objects that it is dependent on
        /// </summary>
        /// <param name="owner">Player that owns this unit</param>
        /// <param name="at">At which field is unit located</param>
        /// <param name="pf">Initialized PathFinder used for movement and printing position</param>
        /// <param name="tm">Initialized TurnManager used for removing unit from the game</param>
        /// <param name="rng">Seeded Random used for attacking</param>
        public Unit(Player owner, Field at, Pathfinder pf, TurnManager tm, Random rng)
        {
            this.tm = tm;
            this.pf = pf;
            attackPower = 200;
            range = 1;
            hitPoints = 1000;
            maxHitPoints = hitPoints;
            unitName = "unknown";
            attackPriority = 100;
            movement = 15;
            flying = false;
            at.setOccupant(this);
            this.at = at;
            this.owner = owner;
            owner.addUnit(this);
            this.rng = rng;
            
        }
        /// <summary>
        /// Constructor without parameters for creating dummy units
        /// </summary>
        public Unit()
        {
            attackPower = 200;
            range = 1;
            hitPoints = 1000;
            unitName = "unknown";
            attackPriority = 100;
            movement = 15;
            flying = false;
        }
        /// <summary>
        /// Removes unit from the game after it loses all its hit points.
        /// </summary>
        public void kill()
        {
            printUnit();
            Console.WriteLine("dies.");
            at.leaveField();
            owner.units.Remove(this);
            tm.removeUnit(this);
        }
        /// <summary>
        /// Abstract method used for printing unit into the console
        /// </summary>
        public abstract void printUnit();
        /// <summary>
        /// Each unit type can define different attack style
        /// </summary>
        /// <param name="target"></param>
        public abstract void attack(Unit target);
        /// <summary>
        /// Each unit has different char that represents her on the console map.
        /// </summary>
        /// <returns></returns>
        public abstract char getPrintChar();
    }
}
