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

            ////Console.WriteLine("0. tábla helye" + tables[0].Location + "\n");
            //int maximumRowHeight = 50 + tables[0].Height;
            int maximumRowHeight = 0;

            for (int i = 1; i < tables.Count; ++i)
            {
                System.Drawing.Point prevPos = tables[i - 1].Location;
                ////Console.WriteLine(i - 1 + ". tábla helye " + prevPos);

                //System.Drawing.Point prevPos = form.PointToClient(
                //       tables[i - 1].Parent.PointToScreen(tables[i - 1].Location));

                /* oszlop hozzáadásnál */
                tables[i].Location = new System.Drawing.Point(prevPos.X + tables[i - 1].Width + 20, prevPos.Y);

                /* sor magasság növelés*/
                if ((prevPos.Y + tables[i - 1].Height > tables[maximumRowHeight].Location.Y + tables[maximumRowHeight].Height))
                {
                    //Console.WriteLine("2. magasság");
                    //if (!(tables[i].Location.X + tables[i].Width > form.Width))
                    //{
                    //maximumRowHeight = prevPos.Y + tables[i - 1].Height;
                    maximumRowHeight = i - 1;
                        ////Console.WriteLine("prevPos.Y : " + prevPos.Y);
                        ////Console.WriteLine("tables[i - 1].Height: " + tables[i - 1].Height);
                    //}
                }

                /* ha kilógna a képből új sorban jelenjen meg */
                if ((tables[i].Location.X + tables[i].Width >= form.Width - 70))
                {
                    ////Console.WriteLine("3. új sor");
                    tables[i].Location = new System.Drawing.Point(20, tables[maximumRowHeight].Location.Y + tables[maximumRowHeight].Height + 20);
                    ////Console.WriteLine("maxrowheight: " + maximumRowHeight);
                    maximumRowHeight = i;
                }
                ////Console.WriteLine(i + ". tábla új helye " + tables[i].Location);
            }

            /* ha a submit gombot eléri a tábla csúsztassuk lejjebb */
            //if ((tables[tables.Count - 1].Location.Y + tables[tables.Count - 1].Height) >= submitButton.Location.Y - 20)
            //{
            //}

                //submitButton.Location = new System.Drawing.Point(SystemInformation.WorkingArea.Width - 150, SystemInformation.WorkingArea.Height - 100);
            ////Console.WriteLine("\nWorking area width " + SystemInformation.WorkingArea.Width);
            ////Console.WriteLine("Working area height " + SystemInformation.WorkingArea.Height + "\n");

            for (int i = 0; i < tables.Count; ++i)
            {
                tables[i].ResumeLayout(false);
                tables[i].PerformLayout();
            }
            form.ResumeLayout(false);
            form.PerformLayout();

            ////Console.WriteLine("\nAutoscroll offset: " + form.AutoScrollOffset);
            ////Console.WriteLine("\nAutoscroll poz offset: " + form.AutoScrollPosition);
            ////Console.WriteLine("\nform.VerticalScroll.Value : " + form.VerticalScroll.Value);
        }

        public static void deleteTable(Form form, int idx)
        {
            for (int i = 0; i < tables.Count; ++i)
            {
                if (tables[i].idx == idx)
                {
                    form.Controls.Remove(tables[i]);
                    tables.RemoveAt(i);
                }
            }

            adjustPositions(form);
        }

        public void submitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (StreamWriter writeText = new StreamWriter("output.csv"))
            {
                for (int i = 0; i < tables.Count; ++i)
                {
                    //writeText.WriteLine("product " + i.ToString());

                    TableLayoutPanel table = tables[i].getTable();

                    for (int row = 0; row < table.RowCount-1; ++row)
                    {
                        for (int col = 0; col < table.ColumnCount-1; ++col)
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
            }
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

        //public void submitToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    using (StreamWriter writeText = new StreamWriter("output.csv"))
        //    {
        //        for (int i = 0; i < tables.Count; ++i)
        //        {
        //            writeText.WriteLine("product " + i.ToString());

        //            TableData data = tables[i].getTableData();
        //            for (int row = 0; row < data.getRowCount(); ++row)
        //            {
        //                for (int col = 0; col < data.getColumnCount(); ++col)
        //                {
        //                    writeText.WriteLine(data.getNumOfRobot(row, col) + ";" + data.getNumOfWorker(row, col) + ";" + data.getValue(row, col));
        //                }
        //            }

        //            writeText.WriteLine("*");
        //            writeText.WriteLine(data.RobotCost + ";" + data.WorkerCost);
        //            writeText.WriteLine(data.ProductValue);
        //            writeText.WriteLine();
        //        }
        //    }
        //}
    }
}
