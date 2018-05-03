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
        /// <summary>
        /// PathFinder is used for finding paths and for distance operations
        /// </summary>
        /// <param name="mapa">Map on which will be the operations computed</param>
        public Pathfinder(Map mapa)
        {
            this.mapa = mapa;
        }

        private struct pathInfo
        {
            public Field pathParent;
            public double distanceToPathTarget;
            public int costFromPathSource;
        }
        /// <summary>
        /// Initializes the dictionary for storing informations about Fields
        /// that are required for A*
        /// </summary>
        /// <returns></returns>
        private Dictionary<Field, pathInfo> initFiledInfo()
        {
            Dictionary<Field, pathInfo> fieldInfo = new Dictionary<Field, pathInfo>();
            foreach (List<Field> radka in mapa.allFields)
            {
                foreach (Field pole in radka)
                {
                    pathInfo p = new pathInfo();
                    p.pathParent = null;
                    p.distanceToPathTarget = Double.MaxValue;
                    p.costFromPathSource = 0;
                    fieldInfo.Add(pole, p);
                }
            }

            return fieldInfo;
        }

        /// <summary>
        /// This funciton applies A* alghorithm on our map. It has low maintainablity, but thats because the alghorithm itself is quite complex.
        /// I tried to devide it into more functions but it only made the code less readable. 
        /// </summary>
        /// <param name="start">Field form which you want to start</param>
        /// <param name="end">Field where you want to get</param>
        /// <param name="u">Unit that is traveling. Unit can either fly or walk. 
        /// I didnt wanted to use object type, because i want to be able to cast spells on units, that makes them fly or get pinned to the ground</param>
        /// <returns>Returns a stack with the Fields ordered by distance from goal, or null if the Field is inaccessible</returns>
        public Stack<Field> findPath(Field start, Field end, Unit u)
        {
            Stack<Field> path = new Stack<Field>();
            List<Field> openList = new List<Field>();
            List<Field> closedList = new List<Field>();

            Dictionary<Field, pathInfo> fieldInfo = initFiledInfo();

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
                                pathInfo p = fieldInfo[n];
                                p.pathParent = current;
                                p.distanceToPathTarget = Math.Abs(n.xPos - end.xPos) + Math.Abs(n.yPos - end.yPos);
                                p.costFromPathSource = n.cost + fieldInfo[p.pathParent].costFromPathSource;
                                fieldInfo[n] = p;
                                openList.Add(n);
                                openList = openList.OrderBy((field) =>
                                    {
                                        if (fieldInfo[field].distanceToPathTarget != -1 && fieldInfo[field].costFromPathSource != -1)
                                            return fieldInfo[field].distanceToPathTarget + fieldInfo[field].costFromPathSource;
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
                temp = fieldInfo[temp].pathParent;
            }
            return path;
        }

        /// <summary>
        /// Simplified A* that returns all aviable move options for unit
        /// It is similar to findPath function.
        /// This solution is not perfect because i repeated myslef, but creating function that is appliable on both
        /// findPath and findAviableMoveOptionsForUnit was unnecessary
        /// </summary>
        /// <param name="u">Unit that is located on map and has its movement parameter set</param>
        /// <returns>List of all fields that are accesible by that Unit in one turn</returns>
        public List<Field> findAviableMoveOptionsForUnit(Unit u)
        {
            Stack<Field> path = new Stack<Field>();
            List<Field> openList = new List<Field>();
            List<Field> closedList = new List<Field>();

            Dictionary<Field, pathInfo> fieldInfo = initFiledInfo();

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
                                pathInfo p = fieldInfo[n];
                                p.pathParent = current;
                                p.costFromPathSource = n.cost + fieldInfo[p.pathParent].costFromPathSource;
                                fieldInfo[n] = p;
                                if (p.costFromPathSource <= u.movement)
                                {
                                    openList.Add(n);
                                    openList = openList.OrderBy(field => fieldInfo[field].costFromPathSource).ToList<Field>();
                                }
                            }
                        }


                    }
                }

            }
            return closedList;
        }

        /// <summary>
        /// Creates integer index from string coordinate, that is used by player
        /// </summary>
        /// <param name="coord">String coordinate. Example: "a1", "z2", "f1".</param>
        /// <returns>Direct index into map row. Example: For "a1" returns 0.</returns>
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

        /// <summary>
        /// Creates coordinate from index into map
        /// </summary>
        /// <param name="index">Index into row in map</param>
        /// <returns>A string representation of index. Example: For "0" returns "a1".</returns>
        public string indexToCoord(int index)
        {
            int ch = index / 26;
            int rest = index % 26;
            return ((char)('a' + rest)) + "" + (ch + 1);
        }
        

    }
}
