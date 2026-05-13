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

        Label[,] cells;

        string[] arr;

        public Form8(string s, int f)
        {
            InitializeComponent();

            referenceString = s;
            frames = f;

            Load += Form8_Load;
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            arr = referenceString.Split(',');

            label2.Text = referenceString.Replace(",", " ");
            label4.Text = frames.ToString();

            CreateGrid();
            RunFIFO();
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