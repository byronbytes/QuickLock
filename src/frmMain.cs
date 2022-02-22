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
            this.KeyPreview = true;
            SetupKeyboardHooks();

        }

        private GlobalKeyboardHook _globalKeyboardHook;

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
                    MessageBox.Show("Clicking OK will lock the computer, make sure to save any unsaved work.", "QuickLock");
                }
                Process.Start(@"C:\WINDOWS\system32\rundll32.exe", "user32.dll,LockWorkStation");

                e.Handled = true;
            }
        }

        public void Disposer()
        {
            _globalKeyboardHook?.Dispose();
        }
    


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

            if (falseToolStripMenuItem.Checked == true)
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
            if (Properties.Settings.Default.Theme == "dark")
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

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void frmMain_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.LControlKey)
            {
                Process.Start(@"C:\WINDOWS\system32\rundll32.exe", "user32.dll,LockWorkStation");
            }
        }
    }
}

// Beyond this point is the Global Keyboard system so it detects the CTRL key being pressed.
class GlobalKeyboardHookEventArgs : HandledEventArgs
{
    public GlobalKeyboardHook.KeyboardState KeyboardState { get; private set; }
    public GlobalKeyboardHook.LowLevelKeyboardInputEvent KeyboardData { get; private set; }

    public GlobalKeyboardHookEventArgs(
        GlobalKeyboardHook.LowLevelKeyboardInputEvent keyboardData,
        GlobalKeyboardHook.KeyboardState keyboardState)
    {
        KeyboardData = keyboardData;
        KeyboardState = keyboardState;
    }
}

//Based on https://gist.github.com/Stasonix
class GlobalKeyboardHook : IDisposable
{
    public event EventHandler<GlobalKeyboardHookEventArgs> KeyboardPressed;

    public GlobalKeyboardHook()
    {
        _windowsHookHandle = IntPtr.Zero;
        _user32LibraryHandle = IntPtr.Zero;
        _hookProc = LowLevelKeyboardProc; // we must keep alive _hookProc, because GC is not aware about SetWindowsHookEx behaviour.

        _user32LibraryHandle = LoadLibrary("User32");
        if (_user32LibraryHandle == IntPtr.Zero)
        {
            int errorCode = Marshal.GetLastWin32Error();
            throw new Win32Exception(errorCode, $"Failed to load library 'User32.dll'. Error {errorCode}: {new Win32Exception(Marshal.GetLastWin32Error()).Message}.");
        }



        _windowsHookHandle = SetWindowsHookEx(WH_KEYBOARD_LL, _hookProc, _user32LibraryHandle, 0);
        if (_windowsHookHandle == IntPtr.Zero)
        {
            int errorCode = Marshal.GetLastWin32Error();
            throw new Win32Exception(errorCode, $"Failed to adjust keyboard hooks for '{Process.GetCurrentProcess().ProcessName}'. Error {errorCode}: {new Win32Exception(Marshal.GetLastWin32Error()).Message}.");
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            // because we can unhook only in the same thread, not in garbage collector thread
            if (_windowsHookHandle != IntPtr.Zero)
            {
                if (!UnhookWindowsHookEx(_windowsHookHandle))
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    throw new Win32Exception(errorCode, $"Failed to remove keyboard hooks for '{Process.GetCurrentProcess().ProcessName}'. Error {errorCode}: {new Win32Exception(Marshal.GetLastWin32Error()).Message}.");
                }
                _windowsHookHandle = IntPtr.Zero;

                // ReSharper disable once DelegateSubtraction
                _hookProc -= LowLevelKeyboardProc;
            }
        }

