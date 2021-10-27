using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Threading;

namespace WindowsFormsApp_OOP_LV4_Tetris
{
    public partial class Form1 : Form
    {
        private Object obj;
        private Object nextO;
        private int next = 0;
        SoundPlayer SP = new SoundPlayer(Properties.Resources.CW);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NewGame();
            HighScores.Instance.Load();
        }

        private void LoadStyle()
        {
            pictureBox1.Size = new Size(Settings.Instance.Colums*Settings.Instance.fieldSize, Settings.Instance.Rows * Settings.Instance.fieldSize);
            pictureBox2.Size = new Size(Settings.Instance.fieldSize * 3, Settings.Instance.fieldSize * 3);
            this.Size = new Size(pictureBox1.Width+pictureBox2.Width*2+60, pictureBox1.Height + 75);
            pictureBox1.Location = new Point(pictureBox2.Width+30,30);
            pictureBox2.Location = new Point(15, 90);
            label2.Location = new Point(pictureBox1.Location.X + pictureBox1.Width + 25, 60);
            label4.Size = new Size(pictureBox2.Width, label3.Height);
            timer1.Interval = Settings.Instance.Speed;
            pictureBox3.Location = new Point(this.Width / 2 - 35, pictureBox1.Height/2);
            pictureBox3.Visible = false;
        }

        private void NewObject()
        {
            if (obj != null)
                ObjectList.Instance.add(obj);
            
            Random r = new Random();
            if (nextO != null)
            {
                obj = new Object(Settings.Instance.Colums / 2, 0, next);
                next = r.Next(0, 6);
                nextO = new Object(1, 0, next);
            }
            else
            {
                obj = new Object(Settings.Instance.Colums / 2, 0, r.Next(0, 5));
                next = r.Next(0, 6);
                nextO = new Object(1, 0, next);
            }
            GameOver(obj);
        }

        private void GameOver(Object Oo)
        {
            bool over = false;
            foreach (Field f in Oo.Fields)
            {
                foreach (Object o in ObjectList.Instance.ObjectsValues)
                {
                    foreach (Field ff in o.Fields)
                    {
                        if (f.x == ff.x && f.y == ff.y)
                            over = true;
                    }
                }
            }
            if(over)
            {
                Settings.Instance.GameOver = true;
                Thread.Sleep(500);
                EnterName en = new EnterName();
                en.ShowDialog();
                pictureBox3.Visible = true;
            }
        }

        private void NewGame()
        {
            ObjectList.Instance.deleteAll();
            obj = null;
            nextO = null;
            Settings.Instance.Reset();
            LoadStyle();
            NewObject();
            SP.Play();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(!Settings.Instance.GameOver)
            {
                if (!obj.MoveDown())
                {
                    NewObject();
                }
                pictureBox1.Invalidate();
                pictureBox2.Invalidate();
            }      
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(!Settings.Instance.GameOver)
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        obj.MoveLeft();
                        pictureBox1.Invalidate();
                        break;
                    case Keys.Right:
                        obj.MoveRight();
                        pictureBox1.Invalidate();
                        break;
                    case Keys.Down:
                        if (!obj.MoveDown())
                        {
                            NewObject();
                        }
                        Settings.Instance.Score++;
                        pictureBox1.Invalidate();
                        break;
                    case Keys.Space:
                        if (!Settings.Instance.Pause)
                        {
                            Settings.Instance.Pause = true;
                            timer1.Enabled = false;
                            pictureBox1.Invalidate();
                        }
                        else
                        {
                            Settings.Instance.Pause = false;
                            timer1.Enabled = true;
                            pictureBox1.Invalidate();
                        }
                        break;
                    case Keys.Up:
                        obj.Rotate();
                        pictureBox1.Invalidate();
                        break;
                }
            }
            
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            // White Lines
            // NOTE: make it so that first a picture is made, then added as the background
            for (int i = Settings.Instance.fieldSize; i < Settings.Instance.Rows * Settings.Instance.fieldSize; i += Settings.Instance.fieldSize)
            {
                g.DrawLine(Pens.Gray, 0, i, pictureBox1.Size.Width, i);
            }
            for (int i = Settings.Instance.fieldSize; i < Settings.Instance.Colums * Settings.Instance.fieldSize; i += Settings.Instance.fieldSize)
            {
                g.DrawLine(Pens.Gray, i, 0, i, pictureBox1.Height);
            }

            // Curent obj
            foreach(Field f in obj.Fields)
            {
                g.FillRectangle(f.Color, new RectangleF(f.x * Settings.Instance.fieldSize, f.y * Settings.Instance.fieldSize, Settings.Instance.fieldSize, Settings.Instance.fieldSize));
            }

            // Object list
            foreach(Object o in ObjectList.Instance.ObjectsValues)
            {
                foreach (Field f in o.Fields)
                {
                    g.FillRectangle(f.Color, new RectangleF(f.x * Settings.Instance.fieldSize, f.y * Settings.Instance.fieldSize, Settings.Instance.fieldSize, Settings.Instance.fieldSize));
                }
            }

            // Picture n stuff
            if(Settings.Instance.GameOver)
            {
                Bitmap bm = new Bitmap(Properties.Resources.gameover);
                bm.MakeTransparent();
                g.DrawImage(bm, 20,20,pictureBox1.Width-40,pictureBox1.Height/2-50);
            }
            if (Settings.Instance.Pause)
            {
                g.DrawImage(Properties.Resources.Pause, 20, 20, pictureBox1.Width - 40, pictureBox1.Height / 2 - 50);
            }

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if(!Settings.Instance.GameOver && !Settings.Instance.Pause)
            {
                Settings.Instance.Time++;
                label2.Text = (Settings.Instance.Time / 100).ToString("000");
                ObjectList.Instance.CheckRows();
                foreach (Object o in ObjectList.Instance.ObjectsValues)
                {
                    o.MoveDownStatic();
                }
                label4.Text = Settings.Instance.Score.ToString();
            }
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics gg = e.Graphics;
            if(nextO != null)
            {
                foreach (Field f in nextO.Fields)
                {
                    gg.FillRectangle(f.Color, new RectangleF(f.x * Settings.Instance.fieldSize, f.y * Settings.Instance.fieldSize, Settings.Instance.fieldSize, Settings.Instance.fieldSize));
                }
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettings fs = new FormSettings();
            if(fs.ShowDialog()==DialogResult.OK)
            {
                NewGame();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Game by Nikola Icic\nEnjoy","Credits");
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            SP.Play();
        }

        private void highScoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHS hs = new FormHS();
            hs.ShowDialog();
        }

        private void musicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(musicToolStripMenuItem.Checked)
            {
                musicToolStripMenuItem.Checked = false;
                SP.Stop();
            }
            else
            {
                musicToolStripMenuItem.Checked = true;
                SP.Play();
            }
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.Image = Properties.Resources.RedoH;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = Properties.Resources.Redo;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            NewGame();
        }
    }
}
