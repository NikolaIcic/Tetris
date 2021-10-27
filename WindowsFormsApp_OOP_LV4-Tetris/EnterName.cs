using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp_OOP_LV4_Tetris
{
    public partial class EnterName : Form
    {
        public EnterName()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if(!textBox1.Text.Empty())
            {
                Player p = new Player(textBox1.Text,Settings.Instance.Score);
                HighScores.Instance.AddPlayer(p);
                HighScores.Instance.Save();
                this.Close();
                FormHS hs = new FormHS();
                hs.ShowDialog();
            }
        }
    }
}
