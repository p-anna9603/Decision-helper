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
//using Xamarin.Forms;
//using Xamarin.Forms.Internals;
using System.Linq;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using FontAwesome.Sharp;

namespace DecisionSupport
{
    public partial class Form1 : Form
    {
        /* Arrays */
        private static List<Table> tables = new List<Table>(); // contains all the Table type (with all controls)

        /* cache = map to store and reuse the calculated values:
         * key: string - searched M_p,l,k
         * value: another map
         *      -> key: List<Index> - stores the given M_p,l,k what allocations were created: product - robot number - operator number
         *      -> value: the double optimum itself (one number)         * 
         * */
        static Dictionary<string, Dictionary<List<Index>, double>> cache = new Dictionary<string, Dictionary<List<Index>, double>>();
        Dictionary<int, Index> optProducts = new Dictionary<int, Index>();

        /* Needed counters */
        static int counter = 0;
        int totalCount = 0;
        private static int saving = 0;      // 0 - need to be saved, 1 - already saved
        public static int modification = 0; // 1 - no change, 0 - any change happened
        public static MenuStrip menu;
        static int saveRes = 0;             // 0 - save was not successful, 1 - save was succesful
        static int newWork = 1;             // 1 - if its a new work from blank sheet, 0 - file was opened 
        static int docOpenings = 0;
        static string openedFileName = "";
        static int saveAs = 0;              // 1 - if Save As option was selected
        static int savedAs = 0;             // 1 - if Save As option was selected and saved
        static string secondFileName = "";  // saving after the save as 
        static string formTitle = "";
        static Form1 form1;                 // used in Table class - deleteTable function

        public Form1()
        {
            InitializeComponent();
             //    FormHelper.SetSizeToScreen(this.FindForm());
            this.MinimumSize = new System.Drawing.Size(600, 600);
            this.StartPosition = FormStartPosition.Manual;

            /* To reduce flickering: */
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            typeof(FlowLayoutPanel).InvokeMember("DoubleBuffered",
                  BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                 null, panelContainer, new object[] { true });

            panelContainer.VerticalScroll.Enabled = false;
            this.HorizontalScroll.Enabled = true;
            this.VerticalScroll.Enabled = true;
            this.AutoScroll = true;

            /*Original Form1 menustrip: */
             /* menu = this.menuStrip1;
                menu.BackColor = System.Drawing.Color.FromArgb(120, System.Drawing.Color.White);
                formTitle = this.Text;
             */

            this.FormClosing += new FormClosingEventHandler(savingData);
            initialize();
            form1 = this;
        }

        /* To reduce flickering: */
        Bitmap Background, BackgroundTemp;
        private void initialize()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            BackgroundTemp = new Bitmap(Properties.Resources.bckg11);
            Background = new Bitmap(BackgroundTemp, BackgroundTemp.Width, BackgroundTemp.Height);
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

