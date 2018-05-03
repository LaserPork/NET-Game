using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.view
{
    class TriangleField : Control
    {
        public Color fieldColor {
            get; set;
        }
        PointF[] shape;
        public Field representing {
            get;set;
        }
        protected override void OnPaint(PaintEventArgs e)
        {

            Graphics graphics = e.Graphics;
            SolidBrush brush = new SolidBrush(fieldColor);
            graphics.FillPolygon(brush, shape);
            
        }

        public Point getCenter()
        {
            Point res = new Point(0, 0); 
            foreach (PointF p in shape)
            {
                res.X +=(int)p.X+this.Left;
                res.Y +=(int)p.Y+this.Top;
            }
            res.X /= shape.Length;
            res.Y /= shape.Length;
            return res;
        }

        public TriangleField(Field representing, Point location, Size size):base()
        {
            fieldColor = Color.FromArgb(1, 90, 0);
            this.representing = representing;
            this.Location = location;
            this.Size = size;
            if (representing.yPos % 2 == 0)
            {
                if (representing.xPos % 2 != 0)
                {
                    shape = makeTriangeUp();
                }
                else
                {
                    shape = makeTriangeDown();
                }
            }
            else
            {
                if (representing.xPos % 2 == 0)
                {
                    shape = makeTriangeUp();
                }
                else
                {
                    shape = makeTriangeDown();
                }
            }
               
            GraphicsPath gp = new GraphicsPath();
            gp.AddPolygon(shape);
            this.Region = new Region(gp);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            Console.WriteLine("clicked");
            base.OnMouseUp(e);
        }

        private PointF[] makeTriangeUp()
        {
            return new PointF[]{
                         new PointF(this.Width / 2, 0),
                         new PointF(0, this.Height),
                         new PointF(this.Width, this.Height) };
        }
        private PointF[] makeTriangeDown()
        {
            return new PointF[]{
                         new PointF(this.Width / 2, this.Height),
                         new PointF(this.Width, 0),
                         new PointF(0, 0) };
        }
    }
}
