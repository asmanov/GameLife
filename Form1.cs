using GameLife.Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GameLife
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private int resolution;
        private Game game;



        public Form1()
        {
            InitializeComponent();
        }

        private void DrawNextGeneration()
        {
            graphics.Clear(Color.FromArgb(255, 0, 0, 0));
            var field = game.GetCurrentGeneration();
            
            for (int x = 0; x < field.GetLength(0); x++)
            {
                for (int y = 0; y < field.GetLength(1); y++)
                {
                    if (field[x, y] != null)
                    {
                        if (field[x, y].Age == 500)
                        {
                            timer1.Stop();
                            label2.BackColor = field[x, y].ColorItem;
                            label2.Text = "500";
                        }
                        graphics.FillEllipse(field[x, y].CellColor, x * resolution, y * resolution, resolution - 1, resolution - 1);
                        field[x, y].Age++;
                    }
                }
            }
            pictureBox1.Refresh();
            game.NextGeneration();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawNextGeneration();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
                return;

            nubRes.Enabled = false;
            nudDens.Enabled = false;
            resolution = (int)nubRes.Value;

            game = new Game
                (
                    rows: pictureBox1.Height / resolution,
                    columns: pictureBox1.Width / resolution,
                    density: (int)(nudDens.Minimum) + (int)nudDens.Maximum - (int)nudDens.Value
                );

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            timer1.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
                return;
            timer1.Stop();
            nubRes.Enabled = true;
            nudDens.Enabled = true;
        }



        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!timer1.Enabled) return;

            if (e.Button == MouseButtons.Left)
            {
                var x = e.Location.X / resolution;
                var y = e.Location.Y / resolution;
                game.AddCell(x, y);
            }
            if (e.Button == MouseButtons.Right)
            {
                var x = e.Location.X / resolution;
                var y = e.Location.Y / resolution;
                game.RemoveCell(x, y);
            }
        }
    }
}
