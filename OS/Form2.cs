using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OS
{
	public partial class Form2 : Form
	{
		int numberOfProcesses;
		int ct = 0;
		List<Process> processes = new List<Process>();
		bool locked = false;
		public Form2()
		{
			InitializeComponent();
			button1.MouseEnter += Button1_MouseEnter;
			button1.MouseLeave += Button1_MouseLeave;
			back.MouseEnter += Back_MouseEnter;
			back.MouseLeave += Back_MouseLeave;
			clear.MouseEnter += Clear_MouseEnter;
			clear.MouseLeave += Clear_MouseLeave;
			button2.MouseEnter += Button2_MouseEnter;
			button2.MouseLeave += Button2_MouseLeave;
			listBox1.KeyDown += ListBox1_KeyDown;
		}


		private void Form2_Load(object sender, EventArgs e)
		{
			label1.Visible = false;
			label4.Visible = false;
			button2.Visible = false;
			listBox1.BackColor = Color.Black;
			listBox1.ForeColor = Color.FromArgb(0, 255, 70);
			listBox1.BorderStyle = BorderStyle.FixedSingle;
			listBox1.Font = new Font("OCR A Extended", 10);
			label45.Width = this.Width;

		}
		private void ListBox1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Back)
			{
				if (!locked)
				{
					if (listBox1.SelectedIndex != -1)
					{
						listBox1.Items.RemoveAt(listBox1.SelectedIndex);
						ct--;
					}
				}
			}
		}


		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			string t = textBox1.Text.Trim();

			if (string.IsNullOrEmpty(t))
			{
				label1.Visible = false;
				return;
			}

			if (!int.TryParse(t, out int value) || value <= 0)
			{
				label1.Text = "Enter a valid positive number";
				label1.Visible = true;
				return;
			}
			// valid
			label1.Visible = false;
			numberOfProcesses = value;
		}

		
	
		private void button1_Click(object sender, EventArgs e)
		{
			if (!locked)
			{
				string input = textBox2.Text.Trim();

				if (ct > 0)
				{
					textBox1.Enabled = false;
				}
				if (!int.TryParse(input, out int value) || value <= 0)
				{
					label4.Text = "Enter a valid positive number";
					label4.Visible = true;
					return;
				}


				if (ct > numberOfProcesses)
				{
					label4.Text = "All processes already entered";

					label4.Visible = true;
					return;
				}

				if (numberOfProcesses > 0)
				{
					listBox1.Items.Add(value);
					textBox1.Enabled = false;
					textBox2.Clear();
					label4.Visible = false;
					ct++;
				}
				if (ct == numberOfProcesses && ct > 0)
				{
					for (int i = 0; i < ct; i++)
					{
						Process p = new Process();
						p.size = (int)listBox1.Items[i];
						p.foundFrame = false;
						processes.Add(p);
					}
					listBox1.SelectionMode = SelectionMode.None;
					textBox2.Enabled = false;
					button1.ForeColor = Color.FromArgb(0, 60, 20);
					button1.Text = " ADD ";
					locked = true;
					button2.Visible = true;
				}
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Form3 f3 = new Form3(processes);
			this.Hide();
			f3.Show();
			f3.Location = new Point(this.Location.X, this.Location.Y);
		}

	
		private void back_Click(object sender, EventArgs e)
		{
			Form1 f1 = new Form1();
			this.Hide();
			f1.Show();
			f1.Location = new Point(this.Location.X, this.Location.Y);
		}

		private void clear_Click(object sender, EventArgs e)
		{
			locked = false;
			
			numberOfProcesses = 0;
			ct = 0;
			
			textBox1.Clear();
			textBox1.Enabled = true;
			
			textBox2.Clear();
			textBox2.Enabled = true;
			
			button1.ForeColor = Color.FromArgb(0, 180, 50);
			button1.Text = " ADD ";

			button2.Visible = false;
			label1.Visible = false ;
			label4.Visible = false;

			listBox1.Items.Clear();
			listBox1.SelectionMode = SelectionMode.One;
			
			processes.Clear();
		}

		private void label44_Click(object sender, EventArgs e)
		{

		}

		private void Button2_MouseLeave(object sender, EventArgs e)
		{
			button2.BackColor = Color.FromArgb(20, 20, 20);
			button2.ForeColor = Color.FromArgb(0, 180, 50);
		}

		private void Button2_MouseEnter(object sender, EventArgs e)
		{
			button2.ForeColor = Color.FromArgb(0, 255, 70);
			button2.BackColor = Color.FromArgb(15, 15, 15);
		}

		private void Clear_MouseLeave(object sender, EventArgs e)
		{
			clear.ForeColor = Color.FromArgb(0, 180, 50);
			clear.Text = "  C L E A R  ";
		}

		private void Clear_MouseEnter(object sender, EventArgs e)
		{
			clear.ForeColor = Color.FromArgb(0, 255, 70);
			clear.Text = "< C L E A R >";
		}

		private void Back_MouseLeave(object sender, EventArgs e)
		{
			back.ForeColor = Color.FromArgb(0, 180, 50);
			back.Text = "  B A C K  ";

		}

		private void Back_MouseEnter(object sender, EventArgs e)
		{

			back.ForeColor = Color.FromArgb(0, 255, 70);
			back.Text = "< B A C K >";

		}

		private void Button1_MouseLeave(object sender, EventArgs e)
		{
			if (!locked)
			{
				button1.ForeColor = Color.FromArgb(0, 180, 50);
				button1.Text = " ADD ";
			}
		}

		private void Button1_MouseEnter(object sender, EventArgs e)
		{
			if (!locked)
			{
				button1.ForeColor = Color.FromArgb(0, 255, 70);
				button1.Text = "<ADD ";
			}
		}


		//mistake
		private void textBox2_TextChanged(object sender, EventArgs e)
		{

		}
	}
	public class Process
	{
		public int size;
		public bool foundFrame;
		public Process()
		{

		}
		public Process(Process p)
		{
			this.size = p.size;
			this.foundFrame = p.foundFrame;
		}
	}
}
