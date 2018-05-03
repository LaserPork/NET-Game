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
    class UnitGamePiece : Control
    {
        public Color fieldColor {
            get; set;
        }
        PointF[] shape;
        public Unit representing {
            get;set;
        }
        protected override void OnPaint(PaintEventArgs e)
        {

            Graphics graphics = e.Graphics;
            SolidBrush brush = new SolidBrush(fieldColor);
            Pen pen = new Pen(Color.Black, 2);
            graphics.FillPolygon(brush, shape);
            graphics.DrawPolygon(pen, shape);
        }

        public UnitGamePiece(Unit representing, Point location, Size size):base()
        {
            fieldColor = Color.FromArgb(90, 1, 0);
            this.representing = representing;
            this.Location = location;
            this.Size = size;
            shape = createShape();
            GraphicsPath gp = new GraphicsPath();
            gp.AddPolygon(shape);
            this.Region = new Region(gp);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            Console.WriteLine(this.GetType().ToString()+" clicked");
            base.OnMouseUp(e);
        }

        private PointF[] createShape()
        {
            return new PointF[]{
                         new PointF(0, 0),
                         new PointF(this.Width, 0),
                         new PointF(this.Width, this.Height),
                         new PointF(0, this.Height)};
        }
      
    }
}
