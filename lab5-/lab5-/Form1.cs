using Lab5_.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5_
{
    public partial class Form1 : Form
    {
        List<BaseObject> objects = new List<BaseObject>();
        Player player;
        Marker marker;
        DeathZone death;       
        Apple apple;
        Apple apple2;
        Random rand = new Random();
        int count = 0;
       
        public Form1()
        {
            InitializeComponent();
            
            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);
            player.OnOverlap += (p, obj) =>
            {
                txtlog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + txtlog.Text;
            };

            // добавил реакцию на пересечение с маркером
            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };
            player.OnAppleOverlap += (m) =>
            {
                
                count++;
                objects.Remove(m);
                apple = null;
                apple = new Apple(rand.Next(0, pbMain.Width), rand.Next(0, pbMain.Height), 0);
                objects.Add(apple);
                if(count == 10)
                {
                    objects.Clear();
                    MessageBox.Show("Ты выиграл!");
                    Environment.Exit(0);
                }

            };
            player.OnZoneOverlap += (m) =>
            {
                objects.Remove(m);
                death = null;
                death = new DeathZone(rand.Next(0, pbMain.Width), rand.Next(0, pbMain.Height), 0);
                objects.Add(death);
                count--;
            };
            


            
            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);
            apple = new Apple(rand.Next(0, pbMain.Width), rand.Next(0, pbMain.Height), 0);
            apple2 = new Apple(rand.Next(0, pbMain.Width), rand.Next(0, pbMain.Height), 0);
            
            death = new DeathZone(rand.Next(0, pbMain.Width), rand.Next(0, pbMain.Height), 0);
            objects.Add(death);
            objects.Add(marker);
            objects.Add(player);
            objects.Add(apple);
            objects.Add(apple2);
            
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            counts.Text = "Очки: " + count;
            var g = e.Graphics;

            g.Clear(Color.White);

            updatePlayer();

            // пересчитываем пересечения
            foreach (var obj in objects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj);
                    obj.Overlap(player);
                }
            }

            // рендерим объекты
            foreach (var obj in objects)
            {
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }
            
        }

        public void updatePlayer()
        {
            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;
                float length = (float)Math.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;
                player.vX += dx * 0.5f;
                player.vY += dy * 0.5f;
                player.Angle = 90 - (float)Math.Atan2(player.vX, player.vY) * 180 / (float)Math.PI;
            }
            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            // пересчет позиция игрока с помощью вектора скорости
            player.X += player.vX;
            player.Y += player.vY;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            

            pbMain.Invalidate();
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            // тут добавил создание маркера по клику если он еще не создан
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker); // и главное не забыть пололжить в objects
            }

            // а это так и остается
            marker.X = e.X;
            marker.Y = e.Y;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txtlog_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.W)
            {
                player.Angle = 270;
                player.vY--;
                Refresh();
            }
            if (e.KeyCode == Keys.S)
            {
                player.Angle = 90;
                player.vY++;
                Refresh();
            }
            if (e.KeyCode == Keys.A)
            {
                player.Angle = 180;
                player.vX--;
                Refresh();
            }
            if (e.KeyCode == Keys.D)
            {
                player.Angle = 0;
                player.vX++;
                Refresh();
            }
        }
    }
}
