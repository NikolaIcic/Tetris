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
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar))
                e.Handled = true;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if(((TextBox)sender).Text != "")
            {
                if (Convert.ToInt32(((TextBox)sender).Text) <= 10)
                {
                    MessageBox.Show("Must be greater then 10");
                    ((TextBox)sender).Text = "";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.Empty() && !textBox2.Text.Empty() && !textBox3.Text.Empty() && !textBox4.Text.Empty())
            {
                Settings.Instance.Colums = Convert.ToInt32(textBox1.Text);
                Settings.Instance.Rows = Convert.ToInt32(textBox2.Text);
                Settings.Instance.fieldSize = Convert.ToInt32(textBox3.Text);
                Settings.Instance.Speed = Convert.ToInt32(textBox4.Text);
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("Fill out all the stuff");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
