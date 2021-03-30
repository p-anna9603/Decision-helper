using System;
using System.Windows.Forms;

namespace DecisionSupport
{
    partial class Form1
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
            BackgroundTemp.Dispose();
            Background.Dispose();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.newProductToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProductToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.commonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.submitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProductMenu2 = new System.Windows.Forms.ToolStripMenuItem();
            this.evaluateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.newProductMenu = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // newProductToolStripMenuItem
            // 
            this.newProductToolStripMenuItem.Name = "newProductToolStripMenuItem";
            this.newProductToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // newProductToolStripMenuItem1
            // 
            this.newProductToolStripMenuItem1.Name = "newProductToolStripMenuItem1";
            this.newProductToolStripMenuItem1.Size = new System.Drawing.Size(32, 19);
            // 
            // commonToolStripMenuItem
            // 
            this.commonToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.submitToolStripMenuItem,
            this.SaveAsStripMenuItem1,
            this.openToolStripMenuItem});
            this.commonToolStripMenuItem.Name = "commonToolStripMenuItem";
            this.commonToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.commonToolStripMenuItem.Text = "File";
            this.commonToolStripMenuItem.Visible = false;
            // 
            // submitToolStripMenuItem
            // 
            this.submitToolStripMenuItem.Name = "submitToolStripMenuItem";
            this.submitToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.submitToolStripMenuItem.Text = "Save";
            // 
            // SaveAsStripMenuItem1
            // 
            this.SaveAsStripMenuItem1.Name = "SaveAsStripMenuItem1";
            this.SaveAsStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.SaveAsStripMenuItem1.Text = "Save As";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.Color.Snow;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.commonToolStripMenuItem,
            this.insertToolStripMenuItem,
            this.evaluateToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(1106, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            this.menuStrip1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.menuStrip1_MouseDoubleClick);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::DecisionSupport.Properties.Resources.saveGray;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(34, 24);
            this.saveToolStripMenuItem.Visible = false;
            this.saveToolStripMenuItem.MouseHover += new System.EventHandler(this.saveToolStripMenuItem_MouseHover);
            // 
            // insertToolStripMenuItem
            // 
            this.insertToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProductMenu2});
            this.insertToolStripMenuItem.Name = "insertToolStripMenuItem";
            this.insertToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.insertToolStripMenuItem.Text = "Insert";
            // 
            // newProductMenu2
            // 
            this.newProductMenu2.Name = "newProductMenu2";
            this.newProductMenu2.Size = new System.Drawing.Size(224, 26);
            this.newProductMenu2.Text = "New product";
            this.newProductMenu2.Click += new System.EventHandler(this.newProductMenu_Click);
            // 
            // evaluateToolStripMenuItem
            // 
            this.evaluateToolStripMenuItem.Name = "evaluateToolStripMenuItem";
            this.evaluateToolStripMenuItem.Size = new System.Drawing.Size(79, 24);
            this.evaluateToolStripMenuItem.Text = "Evaluate";
            this.evaluateToolStripMenuItem.Visible = false;
            this.evaluateToolStripMenuItem.Click += new System.EventHandler(this.evaluateToolStripMenuItem_Click);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // newProductMenu
            // 
            this.newProductMenu.BackColor = System.Drawing.Color.White;
            this.newProductMenu.Font = new System.Drawing.Font("Microsoft Tai Le", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newProductMenu.ForeColor = System.Drawing.Color.MidnightBlue;
            this.newProductMenu.Location = new System.Drawing.Point(15, 12);
            this.newProductMenu.Name = "newProductMenu";
            this.newProductMenu.Size = new System.Drawing.Size(111, 39);
            this.newProductMenu.TabIndex = 10;
            this.newProductMenu.Text = "NEW PRODUCT";
            this.newProductMenu.UseVisualStyleBackColor = false;
            this.newProductMenu.Click += new System.EventHandler(this.newProductMenu_Click);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.DarkRed;
            this.BackgroundImage = global::DecisionSupport.Properties.Resources.backg_1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1106, 679);
            this.Controls.Add(this.newProductMenu);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Decision helper";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.ToolStripMenuItem newProductToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newProductToolStripMenuItem1;
        private ToolStripMenuItem commonToolStripMenuItem;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem submitToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem evaluateToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolTip toolTip1;
        private ToolStripMenuItem SaveAsStripMenuItem1;
        private ToolStripMenuItem insertToolStripMenuItem;
        private ToolStripMenuItem newProductMenu2;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private Button newProductMenu;
    }
}

