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
using Xamarin.Forms.Internals;
using System.Linq;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace DecisionSupport
{
    public partial class Form1 : Form
    {
        static List<Table> tables = new List<Table>(); // contains all the Table type (with all controls)
        static int counter = 0;
        int totalCount = 0;
        static System.Windows.Forms.Button submitButton;
        Dictionary<string, Dictionary<List<Index>, double>> cache = new Dictionary<string, Dictionary<List<Index>, double>>();

        Dictionary<int, Index> optProducts = new Dictionary<int, Index>();
        List<Index> optProdIdx = new List<Index>();
       // product,robot,worker index,  indexlista optimumra   érték(optimum)
       
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

            this.FormClosing += new FormClosingEventHandler(savingData);

            //this.FormBorderStyle = FormBorderStyle.None;
            //this.FormBorderStyle = FormBorderStyle.FixedSingle;
            //this.WindowState = FormWindowState.Maximized;
            //this.ControlBox = true;
            //this.TopMost = true;
            //this.Bounds = Screen.PrimaryScreen.Bounds;
        }

        //Up,up,vp
        public double getU(int p, int up, int vp) {
            if (p  < 0 || p  > tables.Count) {
                return 0;
            }
            Table t = tables[p];

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

        int count = 0;
        int readCache = 0;
        public double getPrevOptVal(int product, int robotLimit, int workerLimit, ref List<Index> indexes)
        {
            totalCount++;
            Console.WriteLine(totalCount + ". fv hívás");
            indexes = new List<Index>();
            List<int> idList = new List<int>();
            idList.Add(product);
            idList.Add(robotLimit);
            idList.Add(workerLimit);
            string[] keys = new string[3] { product.ToString(), robotLimit.ToString(), workerLimit.ToString() };
            string k = product.ToString() + "," +  robotLimit.ToString() + "," + workerLimit.ToString() + ",";

            //Console.WriteLine("kombinációk: " + keys[0] + " " + keys[1] + " " + keys[2]);

            if (product < 0)
            {
                return 0;
            }
            Console.WriteLine("\tújra: " + k);
            if (cache.Keys.Contains(k))
            {
                this.readCache++;
                indexes = cache[k].First().Key;
                return cache[k].First().Value;
            }
            else
            {
                count++;
            }

            Table table = tables[product];
            List<Index> maxIndexes = new List<Index>();
            string maxindexes = "";

            int row = 0;
            int col;
            double max = -1;

            //Console.WriteLine(product + ". tábla");
            while (row < table.Product01.RowCount - 1)
            {
                int u = row == 0 ? 0 : Int32.Parse(table.Product01.GetControlFromPosition(0, row).Text); // sorfő érték (robot)
                //Console.WriteLine("\t\tu: " + u);
                if (u > robotLimit)
                {
                    break;
                }
                col = 0;
                while (col < table.Product01.ColumnCount - 1) 
                {
                    int v = col == 0 ? 0 : Int32.Parse(table.Product01.GetControlFromPosition(col, 0).Text); // oszlopfő érték (munkás)
                    //Console.WriteLine("\tv: " + v);
                    if (v > workerLimit)
                    {
                        break;
                    }
                    double curr;
                    curr = getU(product, u, v);
                    List<Index> prevIndexes = new List<Index>();

                    double prevVal;
                    if(product != 0)
                    {
                        prevVal = getPrevOptVal(product - 1, robotLimit - u, workerLimit - v, ref prevIndexes);
                        curr += prevVal;
                        //opt.Value += prevVal;
                    }
                    if (max < curr)
                    {
                        Index id = new Index(product, u ,v);
                        //       maxIndexes = prevIndexes;
                        maxIndexes.Clear();
                        foreach (Index i in prevIndexes)
                        {
                            Console.WriteLine("prod: " + i.Product + ", robot: " + i.Robot + ", operator: " + i.Worker);
                            maxIndexes.Add(i);
                        }
                        maxIndexes.Add(id);                       
                    }
                    max = max > curr ? max : curr;
                    ++col;



                    //Console.WriteLine("\tcurr: " + curr);
                }
                ++row;
            }

            //Console.WriteLine("\topt: " + max);
            List<Index> idx = new List<Index>();
            foreach (Index i in maxIndexes)
            {
                idx.Add(i);
                Console.WriteLine("prod: " + i.Product + ", robot: " + i.Robot + ", operator: " + i.Worker);
            }
            Dictionary<List<Index>, double> optimum = new Dictionary<List<Index>, double>();
            optimum.Add(idx, max);
            Cache.Add(k, optimum);

            indexes = maxIndexes;
            return max;
        }
        
        public void getOptimum()
        {
            if(tables.Count == 0)
            {
                MessageBox.Show("There is nothing to evaluate.\nPlease add a new product.", "No data");
                return;
            }
            Console.WriteLine(" getOptimumban");

            ShowSolution showSol = new ShowSolution(ref tables, this);
            showSol.ShowDialog();
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

        public int ReadCache { get => readCache; set => readCache = value; }
        public Dictionary<string, Dictionary<List<Index>, double>> Cache { get => cache; set => cache = value; }
        public int Count { get => count; set => count = value; }
        public int TotalCount { get => totalCount; set => totalCount = value; }

        int saveRes = 0;
        private void savingData(Object sender, FormClosingEventArgs e)
        {
            if (tables.Count != 0)
            {
                const string message = "Do you want to save the data before exit?";
                const string caption = "Exit application";
                var result = MessageBox.Show(message, caption,
                                            MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    savingMenuItemClicked(sender, e);
                }
                else if (result == DialogResult.No)
                {
                    e.Cancel = false;
                }
                if (saveRes == 0 && result == DialogResult.Yes)
                {
                    MessageBox.Show("Saving failed", "Please try again!");
                    e.Cancel = true;
                }
                else if (saveRes == 1 && result == DialogResult.Yes)
                {
                    MessageBox.Show("Success", "Saving was successful!");
                }
            }
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

        public void savingMenuItemClicked(object sender, EventArgs e)
        {
            if(tables.Count == 0)
            {
                MessageBox.Show("There is nothing to save.\nStart you work now!", "Saving?");
                return;
            }
            //getOptimum();
            string filename;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Csv files|*.csv";
            DialogResult res = sfd.ShowDialog();
            filename = sfd.FileName;
            var regExp = @"^(?:[\w]\:|\\)(\\[a-zA-Z_\-\s0-9]+)+\.(csv)$";
            Regex regex = new Regex(regExp);
            if (res == DialogResult.OK && filename.EndsWith(".csv") && regex.IsMatch(filename))
            {
                filename = sfd.FileName;
            }
            else
            {
                saveRes = 0;
                return;
            }
            using (StreamWriter writeText = new StreamWriter(filename))
            {
                for (int i = 0; i < tables.Count; ++i)
                {
                    //writeText.WriteLine("product " + i.ToString());

                    TableLayoutPanel table = tables[i].getTable();

                    for (int row = 0; row < table.RowCount - 1; ++row)
                    {
                        for (int col = 0; col < table.ColumnCount - 1; ++col)
                        {
                            if (row == 0 && col == 0)
                            {
                                writeText.Write(";");
                                continue;
                            }
                            writeText.Write(table.GetControlFromPosition(col, row).Text);
                            if (col != table.ColumnCount - 2)
                            {
                                writeText.Write(";");
                            }
                        }
                        writeText.Write("\n");
                    }

                    writeText.WriteLine("*");
                    writeText.WriteLine(tables[i].getCostWorkerValue() + ";" + tables[i].getCostRobotValue() + ";" + tables[i].getCostProductValue());
                    writeText.WriteLine();
                }
                writeText.WriteLine("***");
                Console.WriteLine("save cahce előtt " + Cache.Count);
               foreach (KeyValuePair<string, Dictionary<List<Index>, double>> entry in this.cache)
                {
                    string idx = ""; 
                    if(entry.Key.Equals("0,20,10"))
                    {
                        Console.WriteLine("mentésben " + entry.Key[0] + entry.Key[1] + entry.Key[2] + entry.Key[3] + entry.Key[4] + entry.Key[5] + entry.Key[6]);
                        Console.WriteLine("mentés méret:  " + entry.Key.Length);
                    }
                    foreach (char i in entry.Key) // 0,20,10
                    {
                        
                        if (i == ',')
                        {
                            writeText.Write(idx + ",");
                            idx = "";
                        }
                        else
                        {
                            idx += i; 
                        }
                    }
                    writeText.Write(idx);
                    writeText.Write(";");
                    List<Index> k = entry.Value.First().Key;                  
                    foreach (Index j in k)
                    {
                        writeText.Write(j.Product + ",");
                        writeText.Write(j.Robot + ",");
                        writeText.Write(j.Worker + ",");
                    }
                    writeText.Write(";" + entry.Value.First().Value + "\n");
                }
                saveRes = 1;
            }
        }

        private void evaluateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getOptimum();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamReader reader;
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Csv files|*.csv";
                DialogResult res = ofd.ShowDialog();
                if(res == DialogResult.OK && ofd.FileName.Contains(".csv"))
                {
                    reader = new StreamReader(ofd.FileName);
                }
                else if(res != DialogResult.OK || !ofd.FileName.Contains(".csv"))
                {
                    const string message = "The file can not be opened.\n Please try again!";
                    const string caption = "Opening failed";
                    MessageBox.Show(message, caption);
                    return;
                }
                else
                {
                    const string message = "The file can not be opened.\n Please try again!";
                    const string caption = "Opening failed";
                    MessageBox.Show(message, caption);
                    return;
                }
            }
            catch(Exception ex)
            {
                const string message = "The file is propably open.\n Please close the file first!";
                const string caption = "Opening failed";
                MessageBox.Show(message, caption);
                Console.WriteLine("Openening failed: " + ex.Message);
                return;
            }

            int rowCount = 0;

            TableData tableData = new TableData();
            while (!reader.EndOfStream)
            {
                Console.WriteLine("not end");
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

                if (strLine == "***")
                {
                    while (!reader.EndOfStream)
                    {
                        strArray = reader.ReadLine().Split(';');
                        //List<int> idlist = new List<int>();
                        string k = "";
                        List<Index> indexlist = new List<Index>(); // optimumhoz robot és worker szám productonként
                        foreach (string str in strArray[0].Split(','))
                        {
                            if (str.Length == 0)
                            {
                                continue;
                            }
                            //idlist.Add(Int32.Parse(str));
                            k += str + ",";
                        }
                        string[] id = strArray[1].Split(',');
                        for(int i = 0; i < id.Length-1; i+=3)
                        {
                            Index index = new Index(Int32.Parse(id[i]), Int32.Parse(id[i + 1]), Int32.Parse(id[i + 2]));
                            indexlist.Add(index);
                        }                        
                        Dictionary<List<Index>, double> optimum = new Dictionary<List<Index>, double>();
                        optimum.Add(indexlist, Double.Parse(strArray[2]));
                        Console.WriteLine("kulcs: " + k);
                        Cache.Add(k, optimum);
                    }
                }
                Console.WriteLine("###############beolvasva: ");
                foreach(var i in cache.Keys)
                {
                    for(int j = 0; j < i.Length; ++j)
                    {
                        Console.Write(i[j]);
                    }
                    Console.Write("\n");
                }
            }
            Console.WriteLine("open");
            adjustPositions(this.FindForm());
            reader.Close();
        }

    }
}
