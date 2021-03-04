using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DecisionSupport
{
    public partial class ShowOpts : Form
    {
        List<Index> idxs = new List<Index>();
        Form1 form;
        double optimum;
        private List<Table> tables;
        int robotLimit;
        int operatorLimit;
        int allRobot = 0;
        int allWorker = 0;
        int prodCount = 1;
        int calculate = 0;
        Stopwatch stopwatch = new Stopwatch();
        ShowSolution showSol;
        List<ShowSolution> showSolList = new List<ShowSolution>();
        Rectangle r1;

        /* For matrix options */
        int defaultInterval = 5;
        int robotInterval = 5;
        int workerInterval = 5;
        List<Index> indexes = new List<Index>();
        List<double> optimums = new List<double>(); // list of the optimum values for the different combinations
        List<Index> indexList = new List<Index>();
        Dictionary<string, Dictionary<List<Index>, double>> combinationMap = new Dictionary<string, Dictionary<List<Index>, double>>();

        public ShowOpts(ref List<Table> tables, Form1 f)
        {
            this.tables = tables;
            form = f;
            InitializeComponent();
            //this.Size = new System.Drawing.Size(1100, 800);
            typeof(DataGridView).InvokeMember("DoubleBuffered",
              BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
             null, optionsTable, new object[] { true });
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
        private void optionsTable_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            // Vertical text from column 0, or adjust below, if first column(s) to be skipped
            //if (e.RowIndex == optionsTable.Rows.Count - 1 && e.ColumnIndex == -1)
            //{
            //    e.PaintBackground(e.CellBounds, true);
            //    e.Graphics.TranslateTransform(e.CellBounds.Left, e.CellBounds.Bottom);
            //    e.Graphics.RotateTransform(270);
            //    e.Graphics.DrawString(e.FormattedValue.ToString(), e.CellStyle.Font, Brushes.Black, 5, 5);
            //    e.Graphics.ResetTransform();
            //    e.Handled = true;
            //}

            if (e.RowIndex == -1 && e.ColumnIndex > -1)
            {
                Rectangle r2 = e.CellBounds;
                r2.Y += e.CellBounds.Height / 2;
                r2.Height = e.CellBounds.Height / 2;
                e.PaintBackground(r2, true);
                e.PaintContent(r2);
                e.Handled = true;
            }

            //if (e.RowIndex > -0 && e.ColumnIndex == -1)
            //{
            //    Rectangle r2 = e.CellBounds;
            //    //r2.Y += e.CellBounds.Height / 2;
            //    //r2.Height = e.CellBounds.Height / 2;

            //    r2.X += e.CellBounds.Width / 2;
            //    r2.Width = e.CellBounds.Width / 2;
            //    e.PaintBackground(r2, true);
            //    e.PaintContent(r2);
            //    e.Handled = true;
            //}
        }

        public void fillOptions()
        {
            int selectedRobRow = 0;
            int selectedOpCol = 0;
            string lessRobot = "";
            string lessWorker = "";
            int matrixRow = 0;
            int matrixCol = 0;
            int sub = 0;
            int colIndex;

            for (int i = 0; i < workerInterval * 2 + 1; ++i) //0..10
            {
                string colName = "column" + i;
                if (i <= workerInterval) // 0..5
                {
                    sub = workerInterval - i; // 5..0
                    if (operatorLimit - sub >= 0)
                    {
                        colIndex = i;
                        //    optionsTable.Columns[i].HeaderCell.Value = (operatorLimit - sub).ToString(); //
                        optionsTable.Columns.Add(colName, (operatorLimit - sub).ToString());
                        optionsTable.Columns[optionsTable.Columns.Count-1].Width = 40;
                    }
                    else
                    {
                        //  optionsTable.Columns[i].HeaderCell.Value = "-";
                        //optionsTable.Columns.Add(colName, "-");
                        //optionsTable.Columns[i].Width = 40;
                    }
                }
                if (i >= workerInterval + 1) // 6..10
                {
                    int add = Math.Abs(workerInterval - i);
                    //   optionsTable.Columns[i].HeaderCell.Value = (operatorLimit + add).ToString(); //
                    optionsTable.Columns.Add(colName, (operatorLimit + add).ToString());
                    optionsTable.Columns[optionsTable.Columns.Count - 1].Width = 40;
                }
                /* To get which cell is currently queried */
                if (operatorLimit - sub >= 0)
                {
                    if (optionsTable.Columns[optionsTable.Columns.Count - 1].HeaderCell.Value.Equals(operatorLimit.ToString()))
                    {
                        selectedOpCol = optionsTable.Columns.Count - 1;
                        optionsTable.Columns[optionsTable.Columns.Count - 1].HeaderCell.Style.BackColor = Color.Yellow;
                    }
                }
            }
            for (int i = 0; i < robotInterval * 2 + 1; ++i) // 0..10
            {
                if (i <= robotInterval)
                {
                    sub = robotInterval - i; // 5..0
                    if (robotLimit - sub >= 0)
                    {
                        optionsTable.Rows.Add();
                        optionsTable.Rows[optionsTable.Rows.Count - 1].HeaderCell.Value = (robotLimit - sub).ToString();
                    }
                    else
                    {
                        //optionsTable.Rows[i].HeaderCell.Value = "-";
                    }
                }

                if (i >= robotInterval + 1) // 6..10
                {
                    optionsTable.Rows.Add();
                    int add = Math.Abs(robotInterval - i);
                    optionsTable.Rows[optionsTable.Rows.Count - 1].HeaderCell.Value = (robotLimit + add).ToString();
                }
                /* To get which cell is currently queried */
                if (robotLimit - sub >= 0)
                {
                    if (optionsTable.Rows[optionsTable.Rows.Count - 1].HeaderCell.Value.Equals(robotLimit.ToString()))
                    {
                        selectedRobRow = optionsTable.Rows.Count - 1;
                        optionsTable.Rows[optionsTable.Rows.Count - 1].HeaderCell.Style.BackColor = Color.Yellow;
                    }
                }
            }
                           
            Console.WriteLine("Selected row: " + selectedRobRow + ", col: " + selectedOpCol);
            optionsTable.Rows[selectedRobRow].Cells[selectedOpCol].Style.BackColor = Color.Yellow;
            foreach (var entry in combinationMap)
            {
                string key = entry.Key;
                string worker = "";
                string robot = "";
                string idx = "";
                double currentProfit = entry.Value.First().Value;
                foreach (char i in entry.Key) // 10,4
                {
                    if (i == ',')
                    {
                        robot = idx;
                        idx = "";
                    }
                    else
                    {
                        idx += i;
                    }
                }
                worker = idx;
                Console.WriteLine("work: " + worker + ", robot: " + robot);
                if(optimum == currentProfit && (Int32.Parse(worker) <= operatorLimit && Int32.Parse(robot) <= robotLimit))
                {
                    lessRobot = robot;
                    lessWorker = worker;
                }
                for (int i = 0; i < optionsTable.Rows.Count; ++i)
                {
                    Console.WriteLine("error loop: " + i);
                    if (optionsTable.Rows[i].HeaderCell.Value.Equals(robot))
                    {
                        matrixRow = i;
                        break;
                    }
                }
                for (int i = 0; i < optionsTable.Columns.Count; ++i)
                {
                    if (optionsTable.Columns[i].HeaderCell.Value.Equals(worker))
                    {
                        matrixCol = i;
                        break;
                    }
                }
                Console.WriteLine("column: " + matrixCol + ", row: " + matrixRow);
                optionsTable.Rows[matrixRow].Cells[matrixCol].Value = currentProfit;
            }
            for (int i = 0; i < optionsTable.Rows.Count; ++i)
            {
                if (optionsTable.Rows[i].HeaderCell.Value.Equals(lessRobot))
                {
                    matrixRow = i;
                    break;
                }
            }
            for (int i = 0; i < optionsTable.Columns.Count; ++i)
            {
                if (optionsTable.Columns[i].HeaderCell.Value.Equals(lessWorker))
                {
                    matrixCol = i;
                    optionsTable.Rows[matrixRow].Cells[matrixCol].Style.BackColor = Color.Green;
                    break;
                }
            }
            optionsTable.CurrentCell = null;
        }

        private void optionCellClick(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine("indexek: " + e.RowIndex + ", " + e.ColumnIndex);
            if(e.RowIndex <= -1 || e.ColumnIndex <= -1)
            {
                return;
            }
            DataGridViewCell cell = optionsTable.Rows[e.RowIndex].Cells[e.ColumnIndex];
            Console.WriteLine("cella: " + cell);
            string worker = "";
            string robot = "";
            string key;
            List<Index> indexList = new List<Index>();
            if (cell.Value != null && (cell.Value.ToString() != 0.ToString() || cell.ColumnIndex != 0 || cell.RowIndex != 0))
            {
                worker = optionsTable.Columns[cell.ColumnIndex].HeaderCell.Value.ToString();
                robot = optionsTable.Rows[cell.RowIndex].HeaderCell.Value.ToString();

                key = robot + "," + worker;
                Console.WriteLine("kattintott: " + key);
                indexList = combinationMap[key].First().Key;

                showSol = new ShowSolution(ref tables, ref indexList, worker, robot);
                showSolList.Add(showSol);
                showSol.Show();
            }         
        }

        private void CalculateButtonClick(object sender, EventArgs e)
        {
            if (maxOperator.Text.Length.Equals(0) || maxRobot.Text.Length.Equals(0))
            {
                MessageBox.Show("Please enter valid resource limits!", "Missing information");
            }
            else
            {
                CalculateOptions();
            }
            Console.WriteLine("OPT: " + optimum);
            Console.WriteLine("cachből: " + form.ReadCache);
            Console.WriteLine("nem cachből: " + form.Count);
            Console.WriteLine("ÖSSZ keresés: " + form.TotalCount);
            Console.WriteLine("cache összméret " + form.Cache.Count);
            Console.WriteLine("ellapsed milliseconds: " + stopwatch.ElapsedMilliseconds);
        }
        private void CalculateOptions()
        {
            coverLabel.Visible = false;
            optionsTable.Visible = true;
            robotLimit = Int32.Parse(maxRobot.Text);
            operatorLimit = Int32.Parse(maxOperator.Text);
            optimums.Clear();
            form.TotalCount = 0;
            form.ReadCache = 0;
            indexes = new List<Index>();
            Console.WriteLine(" nullázás " + form.ReadCache);
            //if (calculate != 0)
            //{
                optionsTable.Rows.Clear();
                optionsTable.Columns.Clear();
            //}
            if(calculate > 0 && Form1.getCacheCount() <= 0)
            {
                combinationMap.Clear();
            }
            if (maxRobot.Text.Length == 0 || maxOperator.Text.Length == 0)
            {
                MessageBox.Show("Please give me the maximum limits!", "Missing number.");
            }
            else
            {
                stopwatch.Start();
                optimum = form.getPrevOptVal(tables.Count - 1, robotLimit, operatorLimit, ref idxs);
                stopwatch.Stop();
                for (int i = 0; i <= robotInterval; ++i)
                {
                for (int z = 0; z <= workerInterval; ++z)
                {
                    string k;
                    // RobotLimit-- OperatorLimit++
                    if (robotLimit - i >= 0)
                    {
                        k = (robotLimit - i).ToString() + "," + (operatorLimit + z).ToString();
                        if (combinationMap.Keys.Contains(k))
                        {
                            continue;
                        }
                        OptimumsCalculator(i, z, k, 1);
                    }
                    // Robotlimit-- Operatorlimit--
                    if (robotLimit - i >= 0 && operatorLimit - z >= 0)
                    {
                        k = (robotLimit - i).ToString() + "," + (operatorLimit - z).ToString();
                        if (combinationMap.Keys.Contains(k))
                        {
                            continue;
                        }
                        OptimumsCalculator(i, z, k, 0);
                    }
                    // Robotlimit++ Operatorlimit--
                    if (operatorLimit - z >= 0)
                    {
                        k = (robotLimit + i).ToString() + "," + (operatorLimit - z).ToString();
                        if (combinationMap.Keys.Contains(k))
                        {
                            continue;
                        }
                        OptimumsCalculator(i, z, k, 2);
                    }
                }
            }
            for (int i = 0; i <= robotInterval; ++i)
            {
                for (int z = 0; z <= workerInterval; ++z)
                {
                    string k = (robotLimit + i).ToString() + "," + (operatorLimit + z).ToString();

                    if (combinationMap.Keys.Contains(k))
                    {
                        continue;
                    }
                    //Plus
                    OptimumsCalculator(i, z, k, 3);
                }
            }
            fillOptions();
            calculate++;
        }
    }
        /* selector:  0 : Robot -- && Operator --
      *            1 : Robot -- && Operator ++
      *            2 : Robot ++ && Operator --
      *            3 : Robot ++ && Operator ++
      * */
        private void OptimumsCalculator(int i, int z, string k, int selector)
        {
            // Robotlimit-- Operatorlimit++
            indexes = new List<Index>();
            double opt = 0.0;
            if (selector == 0)
            {
                opt = form.getPrevOptVal(tables.Count - 1, robotLimit - i, operatorLimit - z, ref indexes);
            }
            else if (selector == 1)
            {
                opt = form.getPrevOptVal(tables.Count - 1, robotLimit - i, operatorLimit + z, ref indexes);
            }
            else if (selector == 2)
            {
                opt = form.getPrevOptVal(tables.Count - 1, robotLimit + i, operatorLimit - z, ref indexes);
            }
            else if (selector == 3)
            {
                opt = form.getPrevOptVal(tables.Count - 1, robotLimit + i, operatorLimit + z, ref indexes);
            }
            optimums.Add(opt);

            Dictionary<List<Index>, double> optimum = new Dictionary<List<Index>, double>();
            List<Index> idx = new List<Index>();
            foreach (Index j in indexes)
            {
                idx.Add(j);
                Console.WriteLine("prod: " + j.Product + ", robot: " + j.Robot + ", operator: " + j.Worker);
            }
            optimum.Add(idx, opt);

            combinationMap.Add(k, optimum);
        }

        private void cellClick(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridViewCell cell = optionsTable.Rows[e.RowIndex].Cells[e.ColumnIndex];
            //if (cell.Value != null && (cell.Value.ToString() != 0.ToString() || cell.ColumnIndex != 0 || cell.RowIndex != 0))
            //{
            //    cell.Style.BackColor = Color.Azure;
            //}
        }

        private void ShowOpts_FormClosed(object sender, FormClosedEventArgs e)
        {
            for(int i = 0; i < showSolList.Count; i++)
            {
                showSolList[i].Close();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if(maxOperator.Text.Length.Equals(0) || maxRobot.Text.Length.Equals(0))
            {
                MessageBox.Show("Missing information", "Please enter valid resource limits!");
            }
            else
            {
                opSlide.Text = trackBar1.Value.ToString();
                workerInterval = trackBar1.Value;
                CalculateOptions();
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (maxOperator.Text.Length.Equals(0) || maxRobot.Text.Length.Equals(0))
            {
                MessageBox.Show("Missing information", "Please enter valid resource limits!");
            }
            else
            {
                robSlide.Text = trackBar2.Value.ToString();
                robotInterval = trackBar2.Value;
                CalculateOptions();
            }
        }

        private void maxRobot_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }
            if(e.KeyChar == '.' || e.KeyChar == ',')
            {
                e.Handled = true;
            }
            TextBox t = sender as TextBox;

            //Delete the placeholder 0
            if (t.Text == "0" && char.IsDigit(e.KeyChar))
            {
                t.Text = "";
            }
        }

        private void maxOperator_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }
            TextBox t = sender as TextBox;

            //Delete the placeholder 0
            if (t.Text == "0" && char.IsDigit(e.KeyChar))
            {
                t.Text = "";
            }
            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                e.Handled = true;
            }
        }

        private void optionsTable_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            optionsTable.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            //optionsTable.row
            //optionsTable.Rows[optionsTable.Rows.Count - 1].HeaderCell.Value = "Number of operators";
        }

        private void optionsTable_Paint(object sender, PaintEventArgs e)
        {
            Console.WriteLine("paintiiiiing");
            /*
            //Offsets to adjust the position of the merged Header.
            int heightOffset = 0;
            int widthOffset = 0;
            int xOffset = 0;
            int yOffset = 0; // 4

            //Index of Header column from where the merging will start.
            int columnIndex = 0;

            //Number of Header columns to be merged.
            int columnCount = optionsTable.Columns.Count;

            //Get the position of the Header Cell.
            Rectangle headerCellRectangle = optionsTable.GetCellDisplayRectangle(columnIndex, 0, true);

            //X coordinate of the merged Header Column.
            int xCord = headerCellRectangle.Location.X + xOffset;

            //Y coordinate of the merged Header Column.
            int yCord = headerCellRectangle.Location.Y - headerCellRectangle.Height + yOffset;

            //Calculate Width of merged Header Column by adding the widths of all Columns to be merged.
            int mergedHeaderWidth = optionsTable.Columns[columnIndex].Width + optionsTable.Columns[columnIndex + columnCount - 1].Width + widthOffset;

            //Generate the merged Header Column Rectangle.
            Rectangle mergedHeaderRect = new Rectangle(xCord, yCord, mergedHeaderWidth, headerCellRectangle.Height + heightOffset);

            //Draw the merged Header Column Rectangle.
            e.Graphics.FillRectangle(new SolidBrush(Color.White), mergedHeaderRect);

            //Draw the merged Header Column Text.
            e.Graphics.DrawString("Address", optionsTable.ColumnHeadersDefaultCellStyle.Font, Brushes.Black, xCord, yCord);
            */
            r1 = optionsTable.GetCellDisplayRectangle(0, -1, true);
        //    int w2 = optionsTable.GetCellDisplayRectangle(2, -1, true).Width;
            int allColsWidth = 0;
            for(int i = 0; i < optionsTable.Columns.Count; ++i)
            { 
                allColsWidth += optionsTable.Columns[i].Width;
            }
            r1.X += 1;
            r1.Y += 1;
            //r1.Width = r1.Width + (w2 * (optionsTable.Columns.Count-1)) - 2;
            r1.Width = allColsWidth - 2;
            r1.Height = r1.Height / 2 - 2;
            e.Graphics.FillRectangle(new SolidBrush(optionsTable.ColumnHeadersDefaultCellStyle.BackColor), r1);

            StringFormat format = new StringFormat();

            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString("Number of operators", optionsTable.ColumnHeadersDefaultCellStyle.Font,
                new SolidBrush(optionsTable.ColumnHeadersDefaultCellStyle.ForeColor), r1, format);

            // For the rows header
            Rectangle r2 = optionsTable.GetCellDisplayRectangle(-1, 0, true);
            int allRowsHeight = 0;
            for (int i = 0; i < optionsTable.Rows.Count; ++i)
            {
                allRowsHeight += optionsTable.Rows[i].Height;
            }
            r2.X += 1;
            r2.Y += 1;
            //r1.Width = r1.Width + (w2 * (optionsTable.Columns.Count-1)) - 2;
            r2.Height = allRowsHeight - 2;
            r2.Width = r2.Width / 2 - 2;
            e.Graphics.FillRectangle(new SolidBrush(optionsTable.RowHeadersDefaultCellStyle.BackColor), r2);
            StringFormat format2 = new StringFormat();

            format2.Alignment = StringAlignment.Center;
            format2.LineAlignment = StringAlignment.Center;
            //     e.Graphics.TranslateTransform(0,0);
            //e.Graphics.TranslateTransform(0, 16);
            //e.Graphics.RotateTransform(-90.0F);
            e.Graphics.DrawString("Number of robots", optionsTable.RowHeadersDefaultCellStyle.Font,
                 new SolidBrush(optionsTable.ColumnHeadersDefaultCellStyle.ForeColor), r2, format2);

            //e.Graphics.DrawString("Number of robots", optionsTable.RowHeadersDefaultCellStyle.Font,
            //    new SolidBrush(optionsTable.RowHeadersDefaultCellStyle.ForeColor),
            //    new PointF(r2.Y + (optionsTable.RowHeadersWidth + 16), r2.X));
            //e.Graphics.TranslateTransform(0,-16);
            //e.Graphics.RotateTransform(90.0F);
            //    e.Graphics.ResetTransform();
        }

        private void optionsTable_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            Rectangle rtHeader = optionsTable.DisplayRectangle;
            rtHeader.Height = optionsTable.ColumnHeadersHeight / 2;
            optionsTable.Invalidate(rtHeader);
        }
        private void optionsTable_RowHeightChanged(object sender, DataGridViewRowEventArgs e)
        {
            Rectangle rtHeader = optionsTable.DisplayRectangle;
            rtHeader.Width = optionsTable.RowHeadersWidth / 2;
            optionsTable.Invalidate(rtHeader);
        }
        private void optionsTable_Scroll(object sender, ScrollEventArgs e)
        {
            Rectangle rtHeader = optionsTable.DisplayRectangle;
            rtHeader.Height = optionsTable.ColumnHeadersHeight / 2;
            optionsTable.Invalidate(rtHeader);
        }

        private void optionsTable_ScrollRow(object sender, ScrollEventArgs e)
        {
            Rectangle rtHeader = optionsTable.DisplayRectangle;
            rtHeader.Width = optionsTable.RowHeadersWidth / 2;
            optionsTable.Invalidate(rtHeader);
        }
        private void ShowOpts_Load(object sender, EventArgs e)
        {
            coverLabel.Visible = true;
            coverLabel.Width = optionsTable.Width;
            coverLabel.Height = optionsTable.Height;
            coverLabel.Location = new System.Drawing.Point(optionsTable.Location.X, optionsTable.Location.Y);
            optionsTable.Rows.Add();

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //trackBar1.BackColor = Color.FromArgb(40, Color.White);
            //trackBar2.BackColor = Color.FromArgb(40, Color.White);
            groupBox1.BackColor = Color.FromArgb(40, Color.White);
            label7.BackColor = Color.Transparent;
            label4.BackColor = Color.Transparent;
            coverLabel.BackColor = Color.FromArgb(60, Color.White);
            //optionsTable.Rows[0].Cells[0].Value = 4;
            //Console.WriteLine("id of row: " + (optionsTable.Rows.Count - 1));
            optionsTable.Columns.Add("valami", "Col 1");
            //optionsTable.Columns.Add("valami", " Col 2");
            //optionsTable.Columns.Add("valami", "Col 3");
            //optionsTable.Rows[0].HeaderCell.Value = 2.ToString();
            //optionsTable.Rows[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            //optionsTable.Rows.Add();
            //optionsTable.Rows.Add();
            //optionsTable.Rows[1].HeaderCell.Value = 2.ToString();
            //optionsTable.Rows[2].HeaderCell.Value = 233333.ToString();

            //Console.WriteLine("number of cols: " + optionsTable.Columns.Count);

            optionsTable.ColumnHeadersHeight = 50;
            optionsTable.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
            optionsTable.CellPainting += new DataGridViewCellPaintingEventHandler(optionsTable_CellPainting);
            optionsTable.Paint += new PaintEventHandler(optionsTable_Paint);
            optionsTable.Scroll += new ScrollEventHandler(optionsTable_Scroll);
            optionsTable.ColumnWidthChanged += new DataGridViewColumnEventHandler(optionsTable_ColumnWidthChanged);

            optionsTable.RowHeadersWidth = 90;
        //    optionsTable.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            optionsTable.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            optionsTable.CellPainting += new DataGridViewCellPaintingEventHandler(optionsTable_CellPainting);
            optionsTable.Paint += new PaintEventHandler(optionsTable_Paint);
            optionsTable.Scroll += new ScrollEventHandler(optionsTable_ScrollRow);
            optionsTable.RowHeightChanged += new DataGridViewRowEventHandler(optionsTable_RowHeightChanged);
            
        }
    }
}
