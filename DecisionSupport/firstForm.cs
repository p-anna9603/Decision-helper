using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

        public firstForm()
        {
            InitializeComponent();
            //Remove form title bar
            this.Text = string.Empty;
            this.ControlBox = false;
            //   this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.MaximizedBounds = Screen.GetWorkingArea(this);
            Console.WriteLine("Max: " + MaximizedBounds.Width + ", " + MaximizedBounds.Height);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            //this.FormBorderStyle = FormBorderStyle.None;

            //New form
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new System.Drawing.Size(7, 60);
            panelMenu.Controls.Add(leftBorderBtn);
            projectSubMenu.Visible = false;
            restoreButton.Visible = true;
        }
        private void OpenChildFOrm(Form childForm)
        {
            if (currentChildForm != null)
            {
                // open only form
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitleChildForm.Text = childForm.Text;
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
            }
        }
        private void projectButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
        }

        private void newProjIcon_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
        }

        private void LoadProjIcon_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
        }

        private void evaluateButton_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            currentChildForm.Close();
            Reset();
        }
        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            iconCurrentChildForm.IconChar = IconChar.Home;
            iconCurrentChildForm.IconColor = Color.MediumPurple;
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
            Console.WriteLine("resiiize");
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
            }
            else
            {
                restoreButton.Visible = true;
                iconButton5.Visible = false;
            }
        }

        private void minimizeBtn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void restoreButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Size = new System.Drawing.Size(800, 600);
            this.Location = new System.Drawing.Point(Screen.PrimaryScreen.WorkingArea.Width / 2 - 400,
               Screen.PrimaryScreen.WorkingArea.Height - this.Size.Height - 200);

            restoreButton.Visible = false;
            iconButton5.Visible = true;
            Console.WriteLine("restoooooore kicsibe");
        }
        int LX, LY, SW, SH;

        private void iconButton5_Click(object sender, EventArgs e)
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

            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
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
        }

        private void iconButton6_Click(object sender, EventArgs e) // Exit button
        {
            Application.Exit();
        }
    }
}
