using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickLock
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

         //   if(Properties.Settings.Default.ShowOnTop == true)
          //  {
          //      this.SetTopLevel(true);
          //  }

         //   if (Properties.Settings.Default.ShowOnTop == false)
          //  {
          //      this.SetTopLevel(false);
          //  }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(Properties.Settings.Default.WarningMessage == true)
            {
                MessageBox.Show("This may close work, so please save your work before continuing.");
            }


            Process.Start(@"C:\WINDOWS\system32\rundll32.exe", "user32.dll,LockWorkStation");
        } 


        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
           if(toolStripMenuItem2.Checked == true)
            {
                Properties.Settings.Default.ShowOnTop = true;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }
    }
}
