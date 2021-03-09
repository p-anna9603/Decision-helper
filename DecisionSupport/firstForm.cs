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
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;
        string hiddenChild = "";
        int newProject = 0;
        private Panel firstPanel;
        Form1 form1 = new Form1();
        ShowOpts evaluateProject;
        ShowOpts showOpts;
        int resize = 0;
        string prevActive = "";
        Dictionary<IconButton, Project> projects = new Dictionary<IconButton, Project>();
        int buttons = 0;
        IconButton iconButton1;
        IconButton xiconButton;
        int evaluated = 0; // 1 - project has been already evaluated, 0 - (new) project has not been evaluated
        public firstForm()
        {
            InitializeComponent();
            panelDesktop.Hide();
            Console.WriteLine("konstrutor");

            /* Form to screen operations */
            //   this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.MaximizedBounds = Screen.GetWorkingArea(this);
         //  this.WindowState = FormWindowState.Maximized;
            Console.WriteLine("Max: " + MaximizedBounds.Width + ", " + MaximizedBounds.Height);
            /*
            if (this.WindowState == FormWindowState.Maximized)
            {
                Console.WriteLine("maximiiized");
                this.FormBorderStyle = FormBorderStyle.None;

            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.FormBorderStyle = FormBorderStyle.None;
            }*/
            //      this.FormBorderStyle = FormBorderStyle.None;
       //   //  int screenLeft = SystemInformation.Sre.Left;
       //     int screenLeft = Screen.GetWorkingArea(this).Left;
       ////     int screenTop = SystemInformation.VirtualScreen.Top;
       //     int screenTop = Screen.GetWorkingArea(this).Top;
       //     //int screenWidth = SystemInformation.VirtualScreen.Width;
       //     int screenWidth = Screen.GetWorkingArea(this).Width;
       //     int screenHeight = Screen.GetWorkingArea(this).Height;

            //this.Size = new System.Drawing.Size(screenWidth, screenHeight);
            //this.Location = new System.Drawing.Point(screenLeft, screenTop);

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
            restoreButton.Visible = true;
        
            panelDesktop2.Location = new System.Drawing.Point(0, 0);
            panelMenu.Location = new System.Drawing.Point(0, 0);

            typeof(Panel).InvokeMember("DoubleBuffered",
                    BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                   null, panelDesktop2, new object[] { true });

  
        }
        private void OpenChildForm(Form childForm)
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
            childForm.MdiParent = this;
            currentChildForm = childForm;
            Console.WriteLine("name:" + currentChildForm.Name);
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
            this.BackgroundImageLayout = ImageLayout.Stretch;
            //    lblTitleChildForm.Text = childForm.Text;

            //childForm.Anchor = AnchorStyles.Top;
            //childForm.Anchor = AnchorStyles.Bottom;
            //childForm.Anchor = AnchorStyles.Right;
            //childForm.Anchor = AnchorStyles.Left;
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
            Console.WriteLine("name:" + currentChildForm.Name);
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
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = System.Drawing.Color.FromArgb(191, 64, 64);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                //Left border button
                leftBorderBtn.BackColor = color;
                Console.WriteLine("button y: " + currentBtn.Location.Y);
                if (currentBtn.Name.Equals(projectButton.Name))
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
                else if (!(currentBtn.Parent.Name.Equals("projectSubMenu")))
                {
                    projectSubMenu.Visible = false;
                }

                if (currentBtn.Parent.Name.Equals("projectSubMenu"))
                {
                    leftBorderBtn.Size = new System.Drawing.Size(7, 40);
                    leftBorderBtn.Location = new System.Drawing.Point(0, currentBtn.Parent.Location.Y + currentBtn.Location.Y);
                }
                else
                {
                    leftBorderBtn.Size = new System.Drawing.Size(7, 60);
                    leftBorderBtn.Location = new System.Drawing.Point(0, currentBtn.Location.Y);
                }
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

                //Icon Current child form
                iconCurrentChildForm.IconChar = currentBtn.IconChar;
                iconCurrentChildForm.IconColor = color;
                lblTitleChildForm.Text = currentBtn.Text;
            }
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                //currentBtn.BackColor = System.Drawing.Color.FromArgb(134, 45, 45); // dark red
                currentBtn.BackColor = System.Drawing.Color.DarkRed; // dark red
                currentBtn.ForeColor = System.Drawing.Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = System.Drawing.Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
                prevActive = currentBtn.Name;
            }
        }
        private void projectButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
        }
        int justLoaded = 0;
        private void newProjIcon_Click(object sender, EventArgs e)
        {
            if (form1.getTablesCount() > 0) // ha már volt betöltve projekt
            {
                Console.WriteLine("kérdés newprojectIcon_Click");
                const string message = "Do you want to save the data before exit?";
                const string caption = "Current project";
                var result = MessageBox.Show(message, caption,
                                            MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    form1.saveMenuClicked();
                    ActivateButton(sender, RGBColors.color4);
                    form1.clearEverything();
                    currentChildForm.Close();
                    form1.Close();                    
                    form1 = new Form1();
                    OpenChildForm(form1);
                    newProject = 1;
                    justLoaded = 0;
                    setIconTitle();
                    evaluated = 0;
                    form1.setNewWork(1);
                }
                else if (result == DialogResult.No)
                {
                    ActivateButton(sender, RGBColors.color4);
                    form1.clearEverything();
                    currentChildForm.Close();
                    form1.Close();                   
                    form1 = new Form1();
                    OpenChildForm(form1);
                    newProject = 1;
                    justLoaded = 0;
                    setIconTitle();
                    evaluated = 0;
                    form1.setNewWork(1);
                }
                else if(result == DialogResult.Cancel)
                {
                    Console.WriteLine("do nothiiing");
                }
                  
            }
            else
            {
                ActivateButton(sender, RGBColors.color4);
                if (currentChildForm != null)
                {
                    currentChildForm.Close();
                    form1.clearEverything();
                    form1.Close();
                }
                form1 = new Form1();
                OpenChildForm(form1);
                newProject = 1;
             //   iconButton1.Text = "Project 1";
                setIconTitle();
                justLoaded = 0;
                form1.setNewWork(1);
            }            
        }

        private void LoadProjIcon_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
            //if (newProject == 0) // nincs még megkezdett projekt és új formot kell kezdeni
            //{
            form1.clearEverything();
            form1.Close();
            form1 = new Form1();
            Reset();
            if (currentChildForm != null) // ha előtte lett volna már kiértékelve projekt
            {
                currentChildForm.Close();
            }
            if (form1.openFile() == 1)
                {
                    form1.MdiParent = this;
                    OpenChildForm(form1);
                    lblTitleChildForm.Text = form1.getOpenedFileName();
                    justLoaded = 1;
                    removeIconTitle();  
                    setIconTitle();                
                    evaluated = 0;
                }
            //}
            //else if (newProject == 1) // ha már van megkezdett projekt és ahhoz csatoljuk
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
            panelTitleBar.Controls.Remove(iconButton1);
            panelTitleBar.Controls.Remove(xiconButton);
        }
        private void setIconTitle()
        {
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
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconButton1.IconColor = System.Drawing.Color.Black;
            iconButton1.BackColor = Color.SeaShell;            
            iconButton1.ForeColor = Color.MidnightBlue;
            iconButton1.FlatAppearance.BorderSize = 0;
          //  this.iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            //this.iconButton1.Location = new System.Drawing.Point(140 + (86 * projects.Count), 17);
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
          
            if (projects.Count < 1) // törölni ha több projekt is opció
            {
                projects.Add(iconButton1, p);
            }
            this.panelTitleBar.Controls.Add(this.iconButton1);
            this.panelTitleBar.Controls.Add(this.xiconButton);
        }
        public void backToProject(object sender, EventArgs e)
        {
            Console.WriteLine("back to project");
            IconButton btn = sender as IconButton;
            Console.WriteLine("btn? " + btn.Name);
            Console.WriteLine("btn? " + btn);
            Console.WriteLine("cnt: " + projects.Count);
            if(projects.ContainsKey(btn))
            {
                Console.WriteLine("ssz? " + projects.Keys);
                if(currentChildForm != null)
                {
                    currentChildForm.Hide();
                }
                projects[btn].Form.Show();
                currentChildForm = form1;
                Console.WriteLine("key");
            }
        }

        public void projectCancelClicked(object sender, EventArgs e)
        {
            Console.WriteLine("kérdés projectCancelClick");
            const string message = "Do you want to save the data before exit?";
            const string caption = "Current project";
            var result = MessageBox.Show(message, caption,
                                        MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                form1.saveMenuClicked();
          //      ActivateButton(saveButton, RGBColors.color4);
                form1.clearEverything();
                currentChildForm.Close();
                currentChildForm = null;
                form1.Close();
                evaluated = 0; 
                if (currentChildForm != null)
                {
                    currentChildForm.Close();
                }
                //form1.Hide();
                Reset();
                justLoaded = 0;
                panelTitleBar.Controls.Remove(iconButton1);
                panelTitleBar.Controls.Remove(xiconButton);
             
            }
            else if (result == DialogResult.No)
            {
         //       ActivateButton(saveButton, RGBColors.color4);
                form1.clearEverything();
                currentChildForm.Close();
                form1.Close();
                currentChildForm = null;
                evaluated = 0;
                if (currentChildForm != null)
                {
            //        currentChildForm.Hide();
                }
             //   form1.Hide();
                Reset();
                justLoaded = 0;
                panelTitleBar.Controls.Remove(iconButton1);
                panelTitleBar.Controls.Remove(xiconButton);
            }
            else if (result == DialogResult.Cancel)
            {
                Console.WriteLine("do nothiiing");
            }
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
           
            form1.saveMenuClicked();

            iconButton1.Text = form1.getSavedFileName();
        }

        private void evaluateButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
            if (hiddenChild.Equals("ShowOpts"))
            {
                currentChildForm.Show();
                hiddenChild = "";
            }
            else
            {
                if (form1.getOptimum() == 1) // van tábla
                {
                    Console.WriteLine("Van tábla");

                    if(evaluated == 1 && form1.getModification() == 1) // volt már kiértékelve és nem történt módosítás
                    {
                        Console.WriteLine("kivolt már értékelve");
                        currentChildForm = evaluateProject;
                        currentChildForm.Show();
                        form1.Hide();
                        Console.WriteLine("childform: " + currentChildForm);
                    }
                    if (form1.getModification() == 1 && evaluated == 0 || // nem volt módosítás , nem volt kiértékelés (vagy új projekt)
                        form1.getModification() == 0 && evaluated == 1 || // volt módosítás és volt már kiértékelve
                        form1.getModification() == 0 && evaluated == 0)   // volt módosítás és még nem volt kiértékelve (betöltés esetén)
                    {
                        Console.WriteLine("elsbeeen");
                        ShowOpts showOpt = new ShowOpts(form1);
                        OpenChildForm(showOpt);
                        evaluateProject = showOpt;
                        //   showOpt.ShowDialog(); // uj ablakban megnyitni
                        evaluated = 1;
                        form1.setModification(1);
                    }
                }
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            if(currentChildForm != null)
            {
                currentChildForm.Hide();
                hiddenChild = currentChildForm.Name;
            }
        
            form1.Hide();
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
            this.MaximizedBounds = Screen.GetWorkingArea(this);
            Console.WriteLine("Max: " + MaximizedBounds.Width + ", " + MaximizedBounds.Height);
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void firstForm_Resize(object sender, EventArgs e)
        {
            resize++;
            Console.WriteLine("resiiize");
            
            this.MaximizedBounds = Screen.GetWorkingArea(this);
            Console.WriteLine("Max: " + MaximizedBounds.Width + ", " + MaximizedBounds.Height);
            Console.WriteLine("0 most: " + this.Width + ", " + this.Height);
            //        adjustPositions(this.FindForm());
            //      this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            //this.WindowState = FormWindowState.Maximized;
            //restoreButton.Visible = false;
            //iconButton5.Visible = true;
            if (this.Size.Width < Screen.PrimaryScreen.WorkingArea.Size.Width ||
                this.Size.Height < Screen.PrimaryScreen.WorkingArea.Size.Height)
            {
                restoreButton.Visible = false;
                iconButton5.Visible = true;
                this.FormBorderStyle = FormBorderStyle.Sizable;
            }
            else
            {
                restoreButton.Visible = true;
                iconButton5.Visible = false;
                this.FormBorderStyle = FormBorderStyle.None;
            }
    
            //if (this.Size.Width >= MaximizedBounds.Width || this.Size.Height >= MaximizedBounds.Width)
            //{
            //    Console.WriteLine("nagyooob");
            //    this.Size = new Size(2400, 1300);
            //    this.Location = new Point(2100, 0);

            //    Console.WriteLine("most: " + this.Width + ", " + this.Height);
            //}
            Console.WriteLine("location: " + this.Location);
            //if (this.WindowState == FormWindowState.Maximized)
            //{
            //    Console.WriteLine("maximiiized");
            //    this.FormBorderStyle = FormBorderStyle.None;
            //}
            //else
            //{
            //    this.FormBorderStyle = FormBorderStyle.Sizable;
            //    //this.FormBorderStyle = FormBorderStyle.None;
            //}
        
            //if (resize % 10 == 0 || resize < 10)
            //{
                if (currentChildForm != null)
                {
                panelDesktop.SuspendLayout();
                panelDesktop.Hide();
                //this.SuspendLayout();
                panelDesktop2.Controls.Remove(currentChildForm);
                currentChildForm.Hide();
                currentChildForm.SuspendLayout();
                currentChildForm.FormBorderStyle = FormBorderStyle.Sizable;
                    Console.WriteLine("resizing parent###################################");
                 //   currentChildForm.Dock = DockStyle.Fill;
                    //  currentChildForm.WindowState = FormWindowState.Maximized;
                    int newHeight = panelDesktop2.Height;
                    int newWidth = panelDesktop2.Width;
                    //currentChildForm.Width = panelDesktop.Width;
                    //currentChildForm.Height = panelDesktop.Height;
                    //currentChildForm.Location = panelDesktop.Location;
                    currentChildForm.Size = new System.Drawing.Size(panelDesktop2.Width, panelDesktop2.Height);

                    Console.WriteLine("child width " + currentChildForm.Width + ", " + currentChildForm.Height);
                    Console.WriteLine("panelDesktop width" + panelDesktop2.Width + ", " + panelDesktop2.Height);
                    Console.WriteLine("width" + newWidth + ", " + newHeight);
                   panelDesktop2.Controls.Add(currentChildForm);
                    currentChildForm.BringToFront();
                    currentChildForm.FormBorderStyle = FormBorderStyle.None;
                    currentChildForm.Show();
                panelDesktop.ResumeLayout(false);
                panelDesktop.PerformLayout(); 
                //this.ResumeLayout(false);
                //this.PerformLayout();

                currentChildForm.ResumeLayout(false);
                currentChildForm.PerformLayout();

                //  OpenChildForm(form1);
                //   form1.adjustPositions(currentChildForm);
            }
            this.Show();
            //}
        }

        private void minimizeBtn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void restoreButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            //this.Size = new System.Drawing.Size(800, 600);
            //this.Location = new System.Drawing.Point(Screen.PrimaryScreen.WorkingArea.Width / 2 - 400,
               //Screen.PrimaryScreen.WorkingArea.Height - this.Size.Height - 200);

            restoreButton.Visible = false;
            iconButton5.Visible = true;
            Console.WriteLine("restoooooore kicsibe");
        }
        int LX, LY, SW, SH;

        private void iconButton2_Click(object sender, EventArgs e)
        {

        }

        private void iconButton5_Click(object sender, EventArgs e) // Maximize
        {
            Console.WriteLine("nagyitas");
            LX = this.Location.X;
            LY = this.Location.Y;
            SW = this.Size.Width;
            SH = this.Size.Height;
            restoreButton.Visible = true;
            iconButton5.Visible = false;
            //this.Size = Screen.GetWorkingArea(this).Size;
            //this.Location = Screen.GetWorkingArea(this).Location;
       //     this.MaximizedBounds = Screen.GetWorkingArea(this);
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            //this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            WindowState = FormWindowState.Maximized;
            //if(WindowState == FormWindowState.Normal)
            //{
            //    this.MaximizedBounds = Screen.GetWorkingArea(this);
            //    Console.WriteLine("Max: " + MaximizedBounds.Width + ", " + MaximizedBounds.Height);
            //    WindowState = FormWindowState.Maximized;
            //}
            //else
            //{
            //    WindowState = FormWindowState.Normal;
            //}

            //if (this.WindowState == FormWindowState.Maximized)
            //{
            //    Console.WriteLine("maximiiized");
            //    this.FormBorderStyle = FormBorderStyle.None;
            //}
            //else
            //{
            //    this.FormBorderStyle = FormBorderStyle.Sizable;
            //    //this.FormBorderStyle = FormBorderStyle.None;
            //}

            ////  int screenLeft = SystemInformation.Sre.Left;
            //int screenLeft = Screen.GetWorkingArea(this).Left;
            ////     int screenTop = SystemInformation.VirtualScreen.Top;
            //int screenTop = Screen.GetWorkingArea(this).Top;
            ////int screenWidth = SystemInformation.VirtualScreen.Width;
            //int screenWidth = Screen.GetWorkingArea(this).Width;
            //int screenHeight = Screen.GetWorkingArea(this).Height;

            //this.Size = new System.Drawing.Size(screenWidth, screenHeight);
            //this.Location = new System.Drawing.Point(screenLeft, screenTop);
        }

        private void iconButton6_Click(object sender, EventArgs e) // Exit button
        {
            Application.Exit();
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
    }
}
