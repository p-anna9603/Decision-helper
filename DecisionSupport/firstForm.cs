using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DecisionSupport
{
    public partial class firstForm : Form
    {
        // Fields
        private UserControl userControl;
        options2 showOpt2;
        private static IconButton CurrentBtn;
        private static Panel leftBorderBtn;
        private Form currentChildForm;
        string hiddenChild = "";
        int newProject = 0;
        private Panel firstPanel;
        Form1 form1;
        options2 evaluateProject;
        // ShowOpts showOpts;
        int resize = 0;
        static string prevActive = "";
        Dictionary<IconButton, Project> projects = new Dictionary<IconButton, Project>();
        int buttons = 0;
        IconButton iconButton1;
        IconButton xiconButton;
        int evaluated = 0; // 1 - project has been already evaluated, 0 - (new) project has not been evaluated
        int shownWindow = 0;
        private static IconPictureBox iconBtn;
        private static Label title;
        public firstForm()
        {
            InitializeComponent();
            panelDesktop.Hide();
            Rectangle rec;
            rec = Screen.GetWorkingArea(this);
            rec.X = this.MaximizedBounds.X;
            rec.Y = this.MaximizedBounds.Y;
            this.MaximizedBounds = rec;        
            this.FormBorderStyle = FormBorderStyle.None;         

            //Remove form title bar
            this.Text = string.Empty;
            this.ControlBox = false;

            firstPanel = this.panelDesktop2;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.HorizontalScroll.Enabled = false;

            //New form
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new System.Drawing.Size(7, 60);
            panelMenu.Controls.Add(leftBorderBtn);
            projectSubMenu.Visible = false;
            restoreButton.Visible = false;
            iconButton5.Visible = true;
        
            panelDesktop2.Location = new System.Drawing.Point(0, 0);
            panelMenu.Location = new System.Drawing.Point(0, 0);

            typeof(Panel).InvokeMember("DoubleBuffered",
                    BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                   null, panelDesktop2, new object[] { true });
            currentChildForm = null;
            userControl = null;
            iconBtn = iconCurrentChildForm;
            title = lblTitleChildForm;
            elementHost1.Hide();
        }
        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                // open only form
                currentChildForm.Hide();
            }
            childForm.MdiParent = this;
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Hide();
            panelDesktop2.Controls.Add(childForm);
            panelDesktop2.Controls.Remove(iconPictureBox1);
            panelDesktop2.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            childForm.AutoScroll = true;
            childForm.AutoSize = false;
            childForm.AutoScaleMode = AutoScaleMode.None;
            panelDesktop2.BackgroundImage = Properties.Resources.backg_1;
            this.BackgroundImage = Properties.Resources.backg_1;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            typeof(Form).InvokeMember("DoubleBuffered",
                    BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                   null, childForm, new object[] { true });
        }
        
        private void OpenChildFormEvaluation(Form childForm)
        {
            if (currentChildForm != null)
            {
                // open only form
                currentChildForm.Hide();
            }
            //if(childForm.Name.Equals("Form1"))
            //{
            //    form1 = childForm;
            //}
            currentChildForm = childForm;
            //Console.WriteLine("name:" + currentChildForm.Name);
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop2.Controls.Add(childForm);
            panelDesktop2.Controls.Remove(iconPictureBox1);
            panelDesktop2.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            childForm.AutoScroll = true;
            childForm.AutoSize = false;
            childForm.AutoScaleMode = AutoScaleMode.None;
            panelDesktop2.BackgroundImage = Properties.Resources.backg_1;
            this.BackgroundImage = Properties.Resources.backg_1;
            //    lblTitleChildForm.Text = childForm.Text;

            //childForm.Anchor = AnchorStyles.Top;
            //childForm.Anchor = AnchorStyles.Bottom;
            //childForm.Anchor = AnchorStyles.Right;
            //childForm.Anchor = AnchorStyles.Left;
            typeof(Form).InvokeMember("DoubleBuffered",
                    BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                   null, childForm, new object[] { true });
        }

        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
        }
        // Methods
        private void ActivateButton(object senderBtn, System.Drawing.Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                //Button
                CurrentBtn = (IconButton)senderBtn;
                CurrentBtn.BackColor = System.Drawing.Color.FromArgb(191, 64, 64);
                CurrentBtn.ForeColor = color;
                CurrentBtn.TextAlign = ContentAlignment.MiddleCenter;
                CurrentBtn.IconColor = color;
                CurrentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                CurrentBtn.ImageAlign = ContentAlignment.MiddleRight;
                //Left border button
                leftBorderBtn.BackColor = color;
                //Console.WriteLine("button y: " + currentBtn.Location.Y);
                if (CurrentBtn.Name.Equals(projectButton.Name))
                {
                    if (projectSubMenu.Visible)
                    {
                        projectSubMenu.Visible = false;
                    }
                    else
                    {
                        projectSubMenu.Visible = true;
                    }
                }
                else if (!(CurrentBtn.Parent.Name.Equals("projectSubMenu")))
                {
                    projectSubMenu.Visible = false;
                }

                if (CurrentBtn.Parent.Name.Equals("projectSubMenu"))
                {
                    leftBorderBtn.Size = new System.Drawing.Size(7, 40);
                    leftBorderBtn.Location = new System.Drawing.Point(0, CurrentBtn.Parent.Location.Y + CurrentBtn.Location.Y);
                }
                else
                {
                    leftBorderBtn.Size = new System.Drawing.Size(7, 60);
                    leftBorderBtn.Location = new System.Drawing.Point(0, CurrentBtn.Location.Y);
                }
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

                //Icon Current child form
                iconCurrentChildForm.IconChar = CurrentBtn.IconChar;
                iconCurrentChildForm.IconColor = color;
                lblTitleChildForm.Text = CurrentBtn.Text;
            }
        }

        public static void DisableButton()
        {
            if (CurrentBtn != null)
            {
                //currentBtn.BackColor = System.Drawing.Color.FromArgb(134, 45, 45); // dark red
                CurrentBtn.BackColor = System.Drawing.Color.DarkRed; // dark red
                CurrentBtn.ForeColor = System.Drawing.Color.Gainsboro;
                CurrentBtn.TextAlign = ContentAlignment.MiddleLeft;
                CurrentBtn.IconColor = System.Drawing.Color.Gainsboro;
                CurrentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                CurrentBtn.ImageAlign = ContentAlignment.MiddleLeft;
                leftBorderBtn.Visible = false;
                prevActive = CurrentBtn.Name;
            }
        }
        private void projectButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
        }
        int justLoaded = 0;
        private void newProjIcon_Click(object sender, EventArgs e)
        {
            if (form1 != null && form1.getTablesCount() > 0) // if there was already a loaded project
            {
                const string message = "Do you want to save the data before exit?";
                const string caption = "Current project";
                var result = MessageBox.Show(message, caption,
                                            MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    form1.saveMenuClicked();
                    if (form1.getSaveRes() == 1)
                    {
                        ActivateButton(sender, RGBColors.color4);
                        currentChildForm.Close();
                        form1.Close();
                        form1.clearEverything();
                        form1 = new Form1();
                        OpenChildForm(form1);
                        newProject = 1;
                        justLoaded = 0;
                        removeIconTitle();
                        setIconTitle();
                        evaluated = 0;
                        form1.setNewWork(1);
                        Console.WriteLine("1");
                        shownWindow = 0;
                    }
                }
                else if (result == DialogResult.No)
                {
                    ActivateButton(sender, RGBColors.color4);
                    currentChildForm.Close();
                    form1.Close();
                    form1.clearEverything();
                    form1 = new Form1();
                    OpenChildForm(form1);
                    newProject = 1;
                    justLoaded = 0;
                    removeIconTitle();
                    setIconTitle();
                    evaluated = 0;
                    form1.setNewWork(1);
                    Console.WriteLine("2");
                    shownWindow = 0;
                }
                else if(result == DialogResult.Cancel)
                {
                    Console.WriteLine("do nothiiing");
                }
                  
            }
            else
            {
                ActivateButton(sender, RGBColors.color4);
                if (currentChildForm != null && form1 != null)
                {
                    currentChildForm.Close();
                    form1.Close();
                    form1.clearEverything();
                }
                form1 = new Form1();
                OpenChildForm(form1);
                newProject = 1;
                setIconTitle();
                justLoaded = 0;
                form1.setNewWork(1);
            }            
        }

        private void LoadProjIcon_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
            if (form1 != null && form1.getTablesCount() > 0 && form1.getModification() == 0) // if there was already a loaded project
            {
                const string message = "Do you want to save the data before exit?";
                const string caption = "Current project";
                var result = MessageBox.Show(message, caption,
                                            MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    form1.saveMenuClicked();
                    if(form1.getSaveRes() == 1)
                    {
                        form1.Close();
                        form1.clearEverything();
                        removeIconTitle();
                        form1 = new Form1();
                        Reset();
                        if (currentChildForm != null) // if a project has been already evaluated before
                        {
                            currentChildForm.Close();
                        }
                        if (form1.openFile() == 1)
                        {
                            form1.MdiParent = this;
                            OpenChildForm(form1);
                            lblTitleChildForm.Text = "Project";
                            iconCurrentChildForm.IconChar = IconChar.Tasks;
                            justLoaded = 1;
                            setIconTitle();
                            evaluated = 0;
                        }
                    }
                }
                else if (result == DialogResult.No)
                {
                    form1.Saving = 1;
                    Console.WriteLine("sav: " + form1.getSaving());
                    form1.Close();
                    form1.clearEverything();
                    removeIconTitle();
                    form1 = new Form1();
                    Reset();
                    if (currentChildForm != null) // if evaluation has been taken place before
                    {
                        currentChildForm.Close();
                    }
                    if (form1.openFile() == 1)
                    {
                        form1.MdiParent = this;
                        OpenChildForm(form1);
                        lblTitleChildForm.Text = "Project";
                        iconCurrentChildForm.IconChar = IconChar.Tasks;
                        justLoaded = 1;                     
                        setIconTitle();
                        evaluated = 0;
                    } 
                    else
                    {
                        form1 = null;
                    }
                }
            }
            else
            {
                if(form1 != null)
                {
                    form1.Close();
                    form1.clearEverything();
                }
                removeIconTitle();
                Form1 formBackup = form1;
                form1 = new Form1();
                Reset();
                if (currentChildForm != null) // if evaluation has been taken place before
                {
                    currentChildForm.Close();
                }
                if (form1.openFile() == 1)
                {
                    form1.MdiParent = this;
                    OpenChildForm(form1);
                    lblTitleChildForm.Text = "Project";
                    iconCurrentChildForm.IconChar = IconChar.Tasks;
                    justLoaded = 1;                  
                    setIconTitle();
                    evaluated = 0;
                }
                else
                {
                    form1 = null;
                }
            }

            //}
            //else if (newProject == 1) // if upon loading project we merge it into the existing one
            //{
            //    if (form1.openFile() == 1)
            //    {
            //        lblTitleChildForm.Text = form1.getOpenedFileName();
            //        justLoaded = 1;
            //        setIconTitle();
            //        evaluated = 0;
            //    }
            //}
        }
        public void removeIconTitle()
        {
            Console.WriteLine("remove icon title");
            if(panelTitleBar.Controls.Contains(iconButton1) && panelTitleBar.Controls.Contains(xiconButton))
            {
                panelTitleBar.Controls.Remove(iconButton1);
                panelTitleBar.Controls.Remove(xiconButton);
            }
        }
        private void setIconTitle()
        {
            Console.WriteLine("seticon title");
            Project p = new Project(currentChildForm);
            projects.Clear();
            iconButton1 = new FontAwesome.Sharp.IconButton();
            xiconButton = new FontAwesome.Sharp.IconButton();

            if (justLoaded == 1)
            {
                iconButton1.Text = form1.getOpenedFileName();
            }
            else
            {
                iconButton1.Text = "Project " + buttons;

            }
            iconButton1.Name = "Project " + buttons;
            buttons++;
            iconButton1.Click += new System.EventHandler(this.backToProject);
            iconButton1.MouseHover += new System.EventHandler(projectHover);
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconButton1.IconColor = System.Drawing.Color.Black;
            iconButton1.BackColor = Color.SeaShell;            
            iconButton1.ForeColor = Color.MidnightBlue;
            iconButton1.FlatAppearance.BorderSize = 0;
            this.iconButton1.Location = new System.Drawing.Point(170, 19);
            this.iconButton1.Size = new System.Drawing.Size(86, 32);
            this.iconButton1.TabIndex = 7;
            this.iconButton1.UseVisualStyleBackColor = false;

            // x button
            this.xiconButton.BackColor = System.Drawing.Color.DarkRed;
            this.xiconButton.ForeColor = System.Drawing.Color.DarkRed;
            this.xiconButton.IconChar = FontAwesome.Sharp.IconChar.Times;
            this.xiconButton.IconColor = System.Drawing.Color.SeaShell;
            this.xiconButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.xiconButton.IconSize = 20;
            xiconButton.FlatAppearance.BorderSize = 1;
            xiconButton.FlatAppearance.BorderColor = Color.White;
            xiconButton.FlatStyle = FlatStyle.Flat;
            this.xiconButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.xiconButton.Location = new System.Drawing.Point(iconButton1.Location.X+iconButton1.Width, iconButton1.Location.Y+1);
            this.xiconButton.Name = "xButton";
            this.xiconButton.Size = new System.Drawing.Size(31-1, iconButton1.Height-2);
            this.xiconButton.TabIndex = 8;
            this.xiconButton.UseVisualStyleBackColor = false;
            xiconButton.Click += new System.EventHandler(this.projectCancelClicked);
          
            if (projects.Count < 1) // delete this if handling more project is an option
            {
                projects.Add(iconButton1, p);
            }
            this.panelTitleBar.Controls.Add(this.iconButton1);
            this.panelTitleBar.Controls.Add(this.xiconButton);
        }
        public void projectHover(object sender, EventArgs e)
        {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(iconButton1, iconButton1.Text);
        }
        public void backToProject(object sender, EventArgs e)
        {
            IconButton btn = sender as IconButton;
            if(projects.ContainsKey(btn))
            {
                shownWindow = 0;
                elementHost1.Hide();
                if (currentChildForm != null)
                {
                    currentChildForm.Hide();
                }
                projects[btn].Form.Show();
                currentChildForm = form1;
                iconCurrentChildForm.IconChar = IconChar.Tasks;
                lblTitleChildForm.Text = "Project";
                DisableButton();
                leftBorderBtn.Visible = false;
                iconCurrentChildForm.IconColor = Color.MediumPurple;
                resizeChildForm();
            }
        }

        public void projectCancelClicked(object sender, EventArgs e)
        {
            if (form1.Tables.Count == 0 ||
                form1.Tables.Count != 0 && form1.getSaving() == 1)
            {
                closeProject();
                return;
            }
            const string message = "Do you want to save the data before exit?";
            const string caption = "Current project";
            var result = MessageBox.Show(message, caption,
                                        MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                form1.saveMenuClicked();
                //      ActivateButton(saveButton, RGBColors.color4);

                if (form1.getSaveRes() == 1)
                {
                    save.Visible = true;                    
                    currentChildForm.Close();
                    currentChildForm = null;
                    form1.Close();
                    form1.clearEverything();
                    evaluated = 0;
                    if (currentChildForm != null)
                    {
                        currentChildForm.Close();
                    }
                    elementHost1.Hide();
                    panelDesktop.Hide();
                    Reset();
                    justLoaded = 0;
                    panelTitleBar.Controls.Remove(iconButton1);
                    panelTitleBar.Controls.Remove(xiconButton);

                    /* Show Successful save for a couple of secs */
                    save.Visible = true;
                    var t = new Timer();
                    t.Interval = 2000;
                    t.Tick += (s, r) =>
                    {
                        save.Visible = false;
                        t.Stop();
                    };
                    t.Start();
                }            
            }
            else if (result == DialogResult.No)
            {
                form1.Saving = 1;
                Console.WriteLine("sav 2: " + form1.getSaving());
                closeProject();
            }
            else if (result == DialogResult.Cancel)
            {
                //Console.WriteLine("do nothiiing");
            }
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
           
            if(form1 != null)
            {
                form1.saveMenuClicked();
            }
            else
            {
                MessageBox.Show("There is nothing to save.\nStart you work now!", "Saving?");
                return;
            }
            if(form1 != null && form1.getSaveRes() == 1)
            {
                iconButton1.Text = form1.getSavedFileName();
                save.Visible = true;
                var t = new Timer();
                t.Interval = 2000;
                t.Tick += (s, r) =>
                {
                    save.Visible = false;
                    t.Stop();
                };
                t.Start();
            }
        }

        private void closeProject()
        {
            currentChildForm.Close();
            form1.Close();
            form1.clearEverything();
            currentChildForm = null;
            evaluated = 0;
            if (currentChildForm != null)
            {
                currentChildForm.Hide();
            }
            //if (elementHost1.Child != null)
            //{
                elementHost1.Hide();
                panelDesktop.Hide();
            //}
            //   form1.Hide();
            Reset();
            justLoaded = 0;
            panelTitleBar.Controls.Remove(iconButton1);
            panelTitleBar.Controls.Remove(xiconButton);
        }
        private void evaluateButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
            if (hiddenChild.Equals("ShowOpts"))
            {
                panelDesktop.Show();
                elementHost1.Show();
                hiddenChild = "";
            }
            else
            {
                if (form1 != null && form1.getOptimum() == 1) // there is a product
                {
                    shownWindow = 1;
                    if(evaluated == 1 && form1.getModification() == 1) // it has already been evaluated before and nothing has changed since then
                    {
                        elementHost1.Show();
                        panelDesktop.Show();
                        form1.Hide();
                    }
                    if (form1.getModification() == 1 && evaluated == 0 || // there were no modifications, and no evaluation (or new project)
                        form1.getModification() == 0 && evaluated == 1 || // it has already been evaluated before and has been modified 
                        form1.getModification() == 0 && evaluated == 0)   // modified and not have been evaluated (onload)
                    {
                        evaluated = 1;
                        if(showOpt2 != null)
                        {
                            for (int i = 0; i < showOpt2.ShowSolList.Count; i++)
                            {
                                showOpt2.ShowSolList[i].Close();
                            }
                        }
                        showOpt2 = new options2(form1);                  
                       
                        elementHost1.Child = showOpt2;
                        evaluateProject = showOpt2;
                        elementHost1.Show();
                        panelDesktop.Show();

                        form1.Hide();
                        currentChildForm.Hide();
                        form1.setModification(1);
                    }
                }
                else
                {
                    iconCurrentChildForm.IconColor = Color.MediumPurple;
                    if(iconButton1 != null)
                    {
                        iconCurrentChildForm.IconChar = IconChar.Tasks;
                        lblTitleChildForm.Text = "Project";
                    }
                    else
                    {
                        iconCurrentChildForm.IconChar = IconChar.Home;
                        lblTitleChildForm.Text = "Home";
                    }
                    DisableButton();
                    leftBorderBtn.Visible = false;
                }    
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {           
            if(form1 != null) // there is a project open and with product
            {
                form1.Close();
                if(form1.CanCloseParent == 1)
                {
                    Application.Exit();
                }
            }
            else
            {
                Application.Exit();
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            if(currentChildForm != null)
            {
                currentChildForm.Hide();
                hiddenChild = currentChildForm.Name;
            }
            elementHost1.Hide();
            Reset();
        }
        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            iconCurrentChildForm.IconChar = IconChar.Home;
            iconCurrentChildForm.IconColor = Color.MediumPurple;
            panelDesktop.Show();
            lblTitleChildForm.Text = "Home";
        }
        
        // Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            Rectangle rec;
            rec = Screen.GetWorkingArea(this);
            rec.X = this.MaximizedBounds.X;
            rec.Y = this.MaximizedBounds.Y;
            this.MaximizedBounds = rec;

            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.DarkRed, ButtonBorderStyle.Solid);
        }
        
        private void firstForm_Resize(object sender, EventArgs e)
        {
            resize++;
            //if(resize != 1)
            //{
            //    return;
            //}
            this.SuspendLayout();           
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                restoreButton.Visible = true;
                iconButton5.Visible = false;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                restoreButton.Visible = false;
                iconButton5.Visible = true;
            }

            if (currentChildForm != null && hiddenChild == "" && shownWindow == 0)
            {
               resizeChildForm();               
            }
            //if(elementHost1 != null)
            //{
            //    options2 ch = (options2)elementHost1.Child;
            //}
            this.ResumeLayout();
            this.Show();
            resize = 0;
        }
        private void resizeChildForm()
        {
            panelDesktop.SuspendLayout();
            panelDesktop.Hide();
            this.SuspendLayout();
            panelDesktop2.Controls.Remove(currentChildForm); 
            currentChildForm.Hide(); 
            currentChildForm.SuspendLayout(); 
            currentChildForm.FormBorderStyle = FormBorderStyle.Sizable; 

            panelDesktop2.Controls.Add(currentChildForm); 
            currentChildForm.BringToFront();
            currentChildForm.FormBorderStyle = FormBorderStyle.None;  

            currentChildForm.Show(); 
            panelDesktop.ResumeLayout(false);
            panelDesktop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

            currentChildForm.ResumeLayout(false);
            currentChildForm.PerformLayout();
        }
        private void minimizeBtn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void restoreButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            restoreButton.Visible = false;
            iconButton5.Visible = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            save.Visible = true;
            timer1.Enabled = false;
        }

        private void iconButton5_Click(object sender, EventArgs e) // Maximize
        {
            restoreButton.Visible = true;
            iconButton5.Visible = false;
            WindowState = FormWindowState.Maximized;
        }

        private void iconButton6_Click(object sender, EventArgs e) // Exit button
        {
            if (form1 != null) // there is an open project and with product
            {
                form1.Close();
                if (form1.CanCloseParent == 1)
                {
                    Application.Exit();
                }
            }
            else
            {
                Application.Exit();
            }
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

        public static IconPictureBox IconBtn { get => iconBtn; set => iconBtn = value; }
        public static Label Title { get => title; set => title = value; }
    }
}
