using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Map
    {
        public int mapHeight
        {
            get;
        }
        public int mapWidth
        {
            get;
        }

        public List<List<Field>> allFields{ get; set;}
        /// <summary>
        /// Creates rectangle map, with proper links between fields
        /// </summary>
        /// <param name="mapHeight">Number of rows</param>
        /// <param name="mapWidth">Number of columns</param>
        public Map(int mapHeight, int mapWidth)
        {
            this.mapHeight = mapHeight;
            this.mapWidth = mapWidth;
            allFields = new List<List<Field>>();
            for (int i = 0; i < mapHeight; i++)
            {
                allFields.Add(new List<Field>());
                for (int j = 0; j < mapWidth; j++)
                {
                    allFields[i].Add(new Field(i,j));
                    setFieldsNeighbours(i,j);
                }
            }
        }
        /// <summary>
        /// Swithces 2 fields.
        /// Used for changing current field to wall
        /// </summary>
        /// <param name="oldField">Reference on new field</param>
        /// <param name="newField">Reference on current field located on Map</param>
        public void changeFields(Field oldField, Field newField)
        {
            allFields[oldField.yPos][oldField.xPos] = newField;
            newField.neighbours = oldField.neighbours;
            foreach (Field n in oldField.neighbours)
            {
                n.neighbours[n.neighbours.IndexOf(oldField)] = newField;
            }
        }

        /// <summary>
        /// Hardcoded way to print colored map into console
        /// Wont be used in final game
        /// </summary>
        public void printMap()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            int numberLenght = mapHeight.ToString().Length;
            for (int i = 0; i < numberLenght; i++)
            {
                Console.Write(" ");
            }
            for (int j = 0; j < mapWidth; j++)
            {
                Console.Write(" "+(char)('a'+j%26));
            }
            Console.WriteLine(" ");
            for (int i = 0; i < mapHeight; i++)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                for (int k = 0; k < numberLenght - ((i + 1).ToString().Length); k++)
                {
                    Console.Write(" ");
                }
                Console.Write((i + 1));
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.Green;
                for (int j = 0; j < mapWidth; j++)
                {
                    if (i % 2 == 0)
                    {
                        if(j == 0)
                        {
                            Console.Write("/");
                        }
                        char field = allFields[i][j].getPrintChar();
                        if (field == 0)
                        {
                            if (j % 2 != 0)
                            {
                                Console.Write("./");
                            }
                            else
                            {
                                Console.Write("_\\");
                            }
                        }
                        else
                        {
                            Console.Write(field);
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                            Console.ForegroundColor = ConsoleColor.Green;
                            if (j % 2 != 0)
                            {
                                Console.Write("/");
                            }
                            else
                            {
                                Console.Write("\\");
                            }
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            Console.Write("\\");
                        }
                        char field = allFields[i][j].getPrintChar();
                        if (field == 0)
                        {
                            if (j % 2 == 0)
                            {
                                Console.Write("./");
                            }
                            else
                            {
                                Console.Write("_\\");
                            }
                        }
                        else
                        {
                            Console.Write(field);
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                            Console.ForegroundColor = ConsoleColor.Green;
                            if (j % 2 == 0)
                            {
                                Console.Write("/");
                            }
                            else
                            {
                                Console.Write("\\");
                            }
                        }
                    }
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                    Console.WriteLine();
            }
            Console.ResetColor();
        }

        /// <summary>
        /// Used for creating links between Fields on a Map
        /// Works only for a rectange map
        /// </summary>
        /// <param name="i">Index of row in map</param>
        /// <param name="j">Index of column in map</param>
        public void setFieldsNeighbours(int i, int j)
        {
            //pridani horizonatlni vazby na pole vlevo a zleva
            if (j != 0)
            {
                allFields[i][j].neighbours.Add(allFields[i][j-1]);
                allFields[i][j - 1].neighbours.Add(allFields[i][j]);
            }

            //pridani verikalni vazby na pole nad a shora (pro lepsi predstavu je dobre vygooglit "triangle net")
            if (i != 0)
            {
                //kazda suda radka ma vazbu nahoru v kazdem lichem poli
                if (i % 2 == 0)
                {
                    if (j % 2 != 0)
                    {
                        allFields[i][j].neighbours.Add(allFields[i - 1][j]);
                        allFields[i - 1][j].neighbours.Add(allFields[i][j]);
                    }
                }
                //kazda licha radka ma vazbu nahoru v kazdem sudem poli
                else
                {
                    if (j % 2 == 0)
                    {
                        allFields[i][j].neighbours.Add(allFields[i - 1][j]);
                        allFields[i - 1][j].neighbours.Add(allFields[i][j]);
                    }
                }
            }

        }
    }
}
