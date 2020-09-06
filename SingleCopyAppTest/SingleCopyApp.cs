using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ai.SingleCopyAppTest
{
    public class SingleCopyApp : IDisposable
    {
        private static readonly string APP_ALREADY_RUNNING_CAPTION = "Warning!";
        private static readonly string APP_ALREADY_RUNNING_MESSAGE = "Apllication has been started already. Second copy cannot be started!";

        private readonly Guid guid;

        private Mutex mutex = null;

        public SingleCopyApp(Guid guid)
        {
            this.guid = guid;
        }

        #region .NET Dispose stuff

        private bool isDisposed = false;

        ~SingleCopyApp()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);

            // tell the GC not to finalize
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed) // only dispose once!
            {
                if (disposing && mutex != null)
                    mutex.Close();

                isDisposed = true;
            }
        }

        #endregion

        public bool InitLock(string appName, bool doFlash = true, bool restoreApp = false, bool showMessage = true)
        {
            mutex = new Mutex(true, String.Format("{0}___{1}", appName, guid.ToString("D")), out bool isCreatedNew);

            if (isCreatedNew)
                return false;

            if (showMessage)
            {
                MessageBox.Show(APP_ALREADY_RUNNING_MESSAGE, APP_ALREADY_RUNNING_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }

            IntPtr wndHandle = IntPtr.Zero;

            foreach (Process p in Process.GetProcesses())
            {
                if (p.ProcessName.Contains(appName))
                {
                    wndHandle = p.MainWindowHandle;
                    break;
                }
            }

            if (wndHandle.Equals(IntPtr.Zero))
                return true;

            if (doFlash)
            {
                FLASHWINFO info = new FLASHWINFO
                {
                    hwnd = wndHandle,
                    dwFlags = FLASHW_TRAY,
                    uCount = 3,
                    dwTimeout = 0
                };
                info.cbSize = Convert.ToUInt32(Marshal.SizeOf(info));

                FlashWindowEx(ref info);
            }

            if (restoreApp)
            {
                ShowWindow(wndHandle, SW_RESTORE);
                SetForegroundWindow(wndHandle);
            }

            return true;
        }

        #region Window API Stuff

        private const UInt32 FLASHW_STOP = 0; //Stop flashing. The system restores the window to its original state.
        private const UInt32 FLASHW_CAPTION = 1; //Flash the window caption.
        private const UInt32 FLASHW_TRAY = 2; //Flash the taskbar button.
        private const UInt32 FLASHW_ALL = 3; //Flash both the window caption and taskbar button.
        private const UInt32 FLASHW_TIMER = 4; //Flash continuously, until the FLASHW_STOP flag is set.
        private const UInt32 FLASHW_TIMERNOFG = 12; //Flash continuously until the window comes to the foreground.

        private const UInt32 SW_FORCEMINIMIZE = 11; //Minimizes a window, even if the thread that owns the window is not responding. This flag should only be used when minimizing windows from a different thread.
        private const UInt32 SW_HIDE = 0; //Hides the window and activates another window.
        private const UInt32 SW_MAXIMIZE = 3; //Maximizes the specified window.
        private const UInt32 SW_MINIMIZE = 6; //Minimizes the specified window and activates the next top-level window in the Z order.
        private const UInt32 SW_RESTORE = 9; //Activates and displays the window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when restoring a minimized window.
        private const UInt32 SW_SHOW = 5; //Activates the window and displays it in its current size and position.
        private const UInt32 SW_SHOWDEFAULT = 10; //Sets the show state based on the SW_ value specified in the STARTUPINFO structure passed to the CreateProcess function by the program that started the application.
        private const UInt32 SW_SHOWMAXIMIZED = 3; //Activates the window and displays it as a maximized window.
        private const UInt32 SW_SHOWMINIMIZED = 2; //Activates the window and displays it as a minimized window.
        private const UInt32 SW_SHOWMINNOACTIVE = 7; //Displays the window as a minimized window. This value is similar to SW_SHOWMINIMIZED, except the window is not activated.
        private const UInt32 SW_SHOWNA = 8; //Displays the window in its current size and position. This value is similar to SW_SHOW, except that the window is not activated.
        private const UInt32 SW_SHOWNOACTIVATE = 4; //Displays a window in its most recent size and position. This value is similar to SW_SHOWNORMAL, except that the window is not activated.
        private const UInt32 SW_SHOWNORMAL = 1; //Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when displaying the window for the first time.

        [StructLayout(LayoutKind.Sequential)]
        private struct FLASHWINFO
        {
            public UInt32 cbSize; //The size of the structure in bytes.
            public IntPtr hwnd; //A Handle to the Window to be Flashed. The window can be either opened or minimized.
            public UInt32 dwFlags; //The Flash Status.
            public UInt32 uCount; // number of times to flash the window
            public UInt32 dwTimeout; //The rate at which the Window is to be flashed, in milliseconds. If Zero, the function uses the default cursor blink rate.
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        [DllImport("user32.dll")]
        private static extern Int32 SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, UInt32 nCmdShow);

        #endregion
    }
}
