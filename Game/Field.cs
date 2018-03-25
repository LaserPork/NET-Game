using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Field : IEquatable<Field>
    {
        public bool isOnPath
        {
            get;set;
        }
        public List<Field> neighbours { get; set; }
        public bool passable { get; set; }

        public Field pathParent
        {
            get;set;
        }

        public double distanceToPathTarget
        {
            get;set;
        }

        public int costFromPathSource
        {
            get;set;
        }
        public bool flyOverAble
        {
            get;set;
        }
        public int movementPrice { get; set; }

        public int xPos
        {
            get;
        }
        public int yPos
        {
            get;
        }

        public int cost
        {
            get;set;
        }

        Unit occupant = null;
        public Field(int i, int j)
        {
            this.isOnPath = false;
            this.cost = 1;
            this.xPos = j;
            this.yPos = i;
            this.passable = true;
            this.flyOverAble = true;
            this.movementPrice = 1;
            this.neighbours = new List<Field>();
        }
       
        public bool isOccupied()
        {
            if (occupant is null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void setOccupant(Unit newUnit)
        {
            if (!isOccupied())
            {
                occupant = newUnit;
            }
            else
            {
                throw new Exception("Field is already occupied by " + occupant.ToString());
            }
        }

        public Unit getOccupant()
        {
            if (isOccupied())
            {
                return occupant;
            }
            else
            {
                throw new Exception("Field is not occupied");
            }
        }

        public Unit leaveField()
        {
            if (isOccupied())
            {
                Unit oc = occupant;
                occupant = null;
                return oc;
            }
            else
            {
                throw new Exception("Field is already occupied by " + occupant.ToString());
            }
        }

        public virtual char getPrintChar()
        {
            if (isOccupied())
            {
                return occupant.getPrintChar();
            }
            if (isOnPath)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            return (char)0;
        }

        public bool Equals(Field other)
        {
            if (this.xPos == other.xPos && this.yPos == other.yPos)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Field);
        }

        public override int GetHashCode()
        {
            int result = 0;
            result = (result * 397) ^ this.xPos;
            result = (result * 397) ^ this.yPos;
            return result;
        }
    }
}