        if (_user32LibraryHandle != IntPtr.Zero)
        {
            if (!FreeLibrary(_user32LibraryHandle)) // reduces reference to library by 1.
            {
                int errorCode = Marshal.GetLastWin32Error();
                throw new Win32Exception(errorCode, $"Failed to unload library 'User32.dll'. Error {errorCode}: {new Win32Exception(Marshal.GetLastWin32Error()).Message}.");
            }
            _user32LibraryHandle = IntPtr.Zero;
        }
    }

    ~GlobalKeyboardHook()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private IntPtr _windowsHookHandle;
    private IntPtr _user32LibraryHandle;
    private HookProc _hookProc;

    delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll")]
    private static extern IntPtr LoadLibrary(string lpFileName);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    private static extern bool FreeLibrary(IntPtr hModule);

    /// <summary>
    /// The SetWindowsHookEx function installs an application-defined hook procedure into a hook chain.
    /// You would install a hook procedure to monitor the system for certain types of events. These events are
    /// associated either with a specific thread or with all threads in the same desktop as the calling thread.
    /// </summary>
    /// <param name="idHook">hook type</param>
    /// <param name="lpfn">hook procedure</param>
    /// <param name="hMod">handle to application instance</param>
    /// <param name="dwThreadId">thread identifier</param>
    /// <returns>If the function succeeds, the return value is the handle to the hook procedure.</returns>
    [DllImport("USER32", SetLastError = true)]
    static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, int dwThreadId);

    /// <summary>
    /// The UnhookWindowsHookEx function removes a hook procedure installed in a hook chain by the SetWindowsHookEx function.
    /// </summary>
    /// <param name="hhk">handle to hook procedure</param>
    /// <returns>If the function succeeds, the return value is true.</returns>
    [DllImport("USER32", SetLastError = true)]
    public static extern bool UnhookWindowsHookEx(IntPtr hHook);

    /// <summary>
    /// The CallNextHookEx function passes the hook information to the next hook procedure in the current hook chain.
    /// A hook procedure can call this function either before or after processing the hook information.
    /// </summary>
    /// <param name="hHook">handle to current hook</param>
    /// <param name="code">hook code passed to hook procedure</param>
    /// <param name="wParam">value passed to hook procedure</param>
    /// <param name="lParam">value passed to hook procedure</param>
    /// <returns>If the function succeeds, the return value is true.</returns>
    [DllImport("USER32", SetLastError = true)]
    static extern IntPtr CallNextHookEx(IntPtr hHook, int code, IntPtr wParam, IntPtr lParam);

    [StructLayout(LayoutKind.Sequential)]
    public struct LowLevelKeyboardInputEvent
    {
        /// <summary>
        /// A virtual-key code. The code must be a value in the range 1 to 254.
        /// </summary>
        public int VirtualCode;

        /// <summary>
        /// A hardware scan code for the key. 
        /// </summary>
        public int HardwareScanCode;

        /// <summary>
        /// The extended-key flag, event-injected Flags, context code, and transition-state flag. This member is specified as follows. An application can use the following values to test the keystroke Flags. Testing LLKHF_INJECTED (bit 4) will tell you whether the event was injected. If it was, then testing LLKHF_LOWER_IL_INJECTED (bit 1) will tell you whether or not the event was injected from a process running at lower integrity level.
        /// </summary>
        public int Flags;

        /// <summary>
        /// The time stamp stamp for this message, equivalent to what GetMessageTime would return for this message.
        /// </summary>
        public int TimeStamp;

        /// <summary>
        /// Additional information associated with the message. 
        /// </summary>
        public IntPtr AdditionalInformation;
    }

    public const int WH_KEYBOARD_LL = 13;
    //const int HC_ACTION = 0;

    public enum KeyboardState
    {
        KeyDown = 0x0100,
        KeyUp = 0x0101,
        SysKeyDown = 0x0104,
        SysKeyUp = 0x0105
    }

    public const int VkSnapshot = 0x2c;
    public const int VkL = 0x4C;
    public const int VkControl = 0xA2;
    const int KfAltdown = 0x2000;
    public const int LlkhfAltdown = (KfAltdown >> 8);

    public IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam)
    {
        bool fEatKeyStroke = false;

        var wparamTyped = wParam.ToInt32();
        if (Enum.IsDefined(typeof(KeyboardState), wparamTyped))
        {
            object o = Marshal.PtrToStructure(lParam, typeof(LowLevelKeyboardInputEvent));
            LowLevelKeyboardInputEvent p = (LowLevelKeyboardInputEvent)o;

            var eventArguments = new GlobalKeyboardHookEventArgs(p, (KeyboardState)wparamTyped);

            EventHandler<GlobalKeyboardHookEventArgs> handler = KeyboardPressed;
            handler?.Invoke(this, eventArguments);

            fEatKeyStroke = eventArguments.Handled;
        }

        return fEatKeyStroke ? (IntPtr)1 : CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
    }
}

