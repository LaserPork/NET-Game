using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.view
{
    class Window : Form
    {
        private Painter gamePainter = new Painter();
        private Graphics g;
        private Panel panel1;
        private PictureBox pictureBox2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private PictureBox pictureBox1;
        List<oldControlSize> oldControlSizes = new List<oldControlSize>();

        public struct oldControlSize
        {
            public string name;
            public Size size;
            public Point location;
        }

        private SizeF oldSize;

        private void InitializeComponent()
        {
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AccessibleName = "GamePanel";
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1264, 681);
            this.panel1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.AccessibleName = "BackgroundGround";
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Location = new System.Drawing.Point(0, 98);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1264, 583);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.AccessibleName = "Background";
            this.pictureBox2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1264, 681);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // Window
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "Window";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        public Window(Map map, TurnManager tm)
        {
            Console.WriteLine("Start");
            g = this.CreateGraphics();
            InitializeComponent();
            oldSize = base.Size;

            gamePainter.drawMapButtons(map, pictureBox1);
            gamePainter.drawUnits(tm, pictureBox1);
            //SaveAllControls();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            foreach (Control cnt in this.Controls)
            {
                ResizeControl(cnt, base.Size);
            }
            oldSize = base.Size;
            
        }

      
        private void ResizeControl(Control cnt, SizeF newSize)
        {
            
            float iWidth = newSize.Width - oldSize.Width;
            cnt.Left += (int)((cnt.Left * iWidth) / oldSize.Width);
            cnt.Width += (int)((cnt.Width * iWidth) / oldSize.Width);
            Console.WriteLine(iWidth);

            float iHeight = newSize.Height - oldSize.Height;
            cnt.Top += (int)((cnt.Top * iHeight) / oldSize.Height);
            cnt.Height += (int)((cnt.Height * iHeight) / oldSize.Height);
            
            //cnt.Scale(new SizeF(newSize.Width/oldSize.Width, newSize.Height / oldSize.Height));
            foreach (Control cnt2 in cnt.Controls)
            {
                ResizeControl(cnt2, newSize);
            }
        }
        /*
        public void SaveAllControls()
        {
            foreach (Control defaultControl in this.Controls)
            {
                oldControlSize defControl = new oldControlSize();
                if (defaultControl != null)
                {
                    defControl.name = defaultControl.Name;
                    defControl.size = defaultControl.Size;
                    defControl.location = defaultControl.Location;
                    this.oldControlSizes.Add(defControl);
                }
            }
        }*/

        
    }
}