        /* Counters for debug and evaluation purposes */
        private int countNotFromCache = 0;
        int readCache = 0;
        public double getPrevOptVal(int product, int robotLimit, int workerLimit, ref List<Index> indexes)
        {
            totalCount++;
            // Console.WriteLine(totalCount + ". fn call");
            indexes = new List<Index>();
            List<int> idList = new List<int>();
            idList.Add(product);
            idList.Add(robotLimit);
            idList.Add(workerLimit);
            string[] keys = new string[3] { product.ToString(), robotLimit.ToString(), workerLimit.ToString() };
            string k = product.ToString() + "," +  robotLimit.ToString() + "," + workerLimit.ToString() + ",";

            // Console.WriteLine("Called M_p,l,k: " + k);

            if (product < 0)
            {
                return 0;
            }

            if (cache.Keys.Contains(k))
            {
                this.readCache++;
              //  Console.WriteLine("reused: " + k);
                indexes = cache[k].First().Key;
                return cache[k].First().Value;
            }
            else
            {
                countNotFromCache++;
            }

            Table table = tables[product];
            List<Index> maxIndexes = new List<Index>();

            int row = 0;
            int col;
            double max = -1;

            //Console.WriteLine(product + ". table");
            while (row < table.Product01.RowCount - 1)
            {
                int u = row == 0 ? 0 : Int32.Parse(table.Product01.GetControlFromPosition(0, row).Text); // rowHeader value (number of robot)
                //Console.WriteLine("\t\tu: " + u);
                if (u > robotLimit)
                {
                    break;
                }
                col = 0;
                while (col < table.Product01.ColumnCount - 1) 
                {
                    int v = col == 0 ? 0 : Int32.Parse(table.Product01.GetControlFromPosition(col, 0).Text); // columnHeader value (number of operator)
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
                    }
                    if (max < curr)
                    {
                        Index id = new Index(product, u ,v);
                        // maxIndexes = prevIndexes; // not working very well (because of references) --> needed to copy the data (foreach loop)
                        maxIndexes.Clear();
                        foreach (Index i in prevIndexes)
                        {
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
            //Console.WriteLine("\t opt: " + max);
            
            List<Index> idx = new List<Index>();
            foreach (Index i in maxIndexes)
            {
                idx.Add(i);
            }
            Dictionary<List<Index>, double> optimum = new Dictionary<List<Index>, double>();
            optimum.Add(idx, max);
            Cache.Add(k, optimum);

            indexes = maxIndexes;
            
            return max;
        }
        
        public int getOptimum()
        {
            if(tables.Count == 0)
            {
                MessageBox.Show("There is nothing to evaluate.\nPlease add a new product.", "No data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }
            else
            {
                return 1;
            }
            /* Originally solution showing class was called here: */
            //ShowSolution showSol = new ShowSolution(ref tables, this);
            //showSol.ShowDialog();

            //ShowOpts showOpt = new ShowOpts(ref tables, this);
            //ShowOpts showOpt = new ShowOpts(this);
            //showOpt.ShowDialog();
        }

        /* A solution to reduce flickering */
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
        /* Attempt to stop flickering */
        private void panelContainer_Scroll(object sender, ScrollEventArgs e)
        {
            panelContainer.Invalidate();
            base.OnScroll(e);
        }
        /* Getters, setters: */
        public int ReadCache { get => readCache; set => readCache = value; }
        public Dictionary<string, Dictionary<List<Index>, double>> Cache { get => cache; set => cache = value; }
        public int TotalCount { get => totalCount; set => totalCount = value; }
        public  List<Table> Tables { get => tables; set => tables = value; }
        public int CanCloseParent { get => canCloseParent; set => canCloseParent = value; }
        public int CountNotFromCache { get => countNotFromCache; set => countNotFromCache = value; }
        public int Saving { get => saving; set => saving = value; }


        private int canCloseParent = 0; // 0 - do not, 1 - can close parent window (firstForm)
        private void savingData(Object sender, FormClosingEventArgs e)
        {
            if (tables.Count != 0 && getSaving() == 0 ||    // there are tables and datas were modified
                tables.Count == 0 && wasTableDeleted == 1)  // there is no table now - but there were - and were deleted
            {
                const string message = "Do you want to save the data before exit?";
                const string caption = "Exit application";
                var result = MessageBox.Show(message, caption,
                                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Console.WriteLine("yes");
                    savingMenuItemClicked(sender, e); // sets saveRes that is further checked
                }
                else if (result == DialogResult.No)
                {
                    Console.WriteLine("no");
                    canCloseParent = 1;
                    e.Cancel = false;
                }
                else if(result == DialogResult.Cancel)
                {
                    Console.WriteLine("cancel");
                    canCloseParent = 0;
                    e.Cancel = true;
                }

                if (saveRes == 0 && result == DialogResult.Yes)
                {
                    MessageBox.Show("Saving failed", "Please try again!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    canCloseParent = 0;
                    e.Cancel = true;
                }
                else if (saveRes == 1 && result == DialogResult.Yes)
                {
                    MessageBox.Show("Success", "Saving was successful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    canCloseParent = 1;
                }
            }
            else if(tables.Count != 0 && getSaving() == 1) // there is table - no need to save because there were no modifications
            {
                canCloseParent = 1;
                e.Cancel = false;
            }
            else if(tables.Count == 0 && wasTableDeleted != 1) // no table now and non was deleted either
            {
                canCloseParent = 1;
                e.Cancel = false;
            }
            else if(wasTableDeleted == 1)
            {

            }
        }
        private void newProductMenu_Click(object sender, EventArgs e)
        {
            panelContainer.SuspendLayout();
            firstForm.DisableButton();
            firstForm.IconBtn.IconChar = IconChar.Tasks;
            firstForm.Title.Text = "Project";
            if (tables.Count != 0)
            {
                System.Drawing.Point prevPos = tables[tables.Count - 1].Location;
                Table table = new Table(counter++, this);
                //     Controls.Add(table); // originally tables were added to the form itself
                table.Margin = new Padding(20, 10, 0, 3);
                tables.Add(table);          
                table.WrapContents = false;
                table.AutoScroll = false;
                typeof(Table).InvokeMember("DoubleBuffered",
                      BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                      null, table, new object[] { true });
                panelContainer.Controls.Add(table); // now tables are added to a flowlayoutpanel
            }
            else
            {
                Table table = new Table(counter++, this);
                //     Controls.Add(table);
                table.Margin = new Padding(20, 10, 0, 3);
                table.WrapContents = false;
                table.AutoScroll = false;
                tables.Add(table);
                panelContainer.Controls.Add(table);
            }
            tables[tables.Count - 1].addPlus(false,false);

            // adjustPositions(this.FindForm());
            setSaving(0);
            clearCache();
            panelContainer.ResumeLayout();
        }
        static int adj = 0;

        /* Before I was aware of FlowLayoutPanel built in wrapping function I manually created the wrap (not in use anymore): */        
        public static void adjustPositions(Form form)
        {
            adj++;
            Console.WriteLine("adjust: " + adj);
            form.HorizontalScroll.Enabled = false;
            if (tables.Count == 0)
            {
                return;
            }

            form.SuspendLayout();
            for (int i = 0; i < tables.Count; ++i)
            {
                tables[i].SuspendLayout();
                tables[i].Hide();
            }
            form.VerticalScroll.Visible = false;

            tables[0].Location = new System.Drawing.Point(20,
                                                form.AutoScrollPosition.Y + 80); // If the first product is deleted
            int maximumRowHeight = 0;

            for (int i = 1; i < tables.Count; ++i)
            {
                System.Drawing.Point prevPos = tables[i - 1].Location;

                /* adding a column */
                tables[i].Location = new System.Drawing.Point(prevPos.X + tables[i - 1].Width + 20, prevPos.Y);

                /* row height increase */
                if ((prevPos.Y + tables[i - 1].Height > tables[maximumRowHeight].Location.Y + tables[maximumRowHeight].Height))
                {
                    maximumRowHeight = i - 1;
                }

                /* if it is over the window -> put it in new line */
                if ((tables[i].Location.X + tables[i].Width >= form.Width - 70))
                {
                    tables[i].Location = new System.Drawing.Point(20, tables[maximumRowHeight].Location.Y + tables[maximumRowHeight].Height + 20);
                    maximumRowHeight = i;
                }
            }
 
            for (int i = 0; i < tables.Count; ++i)
            {
                tables[i].ResumeLayout(false);
                tables[i].PerformLayout();
                tables[i].Show();
                if(i == tables.Count-1)
                {
                    tables[i].Anchor = AnchorStyles.Top;
                }
            }
            
            form.ResumeLayout(false);
            form.PerformLayout();
            form.VerticalScroll.Visible = true;
        }
        
        public static int wasTableDeleted = 0; // to know if a table was deleted

        public void deleteTable(Form form, int idx)
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
                    panelContainer.Controls.Remove(tables[i]);
                    tables.RemoveAt(i);
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
            }
            counter--;
            wasTableDeleted = 1;
         //   adjustPositions(form);
            clearCache();
            setSaving(0);            
        }
        public static void clearCache()
        {
            cache.Clear();
        }
        static int savingCount = 0;

        public static void setSaving(int s)
        {           
            saving = s;
            modification = s;

            /* When one save icon and save as icon are also shown - in Form1 original menustrip */
            /*
            if(saving == 0 && savingCount == 0)
            {
                int toolId;
                ToolStripItemCollection tools;
                toolId = menu.Items.IndexOfKey("saveToolStripMenuItem");
                menu.Items.RemoveAt(toolId);

                ToolStripMenuItem saveBlue = new ToolStripMenuItem("");
                saveBlue.Name = "saveToolStripMenuItem";
                saveBlue.ToolTipText = "Save";
                saveBlue.Click +=  new EventHandler(form1.savingMenuItemClicked);
          //      saveBlue.Image = Properties.Resources.saveBlue; // to hide
          //      menu.Items.Add(saveBlue);
                menu.Items.Insert(0, saveBlue);
                savingCount++;
            }
            else if(saving == 1)
            {
                int toolId;
                ToolStripItemCollection tools;
                toolId = menu.Items.IndexOfKey("saveToolStripMenuItem");
                menu.Items.RemoveAt(toolId);

                ToolStripMenuItem saveGray = new ToolStripMenuItem("");
                saveGray.Name = "saveToolStripMenuItem";
                saveGray.Click += new EventHandler(form1.savingMenuItemClicked);
                saveGray.ToolTipText = "Save";
          //      saveGray.Image = Properties.Resources.saveGray; // hide
               // menu.Items.Add(saveGray);
                menu.Items.Insert(0, saveGray);
                savingCount = 0;
            }
            */
        }
        public int getTablesCount()
        {
            return tables.Count;
        }
        public static int getDocOpenings()
        {
            return docOpenings;
        }
        public static int getCacheCount()
        {
            return cache.Count;
        }
        public int getSaving()
        {
            return saving;
        }
        public int getModification()
        {
            return modification;
        }
        public void setModification(int m)
        {
            modification = m;
        }
        public void savingMenuItemClicked(object sender, EventArgs e)
        {
            saveMenuClicked();
        }
        public string savedFileName;
        public string getSavedFileName()
        {
            return savedFileName;
        }
        public void setNewWork(int s) // starting "New Project" set this to 1
        {
            newWork = s;
        }

        public int getSaveRes() // returns if save was successful(1) or not (0)
        {
            return saveRes;
        }
        public void saveMenuClicked()
        {
            if (tables.Count == 0)
            {
                MessageBox.Show("There is nothing to save.\nStart you work now!", "Saving?", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string filename = "";
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Csv files|*.csv";
            if (newWork == 1 || saveAs == 1)
            {
                DialogResult res = sfd.ShowDialog();
                filename = sfd.FileName;

                //var regExp = @"^(?:[\w]\:|\\)(\\[A-zÁ-ú_\-\s0-9]+)+\.(csv)$"; // this does not works
                var regExp = @"^(?:[\w]\:|\\)(\\[\p{L}_\-\s0-9]+)+\.(csv)$";
                Regex regex = new Regex(regExp);
                if (res == DialogResult.OK && filename.EndsWith(".csv") && regex.IsMatch(filename))
                {
                    filename = sfd.FileName;
                    openedFileName = filename;
                    newWork = 0;
                }
                else if(res == DialogResult.Cancel)
                {
                    return;
                }
                else
                {     
                    const string message = "The given name is incorrect.\n Please give a correct name!";
                    const string caption = "Saving failed";
                    MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    saveRes = 0;
                    return;
                }
                if(saveAs == 1)
                {
                    savedAs = 1;
                    saveAs = 0;
                    secondFileName = filename;
                }
            }
            else if(savedAs == 1) // save a new project that has been already saved (do not ask)
            {
                filename = secondFileName;
            }
            else // save loaded file (do not ask)
            {
                filename = openedFileName;
            }

            savedFileName = Path.GetFileName(filename);

            try
            { 
            using (StreamWriter writeText = new StreamWriter(filename))
            {
                for (int i = 0; i < tables.Count; ++i)
                {
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

                foreach (KeyValuePair<string, Dictionary<List<Index>, double>> entry in cache)
                {
                    string idx = "";
                    foreach (char i in entry.Key) 
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
        catch(Exception ex)
        {
            const string message = "The file is propably open.\n Please close the file first!";
            const string caption = "Saving failed";
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            Console.WriteLine("Openening failed: " + ex.Message);
            saveRes = 0;
            return;
        }
         setSaving(1); // save was successful
        }
        /* Form1 original menustrip: */
        /*
            private void evaluateToolStripMenuItem_Click(object sender, EventArgs e)
            {
                getOptimum();
            }

            private void saveToolStripMenuItem_MouseHover(object sender, EventArgs e)
            {
                toolTip1.SetToolTip(menuStrip1,"Save");
            }

            private void SaveAsStripMenuItem1_Click(object sender, EventArgs e)
            {
                saveAs = 1;
                saveMenuClicked();
            }

            private void menuStrip1_MouseDoubleClick(object sender, MouseEventArgs e)
            {
                this.WindowState = FormWindowState.Maximized;
                Console.WriteLine("double cliiiick");
            }

            private void openToolStripMenuItem_Click(object sender, EventArgs e)
            {
                openFile();
            }
        */
        int formres = 0; // Local variable for debug purposes
        private void Form1_Resize(object sender, EventArgs e)
        {
            formres++;
           //   adjustPositions(this.FindForm());
        }

        string onlyFileName = ""; // file name without the path (.extension included)
        public string getOpenedFileName()
        {
            return onlyFileName;
        }
        public int openFile()
        {
            StreamReader reader;
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Csv files|*.csv";
                DialogResult res = ofd.ShowDialog();
                if (res == DialogResult.OK && ofd.FileName.Contains(".csv"))
                {
                    reader = new StreamReader(ofd.FileName);
                    openedFileName = ofd.FileName;
                    newWork = 0;
                    Console.WriteLine("fájl név: " + openedFileName);

                    onlyFileName = Path.GetFileName(openedFileName);
                }
                else if (res == DialogResult.Cancel || res == DialogResult.Abort)
                {
                    return 0;
                }
                else if (res != DialogResult.OK || !ofd.FileName.Contains(".csv"))
                {
                    const string message = "The file can not be opened.\n Please try again!";
                    const string caption = "Opening failed";
                    MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }              
                else
                {
                    const string message = "The file can not be opened.\n Please try again!";
                    const string caption = "Opening failed";
                    MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                const string message = "The file is propably open.\n Please close the file first!";
                const string caption = "Opening failed";
                MessageBox.Show(message, caption);
                Console.WriteLine("Openening failed: " + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }

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
                    Double costWorker, costRobot, costProductValue;
                    costWorker = Double.Parse(strArray[0]);
                    costRobot = Double.Parse(strArray[1]);
                    costProductValue = Double.Parse(strArray[2]);

                    tableData.WorkerCost = costWorker;
                    tableData.RobotCost = costRobot;
                    tableData.ProductValue = costProductValue;

                    Table table = Table.createFromTableData(tableData, counter++, this);
                    //   Controls.Add(table);
                    typeof(Table).InvokeMember("DoubleBuffered",
                        BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                        null, table, new object[] { true });
                    table.Margin = new Padding(20, 10, 0, 3);
                    table.WrapContents = false;
                    table.AutoScroll = false;
                    panelContainer.Controls.Add(table);
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
                        List<Index> indexlist = new List<Index>(); // for the optimum - robot and worker number by product
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
                        for (int i = 0; i < id.Length - 1; i += 3)
                        {
                            Index index = new Index(Int32.Parse(id[i]), Int32.Parse(id[i + 1]), Int32.Parse(id[i + 2]));
                            indexlist.Add(index);
                        }
                        Dictionary<List<Index>, double> optimum = new Dictionary<List<Index>, double>();
                        optimum.Add(indexlist, Double.Parse(strArray[2]));
                        Cache.Add(k, optimum);
                    }
                }
            }
            docOpenings++;
            Console.WriteLine("open");
        //    adjustPositions(this.FindForm());
            reader.Close();
            if (docOpenings > 1) // not the first document opening
            {
                cache.Clear();
                setSaving(0);
            }
            if (docOpenings == 1) // first document opening
            {
                setSaving(1);                
            }
            return 1;
        }

        public void clearEverything() // upon creating a new project or loading an existing one this need to be called to initialize certain variables / arrays
        {
            Console.WriteLine("clear");
            Tables.Clear();
            tables.Clear();
            cache.Clear();
            counter = 0;
            docOpenings = 0;
            saving = 0; // 0 - need to be saved, 1 - already saved
            saveRes = 0; // 0 - save was not successful, 1 - save was succesful
            newWork = 1; // 1 - if its a new work from blank sheet, 0 - file was opened 
            openedFileName = "";
            saveAs = 0; // 1 - if Save As option was selected
            savedAs = 0; // 1 - if Save As option was selected and saved
            secondFileName = ""; // saving after the save as 
        }
    }
}
