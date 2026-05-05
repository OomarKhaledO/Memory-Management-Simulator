using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OS
{
	public partial class Form3 : Form
	{
		int numberOfMemoryPortions;
		List<int> memoryPortions = new List<int>();
		List<Process> processes = new List<Process>();
		bool locked = false;
		int ct = 0;
		public Form3(List<Process>processeslist)
		{
			InitializeComponent();
			this.processes = processeslist;
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

		private void Form3_Load(object sender, EventArgs e)
		{
			label1.Visible = false;
			label4.Visible = false;
			button2.Visible = false;
			for (int i = 0; i < processes.Count; i++)
			{
				listBox2.Items.Add(processes[i].size);
			}
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
			numberOfMemoryPortions = value;
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


				if (ct > numberOfMemoryPortions)
				{
					label4.Text = "All sizes already entered";

					label4.Visible = true;
					return;
				}

				if (numberOfMemoryPortions > 0)
				{
					memoryPortions.Add(value);
					listBox1.Items.Add(value);
					textBox1.Enabled = false;
					textBox2.Clear();
					label4.Visible = false;
					ct++;
				}
				if (ct == numberOfMemoryPortions && ct > 0)
				{
					for (int i = 0; i < ct; i++)
					{
						memoryPortions.Add((int)listBox1.Items[i]);
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
			Form4 f4 = new Form4(memoryPortions, processes);
			this.Hide();
			f4.Show();
			f4.Location = new Point(this.Location.X, this.Location.Y);
		}

		private void back_Click(object sender, EventArgs e)
		{
			Form2 f2 = new Form2();
			this.Hide();
			f2.Show();
			f2.Location = new Point(this.Location.X, this.Location.Y);
		}

		private void clear_Click(object sender, EventArgs e)
		{
			locked = false;

			numberOfMemoryPortions = 0;
			ct = 0;

			textBox1.Clear();
			textBox1.Enabled = true;

			textBox2.Clear();
			textBox2.Enabled = true;

			button1.ForeColor = Color.FromArgb(0, 180, 50);
			button1.Text = " ADD ";

			button2.Visible = false;
			label1.Visible = false;
			label4.Visible = false;

			listBox1.Items.Clear();
			listBox1.SelectionMode = SelectionMode.One;

			memoryPortions.Clear();		
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


	}
}

/*
 void firstFit()
		{
			for (int i = 0; i < processes.Count; i++)
			{
				int pros = processes[i];
				int mem = sizes[j];
				if (pros < mem)
				{
					memoryPortion p1 = new memoryPortion();
					p1.index = j;
					p1.process = pros;
					p1.internalFragement = mem - pros;
					memory.Add(p1);
					sizes.RemoveAt(j);
					sizes.Insert(j, p1.internalFragement);
					processes.RemoveAt(i);
				}
				else
				{
					j++;
				}
			}
			j = 0;
		}
 */
