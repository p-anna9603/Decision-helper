using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DecisionSupport
{
    public partial class tableExample : Form
    {
        Label addMan = new Label();
        Label addRobot = new Label();

        public tableExample()
        {
            InitializeComponent();
            
        }

        private void tableExample_Load(object sender, EventArgs e)
        {
            /*
         //   table1.Rows.Add();
            table1.Rows[0].HeaderCell.Value = "+";
            table1.Columns[0].HeaderCell.Value = "+";
            //     table1.Rows.RemoveAt(table1.RowCount - 1);

            // Table 2
  
            addMan.Text = "+";
            addRobot.Text = "+";

            addMan.TextAlign = ContentAlignment.MiddleCenter;
            addRobot.TextAlign = ContentAlignment.MiddleCenter;
            addMan.Click += new System.EventHandler(this.addMan_Click);
            addRobot.Click += new System.EventHandler(this.addRobot_Click);

            addRobot.BackColor = Color.Transparent;
            addMan.BackColor = Color.Transparent;
            addRobot.Dock = DockStyle.Fill;
            addMan.Dock = DockStyle.Fill;
            product01.Controls.Add(addMan, product01.ColumnCount - 1, 0);
            product01.Controls.Add(addRobot, 0, product01.RowCount-1);
            int headerRow;
            headerRow = product01.GetColumn(addRobot);
            

            TextBox textBox = new TextBox();

       //     textBox.Margin = new Padding(0);
            textBox.Dock = DockStyle.Fill;
            textBox.BorderStyle = BorderStyle.None;
            textBox.Anchor = AnchorStyles.Top;
            textBox.Anchor = AnchorStyles.Right;
            textBox.Anchor = AnchorStyles.Bottom;
            textBox.Anchor = AnchorStyles.Left;
            product01.Controls.Add(textBox, 1, 1);*/
          //  product01.Controls.Add(textBox, 0, 1);
        }
        /*
        private void table1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
       //     DataGridViewCell cell = table1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if(e.ColumnIndex == table1.Columns.Count-1)
            {
                //        cell.Value = "";
                Console.WriteLine("column is -1");
                table1.Columns[table1.Columns.Count - 1].HeaderCell.Value = "";
                table1.Columns.Add("new", "+");
                table1.Columns[table1.Columns.Count - 1].Width = 60;
            }
        }*/

        private void table1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            /*
            if (e.RowIndex == table1.Rows.Count - 1)
            {
                //        cell.Value = "";
                Console.WriteLine("column is -1");
                table1.Rows[table1.Rows.Count-1].HeaderCell.Value = "";
              //  table1.Rows[table1.Rows.Count-1].HeaderCell.ed
                table1.Rows.Add();
                table1.Rows[table1.Rows.Count - 1].HeaderCell.Value = "+";
                //        table1.Columns[table1.Columns.Count - 1].Width = 60;
            
            }*/
        }

        private void table2_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if(e.Column == 0 || e.Row == 0)
            {
                e.Graphics.FillRectangle(Brushes.LightGray, e.CellBounds);
            }
        }

        private void addMan_Click(object sender, EventArgs e)
        {
            handleClick(true);
        }
        private void addRobot_Click(object sender, EventArgs e)
        {
            handleClick(false);
        }
        private void handleClick(Boolean isColumn)
        {
            /*
            this.FindForm().SuspendLayout();
            product01.SuspendLayout();
            this.SuspendLayout();
            foreach (Control C in this.product01.Controls)
            {
                C.SuspendLayout();
            }
            product01.Visible = false;

            //NumericUpDown numMen02 = new NumericUpDown();
            //NumericUpDown numRobot = new NumericUpDown();
            TextBox textBox = new TextBox(); // for the number
            TextBox textBox2 = new TextBox();
            textBox.BackColor = Color.LightGray;
            textBox2.BackColor = Color.LightGray;
            int isDeleted = 0;


            if (isColumn)
            {
                product01.ColumnCount = product01.ColumnCount + 1;
                Console.WriteLine("uj oszlop");
            }
            else
            {
                product01.RowCount = product01.RowCount + 1;
                Console.WriteLine("uj sor");
            }

            //numMen02.Width = 50;
            //numRobot.Width = 50;
            //numMen02.Maximum = 10000;
            //numRobot.Maximum = 10000;
            //numMen02.KeyPress += numVal_KeyPress;
            //numRobot.KeyPress += numVal_KeyPress;
            //addMan.AutoSize = true;
            //addRobot.AutoSize = true;

            if (isColumn)
            {
                product01.Controls.Add(textBox, product01.ColumnCount - 2, 0);
                product01.Controls.Add(addMan, product01.ColumnCount - 1, 0);
                product01.GetControlFromPosition(product01.ColumnCount - 2, 0).Width = 40;
            //    textBox.ValueChanged += addWorkerNum_ValueChanged;
            //    textBox.MouseWheel += new MouseEventHandler(this.ScrollHandlerFunction);
             //   product01.Add(numMen02);
             //   isDeleted = checkForNum(numMen02, numMens, 1);
            }
            else
            {
                product01.Controls.Add(textBox2, 0, product01.RowCount - 2);
                product01.Controls.Add(addRobot, 0, product01.RowCount - 1);
              //  numRobot.ValueChanged += addRobotNum_ValueChanged;
               // numRobot.MouseWheel += new MouseEventHandler(this.ScrollHandlerFunction);
             //   product01.Add(numRobot);
           //     isDeleted = checkForNum(numRobot, numRobots, 2);
            }

            for (int i = 1; i < (isColumn ? product01.RowCount - 1 : product01.ColumnCount - 1); i++)
            {
                if (isDeleted != 0)
                {
                    break;
                }
                NumericUpDown Man02 = new NumericUpDown();
                Man02.Increment = 1m / SystemInformation.MouseWheelScrollLines;
                Man02.Anchor = (AnchorStyles.Right | AnchorStyles.Left);
                Man02.Maximum = 10000;
                //Man02.ValueChanged += numVal_ValueChaned;
                //Man02.KeyPress += numVal_KeyPress;

                //Man02.AutoSize = true;
                Man02.Width = 50;

                TextBox textBox3 = new TextBox();

                if (isColumn)
                {
                    Console.Write("csicska\n");
                    product01.Controls.Add(textBox3, product01.ColumnCount - 2, i);
                    product01.GetControlFromPosition(product01.ColumnCount - 2, i).Width = 50;
                }
                else
                {
                    Console.Write("icska\n");
                    product01.Controls.Add(textBox3, i, product01.RowCount - 2);
                }
            }

            product01.Visible = true;
            this.FindForm().ResumeLayout(false);
            this.FindForm().PerformLayout();
            product01.ResumeLayout(false);
            product01.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
            foreach (Control C in this.product01.Controls)
            {
                C.PerformLayout();
                C.ResumeLayout(false);
            }
            //Form1.adjustPositions(this.FindForm());

            //Form1.clearCache();
            //Form1.setSaving(0);

            */
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
