using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DecisionSupport
{
    public class Table : FlowLayoutPanel
    {
        TableLayoutPanel product01;
        Button addMan;
        Button addRobot;
        Label label3;
        Label label4;
        Label label5;
        TextBox CostWorker;
        TextBox CostRobot;
        Button deleteButton;
        FlowLayoutPanel bottomContainer;
        FlowLayoutPanel costWorkerContainer;
        FlowLayoutPanel costRobotContainer;
        Splitter splitter1;
        public int idx;

        private void addMan_Click(object sender, EventArgs e)
        {
            handleClick(true);
        }

        private void addRobot_Click(object sender, EventArgs e)
        {
            handleClick(false);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var parms = base.CreateParams;
                parms.Style &= ~0x02000000;  // Turn off WS_CLIPCHILDREN
                return parms;
            }
        }

        public Table(int idx, int x, int y)
        {
            this.idx = idx;
            this.product01 = new System.Windows.Forms.TableLayoutPanel();
            this.addMan = new System.Windows.Forms.Button();
            this.addRobot = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.CostWorker = new System.Windows.Forms.TextBox();
            this.CostRobot = new System.Windows.Forms.TextBox();
            this.deleteButton = new System.Windows.Forms.Button();
            this.bottomContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.costWorkerContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.costRobotContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.product01.SuspendLayout();
            this.SuspendLayout();
            this.bottomContainer.SuspendLayout();
            this.costWorkerContainer.SuspendLayout();
            this.costRobotContainer.SuspendLayout();
            // 
            // product01
            // 
            foreach(Control C in this.product01.Controls)
            {
                C.SuspendLayout();
            }
            //this.product01.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            //| System.Windows.Forms.AnchorStyles.Left)
            //| System.Windows.Forms.AnchorStyles.Right)));
            this.product01.AutoSize = true;
            this.product01.Dock = DockStyle.Top; // space probléma
            this.product01.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.product01.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.product01.ColumnCount = 2;
            this.product01.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.product01.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.product01.Controls.Add(this.addMan, 1, 0);
            this.product01.Controls.Add(this.addRobot, 0, 1);
            this.product01.Controls.Add(this.label3, 0, 0);
            this.product01.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.product01.Location = new System.Drawing.Point(3, 3);
            //this.product01.Name = "product01";
            this.product01.RowCount = 2;
            this.product01.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.product01.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            //this.product01.Size = new System.Drawing.Size(296, 63);
            this.product01.TabIndex = 0;
            // 
            // addMan
            // 
            //this.addMan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addMan.Location = new System.Drawing.Point(55, 4);
            this.addMan.Name = "addMan";
            this.addMan.Size = new System.Drawing.Size(44, 24);
            this.addMan.TabIndex = 0;
            this.addMan.Text = "+";
            this.addMan.UseVisualStyleBackColor = true;
            this.addMan.Click += new System.EventHandler(this.addMan_Click);
            // 
            // addRobot
            // 
            this.addRobot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addRobot.Location = new System.Drawing.Point(4, 35);
            this.addRobot.Name = "addRobot";
            this.addRobot.Size = new System.Drawing.Size(44, 24);
            this.addRobot.TabIndex = 1;
            this.addRobot.Text = "+";
            this.addRobot.UseVisualStyleBackColor = true;
            this.addRobot.Click += new System.EventHandler(this.addRobot_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(4, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 30);
            this.label3.TabIndex = 2;
            this.label3.Text = "Manpower\r\nRobot\r\n";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 18);
            this.label4.TabIndex = 1;
            this.label4.Text = "Cost of one worker";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 18);
            this.label5.TabIndex = 2;
            this.label5.Text = "Cost of one robot";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CostWorker
            // 
            this.CostWorker.Location = new System.Drawing.Point(145, 3);
            this.CostWorker.Name = "CostWorker";
            this.CostWorker.Size = new System.Drawing.Size(110, 22);
            this.CostWorker.TabIndex = 3;
            this.CostWorker.Text = "0";
            // 
            // CostRobot
            // 
            this.CostRobot.Location = new System.Drawing.Point(143, 3);
            this.CostRobot.Name = "CostRobot";
            this.CostRobot.Size = new System.Drawing.Size(110, 24);
            this.CostRobot.TabIndex = 4;
            this.CostRobot.Text = "0";
            // 
            // deleteButton
            // 
            this.deleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.deleteButton.Location = new System.Drawing.Point(3, 90);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 5;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteTable);
            //
            // flowLayoutPanel1 - minden tába + sorok + submit
            // 
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.product01);
            this.Controls.Add(this.bottomContainer);
            this.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.Location = new System.Drawing.Point(28, 150);
            this.Name = "flowLayoutPanel1";
            //this.Size = new System.Drawing.Size(302, 230);
            this.TabIndex = 6;
            // 
            // bottomContainer - két sor + gomb
            // 
            this.bottomContainer.Controls.Add(this.costWorkerContainer); // első sor
            this.bottomContainer.Controls.Add(this.costRobotContainer); // második sor
            this.bottomContainer.Controls.Add(this.deleteButton);
            this.bottomContainer.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.bottomContainer.Location = new System.Drawing.Point(3, 72);
            this.bottomContainer.Name = "flowLayoutPanel2";
            this.bottomContainer.Size = new System.Drawing.Size(296, 155);
            this.bottomContainer.TabIndex = 7;
            // 
            // costWorkerContainer - munkás bér
            // 
            this.costWorkerContainer.Controls.Add(this.label4);
            this.costWorkerContainer.Controls.Add(this.CostWorker);
            this.costWorkerContainer.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.costWorkerContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.costWorkerContainer.Location = new System.Drawing.Point(3, 3);
            this.costWorkerContainer.Name = "flowLayoutPanel3";
            this.costWorkerContainer.Size = new System.Drawing.Size(266, 31);
            this.costWorkerContainer.TabIndex = 7;
            // 
            // costRobotContainer - robot ár
            // 
            this.costRobotContainer.Controls.Add(this.label5);
            this.costRobotContainer.Controls.Add(this.splitter1);
            this.costRobotContainer.Controls.Add(this.CostRobot);
            this.costRobotContainer.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.costRobotContainer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.costRobotContainer.Location = new System.Drawing.Point(3, 40);
            this.costRobotContainer.Name = "flowLayoutPanel4";
            this.costRobotContainer.Size = new System.Drawing.Size(266, 44);
            this.costRobotContainer.TabIndex = 8;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(134, 3);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 24);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            // 
            //// 
            //// 
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            //this.BackColor = System.Drawing.SystemColors.HighlightText;
            //this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            //this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

            this.ClientSize = new System.Drawing.Size(SystemInformation.WorkingArea.Width, SystemInformation.WorkingArea.Height);
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            //Console.WriteLine("Screen: " + SystemInformation.WorkingArea);

            //this.Controls.Add(this.label6);
            //this.Controls.Add(this.menuStrip1);
            //this.Controls.Add(this.flowLayoutPanel1);
            this.DoubleBuffered = true;
            //this.MainMenuStrip = this.menuStrip1;
            //this.Name = "Form1";
            //this.Text = "Decision helper";
            this.product01.ResumeLayout(false);
            this.product01.PerformLayout();
            //this.flowLayoutPanel1.ResumeLayout(false);
            //this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
            
            this.bottomContainer.ResumeLayout(false);
            this.bottomContainer.PerformLayout();

            this.costWorkerContainer.ResumeLayout(false);
            this.costWorkerContainer.PerformLayout();
            this.costRobotContainer.ResumeLayout(false);
            this.costRobotContainer.PerformLayout();
            //this.menuStrip1.ResumeLayout(false);
            //this.menuStrip1.PerformLayout();

            //this.product01.SuspendLayout();
            //this.SuspendLayout();
            //this.bottomContainer.SuspendLayout();
            //this.costWorkerContainer.SuspendLayout();
            //this.costRobotContainer.SuspendLayout();

            addMan.Width = 50;
            addRobot.Width = 50;
            this.Location = new System.Drawing.Point(x, y);

            this.CostRobot.KeyPress += Cost_KeyPress;
            this.CostWorker.KeyPress += Cost_KeyPress;
            this.CostRobot.LostFocus += Cost_FocusLost;
            this.CostWorker.LostFocus += Cost_FocusLost;

            foreach (Control C in this.product01.Controls)
            {
                C.PerformLayout();
                C.ResumeLayout(false);
            }
        }

        private void handleClick(Boolean isColumn)
        {
            this.FindForm().SuspendLayout();
            product01.SuspendLayout();
            this.SuspendLayout();
            foreach (Control C in this.product01.Controls)
            {
                C.SuspendLayout();
            }
            product01.Visible = false;

            NumericUpDown numMen02 = new NumericUpDown();
            numMen02.Anchor = (AnchorStyles.Right | AnchorStyles.Left);
            if (isColumn)
            {
                product01.ColumnCount = product01.ColumnCount + 1;
            }
            else
            {
                product01.RowCount = product01.RowCount + 1;
            }


            //numMen02.AutoSize = true;
            numMen02.Width = 50;

            //addMan.AutoSize = true;
            //addRobot.AutoSize = true;

            if (isColumn)
            {
                product01.Controls.Add(numMen02, product01.ColumnCount - 2, 0);
                product01.Controls.Add(addMan, product01.ColumnCount - 1, 0);

                product01.GetControlFromPosition(product01.ColumnCount - 2, 0).Width = 50;
            }
            else
            {
                product01.Controls.Add(numMen02, 0, product01.RowCount - 2);
                product01.Controls.Add(addRobot, 0, product01.RowCount - 1);
            }

            for (int i = 1; i < (isColumn ? product01.RowCount - 1 : product01.ColumnCount - 1); i++)
            {
                NumericUpDown Man02 = new NumericUpDown();
                Man02.Anchor = (AnchorStyles.Right | AnchorStyles.Left);
                Man02.Maximum = 10000;

                //Man02.AutoSize = true;
                Man02.Width = 50;

                if (isColumn)
                {
                    product01.Controls.Add(Man02, product01.ColumnCount - 2, i);
                    product01.GetControlFromPosition(product01.ColumnCount - 2, i).Width = 50;
                }
                else
                {
                    product01.Controls.Add(Man02, i, product01.RowCount - 2);
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
            Form1.adjustPositions(this.FindForm());
        }

        private void Cost_FocusLost(object sender, EventArgs e)
        {
            TextBox t = sender as TextBox;
            if (t.Text == "")
            {
                t.Text = "0";
            }
        }

        private void Cost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }

            TextBox t = sender as TextBox;

            // only allow one decimal point
            if ((e.KeyChar == '.' || e.KeyChar == ',') && (t.Text.IndexOf('.') > -1 || t.Text.IndexOf(',') > -1))
            {
                e.Handled = true;
                Console.WriteLine("handledtrue");
            }

            int a = t.SelectionStart;
            int b = t.SelectionLength;
            t.Text = t.Text.Replace(".", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            t.Text = t.Text.Replace(",", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);

            t.SelectionStart = a;
            t.SelectionLength = b;
        }

        public CostData getTableData()
        {
            int i, j;
            int numOfRobot, numOfWorker;
            int value;
            double robotCost = 0, workerCost = 0;

            robotCost = Double.Parse(CostRobot.Text);
            workerCost = Double.Parse(CostWorker.Text);
           
            CostData result = new CostData(robotCost, workerCost);

            for (i = 1; i < product01.RowCount - 1; ++i)
            {
                for (j = 1; j < product01.ColumnCount - 1; ++j)
                {
                    Control robot = product01.GetControlFromPosition(0, i);
                    Control worker = product01.GetControlFromPosition(j, 0);
                    Control val = product01.GetControlFromPosition(j, i);

                    numOfRobot = Int32.Parse(robot.Text);
                    numOfWorker = Int32.Parse(worker.Text);
                    value = Int32.Parse(val.Text);

                    result.addToRow(i - 1, numOfRobot, numOfWorker, value);
                }
            }

            return result;
        }

        public void deleteTable(object sender, EventArgs e)
        {
            Form1.deleteTable(this.FindForm(), this.idx);
        }
        //public void submitButtonFun(object sender, EventArgs e)
        //{
        //    Button t = sender as Button;
        //    Form1.submitButton_Click(t);
        //}
    }

}
