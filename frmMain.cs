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
                MessageBox.Show("Please save your work before continuing.");
            }
            Process.Start(@"C:\WINDOWS\system32\rundll32.exe", "user32.dll,LockWorkStation");
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (keyL && keyWIN)
            {
                Process.Start(@"C:\WINDOWS\system32\rundll32.exe", "user32.dll,LockWorkStation");
            }
               
        }

        bool keyL = false;
        bool keyWIN = false;

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.LWin)
            {
                    keyWIN = true;
            }
            else if (e.KeyCode == Keys.L)
            {
                keyL = true;
            }
        }

        private void frmMain_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.LWin)
            {
                keyWIN = false;
            }
            else if (e.KeyCode == Keys.L)
            {
                keyL = false;
            }
        }
    }
}
