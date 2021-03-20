using System.Windows.Forms;

namespace DecisionSupport
{
    partial class firstForm
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
            this.panelMenu = new System.Windows.Forms.Panel();
            this.exitButton = new FontAwesome.Sharp.IconButton();
            this.evaluateButton = new FontAwesome.Sharp.IconButton();
            this.saveButton = new FontAwesome.Sharp.IconButton();
            this.projectSubMenu = new System.Windows.Forms.Panel();
            this.LoadProjIcon = new FontAwesome.Sharp.IconButton();
            this.newProjIcon = new FontAwesome.Sharp.IconButton();
            this.projectButton = new FontAwesome.Sharp.IconButton();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.btnHome = new FontAwesome.Sharp.IconPictureBox();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.restoreButton = new FontAwesome.Sharp.IconButton();
            this.iconButton6 = new FontAwesome.Sharp.IconButton();
            this.iconButton5 = new FontAwesome.Sharp.IconButton();
            this.minimizeBtn = new FontAwesome.Sharp.IconButton();
            this.lblTitleChildForm = new System.Windows.Forms.Label();
            this.iconCurrentChildForm = new FontAwesome.Sharp.IconPictureBox();
            this.panelDesktop2 = new System.Windows.Forms.Panel();
            this.panelDesktop = new System.Windows.Forms.Panel();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.iconPictureBox2 = new FontAwesome.Sharp.IconPictureBox();
            this.iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            this.panelMenu.SuspendLayout();
            this.projectSubMenu.SuspendLayout();
            this.panelLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnHome)).BeginInit();
            this.panelTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconCurrentChildForm)).BeginInit();
            this.panelDesktop2.SuspendLayout();
            this.panelDesktop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.AutoScroll = true;
            this.panelMenu.BackColor = System.Drawing.Color.DarkRed;
            this.panelMenu.Controls.Add(this.exitButton);
            this.panelMenu.Controls.Add(this.evaluateButton);
            this.panelMenu.Controls.Add(this.saveButton);
            this.panelMenu.Controls.Add(this.projectSubMenu);
            this.panelMenu.Controls.Add(this.projectButton);
            this.panelMenu.Controls.Add(this.panelLogo);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(220, 771);
            this.panelMenu.TabIndex = 9;
            // 
            // exitButton
            // 
            this.exitButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.exitButton.FlatAppearance.BorderSize = 0;
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.exitButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.exitButton.IconChar = FontAwesome.Sharp.IconChar.SignOutAlt;
            this.exitButton.IconColor = System.Drawing.Color.White;
            this.exitButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.exitButton.IconSize = 32;
            this.exitButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.exitButton.Location = new System.Drawing.Point(0, 417);
            this.exitButton.Name = "exitButton";
            this.exitButton.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.exitButton.Size = new System.Drawing.Size(220, 60);
            this.exitButton.TabIndex = 8;
            this.exitButton.Text = "Exit";
            this.exitButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.exitButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // evaluateButton
            // 
            this.evaluateButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.evaluateButton.FlatAppearance.BorderSize = 0;
            this.evaluateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.evaluateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.evaluateButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.evaluateButton.IconChar = FontAwesome.Sharp.IconChar.Calculator;
            this.evaluateButton.IconColor = System.Drawing.Color.White;
            this.evaluateButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.evaluateButton.IconSize = 32;
            this.evaluateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.evaluateButton.Location = new System.Drawing.Point(0, 357);
            this.evaluateButton.Name = "evaluateButton";
            this.evaluateButton.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.evaluateButton.Size = new System.Drawing.Size(220, 60);
            this.evaluateButton.TabIndex = 7;
            this.evaluateButton.Text = "Evaluation";
            this.evaluateButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.evaluateButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.evaluateButton.UseVisualStyleBackColor = true;
            this.evaluateButton.Click += new System.EventHandler(this.evaluateButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.saveButton.FlatAppearance.BorderSize = 0;
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.saveButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.saveButton.IconChar = FontAwesome.Sharp.IconChar.Save;
            this.saveButton.IconColor = System.Drawing.Color.White;
            this.saveButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.saveButton.IconSize = 32;
            this.saveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveButton.Location = new System.Drawing.Point(0, 297);
            this.saveButton.Name = "saveButton";
            this.saveButton.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.saveButton.Size = new System.Drawing.Size(220, 60);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save work";
            this.saveButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // projectSubMenu
            // 
            this.projectSubMenu.Controls.Add(this.LoadProjIcon);
            this.projectSubMenu.Controls.Add(this.newProjIcon);
            this.projectSubMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.projectSubMenu.Location = new System.Drawing.Point(0, 200);
            this.projectSubMenu.Name = "projectSubMenu";
            this.projectSubMenu.Size = new System.Drawing.Size(220, 97);
            this.projectSubMenu.TabIndex = 5;
            // 
            // LoadProjIcon
            // 
            this.LoadProjIcon.Dock = System.Windows.Forms.DockStyle.Top;
            this.LoadProjIcon.FlatAppearance.BorderSize = 0;
            this.LoadProjIcon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoadProjIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LoadProjIcon.ForeColor = System.Drawing.Color.Gainsboro;
            this.LoadProjIcon.IconChar = FontAwesome.Sharp.IconChar.FolderOpen;
            this.LoadProjIcon.IconColor = System.Drawing.Color.Gainsboro;
            this.LoadProjIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.LoadProjIcon.IconSize = 30;
            this.LoadProjIcon.Location = new System.Drawing.Point(0, 40);
            this.LoadProjIcon.Name = "LoadProjIcon";
            this.LoadProjIcon.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.LoadProjIcon.Size = new System.Drawing.Size(220, 40);
            this.LoadProjIcon.TabIndex = 1;
            this.LoadProjIcon.Text = "Load Project";
            this.LoadProjIcon.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.LoadProjIcon.UseVisualStyleBackColor = true;
            this.LoadProjIcon.Click += new System.EventHandler(this.LoadProjIcon_Click);
            // 
            // newProjIcon
            // 
            this.newProjIcon.Dock = System.Windows.Forms.DockStyle.Top;
            this.newProjIcon.FlatAppearance.BorderSize = 0;
            this.newProjIcon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newProjIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.newProjIcon.ForeColor = System.Drawing.Color.Gainsboro;
            this.newProjIcon.IconChar = FontAwesome.Sharp.IconChar.File;
            this.newProjIcon.IconColor = System.Drawing.Color.Gainsboro;
            this.newProjIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.newProjIcon.IconSize = 30;
            this.newProjIcon.Location = new System.Drawing.Point(0, 0);
            this.newProjIcon.Name = "newProjIcon";
            this.newProjIcon.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.newProjIcon.Size = new System.Drawing.Size(220, 40);
            this.newProjIcon.TabIndex = 0;
            this.newProjIcon.Text = "New Project";
            this.newProjIcon.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.newProjIcon.UseVisualStyleBackColor = true;
            this.newProjIcon.Click += new System.EventHandler(this.newProjIcon_Click);
            // 
            // projectButton
            // 
            this.projectButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.projectButton.FlatAppearance.BorderSize = 0;
            this.projectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.projectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.projectButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.projectButton.IconChar = FontAwesome.Sharp.IconChar.Tasks;
            this.projectButton.IconColor = System.Drawing.Color.White;
            this.projectButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.projectButton.IconSize = 32;
            this.projectButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.projectButton.Location = new System.Drawing.Point(0, 140);
            this.projectButton.Name = "projectButton";
            this.projectButton.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.projectButton.Size = new System.Drawing.Size(220, 60);
            this.projectButton.TabIndex = 1;
            this.projectButton.Text = "Project";
            this.projectButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.projectButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.projectButton.UseVisualStyleBackColor = true;
            this.projectButton.Click += new System.EventHandler(this.projectButton_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.Controls.Add(this.btnHome);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.panelLogo.Size = new System.Drawing.Size(220, 140);
            this.panelLogo.TabIndex = 0;
            // 
            // btnHome
            // 
            this.btnHome.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btnHome.BackgroundImage = global::DecisionSupport.Properties.Resources.logo1;
            this.btnHome.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHome.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.btnHome.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnHome.IconColor = System.Drawing.SystemColors.ControlDark;
            this.btnHome.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnHome.IconSize = 88;
            this.btnHome.Location = new System.Drawing.Point(13, 19);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(185, 88);
            this.btnHome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnHome.TabIndex = 0;
            this.btnHome.TabStop = false;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.DarkRed;
            this.panelTitleBar.Controls.Add(this.restoreButton);
            this.panelTitleBar.Controls.Add(this.iconButton6);
            this.panelTitleBar.Controls.Add(this.iconButton5);
            this.panelTitleBar.Controls.Add(this.minimizeBtn);
            this.panelTitleBar.Controls.Add(this.lblTitleChildForm);
            this.panelTitleBar.Controls.Add(this.iconCurrentChildForm);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(220, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(963, 75);
            this.panelTitleBar.TabIndex = 10;
            this.panelTitleBar.DragDrop += new System.Windows.Forms.DragEventHandler(this.panelTitleBar_DragDrop);
            this.panelTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTitleBar_MouseDown);
            // 
            // restoreButton
            // 
            this.restoreButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.restoreButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.restoreButton.ForeColor = System.Drawing.Color.DarkRed;
            this.restoreButton.IconChar = FontAwesome.Sharp.IconChar.WindowRestore;
            this.restoreButton.IconColor = System.Drawing.Color.White;
            this.restoreButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.restoreButton.IconSize = 30;
            this.restoreButton.Location = new System.Drawing.Point(880, 10);
            this.restoreButton.Name = "restoreButton";
            this.restoreButton.Size = new System.Drawing.Size(38, 33);
            this.restoreButton.TabIndex = 6;
            this.restoreButton.UseVisualStyleBackColor = true;
            this.restoreButton.Click += new System.EventHandler(this.restoreButton_Click);
            // 
            // iconButton6
            // 
            this.iconButton6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconButton6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton6.ForeColor = System.Drawing.Color.DarkRed;
            this.iconButton6.IconChar = FontAwesome.Sharp.IconChar.Times;
            this.iconButton6.IconColor = System.Drawing.Color.White;
            this.iconButton6.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton6.IconSize = 30;
            this.iconButton6.Location = new System.Drawing.Point(915, 9);
            this.iconButton6.Name = "iconButton6";
            this.iconButton6.Size = new System.Drawing.Size(36, 35);
            this.iconButton6.TabIndex = 5;
            this.iconButton6.UseVisualStyleBackColor = true;
            this.iconButton6.Click += new System.EventHandler(this.iconButton6_Click);
            // 
            // iconButton5
            // 
            this.iconButton5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconButton5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton5.ForeColor = System.Drawing.Color.DarkRed;
            this.iconButton5.IconChar = FontAwesome.Sharp.IconChar.WindowMaximize;
            this.iconButton5.IconColor = System.Drawing.Color.White;
            this.iconButton5.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton5.IconSize = 30;
            this.iconButton5.Location = new System.Drawing.Point(879, 6);
            this.iconButton5.Name = "iconButton5";
            this.iconButton5.Size = new System.Drawing.Size(38, 33);
            this.iconButton5.TabIndex = 4;
            this.iconButton5.Text = "iconButton5";
            this.iconButton5.UseVisualStyleBackColor = true;
            this.iconButton5.Visible = false;
            this.iconButton5.Click += new System.EventHandler(this.iconButton5_Click);
            // 
            // minimizeBtn
            // 
            this.minimizeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimizeBtn.BackColor = System.Drawing.Color.DarkRed;
            this.minimizeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimizeBtn.ForeColor = System.Drawing.Color.DarkRed;
            this.minimizeBtn.IconChar = FontAwesome.Sharp.IconChar.WindowMinimize;
            this.minimizeBtn.IconColor = System.Drawing.Color.White;
            this.minimizeBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.minimizeBtn.IconSize = 30;
            this.minimizeBtn.Location = new System.Drawing.Point(846, 9);
            this.minimizeBtn.Margin = new System.Windows.Forms.Padding(0);
            this.minimizeBtn.Name = "minimizeBtn";
            this.minimizeBtn.Size = new System.Drawing.Size(35, 26);
            this.minimizeBtn.TabIndex = 3;
            this.minimizeBtn.UseVisualStyleBackColor = false;
            this.minimizeBtn.Click += new System.EventHandler(this.minimizeBtn_Click);
            // 
            // lblTitleChildForm
            // 
            this.lblTitleChildForm.AutoSize = true;
            this.lblTitleChildForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblTitleChildForm.ForeColor = System.Drawing.Color.SeaShell;
            this.lblTitleChildForm.Location = new System.Drawing.Point(54, 24);
            this.lblTitleChildForm.Name = "lblTitleChildForm";
            this.lblTitleChildForm.Size = new System.Drawing.Size(54, 20);
            this.lblTitleChildForm.TabIndex = 1;
            this.lblTitleChildForm.Text = "Home";
            // 
            // iconCurrentChildForm
            // 
            this.iconCurrentChildForm.BackColor = System.Drawing.Color.DarkRed;
            this.iconCurrentChildForm.ForeColor = System.Drawing.Color.MediumPurple;
            this.iconCurrentChildForm.IconChar = FontAwesome.Sharp.IconChar.Home;
            this.iconCurrentChildForm.IconColor = System.Drawing.Color.MediumPurple;
            this.iconCurrentChildForm.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconCurrentChildForm.Location = new System.Drawing.Point(16, 19);
            this.iconCurrentChildForm.Name = "iconCurrentChildForm";
            this.iconCurrentChildForm.Size = new System.Drawing.Size(32, 32);
            this.iconCurrentChildForm.TabIndex = 0;
            this.iconCurrentChildForm.TabStop = false;
            // 
            // panelDesktop2
            // 
            this.panelDesktop2.AutoScroll = true;
            this.panelDesktop2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelDesktop2.Controls.Add(this.panelDesktop);
            this.panelDesktop2.Controls.Add(this.iconPictureBox1);
            this.panelDesktop2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDesktop2.Location = new System.Drawing.Point(220, 75);
            this.panelDesktop2.Name = "panelDesktop2";
            this.panelDesktop2.Size = new System.Drawing.Size(963, 696);
            this.panelDesktop2.TabIndex = 12;
            // 
            // panelDesktop
            // 
            this.panelDesktop.AutoSize = true;
            this.panelDesktop.Controls.Add(this.elementHost1);
            this.panelDesktop.Controls.Add(this.iconPictureBox2);
            this.panelDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDesktop.Location = new System.Drawing.Point(0, 0);
            this.panelDesktop.Name = "panelDesktop";
            this.panelDesktop.Size = new System.Drawing.Size(963, 696);
            this.panelDesktop.TabIndex = 2;
            // 
            // elementHost1
            // 
            this.elementHost1.AutoSize = true;
            this.elementHost1.BackColor = System.Drawing.Color.Maroon;
            this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost1.Location = new System.Drawing.Point(0, 0);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(963, 696);
            this.elementHost1.TabIndex = 3;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = null;
            // 
            // iconPictureBox2
            // 
            this.iconPictureBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.iconPictureBox2.BackColor = System.Drawing.SystemColors.HighlightText;
            this.iconPictureBox2.BackgroundImage = global::DecisionSupport.Properties.Resources.logo1;
            this.iconPictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.iconPictureBox2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.iconPictureBox2.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconPictureBox2.IconColor = System.Drawing.SystemColors.ControlDark;
            this.iconPictureBox2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBox2.IconSize = 149;
            this.iconPictureBox2.Location = new System.Drawing.Point(346, 274);
            this.iconPictureBox2.Name = "iconPictureBox2";
            this.iconPictureBox2.Size = new System.Drawing.Size(270, 149);
            this.iconPictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconPictureBox2.TabIndex = 2;
            this.iconPictureBox2.TabStop = false;
            // 
            // iconPictureBox1
            // 
            this.iconPictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.iconPictureBox1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.iconPictureBox1.BackgroundImage = global::DecisionSupport.Properties.Resources.logo1;
            this.iconPictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.iconPictureBox1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconPictureBox1.IconColor = System.Drawing.SystemColors.ControlDark;
            this.iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBox1.IconSize = 149;
            this.iconPictureBox1.Location = new System.Drawing.Point(333, 205);
            this.iconPictureBox1.Name = "iconPictureBox1";
            this.iconPictureBox1.Size = new System.Drawing.Size(270, 149);
            this.iconPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconPictureBox1.TabIndex = 1;
            this.iconPictureBox1.TabStop = false;
            // 
            // firstForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Lime;
            this.ClientSize = new System.Drawing.Size(1183, 771);
            this.Controls.Add(this.panelDesktop2);
            this.Controls.Add(this.panelTitleBar);
            this.Controls.Add(this.panelMenu);
            this.DoubleBuffered = true;
            this.IsMdiContainer = true;
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "firstForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "firstForm";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.firstForm_DragDrop);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.firstForm_Paint);
            this.Resize += new System.EventHandler(this.firstForm_Resize);
            this.panelMenu.ResumeLayout(false);
            this.projectSubMenu.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnHome)).EndInit();
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconCurrentChildForm)).EndInit();
            this.panelDesktop2.ResumeLayout(false);
            this.panelDesktop2.PerformLayout();
            this.panelDesktop.ResumeLayout(false);
            this.panelDesktop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private FontAwesome.Sharp.IconButton exitButton;
        private FontAwesome.Sharp.IconButton evaluateButton;
        private FontAwesome.Sharp.IconButton saveButton;
        private System.Windows.Forms.Panel projectSubMenu;
        private FontAwesome.Sharp.IconButton LoadProjIcon;
        private FontAwesome.Sharp.IconButton newProjIcon;
        private FontAwesome.Sharp.IconButton projectButton;
        private System.Windows.Forms.Panel panelLogo;
        private FontAwesome.Sharp.IconPictureBox btnHome;
        private System.Windows.Forms.Panel panelTitleBar;
        private FontAwesome.Sharp.IconButton restoreButton;
        private FontAwesome.Sharp.IconButton iconButton6;
        private FontAwesome.Sharp.IconButton iconButton5;
        private FontAwesome.Sharp.IconButton minimizeBtn;
        private System.Windows.Forms.Label lblTitleChildForm;
        private FontAwesome.Sharp.IconPictureBox iconCurrentChildForm;
        private System.Windows.Forms.Panel panelDesktop2;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private Panel panelDesktop;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox2;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
    }
}