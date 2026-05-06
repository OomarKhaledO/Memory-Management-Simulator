using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using static OS.Form4;

namespace OS
{
	public partial class Form5 : Form
	{
		List<Process> processes = new List<Process>();
		List<memoryBlock> memory = new List<memoryBlock>();
		int choice = -1;
		List<Process> processesO;
		List<memoryBlock> memoryO;
		public Form5(List<Process> processes,
			List<memoryBlock> memory, int choice,
			List<Process> processesO,
			List<memoryBlock> memoryO)
		{
			InitializeComponent();
			this.processes = processes;
			this.memory = memory;
			this.processesO = processesO;
			this.memoryO = memoryO;
			back.MouseEnter += Back_MouseEnter;
			back.MouseLeave += Back_MouseLeave;
			label4.MouseEnter += Label4_MouseEnter;
			label4.MouseLeave += Label4_MouseLeave;
			tryDifferentAlgo.MouseEnter += TryDifferentAlgo_MouseEnter;
			tryDifferentAlgo.MouseLeave += TryDifferentAlgo_MouseLeave;
			this.choice = choice;
		}

		private void TryDifferentAlgo_MouseLeave(object sender, EventArgs e)
		{
			tryDifferentAlgo.ForeColor = Color.FromArgb(0, 180, 50);
			tryDifferentAlgo.Text = "    T R Y    ";
		}

		private void TryDifferentAlgo_MouseEnter(object sender, EventArgs e)
		{
			tryDifferentAlgo.ForeColor = Color.FromArgb(0, 255, 70);
			tryDifferentAlgo.Text = "  < T R Y >  ";
		}

		private void Label4_MouseLeave(object sender, EventArgs e)
		{
			label4.ForeColor = Color.FromArgb(0, 180, 50);
			label4.Text = "   E X I T   ";
		}

		private void Label4_MouseEnter(object sender, EventArgs e)
		{
			label4.ForeColor = Color.FromArgb(0, 255, 70);
			label4.Text = " < E X I T > ";
		}

		private void Back_MouseLeave(object sender, EventArgs e)
		{
			back.ForeColor = Color.FromArgb(0, 180, 50);
			back.Text =   "    N E W    ";
		}

		private void Back_MouseEnter(object sender, EventArgs e)
		{
			back.ForeColor = Color.FromArgb(0, 255, 70);
			back.Text =   "  < N E W >  ";
		}

		private void Form5_Load(object sender, EventArgs e)
		{
			if (choice == 1)
			{
				label1.Text = "F I R S T - F I T";
			}
			if (choice == 2)
			{
				label1.Text = " B E S T - F I T ";

			}
			if (choice == 3)
			{
				label1.Text = "W O R S T - F I T";
			}
			listBox1.Items.Clear();
			listBox2.Items.Clear();
			for (int i = 0; i < memory.Count; i++)
			{
				memoryBlock m = memory[i];
				if (!m.isFrag && m.originalBlockSize != 0 && m.process != -1)
				{
					listBox2.Items.Add(m.originalBlockSize + " |  ");
					listBox1.Items.Add(m.process);
				}
				else if (m.isFrag && m.originalBlockSize != 0 && m.process != -1)
				{
					listBox2.Items.Add(m.originalBlockSize + " | F ");
					listBox1.Items.Add(m.process);

				}
				if (!m.allocated && m.originalBlockSize != 0 && m.process == -1)
				{
					listBox2.Items.Add(m.originalBlockSize + " | E ");
					listBox1.Items.Add("---");
				}
			}
			int ct = 0;
			for (int i = 0; i < processes.Count; i++)
			{
				if (processes[i].foundFrame == false)
				{
					ct++;
					break;
				}
			}
			if (ct > 0)
			{
				listBox1.Items.Add("WAITING");
				for (int i = 0; i < processes.Count; i++)
				{
					if (!processes[i].foundFrame)
					{
						listBox1.Items.Add(processes[i].size);
					}
				}

			}
		}

		private void back_Click(object sender, EventArgs e)
		{
			Form1 f1 = new Form1();
			this.Hide();
			f1.Show();
			f1.Location = new Point(this.Location.X, this.Location.Y);
		}

		private void label4_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void tryDifferentAlgo_Click(object sender, EventArgs e)
		{
			List<Process> newProcesses = new List<Process>();
			for (int i = 0; i < processesO.Count; i++)
			{
				newProcesses.Add(new Process(processesO[i]));
			}

			List<memoryBlock> newMemory = new List<memoryBlock>();
			for (int i = 0; i < memoryO.Count; i++)
			{
				newMemory.Add(new memoryBlock(memoryO[i]));
			}

			Form4 f4 = new Form4(newMemory, newProcesses);
			this.Hide();
			f4.Show();
			f4.Location = new Point(this.Location.X, this.Location.Y);
		}
	}
}
