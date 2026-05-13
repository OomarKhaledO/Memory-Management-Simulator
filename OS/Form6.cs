using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OS
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            Load += Form6_Load;
           button2.Enabled = false;        
           
        }

        private void Form6_Load(object sender, EventArgs e)
        {

            label42.Visible = false;
            label1.Visible = false;
        }

        private void back_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.Show();
            f1.Location = new Point(this.Location.X, this.Location.Y);
        }

        private void label42_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool valid = true;
            label42.Visible = false;
            label1.Visible = false;
            string s = textBox1.Text.Trim();
            string f = textBox2.Text.Trim();

            if (s == "")
            {
                label42.Text = "Reference string is empty";
                label42.Visible = true;
                valid = false;
            }
            else
            {
           
                if (!char.IsDigit(s[0]) || !char.IsDigit(s[s.Length - 1]))
                {
                    label42.Text = "Must begin and end with a number";
                    label42.Visible = true;
                    valid = false;
                }
                else
                {
                    string[] arr = s.Split(',');

                    foreach (string item in arr)
                    {
                      
                        if (item.Length != 1 || !char.IsDigit(item[0]))
                        {
                            label42.Text = "Use single digit numbers only";
                            label42.Visible = true;
                            valid = false;
                            break;
                        }
                    }

                    
                    int count = arr.Length;

                   
                    MessageBox.Show("Numbers Count = " + count);
                }
            }
            int frames;

            if (!int.TryParse(f, out frames))
            {
                label1.Text = "Frames must be a number";
                label1.Visible = true;
                valid = false;
            }
            else if (frames <= 0)
            {
                label1.Text = "Frames must be greater than 0";
                label1.Visible = true;
                valid = false;
            }
            if (valid)
            {
                MessageBox.Show("Valid Input!");

               
                textBox1.Enabled = false;
                textBox2.Enabled = false;

                button2.Enabled = true;
                button1.Enabled = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void clear_Click(object sender, EventArgs e)
        {
         
            textBox1.Text = "";
            textBox2.Text = "";

          
            label42.Visible = false;
            label1.Visible = false;

          
            textBox1.Enabled = true;
            textBox2.Enabled = true;

          
            button1.Enabled = true;
            button2.Enabled = false;

            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s = textBox1.Text.Trim();
            int frames = int.Parse(textBox2.Text);

            Form7 f7 = new Form7(s, frames);

            this.Hide();
            f7.Show();
            f7.Location = new Point(this.Location.X, this.Location.Y);
        }
    }
}
