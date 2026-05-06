using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace OS
{
	public partial class Form4 : Form
	{	
		List<memoryBlock> memoryBlocks = new List<memoryBlock>();
		List<memoryBlock> memoryBlocksO = new List<memoryBlock>();
		List<Process> processes;
		List<Process> processesO;
		memoryBlock best = new memoryBlock();
		int bestIndex = -1;
		public Form4(List<memoryBlock> memoryBlocks, List<Process> processes)
		{
			InitializeComponent();
			this.memoryBlocks = memoryBlocks;
			this.processes = processes;
			this.processesO = new List<Process>();
			for (int i = 0; i < processes.Count; i++)
			{
				this.processesO.Add(new Process(processes[i]));
			}

			this.memoryBlocksO = new List<memoryBlock>();
			for (int i = 0; i < memoryBlocks.Count; i++)
			{
				this.memoryBlocksO.Add(new memoryBlock(memoryBlocks[i]));
			}

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
			Form5 f5 = new Form5(processes, memoryBlocks, 1, processesO, memoryBlocksO);
			this.Hide();
			f5.Show();
			f5.Location = new Point(this.Location.X, this.Location.Y);
		}


		void bestFit()
		{
			Process pros;
			for (int i = 0; i < processes.Count; i++)
			{
				pros = processes[i];
				best.blockSize = 8888;
				for (int j = 0; j < memoryBlocks.Count; j++)
				{
					if (memoryBlocks[j].blockSize < best.blockSize &&
						memoryBlocks[j].blockSize >= pros.size &&
						!memoryBlocks[j].allocated
						)
					{
						best.blockSize = memoryBlocks[j].blockSize;
						bestIndex = j;
					}
				}
				if (bestIndex != -1)
				{
					int originalSize = memoryBlocks[bestIndex].blockSize;
					memoryBlocks[bestIndex].process = pros.size;
					memoryBlocks[bestIndex].allocated = true;
					memoryBlocks[bestIndex].remaining = originalSize - pros.size;
					memoryBlocks[bestIndex].originalBlockSize = originalSize;
					pros.foundFrame = true;
					int fragment = originalSize - pros.size;
					if (fragment > 0)
					{
						memoryBlock newBlock = new memoryBlock();
						newBlock.blockSize = fragment;
						newBlock.remaining = fragment;
						newBlock.process = -1;
						newBlock.allocated = false;
						newBlock.index = bestIndex + 1;
						newBlock.isFrag = true;
						memoryBlocks.Insert(bestIndex + 1, newBlock);
					}
					
				}
				best.blockSize = 8888;
				bestIndex = -1;
			}
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
							int minpro = 8888;
							for (int p = 0; p < processes.Count; p++)
							{
								if (processes[p].size < minpro && !processes[p].foundFrame)
								{
									minpro = processes[p].size;
								}
							}
							if (fragment > minpro)
							{
								memoryBlock newBlock = new memoryBlock();
								newBlock.blockSize = fragment;
								newBlock.remaining = fragment;
								newBlock.process = -1;
								newBlock.allocated = false;
								newBlock.index = j + 1;
								newBlock.originalBlockSize = fragment;
								newBlock.isFrag = true;
								memoryBlocks.Insert(j + 1, newBlock);
							}
						}
						break;
					}
				}
			}
		}


		void worstFit()
		{
			for (int i = 0; i < processes.Count; i++)
			{
				Process pros = processes[i];
				int max = -8888;
				int maxIndex = -1;
				for (int j = 0; j < memoryBlocks.Count; j++)
				{
					 if (memoryBlocks[j].blockSize > max && !memoryBlocks[j].allocated)
					{
						max = memoryBlocks[j].blockSize;
						maxIndex = j;
					 }
				}
				if (maxIndex != -1 &&  !memoryBlocks[maxIndex].allocated && pros.size <= memoryBlocks[maxIndex].blockSize)
				{
						int originalSize = memoryBlocks[maxIndex].blockSize;
						memoryBlocks[maxIndex].process = pros.size;
						memoryBlocks[maxIndex].allocated = true;
						memoryBlocks[maxIndex].remaining = originalSize - pros.size;
						memoryBlocks[maxIndex].originalBlockSize = originalSize;
						pros.foundFrame = true;
						int fragment = originalSize - pros.size;
						if (fragment > 0)
						{
							int minpro = 8888;
							for (int p = 0; p < processes.Count; p++)
							{
								if (processes[p].size < minpro && !processes[p].foundFrame)
								{
									minpro = processes[p].size;
								}
							}
							if (fragment > minpro)
							{
								memoryBlock newBlock = new memoryBlock();
								newBlock.blockSize = fragment;
								newBlock.remaining = fragment;
								newBlock.process = -1;
								newBlock.allocated = false;
								newBlock.index = maxIndex + 1;
								newBlock.originalBlockSize = fragment;
								newBlock.isFrag = true;
								memoryBlocks.Insert(maxIndex + 1, newBlock);
							}
						}
				}
				
			}
		}


		private void Form4_Load(object sender, EventArgs e)
		{

		}

		private void back_Click(object sender, EventArgs e)
		{
			Form3 f3 = new Form3(processes);
			this.Hide();
			f3.Show();
			f3.Location = new Point(this.Location.X, this.Location.Y);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			bestFit();
			Form5 f5 = new Form5(processes, memoryBlocks, 2, processesO, memoryBlocksO);
			this.Hide();
			f5.Show();
			f5.Location = new Point(this.Location.X, this.Location.Y);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			worstFit();
			Form5 f5 = new Form5(processes, memoryBlocks, 3, processesO, memoryBlocksO);
			this.Hide();
			f5.Show();
			f5.Location = new Point(this.Location.X, this.Location.Y);
		}
	}
	
}