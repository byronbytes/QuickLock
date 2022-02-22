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

// Todo: Add try statements for next update.
// Idea: Logoff method as well.

namespace QuickLock
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            ThemeSet();
        }

        public static bool LockKey;
        public static string KeyBind;

        private void button1_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.WarningMessage == true)
            {
                MessageBox.Show("Clicking OK will lock the computer, make sure to save any unsaved work.", "QuickLock");
            }
            Process.Start(@"C:\WINDOWS\system32\rundll32.exe", "user32.dll,LockWorkStation");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.WarningMessage == true)
            {
                MessageBox.Show("Clicking OK will lock the computer, make sure to save any unsaved work.", "QuickLock");
            }
            Application.SetSuspendState(PowerState.Suspend, true, true);
        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Theme = "dark";
            ThemeSet();
            Properties.Settings.Default.Save();
        }

        private void lightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Theme = "light";
            ThemeSet();
            Properties.Settings.Default.Save();
        }

        private void trueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trueToolStripMenuItem.Checked = true;
            Properties.Settings.Default.WarningMessage = true;
            Properties.Settings.Default.Save();

            if(falseToolStripMenuItem.Checked == true)
            {
                falseToolStripMenuItem.Checked = false;
            }
        }

        private void falseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            falseToolStripMenuItem.Checked = true;
            Properties.Settings.Default.WarningMessage = false;
            Properties.Settings.Default.Save();

            if (trueToolStripMenuItem.Checked == true)
            {
                trueToolStripMenuItem.Checked = false;
            }
        }
        
        
        private void ThemeSet()
        {
            if(Properties.Settings.Default.Theme == "dark")
            {
                this.BackColor = Color.Black;
                toolStripMenuItem1.BackColor = Color.Gray;
                lightToolStripMenuItem.BackColor = Color.Gray;
                darkToolStripMenuItem.BackColor = Color.Gray;
                optionsToolStripMenuItem.BackColor = Color.Gray;
                giveWarningMessageToolStripMenuItem.BackColor = Color.Gray;
                menuStrip1.ForeColor = Color.White;
                menuStrip1.BackColor = Color.Black;
                label1.ForeColor = Color.White;
                button1.BackColor = Color.Black;
                button1.ForeColor = Color.Maroon;
                button2.BackColor = Color.Black;
                button2.ForeColor = Color.Maroon;
            }

            if (Properties.Settings.Default.Theme == "light")
            {
                this.BackColor = Color.White;
                toolStripMenuItem1.BackColor = Color.WhiteSmoke;
                lightToolStripMenuItem.BackColor = Color.WhiteSmoke;
                darkToolStripMenuItem.BackColor = Color.WhiteSmoke;
                optionsToolStripMenuItem.BackColor = Color.WhiteSmoke;
                giveWarningMessageToolStripMenuItem.BackColor = Color.WhiteSmoke;
                menuStrip1.ForeColor = Color.Black;
                menuStrip1.BackColor = Color.White;
                label1.ForeColor = Color.Black;
                button1.BackColor = Color.White;
                button1.ForeColor = Color.Maroon;
                button2.BackColor = Color.White;
                button2.ForeColor = Color.Maroon;
            }

        }


    }
}
