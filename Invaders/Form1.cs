using System;
using System.Drawing;
using System.Windows.Forms;

namespace Invaders
{
    public partial class Form1 : Form
    {
        Graphics g;

        int spacePos = 220;
        int shootPos = 0;
        int shootPos1 = 0;
        bool shooting;

        int[] enemyones = { 1, 1, 1, 1, 1 };
        int[] enemytwoss = { 1, 1, 1, 1, 1 };

        public Form1()
        {
            InitializeComponent();
        }

        Image player = global::Invaders.Properties.Resources.plyer;
        Image bullet = global::Invaders.Properties.Resources.bullet;
        Image enemyone = global::Invaders.Properties.Resources.enemyone;
        Image enemyone1 = global::Invaders.Properties.Resources.enemyone1;
        Image enemytwo = global::Invaders.Properties.Resources.enemytwo;
        Image enemytwo1 = global::Invaders.Properties.Resources.enemytwo1;

        private void Shoot()
        {
            shootPos = spacePos + 19;
            shootPos1 = 350;
            shooting = true;
        }

        private void DoneShoot()
        {
            shootPos = 0;
            shootPos1 = 0;
            shooting = false;
        }

        int ind = 0;
        bool toggle = true;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ind > 5)
            {
                ind = 0;
                toggle = !toggle;
            }
            ind++;
            g = Graphics.FromHwnd(panel1.Handle);
            g.Clear(Color.Black);

            g.DrawImage(player, spacePos, 355, 40, 25);

            for (int i = 0; i < 5; i++)
            {
                if (enemyones[i] == 1)
                    if(toggle)
                        g.DrawImage(enemyone, 100+(60*i), 20, 30, 30);
                    else
                        g.DrawImage(enemyone1, 100 + (60 * i), 20, 30, 30);
            }

            for (int i = 0; i < 5; i++)
            {
                if (enemytwoss[i] == 1)
                    if (toggle)
                        g.DrawImage(enemytwo, 100+(60 * i), 100, 30, 30);
                    else
                        g.DrawImage(enemytwo1, 100 + (60 * i), 100, 30, 30);
            }

            if (shooting)
            {
                if (!CheckShot())
                g.DrawImage(bullet, shootPos, shootPos1, 3, 6);
                shootPos1 -= 20;
            }

            g.Dispose();
        }

        private bool CheckShot()
        {
            if (shooting == true)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (enemyones[i] == 1)
                    {
                        double pos = 100 + (60 * i);
                        if ((shootPos1 >= 20 && shootPos1 <= 70) && (shootPos >= pos && shootPos <= pos + 30))
                        {
                            enemyones[i] = 0;
                            DoneShoot();
                            return true;
                        }
                    }
                }

                for (int i = 0; i < 5; i++)
                {
                    if (enemytwoss[i] == 1)
                    {
                        double pos = 100 + (60 * i);
                        if ((shootPos1 >= 100 && shootPos1 <= 150) && (shootPos >= pos && shootPos <= pos + 30))
                        {
                            enemytwoss[i] = 0;
                            DoneShoot();
                            return true;
                        }
                    }
                }

                if (shootPos1 <= 0)
                {
                    DoneShoot();
                    return true;
                }
            }

            return false;
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
                spacePos+=5;
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left) 
                spacePos-=5;
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
                if(!shooting)
                    Shoot();
        }

    }
}
