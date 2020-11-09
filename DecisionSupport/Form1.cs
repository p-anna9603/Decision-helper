using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xamarin.Forms;

namespace DecisionSupport
{
    public partial class Form1 : Form
    {
        static List<Table> tables = new List<Table>();
        static int counter = 0;
        static System.Windows.Forms.Button submitButton;
        Dictionary<int, Dictionary<int, Dictionary<int, OptimumValue>>> cache = new Dictionary<int, Dictionary<int, Dictionary<int, OptimumValue>>>();
        int l = 10, k = 4;

        //Up,up,vp
        public double getU(int p, int up, int vp) {
            if (p - 1 < 0 || p - 1 > tables.Count) {
                return 0;
            }
            Table t = tables[p - 1];

            int row = 0;
            int col = 0;

            for (int i = 1; i < t.Product01.RowCount - 1; ++i) {
                if (up == Int32.Parse(t.Product01.GetControlFromPosition(0, i).Text)) {
                    row = i;
                    break;
                }
            }
            
            for (int i = 1; i < t.Product01.ColumnCount - 1; ++i) {
                if (vp == Int32.Parse(t.Product01.GetControlFromPosition(i, 0).Text)) {
                    col = i;
                    break;
                }
            }
            
            if (col == 0 || row == 0) {
                return 0;
            }

            int intersection = Int32.Parse(t.Product01.GetControlFromPosition(col, row).Text);

            double result = (t.getCostProductValue() * intersection) - (up * t.getCostRobotValue()) - (vp * t.getCostWorkerValue());

            return result; 
        }

