using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace OS
{
	public partial class Form4 : Form
	{
		List<int> blocksSizes;
		

		List<memoryBlock> memoryBlocks = new List<memoryBlock>();
		List<Process> processes;

		public Form4(List<int> memoryPortions, List<Process> processes)
		{
			InitializeComponent();
			this.blocksSizes = memoryPortions;
			this.processes = processes;
			back.MouseEnter += Back_MouseEnter;
			back.MouseLeave += Back_MouseLeave;
			button2.MouseEnter += Button2_MouseEnter; 
			button2.MouseLeave += Button2_MouseLeave;
			button1.MouseEnter += Button1_MouseEnter;
			button1.MouseLeave += Button1_MouseLeave;
			button3.MouseEnter += Button3_MouseEnter;
			button3.MouseLeave += Button3_MouseLeave;

		}

		private void Button3_MouseLeave(object sender, EventArgs e)
		{
			button3.ForeColor = Color.FromArgb(0, 180, 50);
			button3.Text = " W O R S T - F I T ";
			button3.BackColor = Color.FromArgb(20, 20, 20);

		}

		private void Button3_MouseEnter(object sender, EventArgs e)
		{
			button3.ForeColor = Color.FromArgb(0, 255, 70);
			button3.Text = " W O R S T - F I T ";
			button2.BackColor = Color.FromArgb(15, 15, 15);

		}

		private void Button1_MouseLeave(object sender, EventArgs e)
		{
			button1.ForeColor = Color.FromArgb(0, 180, 50);
			button1.Text = " B E S T - F I T ";
			button1.BackColor = Color.FromArgb(20, 20, 20);

		}

		private void Button1_MouseEnter(object sender, EventArgs e)
		{
			button1.ForeColor = Color.FromArgb(0, 255, 70);
			button1.Text = " B E S T - F I T ";
			button1.BackColor = Color.FromArgb(15, 15, 15);

		}

		private void Button2_MouseLeave(object sender, EventArgs e)
		{
			button2.ForeColor = Color.FromArgb(0, 180, 50);
			button2.BackColor = Color.FromArgb(20, 20, 20);

			button2.Text = " F I R S T - F I T ";
		}

		private void Button2_MouseEnter(object sender, EventArgs e)
		{
			button2.ForeColor = Color.FromArgb(0, 255, 70);
			button2.Text = " F I R S T - F I T ";
			button2.BackColor = Color.FromArgb(15, 15, 15);


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

		private void button2_Click(object sender, EventArgs e)
		{
			firstFit();

			Form5 f5 = new Form5(processes, memoryBlocks);
			this.Hide();
			f5.Show();
			f5.Location = new Point(this.Location.X, this.Location.Y);
		}


		void firstFit()
		{
			for (int i = 0; i < processes.Count; i++)
			{
				Process pros = processes[i];

				for (int j = 0; j < memoryBlocks.Count; j++)
				{
					if (!memoryBlocks[j].allocated && pros.size <= memoryBlocks[j].blockSize)
					{
						int originalSize = memoryBlocks[j].blockSize;
						memoryBlocks[j].process = pros.size;
						memoryBlocks[j].allocated = true;
						memoryBlocks[j].remaining = originalSize - pros.size;
						memoryBlocks[j].originalBlockSize = originalSize;
						pros.foundFrame = true;
						int fragment = originalSize - pros.size;
						if (fragment > 0)
						{
							memoryBlock newBlock = new memoryBlock();
							newBlock.blockSize = fragment;
							newBlock.remaining = fragment;
							newBlock.process = -1;
							newBlock.allocated = false;
							newBlock.index = j + 1;
							newBlock.isFrag = true;
							memoryBlocks.Insert(j + 1, newBlock);
						}
						break;
					}
				}
			}
		}

		

		private void Form4_Load(object sender, EventArgs e)
		{
			for (int i = 0; i < blocksSizes.Count; i++)
			{
				memoryBlock b = new memoryBlock();
				b.index = i;
				b.blockSize = blocksSizes[i];
				b.remaining = blocksSizes[i];
				b.process = -1;
				b.allocated = false;
				b.originalBlockSize = blocksSizes[i];
				memoryBlocks.Add(b);
			}
		}

		private void back_Click(object sender, EventArgs e)
		{
			Form3 f3 = new Form3(processes);
			this.Hide();
			f3.Show();
			f3.Location = new Point(this.Location.X, this.Location.Y);
		}

	}
	public class memoryBlock
	{
		public int index;
		public int blockSize;
		public int remaining;
		public int process;
		public bool allocated;
		public bool isFrag;
		public int originalBlockSize;
	}
}