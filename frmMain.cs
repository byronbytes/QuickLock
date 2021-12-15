using Microsoft.VisualBasic.Devices;
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.WarningMessage == true)
            {
                MessageBox.Show("Please save your work before continuing. Clicking OK will lock your computer.");
            }

            Process.Start(@"C:\WINDOWS\system32\rundll32.exe", "user32.dll,LockWorkStation");
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (keyL && keyCTRL == true)
            {
                Process.Start(@"C:\WINDOWS\system32\rundll32.exe", "user32.dll,LockWorkStation");
            }
        }

        bool keyL = false;
        bool keyCTRL = false;

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.LCTRL)
            {
                keyCTRL = true;
            }
            else if (e.KeyCode == Keys.L)
            {
                keyL = true;
            }
        }

        private void frmMain_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.LCTRL)
            {
                keyCTRL = false;
            }
            else if (e.KeyCode == Keys.L)
            {
                keyL = false;
            }
        }

        private void giveWarningMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.WarningMessage = true;
            Properties.Settings.Default.Save();
        }
        
        
        private void SlideToShutdown()
        {
           Process.Start(@"C:\Windows\system32\SlideToShutdown.exe");
        }
        
        private void SleepAndLock()
        {
        
        }
    }
}
