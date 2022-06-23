/*
    (c) LiteTools 2022 (https://github.com/LiteTools)
    All rights reserved under the GNU General Public License v3.0.
*/

using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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
            ThemeSet();
            WarningSet();
            this.KeyPreview = true;
            SetupKeyboardHooks();
        }
        private GlobalKeyboardHook _globalKeyboardHook;
        public static string LockMessage = "Clicking OK will lock the computer, make sure to save any unsaved work.";

        public void SetupKeyboardHooks()
        {
            _globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.KeyboardPressed += OnKeyPressed;
        }

        private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            if (e.KeyboardData.VirtualCode != GlobalKeyboardHook.VkControl)
                return;
                
            if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown)
            {
                if (Properties.Settings.Default.WarningMessage == true)
                {
                    MessageBox.Show(LockMessage, "QuickLock");
                }
                Process.Start(@"C:\WINDOWS\system32\rundll32.exe", "user32.dll,LockWorkStation");
                e.Handled = true;
            }
        }

        private void WarningSet()
        {
            if (Properties.Settings.Default.WarningMessage == true)
            {
                radioButton1.Checked = true;
            }

            if (Properties.Settings.Default.WarningMessage == false)
            {
                radioButton2.Checked = true;
            }
        }
        private void ThemeSet()
        {
            if (Properties.Settings.Default.Theme == "dark")
            {
                panel2.BackColor = Color.FromArgb(12, 12, 12);
                panel1.BackColor = Color.FromArgb(24,24,24);
                label1.ForeColor = Color.White;
                label2.ForeColor = Color.White;
                label3.ForeColor = Color.White;
                label4.ForeColor = Color.White;
                label5.ForeColor = Color.White;
                button1.ForeColor = Color.Red;
                button2.ForeColor = Color.Red;
                button3.ForeColor = Color.Red;
                radioButton1.ForeColor = Color.White;
                radioButton2.ForeColor = Color.White;
                radioButton3.ForeColor = Color.White;
                radioButton4.ForeColor = Color.White;
                button1.BackColor = Color.FromArgb(18,18,18);
                button2.BackColor = Color.FromArgb(18, 18, 18);
                button3.BackColor = Color.FromArgb(18, 18, 18);

            }

            if (Properties.Settings.Default.Theme == "light")
            {
                panel1.BackColor = Color.White;
                panel2.BackColor = Color.GhostWhite;
                label1.ForeColor = Color.Black;
                label2.ForeColor = Color.Black;
                label3.ForeColor= Color.Black;
                label4.ForeColor = Color.Black;
                label5.ForeColor = Color.Black;
                button1.BackColor = Color.FromArgb(200, 200, 200);
                button2.BackColor = Color.FromArgb(200, 200, 200);
                button3.BackColor= Color.FromArgb(200, 200, 200);
                radioButton1.ForeColor = Color.Black;
                radioButton2.ForeColor = Color.Black;
                radioButton3.ForeColor = Color.Black;
                radioButton4.ForeColor = Color.Black;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.WarningMessage == true)
            {
                MessageBox.Show(LockMessage, "QuickLock");
            }
            Process.Start(@"C:\WINDOWS\system32\rundll32.exe", "user32.dll,LockWorkStation");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.WarningMessage == true)
            {
                MessageBox.Show(LockMessage, "QuickLock");
            }
            Application.SetSuspendState(PowerState.Suspend, true, true);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.WarningMessage = true;
            Properties.Settings.Default.Save();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Theme = "dark";
            ThemeSet();
            Properties.Settings.Default.Save();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Theme = "light";
            ThemeSet();
            Properties.Settings.Default.Save();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.WarningMessage = false;
            Properties.Settings.Default.Save();
        }

        public void Disposer()
        {
            _globalKeyboardHook?.Dispose();
        }
    }
}





