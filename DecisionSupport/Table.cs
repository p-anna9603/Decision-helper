using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DecisionSupport
{
    public class Table : FlowLayoutPanel
    {
        TableLayoutPanel product01;
        IconButton addMan;
        IconButton addRobot;
        //Button addMan;
        //Button addRobot;
        Label robotLabel;
        Label label4;
        Label label5;
        Label productValueLabel;
        Label workerLabel;
        Label productCountLabel;
        TextBox CostWorker;
        TextBox CostRobot;
        TextBox productValue;
        Button deleteButton;
        FlowLayoutPanel manPowerLabelContainer;
        FlowLayoutPanel robotLabelContainer;
        FlowLayoutPanel bottomContainer;
        FlowLayoutPanel costWorkerContainer;
        FlowLayoutPanel costRobotContainer;
        FlowLayoutPanel productValueContainer;
        FlowLayoutPanel robotTableLabelLayout;
        FlowLayoutPanel productLabelLayout;
        FlowLayoutPanel buttomLayoutPanel;
        Panel bottomPanel;
        Splitter robotSplitter;
        Splitter splitter2;
        Splitter workerSplitter;
        Splitter robotLabelsplitter;
        Splitter bottomSplitter;
        public int idx;
        public int prodCnt;
        List<MyNumericUpDown> numMens = new List<MyNumericUpDown>();
        List<MyNumericUpDown> numRobots = new List<MyNumericUpDown>();
      
        
        private void addMan_Click(object sender, EventArgs e)
        {
            
            handleClick(true);
            
        }

        private void addRobot_Click(object sender, EventArgs e)
        {
            handleClick(false);
        }

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        var parms = base.CreateParams;
        //        parms.Style &= ~0x02000000;  // Turn off WS_CLIPCHILDREN
        //        return parms;
        //    }
        //}
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return handleParam;
            }
        }
        public Label ProductCountLabel { get => productCountLabel; set => productCountLabel = value; }
        public TableLayoutPanel Product01 { get => product01; set => product01 = value; }
        public TextBox CostRobot1 { get => CostRobot; set => CostRobot = value; }
        public TextBox ProductValue { get => productValue; set => productValue = value; }

        public void addPlus(bool extendRow = true, bool extendColumn = true)
        {
            if (extendRow)
            {
                this.product01.RowCount = this.product01.RowCount + 1;
            }
            if(extendColumn)
            {
                this.product01.ColumnCount = this.product01.ColumnCount + 1;
            }
            this.product01.Controls.Add(this.addMan, this.product01.ColumnCount-1, 0);
            this.product01.Controls.Add(this.addRobot, 0, this.product01.RowCount - 1);
        }
        public static Table createFromTableData(TableData tableData, int idx, int x, int y)
        {
            Table result = new Table(idx, x, y); // TODO remove xy params
            for(int i = 0; i < tableData.getRowCount(); ++i)
            {
                for(int j = 0; j < tableData.getColumnCount(); ++j)
                {
                    result.addCell(i, j, tableData.get(i, j));
                }
            }
            result.CostRobot.Text = (tableData.RobotCost).ToString();
            result.CostWorker.Text = (tableData.WorkerCost).ToString();
            result.productValue.Text = (tableData.ProductValue).ToString();

            Console.WriteLine("count: " + tableData.getColumnCount());
            result.addPlus(tableData.getRowCount() > 1, tableData.getColumnCount() > 1 && 
                tableData.get(tableData.getRowCount()-1, tableData.getColumnCount()-1) != -1);

            return result;
        }

        public Table(int idx, int x, int y)
        {
            this.idx = idx;
            this.prodCnt = idx + 1;
            this.product01 = new System.Windows.Forms.TableLayoutPanel();
            this.addMan = new FontAwesome.Sharp.IconButton();
            this.addRobot = new FontAwesome.Sharp.IconButton();
            //this.addMan = new System.Windows.Forms.Button();
            //this.addRobot = new System.Windows.Forms.Button();
            this.robotLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.productCountLabel = new System.Windows.Forms.Label();
            this.workerLabel = new System.Windows.Forms.Label();
            this.productValueLabel = new System.Windows.Forms.Label();
            this.CostWorker = new System.Windows.Forms.TextBox();
            this.CostRobot = new System.Windows.Forms.TextBox();
            this.productValue = new System.Windows.Forms.TextBox();
            this.deleteButton = new System.Windows.Forms.Button();
            this.manPowerLabelContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.robotLabelContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.bottomContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.costWorkerContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.costRobotContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.productValueContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.robotTableLabelLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.productLabelLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.buttomLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.robotSplitter = new System.Windows.Forms.Splitter();
            this.workerSplitter = new System.Windows.Forms.Splitter();
            this.robotLabelsplitter = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            bottomSplitter = new Splitter();
            bottomPanel = new Panel();
            this.product01.SuspendLayout();
            this.SuspendLayout();
            this.bottomContainer.SuspendLayout();
            this.costWorkerContainer.SuspendLayout();
            this.costRobotContainer.SuspendLayout();
            
        //    numRobots.RemoveAt(0);
       
            // 
            // product01
            // 
            foreach(Control C in this.product01.Controls)
            {
                C.SuspendLayout();
            }
            //
            // product01 = táblázat maga
            //
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
            //this.product01.Controls.Add(this.addMan, 1, 0);
            //this.product01.Controls.Add(this.addRobot, 0, 1);
            //this.product01.Controls.Add(this.robotLabel, 0, 0);
            this.product01.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.product01.Location = new System.Drawing.Point(3, 6);
            //this.product01.Name = "product01";
            this.product01.RowCount = 2;
            this.product01.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.product01.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            //this.product01.Size = new System.Drawing.Size(296, 63);
      
            //
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(0, 1);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(76, 5);

            this.splitter2.TabStop = false;
            splitter2.BackColor = Color.White;
            // 
            // Manpower label
            //
            this.workerLabel.Text = "MANPOWER";
            this.workerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            //this.workerLabel.Location = new System.Drawing.Point(0, 3);
            this.workerLabel.Size = new System.Drawing.Size(100, 20);
            this.workerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.workerLabel.Location = new System.Drawing.Point(this.splitter2.Location.X + this.splitter2.Width, 3);
            // 
            // Manpower label flowlayout
            //
            this.manPowerLabelContainer.Controls.Add(this.splitter2);
            this.manPowerLabelContainer.Controls.Add(this.workerLabel);
            this.manPowerLabelContainer.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.manPowerLabelContainer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.manPowerLabelContainer.Location = new System.Drawing.Point(0, 5);
            this.manPowerLabelContainer.Size = new System.Drawing.Size(280, 20);
            manPowerLabelContainer.Dock = DockStyle.Top;
            //this.manPowerLabelContainer.AutoSize = true;
            //this.manPowerLabelContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            //
            // productCountLabel
            //
            this.ProductCountLabel.Text = this.prodCnt.ToString() + ". product";
            this.productCountLabel.Font = new Font("Microsoft Sans Serif", 12.0f);
            //this.productCountLabel.Text = this.prodCnt.ToString() + ". product";
            //this.productCountLabel.Size = new System.Drawing.Size(100, 20);
            this.ProductCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ProductCountLabel.BackColor = Color.FromArgb(60, Color.Bisque);
            ProductCountLabel.Dock = DockStyle.Top;
            ProductCountLabel.Height += 10;
            //
            // addMan
            // 
            this.addMan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addMan.Location = new System.Drawing.Point(55, 4);
            this.addMan.Name = "addMan";
       //     addMan.Text = "+";
            addMan.ForeColor = Color.White;
            addMan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));

            this.addMan.Size = new System.Drawing.Size(44, 24);          
            this.addMan.Click += new System.EventHandler(this.addMan_Click);
            this.addMan.BackColor = System.Drawing.Color.DarkBlue;
            this.addMan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addMan.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.addMan.IconColor = System.Drawing.Color.White;
            this.addMan.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.addMan.IconSize = 17;
            this.addMan.Size = new System.Drawing.Size(53, 31);
            this.addMan.UseVisualStyleBackColor = false;
            // 
            // addRobot
            // 
            this.addRobot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addRobot.Location = new System.Drawing.Point(4, 35);
            this.addRobot.Name = "addRobot";
          //  addRobot.Text = "+";
            addRobot.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            addRobot.ForeColor = Color.White;
            addRobot.FlatStyle = FlatStyle.Flat;
            this.addRobot.Size = new System.Drawing.Size(44, 24);
            this.addRobot.Click += new System.EventHandler(this.addRobot_Click);
            this.addRobot.BackColor = System.Drawing.Color.DarkBlue;
            this.addRobot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addRobot.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.addRobot.IconColor = System.Drawing.Color.White;
            this.addRobot.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.addRobot.IconSize = 17;
            this.addRobot.Size = new System.Drawing.Size(53, 31);
            this.addRobot.UseVisualStyleBackColor = false;

            //
            // robotLabelsplitter
            // 
            //    this.robotLabelsplitter.Location = new System.Drawing.Point(0, 1);
            this.robotLabelsplitter.Size = new System.Drawing.Size(0, 0); // (width, height)
       
            this.robotLabelsplitter.TabStop = false;
            //this.robotLabelsplitter.BackColor = Color.Red;
            // 
            // 
            // robotLabel
            // 
            float currentsize;
            this.robotLabel.AutoSize = false;
            this.robotLabel.Name = "robotLabel";
            this.robotLabel.Size = new System.Drawing.Size(16, 86);         
            this.robotLabel.Text = "ROBOT"; 
            robotLabel.TextAlign = ContentAlignment.TopCenter;
            //StringFormat stringf = new StringFormat();
            //stringf.FormatFlags = StringFormatFlags.DirectionVertical;
        //    this.robotLabel.Paint += rotateLabel_Paint;
            // 
            // 
            // robotLabelLayout
            // 
      //      this.robotLabelContainer.Controls.Add(this.robotLabelsplitter);
            this.robotLabelContainer.Controls.Add(this.robotLabel);
            this.robotLabelContainer.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.robotLabelContainer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.robotLabelContainer.Location = new System.Drawing.Point(0, 30);
            this.robotLabelContainer.Size = new System.Drawing.Size(23, 140); // (WIDTH, HEIGHT) height to change!
            robotLabelContainer.MinimumSize = new Size(23, 90);
       //     robotLabelContainer.BorderStyle = BorderStyle.FixedSingle;
            robotLabelContainer.Dock = DockStyle.Right;
            // 
            // workerCost label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
         //   this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 22);
      
            this.label4.Text = "Cost of one worker";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // robotCost label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 22);

            this.label5.Text = "Cost of one robot";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // productValue label
            //
            this.productValueLabel.AutoSize = true;
            this.productValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.productValueLabel.Location = new System.Drawing.Point(3, 3);
            this.productValueLabel.Size = new System.Drawing.Size(138, 22);
            this.productValueLabel.Text = "Value of one product";
            this.productValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CostWorker
            // 
         //   this.CostWorker.Location = new System.Drawing.Point(145, 0);
            this.CostWorker.Name = "CostWorker";
            this.CostWorker.Size = new System.Drawing.Size(110, 22);

            this.CostWorker.Text = "0";
            // 
            // CostRobot
            // 
            this.CostRobot.Location = new System.Drawing.Point(145, 0);
            this.CostRobot.Name = "CostRobot";
            this.CostRobot.Size = new System.Drawing.Size(110, 22);
     
            this.CostRobot.Text = "0";
            //
            // productValue textBox
            //
            this.productValue.Location = new System.Drawing.Point(145, 0);
            this.productValue.Size = new System.Drawing.Size(110, 22);
    
            this.productValue.Text = "0";
            //
            // 
            // deleteButton
            // 
            this.deleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.deleteButton.Location = new System.Drawing.Point(3, 90);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 30);
       
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteTable);
            deleteButton.Dock = DockStyle.Bottom;
            deleteButton.BackColor = Color.DarkBlue;
            deleteButton.ForeColor = Color.White;
            //
            // robotLabelLayout - robot label + table (horizontal)
            //
            this.robotTableLabelLayout.AutoSize = true;
            this.robotTableLabelLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            //this.robotTableLabelLayout.Controls.Add(this.manPowerLabelContainer);
            this.robotTableLabelLayout.Controls.Add(this.robotLabelContainer);
            //this.robotTableLabelLayout.Controls.Add(this.robotLabel);
            this.robotTableLabelLayout.Controls.Add(this.product01);
            this.robotTableLabelLayout.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.robotTableLabelLayout.Location = new System.Drawing.Point(3, 25);
            //
            // productLabelLayout - flowLayoutPanel1 + label of product
            //
            //this.productLabel.Controls.Add(flowLayoutPanel1)
            this.productLabelLayout.AutoSize = true;
            this.productLabelLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            //this.productLabelLayout.Controls.Add(this.productCountLabel);
            this.productLabelLayout.Controls.Add(this.manPowerLabelContainer);
            //this.Controls.Add(this.workerLabel);
            this.productLabelLayout.Controls.Add(this.robotTableLabelLayout);
            this.productLabelLayout.Controls.Add(this.buttomLayoutPanel);
            this.productLabelLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            //this.Location = new System.Drawing.Point(28, 150);
            this.productLabelLayout.Name = "flowLayoutPanel1";
            this.productLabelLayout.Size = new System.Drawing.Size(300, 230);
            //this.BackColor = Color.AntiqueWhite;
            this.productLabelLayout.BackColor = Color.White;

            //
            // flowLayoutPanel1 - minden tába + sorok
            // 
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.Controls.Add(this.productCountLabel);
            this.Controls.Add(this.productLabelLayout);
           this.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            //this.Location = new System.Drawing.Point(28, 150);
            this.Name = "flowLayoutPanel1";
            this.Size = new System.Drawing.Size(300, 230);
            //this.BackColor = Color.AntiqueWhite;
            this.BackColor = Color.Transparent;
            //this.Anchor = AnchorStyles.Top;
            //this.Anchor = AnchorStyles.Bottom;
            //this.Anchor = AnchorStyles.Right;
            //this.Anchor = AnchorStyles.Left;
      //      this.BackColor = Color.SeaShell;
            this.BorderStyle = BorderStyle.Fixed3D;
            //
            // bottomPanel
            //
            //bottomPanel.Controls.Add(bottomContainer);
            bottomPanel.Dock = DockStyle.Left;
            this.bottomPanel.Size = new System.Drawing.Size(40, 155);
            bottomPanel.Width = 30;
       //     this.bottomPanel.Location = new System.Drawing.Point(3, 72);
            bottomPanel.BackColor = Color.Red;
            bottomPanel.Anchor = AnchorStyles.Top;
            bottomPanel.Anchor = AnchorStyles.Left;
            bottomPanel.AutoSize = true;
            //bottomPanel.BackColor = Color.Red;
            this.bottomPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            //
            // bottomSplitter
            //
            this.bottomSplitter.Location = new System.Drawing.Point(3, 72);
            this.bottomSplitter.Size = new System.Drawing.Size(10, 30);
            this.bottomSplitter.TabStop = false;
            //
            // 
            // bottomContainer - két sor + gomb
            // 
            this.bottomContainer.Controls.Add(this.costWorkerContainer); // első sor (worker and its cost)
            this.bottomContainer.Controls.Add(this.costRobotContainer); // második sor (robot and its cost)
            this.bottomContainer.Controls.Add(this.productValueContainer); // második sor (product and its value)
            this.bottomContainer.Controls.Add(this.deleteButton);
            this.bottomContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.bottomContainer.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.bottomContainer.Location = new System.Drawing.Point(3, 72);
            this.bottomContainer.Name = "flowLayoutPanel2";
            this.bottomContainer.Size = new System.Drawing.Size(270, 155);
            //this.bottomContainer.BackColor = Color.Transparent;
   
            bottomContainer.Dock = DockStyle.Right;
            bottomContainer.Anchor = AnchorStyles.Top;
            //bottomContainer.Anchor = AnchorStyles.Right;
            //bottomContainer.Anchor = AnchorStyles.Bottom;
            bottomContainer.Anchor = AnchorStyles.Left;
            bottomContainer.AutoSize = false;

            //
            //buttomLayoutPanel
            //
            buttomLayoutPanel.Controls.Add(bottomSplitter);
            buttomLayoutPanel.Controls.Add(bottomContainer);
            this.buttomLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.buttomLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.buttomLayoutPanel.Location = new System.Drawing.Point(3, 72);
            this.buttomLayoutPanel.Size = new System.Drawing.Size(330, 155);
         //   bottomContainer.Dock = DockStyle.Right;
            buttomLayoutPanel.Anchor = AnchorStyles.Top;
            //buttomLayoutPanel.Anchor = AnchorStyles.Right;
            //buttomLayoutPanel.Anchor = AnchorStyles.Bottom;
            buttomLayoutPanel.Anchor = AnchorStyles.Left;
            buttomLayoutPanel.AutoSize = true;
            //
            // workerSplitter
            // 
            //      this.robotSplitter.Location = new System.Drawing.Point(138, 3);
            this.workerSplitter.Size = new System.Drawing.Size(2, 24);
         
            this.workerSplitter.TabStop = false;
            this.workerSplitter.BackColor = Color.White;
            // 
            //// 
            // 
            // costWorkerContainer - munkás bér
            // 
            this.costWorkerContainer.Controls.Add(this.label4);
            this.costWorkerContainer.Controls.Add(this.workerSplitter);
            this.costWorkerContainer.Controls.Add(this.CostWorker);
            this.costWorkerContainer.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
         //   this.costWorkerContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.costWorkerContainer.Location = new System.Drawing.Point(3, 3);
            this.costWorkerContainer.Name = "flowLayoutPanel3";
            this.costWorkerContainer.Size = new System.Drawing.Size(266, 31); // (width, height)
           
     //       costWorkerContainer.Dock = DockStyle.Bottom;
            Console.WriteLine("label w: " + label4.Width + ", " + label4.Height); // 138
            Console.WriteLine("workerSplitter w: " + workerSplitter.Width + ", " + workerSplitter.Height); //4, 24
            Console.WriteLine("CostWorker w: " + CostWorker.Width + ", " + CostWorker.Height); // 110

            // 
            // costRobotContainer - robot ár
            // 
            this.costRobotContainer.Controls.Add(this.label5);
            this.costRobotContainer.Controls.Add(this.robotSplitter);
            this.costRobotContainer.Controls.Add(this.CostRobot);
            this.costRobotContainer.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            //this.costRobotContainer.Location = new System.Drawing.Point(3, 40);
            this.costRobotContainer.Name = "flowLayoutPanel4";
            this.costRobotContainer.Size = new System.Drawing.Size(266, 31);
       
            //
            // productValueContainer  - termék ára
            //
            this.productValueContainer.Controls.Add(this.productValueLabel);
            this.productValueContainer.Controls.Add(this.productValue);
            this.productValueContainer.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            //this.costRobotContainer.Location = new System.Drawing.Point(3, 75);
            this.productValueContainer.Size = new System.Drawing.Size(266, 31);
  
            // 
            // robotSplitter
            // 
            //this.robotSplitter.Location = new System.Drawing.Point(141, 3);
            this.robotSplitter.Name = "robotSplitter";
            this.robotSplitter.Size = new System.Drawing.Size(13, 24);
            this.robotSplitter.TabStop = false;
            this.robotSplitter.BackColor = Color.White;
            // 
            //// 
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            //this.BackColor = System.Drawing.SystemColors.HighlightText;
            //this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            //this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;



            //manPowerLabelContainer.BackColor = Color.FromArgb(120, Color.White);
            //robotLabelContainer.BackColor = Color.FromArgb(120, Color.White);
            //bottomContainer.BackColor = Color.FromArgb(120, Color.White);
            //costWorkerContainer.BackColor = Color.FromArgb(120, Color.White);
            //costRobotContainer.BackColor = Color.FromArgb(120, Color.White);
            //productValueContainer.BackColor = Color.FromArgb(120, Color.White);
            //robotTableLabelLayout.BackColor = Color.FromArgb(120, Color.White);
            //productLabelLayout.BackColor = Color.FromArgb(120, Color.White);
            //buttomLayoutPanel.BackColor = Color.FromArgb(120, Color.White);
            //bottomPanel.BackColor = Color.FromArgb(120, Color.White);           

            this.ClientSize = new System.Drawing.Size(SystemInformation.WorkingArea.Width, SystemInformation.WorkingArea.Height);
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            ////Console.WriteLine("Screen: " + SystemInformation.WorkingArea);

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

            buttomLayoutPanel.ResumeLayout(false);
            buttomLayoutPanel.PerformLayout();

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
            System.Drawing.Point loc = this.Location;

            loc.X = x;
            if (y != 0)
            {
                loc.Y = y;
            }
            this.Location = loc;

            this.CostRobot.KeyPress += Cost_KeyPress;
            this.CostWorker.KeyPress += Cost_KeyPress;
            this.productValue.KeyPress += Cost_KeyPress;
            this.CostRobot.LostFocus += Cost_FocusLost;
            this.CostWorker.LostFocus += Cost_FocusLost;
            this.productValue.LostFocus += Cost_FocusLost;
           
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

            MyNumericUpDown numMen02 = new MyNumericUpDown();
            MyNumericUpDown numRobot = new MyNumericUpDown();
            int isDeleted = 0;
            //numMen02.Increment = 1m / SystemInformation.MouseWheelScrollLines;
            //numRobot.Increment = 1m / SystemInformation.MouseWheelScrollLines;
            //numMen02.Increment = 1;
            //numRobot.Increment = 1;
            //numMen02.Anchor = (AnchorStyles.Right | AnchorStyles.Left);

            if (isColumn)
            {
                product01.ColumnCount = product01.ColumnCount + 1;
            }
            else
            {
                product01.RowCount = product01.RowCount + 1;
            }

            //numMen02.AutoSize = true;
            numMen02.BorderStyle = BorderStyle.None;
            numRobot.BorderStyle = BorderStyle.None;
            numMen02.TextAlign = HorizontalAlignment.Center;
            numRobot.TextAlign = HorizontalAlignment.Center;
            numMen02.Margin = new Padding(0, 10, 0, 0);
            numRobot.Dock = DockStyle.Fill;
           
            numMen02.Dock = DockStyle.Fill;
          //  numMen02.Margin = new Padding(0, 3, 0, 0);
            numMen02.Width = 50;
            numRobot.Width = 50;
            numMen02.Maximum = 10000;
            numRobot.Maximum = 10000;
            numMen02.KeyPress += numVal_KeyPress;
            numRobot.KeyPress += numVal_KeyPress;
            addMan.Width = 40;
            addMan.Height = 30;
            addRobot.Width = 40;
            addRobot.Height = 30;
            //addMan.AutoSize = true;
            //addRobot.AutoSize = true;

            if (isColumn)
            {

                product01.Controls.Add(numMen02, product01.ColumnCount - 2, 0);
                product01.Controls.Add(addMan, product01.ColumnCount - 1, 0);
                product01.GetControlFromPosition(product01.ColumnCount - 2, 0).Width = 50;
                numMen02.ValueChanged +=  addWorkerNum_ValueChanged;
                numMen02.MouseWheel += new MouseEventHandler(this.ScrollHandlerFunction);
                numMens.Add(numMen02);
                isDeleted = checkForNum(numMen02, numMens, 1);

                if(product01.Width > buttomLayoutPanel.Width)
                {
                    Console.WriteLine("here");
                    int width = bottomSplitter.Width + 30;
                 //   this.bottomPanel.Size = new System.Drawing.Size(width, 155);
                    this.bottomSplitter.Size = new System.Drawing.Size(width, 30);
                }
                
            }
            else
            {
                product01.Controls.Add(numRobot, 0, product01.RowCount - 2);
                product01.Controls.Add(addRobot, 0, product01.RowCount - 1);
                numRobot.ValueChanged += addRobotNum_ValueChanged;
                numRobot.MouseWheel += new MouseEventHandler(this.ScrollHandlerFunction);
                numRobots.Add(numRobot);
                isDeleted = checkForNum(numRobot, numRobots, 2);
            }

            for (int i = 1; i < (isColumn ? product01.RowCount - 1 : product01.ColumnCount - 1); i++)
            {
                if (isDeleted != 0)
                {
                    break;
                }
                MyNumericUpDown Man02 = new MyNumericUpDown();
                Man02.Dock = DockStyle.Fill;
                Man02.BorderStyle = BorderStyle.None;
                Man02.TextAlign = HorizontalAlignment.Center;
                Man02.Increment = 1m / SystemInformation.MouseWheelScrollLines;
                Man02.Anchor = (AnchorStyles.Right | AnchorStyles.Left);
                Man02.Maximum = 10000;
                Man02.ValueChanged += numVal_ValueChaned;
                Man02.KeyPress += numVal_KeyPress;

                //Man02.AutoSize = true;
                Man02.Width = 50;

                if (isColumn)
                {
                    //Console.Write("csicska\n");
                    product01.Controls.Add(Man02, product01.ColumnCount - 2, i);
                    product01.GetControlFromPosition(product01.ColumnCount - 2, i).Width = 50;
                }
                else
                {
                    //Console.Write("icska\n");
                    product01.Controls.Add(Man02, i, product01.RowCount - 2);
                }
            }
            product01.GetControlFromPosition(product01.ColumnCount - 1, 0).Height = 30;
            TableLayoutRowStyleCollection styles = product01.RowStyles;
            foreach (RowStyle style in styles)
            {
                style.SizeType = SizeType.AutoSize;
                //style.Height = 30.0F;
            }
            TableLayoutColumnStyleCollection colStyles = product01.ColumnStyles;
            foreach (ColumnStyle style in colStyles)
            {
                style.SizeType = SizeType.AutoSize;
                //style.Width = 50.0F;
            }
            if (product01.Height >= 120)
            {
                this.robotLabelsplitter.Size = new System.Drawing.Size(16, 27); // (width, height)
                                                                                //   this.robotLabelContainer.Controls.Add(this.robotLabelsplitter);
            }
            if(Form1.getDocOpenings() > 0)
            {
                foreach(Control c in product01.Controls)
                {
                    c.Dock = DockStyle.Fill;

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

            Form1.clearCache();
            Form1.setSaving(0);
            Form1.adjustPositions(this.FindForm());
            arrangeTabOrder();
        }
        private void arrangeTabOrder()
        {
            //  this.productLabelLayout.TabIndex = 6;
            int tabId = 0;
            //foreach(Control c in this.product01.Controls)
            //{
            //    Console.WriteLine("id: " + product01.GetCellPosition(c));
            //    c.TabIndex = tabId;
            //    tabId++;
            //}
            for (int i = 0; i < product01.RowCount - 1; ++i)
            {
                for (int j = 0; j < product01.ColumnCount - 1; ++j)
                {
                    if (product01.GetControlFromPosition(i, j) != null)
                    {
                        
                        product01.GetControlFromPosition(i, j).TabIndex = tabId;
                        tabId++;
                    }
                }
            }
            CostWorker.TabIndex = tabId;
            CostRobot.TabIndex = ++tabId;
            productValue.TabIndex = ++tabId;
            deleteButton.TabIndex = ++tabId;
        }
        // Called when adding a new robot or worker to the table. listID == 1 (numMens); listID == 2 (numRobots)
        private int checkForNum(MyNumericUpDown num, List<MyNumericUpDown> list, int listID)
        {
            int isDeleted = 0;
            int idx = list.IndexOf(num);
            //Console.WriteLine("idx: " + idx);
            try
            {
                if (list.Count == 1)
                {
                    num.Value = 0;
                }
                else
                {
                    //Console.WriteLine(" list[idx-1] " + list[idx - 1].Value);
                    num.Value = list[idx - 1].Value + 1;
                }
            }
            catch(Exception e)
            {
                //Console.WriteLine("{0} exception caught.", e);
                isDeleted = deleteFromTable(num, listID);
                list.Remove(num);
            }
            return isDeleted; // to avoid continuing in handleClick function (that would add more numericUpDowns to table which must not happen)
        }

        // Called when next MyNumericUpDown value upon adding to the table would be greater then the maximum (>10000)
        public int deleteFromTable(MyNumericUpDown num, int listID)
        {
            product01.Controls.Remove(num);
            //Console.WriteLine("table row " + product01.RowCount + ", table columnCount: " + product01.ColumnCount);
            if (listID == 1)
            {
                this.product01.ColumnCount = this.product01.ColumnCount - 1;
            }
            else if(listID == 2)
            {
                this.product01.RowCount = this.product01.RowCount - 1;
            }
            //Console.WriteLine("table row " + product01.RowCount + ", table columnCount: " + product01.ColumnCount);
            //Console.WriteLine("list count (robots): " + numRobots.Count + ", list (numMens): " + numMens.Count);
            Form1.adjustPositions(this.FindForm());
            Form1.setSaving(0);
            Form1.clearCache();
            return 1; // to avoid continuing in handleClick function (that would add more numericUpDowns to table which must not happen)
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

            //Delete the placeholder 0
            if(t.Text == "0" && char.IsDigit(e.KeyChar))
            {
                t.Text = "";
            }

            // only allow one decimal point
            if ((e.KeyChar == '.' || e.KeyChar == ',') && (t.Text.IndexOf('.') > -1 || t.Text.IndexOf(',') > -1))
            {
                e.Handled = true;
                //Console.WriteLine("handledtrue");
            }

            int a = t.SelectionStart;
            int b = t.SelectionLength;
            t.Text = t.Text.Replace(".", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            t.Text = t.Text.Replace(",", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);

            t.SelectionStart = a;
            t.SelectionLength = b;
            Form1.clearCache();
            Form1.setSaving(0);
        }

        private void addWorkerNum_ValueChanged(object sender, EventArgs e)
        {
            //Console.WriteLine("\nkezdeees################");
            MyNumericUpDown t = sender as MyNumericUpDown;
            if(t.Value > t.Maximum)
            {
                numsMaximum(t);
                return;
            }
            int idx = numMens.IndexOf(t);
            int count = 1;
            //Console.WriteLine("darabszam össz : " + numMens.Count);
            //Console.WriteLine("index  " + idx + " value: " + numMens[idx].Value);
            if(numMens[idx].Value == t.Maximum && idx != numMens.Count-1)
            {
                numMens[idx].Value = numMens[idx].Value - (numMens.Count-1) - idx;
                //Console.WriteLine("jelenlegi modosított: " + numMens[idx].Value);
            }
            if (idx != 0)
            {
                decimal prevVal = numMens[idx - 1].Value;

                // Don't let current value smaller then the previous value --> add 1 to the current
                if(prevVal == t.Maximum)
                {
                    numMens[idx - 1].Value = numMens[idx - 1].Value - numMens.Count - (idx - 1);
                }
                if (prevVal >= numMens[idx].Value)
                {
                    //Console.WriteLine("Yes");
                    numMens[idx].Value = prevVal + 1;
                }
            }
            for (int i = idx + 1; i < numMens.Count; i++)
            {
                // Upcoming values can not be lower then current one --> set them to the current + 1
                if (numMens[idx].Value >= numMens[i].Value)
                {
                    numMens[i].Value = numMens[idx].Value + count;
                    //Console.WriteLine("utána a " + i + ".-ik: " + numMens[i].Value);
                    count++;
                }
            }
            Form1.clearCache();
            Form1.setSaving(0);
        }
        private void numVal_ValueChaned(object sender, EventArgs e)
        {
            MyNumericUpDown t = sender as MyNumericUpDown;
            if (t.Value > t.Maximum)
            {
                numsMaximum(t);
            }
            Form1.clearCache();
            Form1.setSaving(0);
        }
        private void numVal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
              e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }
            MyNumericUpDown t = sender as MyNumericUpDown;

            //Delete the placeholder 0
            if (t.Text == "0" && char.IsDigit(e.KeyChar))
            {
                t.Text = "";
            }
            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                e.Handled = true;
            }
            Form1.clearCache();
            Form1.setSaving(0);
        }
        public void numsMaximum(MyNumericUpDown num)
        {
            num.Value = num.Maximum;
        }
        private void addRobotNum_ValueChanged(object sender, EventArgs e)
        {
            MyNumericUpDown t = sender as MyNumericUpDown;
            int idx = numRobots.IndexOf(t);
            int count = 1;
            //Console.WriteLine("index  " + idx + " value: " + numRobots[idx].Value);
            if (numRobots[idx].Value == t.Maximum && idx != numRobots.Count - 1)
            {
                numRobots[idx].Value = numRobots[idx].Value - (numRobots.Count - 1) - idx;
                Console.WriteLine("jelenlegi modosított: " + numRobots[idx].Value);
            }
            if (idx != 0)
            {
                decimal prevVal = numRobots[idx - 1].Value;
                if (prevVal == t.Maximum)
                {
                    numRobots[idx - 1].Value = numRobots[idx - 1].Value - numRobots.Count - (idx - 1);
                }
                if (prevVal >= numRobots[idx].Value)
                {
                    //Console.WriteLine("Yes");
                    numRobots[idx].Value = prevVal + 1;
                }
            }
            for (int i = idx+1; i < numRobots.Count; i++)
            {
                if (numRobots[idx].Value >= numRobots[i].Value)
                {
                    numRobots[i].Value = numRobots[idx].Value + count;
                    count++;
                }
            }
            Form1.clearCache();
            Form1.setSaving(0);
        }
      
        private void rotateLabel_Paint(object sender, PaintEventArgs e)
        {
            Font myFont = new Font("Arial", 12);
            Brush mybrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            e.Graphics.TranslateTransform(30, 20);
            e.Graphics.RotateTransform(90);
            e.Graphics.DrawString("Robot", myFont, mybrush, 0, 0);
        }

        //Fix mousewheel scrolling issue:
        private void ScrollHandlerFunction(object sender, MouseEventArgs e)
        {
            MyNumericUpDown t = sender as MyNumericUpDown;
            HandledMouseEventArgs handledArgs = e as HandledMouseEventArgs;
            handledArgs.Handled = true;
            t.Value += (handledArgs.Delta > 0) ? 1 : t.Value == 0 ? 0 : -1;
        }
        public TableLayoutPanel getTable()
        {
            return product01;
        }

        public Double getCostRobotValue()
        {
            return Double.Parse(CostRobot.Text);
        }

        public Double getCostWorkerValue()
        {
            return Double.Parse(CostWorker.Text);
        }

        public Double getCostProductValue()
        {
            return Double.Parse(productValue.Text);
        }

        //public TableData getTableData()
        //{
        //    //int i, j;
        //    //int numOfRobot, numOfWorker;
        //    //int value;
        //    //double robotCost = 0, workerCost = 0, productVal = 0;

        //    //robotCost = Double.Parse(CostRobot.Text);
        //    //workerCost = Double.Parse(CostWorker.Text);
        //    //productVal = Double.Parse(productValue.Text);

        //    //TableData result = new TableData(robotCost, workerCost, productVal);

        //    //for (i = 1; i < product01.RowCount - 1; ++i)
        //    //{
        //    //    for (j = 1; j < product01.ColumnCount - 1; ++j)
        //    //    {
        //    //        Control robot = product01.GetControlFromPosition(0, i);
        //    //        Control worker = product01.GetControlFromPosition(j, 0);
        //    //        Control val = product01.GetControlFromPosition(j, i);

        //    //        numOfRobot = Int32.Parse(robot.Text);
        //    //        numOfWorker = Int32.Parse(worker.Text);
        //    //        value = Int32.Parse(val.Text);

        //    //        result.addToRow(i - 1, numOfRobot, numOfWorker, value);
        //    //    }
        //    //}

        //    //return result;
        //}

        public void deleteTable(object sender, EventArgs e)
        {
            Form1.deleteTable(this.FindForm(), this.idx);
        }
        //public void submitButtonFun(object sender, EventArgs e)
        //{
        //    Button t = sender as Button;
        //    Form1.submitButton_Click(t);
        //}

        public void addCell(int row, int col, int val)
        {
            //Console.WriteLine("row: " + row + ", col: " + col + "\n");
            if (col > product01.ColumnCount-1)
            {
                product01.ColumnCount = product01.ColumnCount + 1;
            }
            else if (row > product01.RowCount-1)
            {
                //Console.WriteLine("rowban\n");
                product01.RowCount = product01.RowCount + 1;
            }

            if(val == -1)
            {
                return;
            }
            MyNumericUpDown numMen02 = new MyNumericUpDown();
            numMen02.BorderStyle = BorderStyle.None;
            numMen02.Dock = DockStyle.Fill;
            numMen02.TextAlign = HorizontalAlignment.Center;
            numMen02.Width = 50;
            numMen02.Maximum = 10000;
            if(row == 0 || col == 0)
            {
                numMen02.Margin = new Padding(0, 8, 0, 0);
            }


            if (row == 0)
            {
                numMen02.ValueChanged += addWorkerNum_ValueChanged;
                numMens.Add(numMen02);
            }

            if (col == 0)
            {
                numMen02.ValueChanged += addRobotNum_ValueChanged;
                numRobots.Add(numMen02);
            }
            numMen02.KeyPress += numVal_KeyPress;
            product01.Controls.Add(numMen02, col, row);
            numMen02.MouseWheel += new MouseEventHandler(this.ScrollHandlerFunction);
            numMen02.Value = val;
        }
    }

}
