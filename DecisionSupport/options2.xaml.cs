using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DecisionSupport
{
    /// <summary>
    /// Interaction logic for options2.xaml
    /// </summary>
    public partial class options2 : UserControl
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
        Stopwatch stopwatchAll = new Stopwatch();
        ShowSolution showSol;
        List<ShowSolution> showSolList = new List<ShowSolution>();
        Rectangle r1;

        /* For matrix options */
        int robotInterval = 5;
        int workerInterval = 5;
        List<Index> indexes = new List<Index>();
        List<double> optimums = new List<double>(); // list of the optimum values for the different combinations
        List<Index> indexList = new List<Index>();
        Dictionary<string, Dictionary<List<Index>, double>> combinationMap = new Dictionary<string, Dictionary<List<Index>, double>>();
        int groupBoxY;
        int coverLabelY;
        System.Windows.Forms.DataGridView optionsTable;
        TextBox maxOperator;
        int initialized = 0;
        public options2(Form1 f)
        {
            this.tables = f.Tables;
            form = f;
            InitializeComponent();
            initialized = 1;
            optionsTable = (System.Windows.Forms.DataGridView)this.winHost.Child;
            maxOperator = maxOperator2;
            optionsTable.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;

           typeof(System.Windows.Forms.DataGridView).InvokeMember("DoubleBuffered",
            BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
            null, optionsTable, new object[] { true });
        }
        int matrixRow = 0;
        int matrixCol = 0;
        int selectedRobRow = 0;
        int selectedOpCol = 0;
        public void fillOptions()
        {
            string lessRobot = "";
            string lessWorker = "";
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
                        optionsTable.Columns[optionsTable.Columns.Count - 1].Width = 40;
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
                        //optionsTable.Columns[optionsTable.Columns.Count - 1].HeaderCell.Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffcc");
                        optionsTable.Columns[optionsTable.Columns.Count - 1].HeaderCell.Style.BackColor = System.Drawing.Color.Yellow;
                    }
                }
            }
            int rowIdx = 0;
            for (int i = 0; i < robotInterval * 2 + 1; ++i) // 0..10
            {
                if (i <= robotInterval)
                {
                    sub = robotInterval - i; // 5..0
                    if (robotLimit - sub >= 0)
                    {
                        rowIdx =  optionsTable.Rows.Add();
                        optionsTable.Rows[rowIdx].HeaderCell.Value = (robotLimit - sub).ToString();
                    }
                    else
                    {
                        //optionsTable.Rows[i].HeaderCell.Value = "-";
                    }
                }

                if (i >= robotInterval + 1) // 6..10
                {
                    rowIdx =  optionsTable.Rows.Add();
                    int add = Math.Abs(robotInterval - i);
                    optionsTable.Rows[rowIdx].HeaderCell.Value = (robotLimit + add).ToString();
                    //Console.WriteLine("idx: " + rowIdx + ", " + optionsTable.Rows[rowIdx].HeaderCell.Value);
                }
                /* To get which cell is currently queried */
                if (robotLimit - sub >= 0)
                {
                    if (optionsTable.Rows[rowIdx].HeaderCell.Value.Equals(robotLimit.ToString()))
                    {
                        selectedRobRow = rowIdx;
                        //optionsTable.Rows[rowIdx].HeaderCell.Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffcc");
                        optionsTable.Rows[rowIdx].HeaderCell.Style.BackColor = System.Drawing.Color.Yellow;
                    }
                }
            }

         //   Console.WriteLine("Selected row: " + selectedRobRow + ", col: " + selectedOpCol);
            //optionsTable.Rows[selectedRobRow].Cells[selectedOpCol].Style.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffcc");
            optionsTable.Rows[selectedRobRow].Cells[selectedOpCol].Style.BackColor = System.Drawing.Color.Yellow;
            foreach (var entry in combinationMap)
            {
                int rowCount = optionsTable.Rows.Count;
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
              //  Console.WriteLine("work: " + worker + ", robot: " + robot);
                if (optimum == currentProfit && (Int32.Parse(worker) <= operatorLimit && Int32.Parse(robot) <= robotLimit))
                {
                    lessRobot = robot;
                    lessWorker = worker;
                }
                for (int i = 0; i < rowCount; ++i)
                {
                    if (optionsTable.Rows[i].HeaderCell.Value.ToString().Equals(robot))
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
              //  Console.WriteLine("column: " + matrixCol + ", row: " + matrixRow);
                optionsTable.Rows[matrixRow].Cells[matrixCol].Value = currentProfit;
             //   Console.WriteLine("profit: " + currentProfit);
            }
            if(lessRobot != "")
            {
                for (int i = 0; i < optionsTable.Rows.Count; ++i)
                {
                    if (optionsTable.Rows[i].HeaderCell.Value.Equals(lessRobot))
                    {
                        matrixRow = i;
                        break;
                    }
                }
            }
            if(lessWorker != "")
            {
                for (int i = 0; i < optionsTable.Columns.Count; ++i)
                {
                    if (optionsTable.Columns[i].HeaderCell.Value.Equals(lessWorker))
                    {
                        matrixCol = i;
                        optionsTable.Rows[matrixRow].Cells[matrixCol].Style.BackColor = System.Drawing.Color.Green;
                        optionsTable.Rows[matrixRow].Cells[matrixCol].Style.ForeColor = System.Drawing.Color.White;
                        break;
                    }
                }
            }
            optionsTable.CurrentCell = null; // to inactivate default selection
            optionsTable.Rows[0].Cells[0].Selected = false;
            getMoreBetters();
        }
        private void getMoreBetters()
        {
            for (int i = 0; i < selectedOpCol; ++i)
            {
                for(int j = 0; j < selectedRobRow; ++j)
                {
                    if(optimum == Double.Parse(optionsTable.Rows[j].Cells[i].Value.ToString()) &&
                       ((i != 0) && optimum != Double.Parse(optionsTable.Rows[j].Cells[i-1].Value.ToString())))
                    {
                        Console.WriteLine("sor: " + j + ", o: " + i + ", érték: " + optionsTable.Rows[i].Cells[j].Value.ToString());
                        optionsTable.Rows[j].Cells[i].Style.BackColor = System.Drawing.Color.Green;
                        optionsTable.Rows[j].Cells[i].Style.ForeColor = System.Drawing.Color.White;
                        break;
                    }
                }
            }
        }
        private void optionCellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e) // double click
        {
            Console.WriteLine("indexek: " + e.RowIndex + ", " + e.ColumnIndex);
            if (e.RowIndex <= -1 || e.ColumnIndex <= -1)
            {
                return;
            }
            System.Windows.Forms.DataGridViewCell cell = optionsTable.Rows[e.RowIndex].Cells[e.ColumnIndex];
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
                hidingPanel.Visibility = Visibility.Collapsed;
                panelOfData.Visibility = Visibility.Visible;
                CalculateOptions();
            }
            Console.WriteLine("OPT: " + optimum);
            Console.WriteLine("cachből: " + form.ReadCache);
            Console.WriteLine("nem cachből: " + form.CountNotFromCache);
            Console.WriteLine("ÖSSZ keresés: " + form.TotalCount);
            Console.WriteLine("cache összméret " + form.Cache.Count);
            Console.WriteLine("ellapsed milliseconds (to 1 limit): " + stopwatch.ElapsedMilliseconds);
            Console.WriteLine("ellapsed milliseconds (to all): " + stopwatchAll.ElapsedMilliseconds);
            form.TotalCount = 0;
            form.CountNotFromCache = 0;
            form.ReadCache = 0;
            stopwatch.Reset();
            stopwatchAll.Reset();
        }

        private void CalculateOptions()
        {
            optionsTable.Visible = true;
            robotLimit = Int32.Parse(maxRobot.Text);
            operatorLimit = Int32.Parse(maxOperator.Text);
            optimums.Clear();
            form.TotalCount = 0;
            form.ReadCache = 0;
            indexes = new List<Index>();
            if (calculate != 0)
            {
                optionsTable.Rows.Clear();
                optionsTable.Columns.Clear();
                optionsTable.ClearSelection();
                combinationMap.Clear();
            }
            if (calculate > 0 && Form1.getCacheCount() <= 0)
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
                stopwatchAll.Start();
                optimum = form.getPrevOptVal(tables.Count - 1, robotLimit, operatorLimit, ref idxs);
                stopwatch.Stop();                
                for (int i = 0; i <= robotInterval; ++i) // default 5
                {
                    for (int z = 0; z <= workerInterval; ++z) // default 5
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
                stopwatchAll.Stop();
                fillOptions();
                calculate++;
            }
        }

        /* selector:  0 : Robot -- && Operator --
      *               1 : Robot -- && Operator ++
      *               2 : Robot ++ && Operator --
      *               3 : Robot ++ && Operator ++
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
                //Console.WriteLine("prod: " + j.Product + ", robot: " + j.Robot + ", operator: " + j.Worker);
            }
            optimum.Add(idx, opt);

            combinationMap.Add(k, optimum);
        }

        int prevSelectedRow = -1;
        int prevSelectedCol = -1;
        private void cellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e) // single click
        {
            System.Drawing.Color yellow = System.Drawing.Color.Yellow;
            System.Drawing.Color green = System.Drawing.Color.Green;
            System.Drawing.Color white = System.Drawing.Color.White;
            System.Drawing.Color aqua = System.Drawing.Color.Aqua;
            System.Drawing.Color lightBlue = System.Drawing.Color.LightBlue;
            System.Drawing.Color control = System.Drawing.SystemColors.Control;
            System.Drawing.Color yellow2 = System.Drawing.ColorTranslator.FromHtml("#ffffcc");
            //DataGridViewCell cell = optionsTable.Rows[e.RowIndex].Cells[e.ColumnIndex];
            //if (cell.Value != null && (cell.Value.ToString() != 0.ToString() || cell.ColumnIndex != 0 || cell.RowIndex != 0))
            //{
            //    cell.Style.BackColor = Color.Azure;
            //}
            if (e.RowIndex <= -1 || e.ColumnIndex <= -1) // clicked on headers
            {
                return;
            }
            if (prevSelectedCol != -1 && prevSelectedRow != -1) // remove previous selection if any
            {
                for (int i = 0; i < prevSelectedRow; ++i)
                {
                   
                    if (optionsTable.Rows[i].Cells[prevSelectedCol].Style.BackColor != yellow &&
                        optionsTable.Rows[i].Cells[prevSelectedCol].Style.BackColor != green)
                    {
                        optionsTable.Rows[i].Cells[prevSelectedCol].Style.BackColor = white;
                    }
                }
                for (int i = 0; i < prevSelectedCol; ++i)
                {
                    if (optionsTable.Rows[prevSelectedRow].Cells[i].Style.BackColor != yellow &&
                        optionsTable.Rows[prevSelectedRow].Cells[i].Style.BackColor != green)
                    {
                        optionsTable.Rows[prevSelectedRow].Cells[i].Style.BackColor = white;
                    }
                }
                if(optionsTable.Rows[prevSelectedRow].HeaderCell.Style.BackColor != System.Drawing.ColorTranslator.FromHtml("#ffffcc"))
                {
                    optionsTable.Rows[prevSelectedRow].HeaderCell.Style.BackColor = control;
                }
                if (optionsTable.Columns[prevSelectedCol].HeaderCell.Style.BackColor != System.Drawing.ColorTranslator.FromHtml("#ffffcc"))
                {
                    optionsTable.Columns[prevSelectedCol].HeaderCell.Style.BackColor = control;
                }
            }
            System.Windows.Forms.DataGridViewCell cell = optionsTable.Rows[e.RowIndex].Cells[e.ColumnIndex]; // clicked cell
            for (int i = 0; i < e.RowIndex; ++i)
            {
                if (optionsTable.Rows[i].Cells[e.ColumnIndex].Style.BackColor != yellow &&
                    optionsTable.Rows[i].Cells[e.ColumnIndex].Style.BackColor != green)
                {
                    optionsTable.Rows[i].Cells[e.ColumnIndex].Style.BackColor = lightBlue;
                }
            }
            for (int i = 0; i < e.ColumnIndex; ++i)
            {
                if (optionsTable.Rows[e.RowIndex].Cells[i].Style.BackColor != yellow &&
                    optionsTable.Rows[e.RowIndex].Cells[i].Style.BackColor != green)
                {
                    optionsTable.Rows[e.RowIndex].Cells[i].Style.BackColor = lightBlue;
                }
            }
            prevSelectedRow = e.RowIndex;
            prevSelectedCol = e.ColumnIndex;

            if (optionsTable.Rows[e.RowIndex].HeaderCell.Style.BackColor != yellow)
            {
                optionsTable.Rows[e.RowIndex].HeaderCell.Style.BackColor = lightBlue;
            }
            if (optionsTable.Columns[e.ColumnIndex].HeaderCell.Style.BackColor != yellow)
            {
                optionsTable.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = lightBlue;
            }

        }
        //private void ShowOpts_FormClosed(object sender, FormClosedEventArgs e) //TODO
        //{
        //    for (int i = 0; i < showSolList.Count; i++)
        //    {
        //        showSolList[i].Close();
        //    }
        //}

        private void trackBar1_Scroll(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(initialized == 0)
            {
                return;
            }
            if (maxOperator.Text.Length.Equals(0) || maxRobot.Text.Length.Equals(0))
            {
                System.Windows.MessageBox.Show("Missing information", "Please enter valid resource limits!");
            }
            else
            {
                opSlide.Content = trackBar1.Value.ToString();
                workerInterval = (int)trackBar1.Value;
                prevSelectedRow = -1;
                prevSelectedCol = -1;
                CalculateOptions();
            }
        }

        private void trackBar2_Scroll(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (initialized == 0)
            {
                return;
            }
            if (maxOperator.Text.Length.Equals(0) || maxRobot.Text.Length.Equals(0))
            {
                System.Windows.Forms.MessageBox.Show("Missing information", "Please enter valid resource limits!");
            }
            else
            {
                robSlide.Content = trackBar2.Value.ToString();
                robotInterval = (int)trackBar2.Value;
                prevSelectedRow = -1;
                prevSelectedCol = -1;
                CalculateOptions();
            }
        }
        //private void maxRobot_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        //{
        //    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',')
        //    {
        //        e.Handled = true;
        //    }
        //    if (e.KeyChar == '.' || e.KeyChar == ',')
        //    {
        //        e.Handled = true;
        //    }
        //    TextBox t = sender as TextBox;

        //    //Delete the placeholder 0
        //    if (t.Text == "0" && char.IsDigit(e.KeyChar))
        //    {
        //        t.Text = "";
        //    }
        //}
        Regex regex = new Regex("[^0-9]+");

        public List<ShowSolution> ShowSolList { get => showSolList; set => showSolList = value; }

        private void maximums_KeyPress(object sender, TextCompositionEventArgs e)
        {
            TextBox ad = sender as TextBox;
            e.Handled = regex.IsMatch(e.Text);
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',')
            //{
            //    e.Handled = true;
            //}
            //TextBox t = sender as TextBox;

            ////Delete the placeholder 0
            //if (t.Text == "0" && char.IsDigit(e.KeyChar))
            //{
            //    t.Text = "";
            //}
            //if (e.KeyChar == '.' || e.KeyChar == ',')
            //{
            //    e.Handled = true;
            //}
        }
        private void ShowOpts_Load(object sender, EventArgs e)
        { //TODO
            TextBlock t = new TextBlock();

            //coverLabel.Visible = true;
            //coverLabel.Width = optionsTable.Width;
            //coverLabel.Height = optionsTable.Height;
            //coverLabel.Location = new System.Drawing.Point(optionsTable.Location.X, optionsTable.Location.Y);
        }
    }
}
