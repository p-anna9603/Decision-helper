using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
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

        /* For matrix options */
        int defaultInterval = 5;
        List<Index> indexes = new List<Index>();
        List<double> optimums = new List<double>(); // list of the optimum values for the different combinations
        List<Index> indexList = new List<Index>();
        Dictionary<string, Dictionary<List<Index>, double>> combinationMap = new Dictionary<string, Dictionary<List<Index>, double>>();

        public ShowOpts(ref List<Table> tables, Form1 f)
        {
            this.tables = tables;
            form = f;
            InitializeComponent();
            this.Size = new System.Drawing.Size(770, 600);
        }

        public void fillOptions()
        {
            int selectedRobRow = 0;
            int selectedOpCol = 0;
            string lessRobot = "";
            string lessWorker = "";
            int matrixRow = 0;
            int matrixCol = 0;
            for (int i = 0; i < defaultInterval * 2 + 1; ++i) //0..10
            {
                optionsTable.Rows.Add();
                if (i <= defaultInterval) // 0..5
                {
                    int sub = defaultInterval - i; // 5..0
                    if (operatorLimit - sub >= 0)
                    {
                        optionsTable.Columns[i].HeaderCell.Value = (operatorLimit - sub).ToString(); //
                    }
                    else
                    {
                        optionsTable.Columns[i].HeaderCell.Value = "-";
                    }
                    if (robotLimit - sub >= 0)
                    {
                        optionsTable.Rows[i].HeaderCell.Value = (robotLimit - sub).ToString();
                    }
                    else
                    {
                        optionsTable.Rows[i].HeaderCell.Value = "-";
                    }
                }
                if (i >= defaultInterval + 1) // 6..10
                {
                    int add = Math.Abs(defaultInterval - i);
                    optionsTable.Columns[i].HeaderCell.Value = (operatorLimit + add).ToString(); //
                    optionsTable.Rows[i].HeaderCell.Value = (robotLimit + add).ToString();
                }
                if (optionsTable.Columns[i].HeaderCell.Value.Equals(operatorLimit.ToString()))
                {
                    selectedOpCol = i;
                }
                if (optionsTable.Rows[i].HeaderCell.Value.Equals(robotLimit.ToString()))
                {
                    selectedRobRow = i;
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
        }

        private void optionCellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = optionsTable.Rows[e.RowIndex].Cells[e.ColumnIndex];
            string worker = "";
            string robot = "";
            string key;
            List<Index> indexList = new List<Index>();
            if (cell.Value != null && (cell.Value.ToString() != 0.ToString() || cell.ColumnIndex != 0 || cell.RowIndex != 0))
            {
                worker = optionsTable.Columns[cell.ColumnIndex].HeaderCell.Value.ToString();
                robot = optionsTable.Rows[cell.RowIndex].HeaderCell.Value.ToString();
            }         
            key = robot + "," + worker;
            Console.WriteLine("kattintott: " + key);
            indexList = combinationMap[key].First().Key;

            showSol = new ShowSolution(ref tables, ref indexList, worker, robot);
            showSolList.Add(showSol);
            showSol.Show();
        }

        private void CalculateButtonClick(object sender, EventArgs e)
        {
            robotLimit = Int32.Parse(maxRobot.Text);
            operatorLimit = Int32.Parse(maxOperator.Text);
            combinationMap.Clear();
            optimums.Clear();
            form.TotalCount = 0;
            form.ReadCache = 0;
            indexes = new List<Index>();
            Console.WriteLine(" nullázás " + form.ReadCache);
            if (calculate != 0)
            {
                //   dataTable.Rows.Clear();
                optionsTable.Rows.Clear();
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

                for (int i = 0; i <= defaultInterval; ++i)
                {
                    for (int z = 0; z <= defaultInterval; ++z)
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
                for (int i = 0; i <= defaultInterval; ++i)
                {
                    for (int z = 0; z <= defaultInterval; ++z)
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
                // int toDelete = -1;
                //for (int i = 1; i < indexes.Count(); ++i)
                //{
                //    if (indexes[i].Product == indexes[i - 1].Product)
                //    {
                //        toDelete = i - 1;
                //        Console.WriteLine(" Ugyanaz: " + indexes[i].Product + " index : " + toDelete);
                //    }
                //}
                //if (toDelete != -1)
                //{
                //    indexes.RemoveAt(toDelete);
                //    Console.WriteLine("todeleteben: " + toDelete);
                //}
                fillOptions();
                Console.WriteLine("0 es: " + optimum); // optimum
                Console.WriteLine("-1 es: " + combinationMap.First().Key + ": " + combinationMap[combinationMap.First().Key].First().Value); // optimum
                                                                                                                                    //Console.WriteLine("8,4 es: " + combinationMap["8,4"].First().Value); // optimum

                foreach (var entry in combinationMap)
                {
                    string k = entry.Key;
                    Console.WriteLine(k + " limitekre : " + entry.Value.First().Value);
                }
                //Console.WriteLine("+1 es: " + combinationMap[0].First().Value); // optimum

                calculate = 1;
            }

            Console.WriteLine("OPT: " + optimum);
            Console.WriteLine("cachből: " + form.ReadCache);
            Console.WriteLine("nem cachből: " + form.Count);
            Console.WriteLine("ÖSSZ keresés: " + form.TotalCount);
            Console.WriteLine("cache összméret " + form.Cache.Count);
            Console.WriteLine("ellapsed milliseconds: " + stopwatch.ElapsedMilliseconds);
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
    }
}
