using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Pathfinder
    {
        Map mapa;
        public Pathfinder(Map mapa)
        {
            this.mapa = mapa;
        }

        public Stack<Field> findPath(Field start, Field end, Unit u)
        {
            Stack<Field> path = new Stack<Field>();
            List<Field> openList = new List<Field>();
            List<Field> closedList = new List<Field>();

            Field current = start;

            openList.Add(start);

            while (openList.Count != 0 && !closedList.Exists((x) => (x.xPos == end.xPos && x.yPos == end.yPos)))
            {
                current = openList[0];
                openList.Remove(current);
                closedList.Add(current);
                foreach (Field n in current.neighbours)
                {
                    if (!closedList.Contains(n))
                    {   
                        if ((u.flying && n.flyOverAble) || (!u.flying && n.passable && !n.isOccupied()))
                        {
                            if (!openList.Contains(n))
                            {
                                n.pathParent = current;
                                n.distanceToPathTarget = Math.Abs(n.xPos - end.xPos) + Math.Abs(n.yPos - end.yPos);
                                n.costFromPathSource = n.cost + n.pathParent.costFromPathSource;
                                openList.Add(n);
                                openList = openList.OrderBy(field =>
                                    {
                                        if (field.distanceToPathTarget != -1 && field.costFromPathSource != -1)
                                            return field.distanceToPathTarget + field.costFromPathSource;
                                        else
                                            return -1;
                                    }
                                ).ToList<Field>();
                            }
                        }
                       

                    }
                }
              
            }
            if (!closedList.Exists(x => (x.xPos == end.xPos && x.yPos == end.yPos)))
            {
                return null;
            }

            Field temp = closedList[closedList.IndexOf(current)];
            while (temp != start && temp != null)
            {
                path.Push(temp);
                temp = temp.pathParent;
            }
            return path;
        }

        public List<Field> findAviableMoveOptionsForUnit(Unit u)
        {
            Stack<Field> path = new Stack<Field>();
            List<Field> openList = new List<Field>();
            List<Field> closedList = new List<Field>();

            Field current = u.at;

            openList.Add(u.at);

            while (openList.Count != 0)
            {
                current = openList[0];
                openList.Remove(current);
                closedList.Add(current);
                foreach (Field n in current.neighbours)
                {
                    if (!closedList.Contains(n))
                    {
                        if ((u.flying && n.flyOverAble) || (!u.flying && n.passable && !n.isOccupied()))
                        {
                            if (!openList.Contains(n))
                            {
                                n.pathParent = current;
                                n.costFromPathSource = n.cost + n.pathParent.costFromPathSource;
                                if (n.costFromPathSource <= u.movement)
                                {
                                    openList.Add(n);
                                    openList = openList.OrderBy(field => field.costFromPathSource).ToList<Field>();
                                }
                            }
                        }


                    }
                }

            }
            return closedList;
        }


        public int coordToIndex(string coord)
        {
            char first = coord.ToLower()[0];
            if (first > 'z' || first < 'a')
            {
                return -1;
            }
            int rest;
            if (!int.TryParse(coord.Substring(1),out rest))
            {
                return -1;
            }
            int result = (int)(first - 'a') + (rest - 1) * 26;
            if (result > mapa.mapWidth)
            {
                return -1;
            }

            return result;
        }

        public string indexToCoord(int index)
        {
            int ch = index / 26;
            int rest = index % 26;
            return ((char)('a' + rest)) + "" + (ch + 1);
        }
        

    }
}
