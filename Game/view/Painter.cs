using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.view
{
    class Painter
    {
        SortedList<int,Control> gameObjects = new SortedList<int, Control>(new KeyComparer<int>());
        private int margin = 20;
        private Size areaSize = new Size(1200,550);
        public void drawMapButtons(Map map, Control GroundPBox)
        {
            for (int i = 0; i < map.mapHeight; i++)
            {
                for (int j = 0; j < map.mapWidth; j++)
                {
                    if (map.allFields[i][j].GetType() == typeof(Field))
                    {
                        TriangleField tf = new TriangleField(map.allFields[i][j],
                        new Point(j * (areaSize.Width / map.mapWidth) + margin, i * (areaSize.Height / map.mapHeight) + margin),
                        new Size(((int)(areaSize.Width / (0.6 * map.mapWidth))), (int)(areaSize.Height / (1.2 * map.mapHeight))));
                        GroundPBox.Controls.Add(tf);
                        gameObjects.Add(i * 10, tf);
                    }
                    else if (map.allFields[i][j].GetType() == typeof(Wall))
                    {
                        Point p = new Point(j * (areaSize.Width / map.mapWidth) + margin, i * (areaSize.Height / map.mapHeight) + margin);
                        Size s = new Size(((int)(areaSize.Width / (0.6 * map.mapWidth))), (int)(areaSize.Height / (1.2 * map.mapHeight)));
                        WallObstacle tf = new WallObstacle(map.allFields[i][j],p,s);
                        GroundPBox.Controls.Add(tf);
                        tf.BringToFront();
                        gameObjects.Add(i * 10, tf);
                    }
                        
                }
            }
        }

        public void drawUnits(TurnManager tm, Control GroundPBox)
        {
            foreach (Unit u in tm.playingUnits)
            {
                TriangleField tf = getFieldGraphicByFiled(u.at, GroundPBox);
                Point c = tf.getCenter();
                Size s = new Size(15, 30);
                c.X -= s.Width/2;
                c.Y -= s.Height;
                UnitGamePiece ugp = new UnitGamePiece(u,c,s);
                GroundPBox.Controls.Add(ugp);
                ugp.BringToFront();
                gameObjects.Add(u.at.yPos * 10 + 1, ugp);
            }
        }

        private TriangleField getFieldGraphicByFiled(Field f, Control GroundPBox)
        {
            foreach (Control child in GroundPBox.Controls)
            {
                if (child is TriangleField)
                {
                    if (((TriangleField)child).representing.Equals(f))
                    {
                        return (TriangleField)child;
                    }
                }
            }
            throw new Exception("No such field");
        }
    }

    internal class KeyComparer<TKey> : IComparer<TKey> where TKey : IComparable
    {
        IComparer<TKey> Members;

        public int Compare(TKey x, TKey y)
        {
            int result = x.CompareTo(y);

            if (result == 0)
                return 1;   
            else
                return result;
        }

    }
}
