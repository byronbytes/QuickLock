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

namespace QuickLock.src
{
    public partial class frmRebind : Form
    {
        public frmRebind()
        {
            InitializeComponent();
        }

        public const int CustomKey = 0xA2; // default keybind
        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // FOR CONSOLE APPLICATIONS USE:
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Keys vKeys);

 
      

    private void textBox1_KeyDown(object sender, KeyEventArgs e)
        { 
            Keys Keybind = e.KeyCode;
       //     Keys Keybind = (Keys)Enum.Parse(typeof(Keys), (string "E"));
          
        }
    }
}
