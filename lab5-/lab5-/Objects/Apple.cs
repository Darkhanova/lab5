using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab5_.Objects
{
    class Apple : BaseObject
    {
        public Apple(float x, float y, float angle) : base(x, y, angle)
        {

        }

        public override void Render(Graphics g)
        {

            g.FillEllipse(new SolidBrush(Color.Green), -15, -15, 30, 30);

        }



        public override GraphicsPath GetGraphicsPath()
        {
            Random rand = new Random();
            Form1 form = new Form1();
            var path = base.GetGraphicsPath();

            path.AddEllipse(-15, -15, 30, 30);

            return path;
        }
    }
}
