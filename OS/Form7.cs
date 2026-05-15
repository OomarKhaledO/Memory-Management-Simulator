using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OS
{
    public partial class Form7 : Form
    {
        string referenceString;
        int frames;
        public Form7(string s, int f)
        {
            InitializeComponent();
            referenceString = s;
            frames = f;
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            Form8 f8 = new Form8(referenceString, frames, "FIFO");

            this.Hide();
            f8.Show();

            f8.Location = new Point(this.Location.X, this.Location.Y);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form8 f8 = new Form8(referenceString, frames, "Optimal");

            this.Hide();
            f8.Show();

            f8.Location = new Point(this.Location.X, this.Location.Y);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form8 f8 = new Form8(referenceString, frames, "LRU");

            this.Hide();
            f8.Show();

            f8.Location = new Point(this.Location.X, this.Location.Y);
        }
    }
}
