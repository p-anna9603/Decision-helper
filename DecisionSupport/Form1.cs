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
            submitButton = new System.Windows.Forms.Button();
            // submitButton
            //
            submitButton.Location = new System.Drawing.Point(SystemInformation.WorkingArea.Width-150, SystemInformation.WorkingArea.Height-100);
            submitButton.Name = "submit0";
            submitButton.Size = new System.Drawing.Size(84, 33);
            submitButton.TabIndex = 8;
            submitButton.Text = "Submit";
            submitButton.UseVisualStyleBackColor = true;
            submitButton.Click += new System.EventHandler(this.submit0_Click);
            //
            this.Controls.Add(submitButton);
        }

        private void newProductMenu_Click(object sender, EventArgs e)
        {
            if (tables.Count != 0)
            {
                System.Drawing.Point prevPos = this.FindForm().PointToClient(
                        tables[tables.Count - 1].Parent.PointToScreen(tables[tables.Count - 1].Location));

                Table table = new Table(counter++, prevPos.X + tables[tables.Count - 1].Width + 20, prevPos.Y);
                Controls.Add(table);

                tables.Add(table);
            }
            else
            {
                Table table = new Table(counter++, 20,50);
                Controls.Add(table);

                tables.Add(table);
            }

            adjustPositions(this.FindForm());
        }

        public static void adjustPositions(Form form)
        {
            if (tables.Count == 0)
            {
                return;
            }

            form.SuspendLayout();
            for(int i = 0; i <tables.Count; ++i)
            {
                tables[i].SuspendLayout();
            }

            tables[0].Location = new System.Drawing.Point(20, 50); // első tábla törlése esetén 
            Console.WriteLine("0. tábla helye" + tables[0].Location + "\n");
            int currentRowPos = 50 + tables[0].Height;

            for (int i = 1; i < tables.Count; ++i)
            {
                tables[i].SuspendLayout();
                System.Drawing.Point prevPos = tables[i - 1].Location;
                Console.WriteLine(i-1 + ". tábla helye " + prevPos);

                //System.Drawing.Point prevPos = form.PointToClient(
                //       tables[i - 1].Parent.PointToScreen(tables[i - 1].Location));

                /* oszlop hozzáadásnál */
                tables[i].Location = new System.Drawing.Point(prevPos.X + tables[i - 1].Width + 20, prevPos.Y);

                /* sor magasság növelés*/
                if (prevPos.Y + tables[i].Height > currentRowPos)
                {
                    Console.WriteLine("2. magasság");
                    currentRowPos = prevPos.Y + tables[i].Height;
                }

                /* ha kilógna a képből új sorban jelenjen meg */
                if (tables[i].Location.X + tables[i].Width > form.Width)
                {
                    Console.WriteLine("3. új sor");
                    tables[i].Location = new System.Drawing.Point(20, currentRowPos + 20);
                    currentRowPos = 0;
                }
                Console.WriteLine(i + ". tábla új helye " + tables[i].Location);
                tables[i].ResumeLayout(false);
                tables[i].PerformLayout();
            }

            /* ha a submit gombot eléri a tábla csúsztassuk lejjebb */
            if((tables[tables.Count-1].Location.Y + tables[tables.Count-1].Height) >= submitButton.Location.Y - 20)
            {
                submitButton.SuspendLayout();
                Console.WriteLine("4. submitba lóg");
                submitButton.Location = new System.Drawing.Point(SystemInformation.WorkingArea.Width - 150, tables[tables.Count - 1].Location.Y + tables[tables.Count - 1].Height + 10);
            }
            for (int i = 0; i < tables.Count; ++i)
            {
                tables[i].ResumeLayout(false);
                tables[i].PerformLayout();
            }
            submitButton.ResumeLayout(false);
            submitButton.PerformLayout();
            form.ResumeLayout(false);
            form.PerformLayout();
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

        public void submit0_Click(object sender, EventArgs e)
        {
            using (StreamWriter writeText = new StreamWriter("output.csv"))
            { 
                for (int i = 0; i < tables.Count; ++i)
                {
                    writeText.WriteLine("product " + i.ToString());

                    CostData data = tables[i].getTableData();
                    for (int row = 0; row < data.getRowCount(); ++row)
                    {
                        for (int col = 0; col < data.getColumnCount(); ++col)
                        {
                            writeText.WriteLine(data.getNumOfRobot(row, col) + ";" + data.getNumOfWorker(row, col) + ";" + data.getValue(row, col));
                        }
                    }

                    writeText.WriteLine("*");
                    writeText.WriteLine(data.RobotCost + ";" + data.WorkerCost);
                    writeText.WriteLine();
                }
            }
        }
    }
}
