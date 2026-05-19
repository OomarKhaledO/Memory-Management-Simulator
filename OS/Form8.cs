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
    public partial class Form8 : Form
    {
        string referenceString;
        int frames;
        string algo;
        Label[,] cells;

        string[] arr;

        public Form8(string s, int f, string str)
        {
            InitializeComponent();

            referenceString = s;
            frames = f;
            algo = str;

            Load += Form8_Load;
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            arr = referenceString.Split(',');

            label2.Text = referenceString.Replace(",", " ");
            label4.Text = frames.ToString();

            CreateGrid();
            if (algo == "FIFO") { RunFIFO(); }
            else if (algo == "Optimal") { RunOptimal(); }
            else if (algo == "LRU") { RunLRU(); }
        }

        private void CreateGrid()
        {
            int cols = arr.Length;
            int rows = frames;

            cells = new Label[rows, cols];

            int startX = 20;
            int startY = 100;

            for (int c = 0; c < cols; c++)
            {
                for (int r = 0; r < rows; r++)
                {
                    Label lbl = new Label();

                    lbl.Width = 40;
                    lbl.Height = 40;

                    lbl.BorderStyle = BorderStyle.FixedSingle;

                    lbl.BackColor = Color.LightBlue;
                    lbl.ForeColor = Color.Black;

                    lbl.TextAlign = ContentAlignment.MiddleCenter;

                    lbl.Location = new Point(
                        startX + c * 50,
                        startY + r * 40
                    );

                    cells[r, c] = lbl;

                    this.Controls.Add(lbl);
                }
            }
        }
        private void RunLRU()
        {
            List<int> memory = new List<int>();

            Dictionary<int, int> lastUsed = new Dictionary<int, int>();

            List<int> previous = new List<int>();

            for (int c = 0; c < arr.Length; c++)
            {
                int page = int.Parse(arr[c]);

                if (memory.Contains(page))
                {
                    lastUsed[page] = c;
                }
                else
                {
                    if (memory.Count < frames)
                    {
                        memory.Add(page);
                        lastUsed[page] = c;
                    }
                    else
                    {
                        int lruPage = memory[0];
                        int minIndex = lastUsed[lruPage];

                        foreach (int p in memory)
                        {
                            if (lastUsed[p] < minIndex)
                            {
                                minIndex = lastUsed[p];
                                lruPage = p;
                            }
                        }

                        int replaceIndex = memory.IndexOf(lruPage);

                        memory[replaceIndex] = page;

                        lastUsed.Remove(lruPage);

                        lastUsed[page] = c;
                    }
                }

                bool same = true;

                if (previous.Count != memory.Count)
                {
                    same = false;
                }
                else
                {
                    for (int i = 0; i < memory.Count; i++)
                    {
                        if (previous[i] != memory[i])
                        {
                            same = false;
                            break;
                        }
                    }
                }

                if (same)
                {
                    for (int r = 0; r < frames; r++)
                    {
                        cells[r, c].Visible = false;
                    }
                }
                else
                {
                    for (int r = 0; r < frames; r++)
                    {
                        cells[r, c].Text = "";
                    }

                    for (int r = 0; r < memory.Count; r++)
                    {
                        cells[r, c].Text = memory[r].ToString();
                    }
                }

                previous = new List<int>(memory);
            }
        }
        private void RunOptimal()
        {
            List<int> memory = new List<int>();
            List<int> previous = new List<int>();

            for (int c = 0; c < arr.Length; c++)
            {
                int page = int.Parse(arr[c]);

                if (!memory.Contains(page))
                {
                    if (memory.Count < frames)
                    {
                        memory.Add(page);
                    }
                    else
                    {
                        int pageToReplace = -1;
                        int farthest = -1;

                        foreach (int p in memory)
                        {
                            int nextUse = int.MaxValue;

                            for (int future = c + 1; future < arr.Length; future++)
                            {
                                if (int.Parse(arr[future]) == p)
                                {
                                    nextUse = future;
                                    break;
                                }
                            }

                            if (nextUse > farthest)
                            {
                                farthest = nextUse;
                                pageToReplace = p;
                            }
                        }

                        int replaceIndex = memory.IndexOf(pageToReplace);
                        memory[replaceIndex] = page;
                    }
                }

                bool same = true;

                if (previous.Count != memory.Count)
                {
                    same = false;
                }
                else
                {
                    for (int i = 0; i < memory.Count; i++)
                    {
                        if (previous[i] != memory[i])
                        {
                            same = false;
                            break;
                        }
                    }
                }

                if (same)
                {
                    for (int r = 0; r < frames; r++)
                    {
                        cells[r, c].Visible = false;
                    }
                }
                else
                {
                    for (int r = 0; r < frames; r++)
                    {
                        cells[r, c].Text = "";
                    }

                    for (int r = 0; r < memory.Count; r++)
                    {
                        cells[r, c].Text = memory[r].ToString();
                    }
                }

                previous = new List<int>(memory);
            }
        }
        private void RunFIFO()
        {
            List<int> memory = new List<int>();

            Queue<int> fifo = new Queue<int>();

            for (int c = 0; c < arr.Length; c++)
            {
                int page = int.Parse(arr[c]);

                if (!memory.Contains(page))
                {
                    if (memory.Count < frames)
                    {
                        memory.Add(page);
                        fifo.Enqueue(page);
                    }
                    else
                    {
                        int old = fifo.Dequeue();

                        int index = memory.IndexOf(old);

                        memory[index] = page;

                        fifo.Enqueue(page);
                    }
                }

                for (int r = 0; r < frames; r++)
                {
                    cells[r, c].Text = "";
                }

                for (int r = 0; r < memory.Count; r++)
                {
                    cells[r, c].Text = memory[r].ToString();
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

    }
}