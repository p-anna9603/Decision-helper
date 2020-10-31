using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DecisionSupport
{
    public class FormHelper
    {
        public static void SetSizeToScreen(Form form)
        {
            //int left = Screen.PrimaryScreen.Bounds.Left;
            //int top = Screen.PrimaryScreen.Bounds.Top;
            //int width = Screen.PrimaryScreen.Bounds.Width;
            //int height = Screen.PrimaryScreen.Bounds.Height;
            //form.Location = new System.Drawing.Point(left, top);
            //form.Size = new System.Drawing.Size(width, height);
            form.WindowState = FormWindowState.Maximized;
            ////Console.WriteLine("point left:" + left + " top: " + top);
            ////Console.WriteLine("width " + width + " height " + height);

        }

        //public static void SetSizeToDesktop(this Form form)
        //{
        //    int left = SystemInformation.WorkingArea.Left;
        //    int top = SystemInformation.WorkingArea.Top;
        //    int width = SystemInformation.WorkingArea.Width;
        //    int height = SystemInformation.WorkingArea.Height;
        //    form.Location = new System.Drawing.Point(left, top);
        //    form.Size = new System.Drawing.Size(width, height);
        //    //Console.WriteLine("point left:" + left + " top: " + top);
        //    //Console.WriteLine("width " + width + " height " + height);
        //}
    }
}
