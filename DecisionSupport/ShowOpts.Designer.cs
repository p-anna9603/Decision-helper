namespace DecisionSupport
{
    partial class ShowOpts
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowOpts));
            this.CalculateButton = new System.Windows.Forms.Button();
            this.maxOperator = new System.Windows.Forms.TextBox();
            this.maxRobot = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.optionsTable = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.opSlide = new System.Windows.Forms.Label();
            this.robSlide = new System.Windows.Forms.Label();
            this.coverLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optionsTable)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CalculateButton
            // 
            this.CalculateButton.BackColor = System.Drawing.Color.Honeydew;
            this.CalculateButton.Location = new System.Drawing.Point(849, 183);
            this.CalculateButton.Name = "CalculateButton";
            this.CalculateButton.Size = new System.Drawing.Size(88, 33);
            this.CalculateButton.TabIndex = 16;
            this.CalculateButton.Text = "Calculate";
            this.CalculateButton.UseVisualStyleBackColor = false;
            this.CalculateButton.Click += new System.EventHandler(this.CalculateButtonClick);
            // 
            // maxOperator
            // 
            this.maxOperator.Location = new System.Drawing.Point(872, 69);
            this.maxOperator.Name = "maxOperator";
            this.maxOperator.Size = new System.Drawing.Size(68, 22);
            this.maxOperator.TabIndex = 15;
            this.maxOperator.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.maxOperator_KeyPress);
            // 
            // maxRobot
            // 
            this.maxRobot.Location = new System.Drawing.Point(872, 33);
            this.maxRobot.Name = "maxRobot";
            this.maxRobot.Size = new System.Drawing.Size(68, 22);
            this.maxRobot.TabIndex = 14;
            this.maxRobot.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.maxRobot_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(767, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "Max robot";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(767, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Max operator";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::DecisionSupport.Properties.Resources.worker;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(43, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(101, 91);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(297, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 29);
            this.label1.TabIndex = 10;
            this.label1.Text = "Production distribution";
            // 
            // optionsTable
            // 
            this.optionsTable.AllowUserToAddRows = false;
            this.optionsTable.AllowUserToDeleteRows = false;
            this.optionsTable.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.optionsTable.ColumnHeadersHeight = 40;
            this.optionsTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.optionsTable.Location = new System.Drawing.Point(43, 243);
            this.optionsTable.MinimumSize = new System.Drawing.Size(890, 500);
            this.optionsTable.Name = "optionsTable";
            this.optionsTable.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.optionsTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.optionsTable.RowHeadersWidth = 51;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.optionsTable.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.optionsTable.RowTemplate.Height = 24;
            this.optionsTable.Size = new System.Drawing.Size(894, 506);
            this.optionsTable.TabIndex = 17;
            this.optionsTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.cellClick);
            this.optionsTable.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.optionCellClick);
            this.optionsTable.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.optionsTable_RowsAdded);
            this.optionsTable.Paint += new System.Windows.Forms.PaintEventHandler(this.optionsTable_Paint);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 125;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox1.Location = new System.Drawing.Point(963, 616);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(215, 133);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Legend";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(49, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(134, 60);
            this.label7.TabIndex = 3;
            this.label7.Text = "Optimum with better resources";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.LawnGreen;
            this.label6.Location = new System.Drawing.Point(7, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 23);
            this.label6.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Yellow;
            this.label5.Location = new System.Drawing.Point(7, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 23);
            this.label5.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Currently queried";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(49, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 17);
            this.label8.TabIndex = 19;
            this.label8.Text = "Operator";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(49, 68);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 17);
            this.label9.TabIndex = 20;
            this.label9.Text = "Robot";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(299, 74);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(194, 17);
            this.label10.TabIndex = 21;
            this.label10.Text = "Choose the interval (optional)";
            // 
            // trackBar1
            // 
            this.trackBar1.BackColor = System.Drawing.SystemColors.Control;
            this.trackBar1.Location = new System.Drawing.Point(152, 9);
            this.trackBar1.Maximum = 30;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(178, 56);
            this.trackBar1.TabIndex = 22;
            this.trackBar1.Value = 5;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(152, 68);
            this.trackBar2.Maximum = 30;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(178, 56);
            this.trackBar2.TabIndex = 23;
            this.trackBar2.Value = 5;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // opSlide
            // 
            this.opSlide.AutoSize = true;
            this.opSlide.BackColor = System.Drawing.Color.Transparent;
            this.opSlide.Location = new System.Drawing.Point(336, 9);
            this.opSlide.Name = "opSlide";
            this.opSlide.Size = new System.Drawing.Size(16, 17);
            this.opSlide.TabIndex = 24;
            this.opSlide.Text = "5";
            // 
            // robSlide
            // 
            this.robSlide.AutoSize = true;
            this.robSlide.BackColor = System.Drawing.Color.Transparent;
            this.robSlide.Location = new System.Drawing.Point(336, 78);
            this.robSlide.Name = "robSlide";
            this.robSlide.Size = new System.Drawing.Size(16, 17);
            this.robSlide.TabIndex = 25;
            this.robSlide.Text = "5";
            // 
            // coverLabel
            // 
            this.coverLabel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.coverLabel.Font = new System.Drawing.Font("Monotype Corsiva", 22.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.coverLabel.ForeColor = System.Drawing.Color.SteelBlue;
            this.coverLabel.Location = new System.Drawing.Point(43, 243);
            this.coverLabel.Name = "coverLabel";
            this.coverLabel.Size = new System.Drawing.Size(897, 506);
            this.coverLabel.TabIndex = 26;
            this.coverLabel.Text = "Data output";
            this.coverLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.trackBar1);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.robSlide);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.opSlide);
            this.panel1.Controls.Add(this.trackBar2);
            this.panel1.Location = new System.Drawing.Point(226, 105);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(473, 132);
            this.panel1.TabIndex = 27;
            // 
            // ShowOpts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1190, 777);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.coverLabel);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.optionsTable);
            this.Controls.Add(this.CalculateButton);
            this.Controls.Add(this.maxOperator);
            this.Controls.Add(this.maxRobot);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ShowOpts";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Optimum Solution";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ShowOpts_FormClosed);
            this.Load += new System.EventHandler(this.ShowOpts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optionsTable)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CalculateButton;
        private System.Windows.Forms.TextBox maxOperator;
        private System.Windows.Forms.TextBox maxRobot;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView optionsTable;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label opSlide;
        private System.Windows.Forms.Label robSlide;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Label coverLabel;
        private System.Windows.Forms.Panel panel1;
    }
}