        public OptimumValue getMPLK(int p, int currentIndex1, int currentIndex2, int limit1, int limit2) {
            Console.WriteLine("\t!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Console.WriteLine("\t" +  p + " " + currentIndex1 + " " + currentIndex2 + ", l2:" +  limit1 + ", lim2: " + limit2);
            if (p < 0 || currentIndex1 < 0 || currentIndex2 < 0) {
                Console.WriteLine("---------------------");
                return new OptimumValue();
            }

            OptimumValue prevOpt = new OptimumValue();
            
            Dictionary<int, Dictionary<int, OptimumValue>> out1;
            if (cache.TryGetValue(p, out out1)) {
                Dictionary<int, OptimumValue> out2;
                if (out1.TryGetValue(currentIndex1, out out2)) {
                    OptimumValue out3;
                    if (out2.TryGetValue(currentIndex2, out out3)) {
                        Console.WriteLine("3. ifben-----------");
                        if(out3.MplkIndexes.Count == 0)
                        {
                            if (currentIndex1 > limit1)
                            {
                                prevOpt = getMPLK(p, currentIndex1 - 1, currentIndex2, limit1, limit2);
                            }
                            else if (currentIndex2 > limit2)
                            {
                                prevOpt = getMPLK(p, currentIndex1, currentIndex2 - 1, limit1, limit2);
                            }
                            else
                            {
                                prevOpt = out3; // előző product optimum érték
                                Console.WriteLine("prev elseben: " + prevOpt.Val);
                            }
                        }
                        else if(out3.MplkIndexes[0] + currentIndex1 > limit1)
                        {
                            prevOpt = getMPLK(p, currentIndex1 - 1, currentIndex2,  limit1, limit2);
                        }
                        else if(out3.MplkIndexes[1] + currentIndex2 > limit2)
                        {
                            prevOpt = getMPLK(p, currentIndex1, currentIndex2 - 1, limit1, limit2);
                        }
                        else
                        {
                            prevOpt = out3; // előző product optimum érték
                            //Console.WriteLine("prev elseben: " + prevOpt.Val);
                        }
                    } else {
                        //Console.WriteLine("a" + p + currentIndex1 + currentIndex2, limit1, limit2);
                        prevOpt = getMPLK(p, currentIndex1, currentIndex2 - 1, limit1, limit2);
                    }
                } else { // nincs olyan sorhoz tartozó érték
                    //Console.WriteLine("b" + p + (l -1).ToString() + k);
                    prevOpt = getMPLK(p, currentIndex1 - 1, currentIndex2, limit1, limit2);
                }
            }
            Console.WriteLine(" \t prevopt: " + prevOpt.Val);
            return prevOpt;
        }

        // adott cella érték
        public OptimumValue calcOpt(int p, int u, int v) {
            OptimumValue prevOpt = getMPLK(p - 1, l - u, k - v, l - u, k - v);
            Console.WriteLine("prevopt: val " + prevOpt.Val);
            //if(prevOpt.MplkIndexes.Count != 0)
            //{
            //    Console.WriteLine("prevopt: MplkIndexes " + prevOpt.MplkIndexes[0] + " " + prevOpt.MplkIndexes[1]);
            //}
            //if (prevOpt.CurrentPMaxIndexes.Count != 0)
            //{
            //    Console.WriteLine("prevopt: CurrentPMaxIndexes " + prevOpt.CurrentPMaxIndexes[0] + " " + prevOpt.MplkIndexes[1]);
            //}
            OptimumValue result = new OptimumValue();
            result.Val = prevOpt.Val + getU(p, u, v);
            result.MplkIndexes = prevOpt.CurrentPMaxIndexes;
            Console.WriteLine("size: " + prevOpt.CurrentPMaxIndexes.Count);
            result.ValList.Add(result.Val);

            return result;
        }

        public OptimumValue getOptValues() {
            int p = 1; 
            OptimumValue maxOptimum = new OptimumValue();

            while (p <= tables.Count) {
                OptimumValue opt = calcOptForTable(p);
                if (opt.Val > maxOptimum.Val) {
                    maxOptimum = opt;
                }
                ++p;
            }

            Console.WriteLine("OPTIMUM: " + maxOptimum.Val);

            double ossz = 0;
            OptimumValue currentOpt = maxOptimum;
            --p;
            while (p > 1) {
                Console.WriteLine(p + ". product");
                Console.WriteLine(currentOpt.CurrentPMaxIndexes[0] + " " + currentOpt.CurrentPMaxIndexes[1]);
                ossz += currentOpt.Val;
                --p;
                Console.WriteLine("MPLK---------" + currentOpt.MplkIndexes[0]);
                currentOpt = cache[p][currentOpt.MplkIndexes[0]][currentOpt.MplkIndexes[1]];
            }
            
            Console.WriteLine(p + ". product");
            Console.WriteLine(currentOpt.CurrentPMaxIndexes[0] + " " + currentOpt.CurrentPMaxIndexes[1]);
            ossz += currentOpt.Val;
            Console.WriteLine("Össz termelés: " + ossz);
            foreach (var x in cache) {
                Console.WriteLine(x.Key + ". product");

                foreach(var y in x.Value) {
                    Console.WriteLine("\t" + y.Key + ". row");

                    foreach(var z in y.Value) {
                        Console.WriteLine("\t\t" + z.Key + ". Column: " + z.Value.Val);
                    }
                }
            }

            return maxOptimum;
        }

        public OptimumValue calcOptForTable(int p) {
            Table table = tables[p - 1];

            int row = 1;
            int col = 1;

            OptimumValue maxOptimum = new OptimumValue();
            while (row < table.Product01.RowCount - 1) {
                col = 1;
                while (col < table.Product01.ColumnCount - 1) {
                    int u = Int32.Parse(table.Product01.GetControlFromPosition(0, row).Text); // sorfő érték
                    int v = Int32.Parse(table.Product01.GetControlFromPosition(col, 0).Text); // oszlopfő érték

                    OptimumValue optimum = calcOpt(p, u, v);
                    Console.WriteLine("-- val: " + optimum.Val);
                    if (optimum.Val > maxOptimum.Val) {
                        maxOptimum = optimum;
                        maxOptimum.CurrentPMaxIndexes.Clear();
                        maxOptimum.CurrentPMaxIndexes.Add(u); 
                        maxOptimum.CurrentPMaxIndexes.Add(v); 
                    }

//Dictionary<int, Dictionary<int, Dictionary<int, OptimumValue>>> cache = new Dictionary<int, Dictionary<int, Dictionary<int, OptimumValue>>>();
                    if (!cache.ContainsKey(p)) {
                        cache.Add(p, new Dictionary<int, Dictionary<int, OptimumValue>>());
                    }

                    if (!cache[p].ContainsKey(u)) {
                        cache[p].Add(u, new Dictionary<int, OptimumValue>());
                    }

                    // Console.WriteLine("CACHE #####");
                    // Console.WriteLine(p + " " + u + " " + v);

                    cache[p][u].Add(v, maxOptimum);
                    ++col;
                }
                ++row;
            }

            return maxOptimum;
        }
        public void checkLimit()
        {
            OptimumValue optVal = new OptimumValue();
            OptimumValue currentOpt = new OptimumValue();
            optVal.ValList.Sort();
            optVal.ValList.Reverse();
            for(int i = 0 ; i < tables.Count; ++i)
            {
                Console.WriteLine(1 + ". product");
                Console.WriteLine(currentOpt.CurrentPMaxIndexes[0] + " " + currentOpt.CurrentPMaxIndexes[1]);
            }
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

        public Form1()
        {
            InitializeComponent();
            FormHelper.SetSizeToScreen(this.FindForm());
            this.MinimumSize = new System.Drawing.Size(600, 600);
            this.StartPosition = FormStartPosition.Manual;
            // To reduce flickering: 
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.HorizontalScroll.Enabled = false;
        }

        private void newProductMenu_Click(object sender, EventArgs e)
        {
            if (tables.Count != 0)
            {
                //System.Drawing.Point prevPos = this.FindForm().PointToClient(
                //tables[tables.Count - 1].Parent.PointToScreen(tables[tables.Count - 1].Location));

                System.Drawing.Point prevPos = tables[tables.Count - 1].Location;
                Table table = new Table(counter++, prevPos.X + tables[tables.Count - 1].Width + 20, prevPos.Y);
                Controls.Add(table);

                tables.Add(table);
            }
            else
            {
                Table table = new Table(counter++, 20, 0);
                Controls.Add(table);

                tables.Add(table);
            }
            tables[tables.Count - 1].addPlus(false,false);

            adjustPositions(this.FindForm());
        }

        public static void adjustPositions(Form form)
        {
            if (tables.Count == 0)
            {
                return;
            }

            form.SuspendLayout();
            for (int i = 0; i < tables.Count; ++i)
            {
                tables[i].SuspendLayout();
            }

            
            tables[0].Location = new System.Drawing.Point(form.AutoScrollPosition.X + 20, 
                                                form.AutoScrollPosition.Y+50); // első tábla törlése esetén 

            ////// Console.WriteLine("0. tábla helye" + tables[0].Location + "\n");
            //int maximumRowHeight = 50 + tables[0].Height;
            int maximumRowHeight = 0;

            for (int i = 1; i < tables.Count; ++i)
            {
                System.Drawing.Point prevPos = tables[i - 1].Location;
                ////// Console.WriteLine(i - 1 + ". tábla helye " + prevPos);

                //System.Drawing.Point prevPos = form.PointToClient(
                //       tables[i - 1].Parent.PointToScreen(tables[i - 1].Location));

                /* oszlop hozzáadásnál */
                tables[i].Location = new System.Drawing.Point(prevPos.X + tables[i - 1].Width + 20, prevPos.Y);

                /* sor magasság növelés*/
                if ((prevPos.Y + tables[i - 1].Height > tables[maximumRowHeight].Location.Y + tables[maximumRowHeight].Height))
                {
                    //// Console.WriteLine("2. magasság");
                    //if (!(tables[i].Location.X + tables[i].Width > form.Width))
                    //{
                    //maximumRowHeight = prevPos.Y + tables[i - 1].Height;
                    maximumRowHeight = i - 1;
                        ////// Console.WriteLine("prevPos.Y : " + prevPos.Y);
                        ////// Console.WriteLine("tables[i - 1].Height: " + tables[i - 1].Height);
                    //}
                }

                /* ha kilógna a képből új sorban jelenjen meg */
                if ((tables[i].Location.X + tables[i].Width >= form.Width - 70))
                {
                    ////// Console.WriteLine("3. új sor");
                    tables[i].Location = new System.Drawing.Point(20, tables[maximumRowHeight].Location.Y + tables[maximumRowHeight].Height + 20);
                    ////// Console.WriteLine("maxrowheight: " + maximumRowHeight);
                    maximumRowHeight = i;
                }
                ////// Console.WriteLine(i + ". tábla új helye " + tables[i].Location);
            }

            /* ha a submit gombot eléri a tábla csúsztassuk lejjebb */
            //if ((tables[tables.Count - 1].Location.Y + tables[tables.Count - 1].Height) >= submitButton.Location.Y - 20)
            //{
            //}

                //submitButton.Location = new System.Drawing.Point(SystemInformation.WorkingArea.Width - 150, SystemInformation.WorkingArea.Height - 100);
            ////// Console.WriteLine("\nWorking area width " + SystemInformation.WorkingArea.Width);
            ////// Console.WriteLine("Working area height " + SystemInformation.WorkingArea.Height + "\n");

            for (int i = 0; i < tables.Count; ++i)
            {
                tables[i].ResumeLayout(false);
                tables[i].PerformLayout();
            }
            form.ResumeLayout(false);
            form.PerformLayout();

            ////// Console.WriteLine("\nAutoscroll offset: " + form.AutoScrollOffset);
            ////// Console.WriteLine("\nAutoscroll poz offset: " + form.AutoScrollPosition);
            ////// Console.WriteLine("\nform.VerticalScroll.Value : " + form.VerticalScroll.Value);
        }

        public static void deleteTable(Form form, int idx)
        {
            int toDelete = 0;
            int toDeleteIdx = 0;
            for (int i = 0; i < tables.Count; ++i)
            {
                if (tables[i].idx == idx)
                {
                    toDelete = i;
                    toDeleteIdx = idx;
                    form.Controls.Remove(tables[i]);
                    tables.RemoveAt(i);
                    //// Console.WriteLine("todelte: " + toDelete);
                    //// Console.WriteLine("toDeleteIdx: " + toDeleteIdx);
                }
            }
            for(int j = toDelete; j < tables.Count; j++)
            {
                if(j == toDelete)
                {
                    tables[j].idx = toDeleteIdx;
                }
                else
                {
                    tables[j].idx = tables[j - 1].idx+1;
                }
                int uj = (tables[j].idx) + 1;
                tables[j].ProductCountLabel.Text = uj + ". product";
                //// Console.WriteLine(j + ". tábla, új felirat " + tables[j].ProductCountLabel.Text);
            }
            counter--;
            adjustPositions(form);
        }

        public void submitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getOptValues();
            //using (StreamWriter writeText = new StreamWriter("output.csv"))
            //{
            //    for (int i = 0; i < tables.Count; ++i)
            //    {
            //        //writeText.WriteLine("product " + i.ToString());

            //        TableLayoutPanel table = tables[i].getTable();

            //        for (int row = 0; row < table.RowCount - 1; ++row)
            //        {
            //            for (int col = 0; col < table.ColumnCount - 1; ++col)
            //            {
            //                if (row == 0 && col == 0)
            //                {
            //                    writeText.Write(";");
            //                    continue;
            //                }
            //                writeText.Write(table.GetControlFromPosition(col, row).Text);
            //                if (col != table.ColumnCount - 2)
            //                {
            //                    writeText.Write(";");
            //                }
            //            }
            //            writeText.Write("\n");
            //        }

            //        writeText.WriteLine("*");
            //        writeText.WriteLine(tables[i].getCostWorkerValue() + ";" + tables[i].getCostRobotValue() + ";" + tables[i].getCostProductValue());
            //        writeText.WriteLine();
            //    }
            //}
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamReader reader = new StreamReader("output.csv");
            int rowCount = 0;

            TableData tableData = new TableData();
            while (!reader.EndOfStream)
            {
                String strLine = reader.ReadLine();
                if (strLine == "")
                {
                    continue;
                }
                String[] strArray = strLine.Split(';');
                if (strLine == "*")
                {
                    strArray = reader.ReadLine().Split(';');
                    //ables[i].getCostWorkerValue() + ";" + tables[i].getCostRobotValue() + ";" + tables[i].getCostProductValue())
                    Double costWorker, costRobot, costProductValue;
                    costWorker = Double.Parse(strArray[0]);
                    costRobot = Double.Parse(strArray[1]);
                    costProductValue = Double.Parse(strArray[2]);

                    tableData.WorkerCost = costWorker;
                    tableData.RobotCost = costRobot;
                    tableData.ProductValue = costProductValue;

                    Table table = Table.createFromTableData(tableData, counter++, 0, 0);
                    Controls.Add(table);
                    tables.Add(table);      
                    rowCount = 0;
                    tableData = new TableData();
                    continue;
                }
                
                for (int i = 0; i < strArray.Length; ++i)
                {
                    //table.addCell(rowCount, i, strArray[i]);
                    tableData.addToRow(rowCount, strArray[i]);
                }
                rowCount++;
            }
            adjustPositions(this.FindForm());
            reader.Close();
        }


    }

    
    public class OptimumValue {
        double val = 0;
        List<int> mplkIndexes = new List<int>(); // robot and operator number of optimum production for one product
        List<int> currentPMaxIndexes = new List<int>(); // adott termék addigi maximumhoz tartozó indexek
        List<double> valList = new List<double>();
        public double Val { get => val; set => val = value; }
        public List<int> MplkIndexes { get => mplkIndexes; set => mplkIndexes = value; }
        public List<int> CurrentPMaxIndexes { get => currentPMaxIndexes; set => currentPMaxIndexes = value; }
        public List<double> ValList { get => valList; set => valList = value; }
    }

}
