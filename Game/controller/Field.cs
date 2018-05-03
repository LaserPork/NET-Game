using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    class Field : IEquatable<Field>
    {
        /// <summary>
        /// Used for drawing yellow chars on map
        /// </summary>
        public bool isOnPath
        {
            get;set;
        }
        /// <summary>
        /// Every field has 3 neighbours
        /// </summary>
        public List<Field> neighbours { get; set; }
        /// <summary>
        /// If the field is passable by walking units
        /// </summary>
        public bool passable { get; set; }

        /// <summary>
        /// If the field is passable by flying units
        /// </summary>
        public bool flyOverAble
        {
            get;set;
        }
      

        public int xPos
        {
            get;
        }
        public int yPos
        {
            get;
        }

        /// <summary>
        /// Movement cost that the units has to pay for moving through this field
        /// Currently 1 for all fields
        /// </summary>
        public int cost
        {
            get;set;
        }

        /// <summary>
        /// Reference on unit that is located on this field
        /// </summary>
        Unit occupant = null;

        /// <summary>
        /// Reference on unit that is only moving through the field
        /// If flying unit is passing over field with unit already on
        /// </summary>
        public Unit passingThrough
        {
            get;set;
        }
        /// <summary>
        /// Creates basic passable field
        /// </summary>
        /// <param name="i">Index of row</param>
        /// <param name="j">Index of column</param>
        public Field(int i, int j)
        {
           isOnPath = false;
           cost = 1;
           xPos = j;
           yPos = i;
           passable = true;
           flyOverAble = true;
           neighbours = new List<Field>();
        }
       
        /// <summary>
        /// Returns if this field is occupied
        /// </summary>
        /// <returns>True if it has occupant, false otherwise</returns>
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

        /// <summary>
        /// Checks if the field is occupied and if it was empty sets occupant
        /// </summary>
        /// <param name="newUnit">New occupant</param>
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
            if (passingThrough != null)
            {
                return passingThrough.getPrintChar();
            }
            else if (isOccupied())
            {
                return occupant.getPrintChar();
            }
            else if (isOnPath)
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
            result += 1000 * this.xPos;
            result += this.yPos;
            return result;
        }

        public override string ToString()
        {
            return this.GetType().ToString() + " ["+xPos+","+yPos+ "]"; 
        }
    }
}
