using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Threading.Tasks;

namespace Lab5_.Objects
{
    class DeathZone : BaseObject
    {
        int w = 10;
        int h = 10;


        public DeathZone(float x, float y, float angle) : base(x, y, angle)
        {

        }




        public override void Render(Graphics g)
        {
            Color color = Color.FromArgb(255, 184, 184);
            g.FillEllipse(new SolidBrush(color), 0 - w / 2, 0 - h / 2, w, h);

        }



        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(0 - w / 2, 0 - h / 2, w, h);
            Task.Delay(30000);
            w++;
            h++;
            return path;
        }
    }



}
