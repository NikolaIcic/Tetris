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
    public partial class FormHS : Form
    {
        public FormHS()
        {
            InitializeComponent();
        }

        private void FormHS_Load(object sender, EventArgs e)
        {
            label1.Text = HighScores.Instance.PlayerValues[0].name;
            label2.Text = HighScores.Instance.PlayerValues[1].name;
            label3.Text = HighScores.Instance.PlayerValues[2].name;
            label4.Text = HighScores.Instance.PlayerValues[3].name;
            label5.Text = HighScores.Instance.PlayerValues[4].name;
            label6.Text = HighScores.Instance.PlayerValues[0].score.ToString();
            label7.Text = HighScores.Instance.PlayerValues[1].score.ToString();
            label8.Text = HighScores.Instance.PlayerValues[2].score.ToString();
            label9.Text = HighScores.Instance.PlayerValues[3].score.ToString();
            label10.Text = HighScores.Instance.PlayerValues[4].score.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
