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
	public partial class Form1 : Form
	{

		Color primaryGreen = Color.FromArgb(0, 255, 70);
		Color secGreen = Color.FromArgb(0, 180, 50);
		public Form1()
		{
			InitializeComponent();
			label41.MouseEnter += Label41_MouseEnter;
			label41.MouseLeave += Label41_MouseLeave;
			label60.MouseEnter += Label60_MouseEnter;
			label60.MouseLeave += Label60_MouseLeave;
			label59.MouseEnter += Label59_MouseEnter;
			label59.MouseLeave += Label59_MouseLeave;

		}

		private void Label59_MouseLeave(object sender, EventArgs e)
		{
			label59.ForeColor = secGreen;
			label59.BackColor = Color.FromArgb(20, 20, 20);
		}

		private void Label59_MouseEnter(object sender, EventArgs e)
		{
			label59.ForeColor = primaryGreen;
			label59.BackColor = Color.FromArgb(15, 15, 15);
		}

		private void Label60_MouseLeave(object sender, EventArgs e)
		{
			label60.ForeColor = secGreen;
			label60.BackColor = Color.FromArgb(20, 20, 20);
		}

		private void Label60_MouseEnter(object sender, EventArgs e)
		{
			label60.ForeColor = primaryGreen;
			label60.BackColor = Color.FromArgb(15, 15, 15);
		}

		private void Label41_MouseLeave(object sender, EventArgs e)
		{
			label41.ForeColor = secGreen;
			label41.BackColor = Color.FromArgb(20, 20, 20);

		}

		private void Label41_MouseEnter(object sender, EventArgs e)
		{
			label41.ForeColor = Color.FromArgb(0,255,0);
			label41.BackColor = Color.FromArgb(15,15,15);
		}
		private void label2_Click(object sender, EventArgs e)
		{
			
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			label1.ForeColor = Color.FromArgb(0, 255, 70);
		}

		private void label41_Click(object sender, EventArgs e)
		{
			Form2 f2 = new Form2();
			this.Hide();
			f2.Show();
			f2.Location = new Point(this.Location.X, this.Location.Y);
		}

		private void label60_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void label59_Click(object sender, EventArgs e)
		{
            Form6 f6 = new Form6();
            this.Hide();
            f6.Show();
            f6.Location = new Point(this.Location.X, this.Location.Y);
        }
	}
}
