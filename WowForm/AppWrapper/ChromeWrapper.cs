using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace WowForm
{
    public class AppWrapper
    {
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        // the keystroke signals. you can look them up at the msdn pages
        private static uint WM_KEYDOWN = 0x100, WM_KEYUP = 0x101;

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, String lpWindowName);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern uint RegisterWindowMessage(string lpString);

        // the reference to the chrome process
        private List<Process> Processes = new List<Process>();

        public AppWrapper(string appExeName, string arguments)
        {
            List<Process> processes = Process.GetProcessesByName(appExeName).ToList();
            foreach (Process process in processes)
            {
                if (process.MainWindowHandle == IntPtr.Zero)// the chrome process must have a window
                    continue;
                Processes.Add(process); //now you have a handle to the main chrome (either a new one or the one that was already open).
                return;
            }
        }

        public AppWrapper(int pid)
        {
            List<Process> processes = Process.GetProcesses().ToList();
            foreach (Process process in processes.Where(x=> x.Id == pid))
            {
                if (process.MainWindowHandle == IntPtr.Zero)// the chrome process must have a window
                    continue;
                Processes.Add(process); //now you have a handle to the main chrome (either a new one or the one that was already open).
                return;
            }
        }

        public void SendKey(char key)
        {
            try
            {
                foreach (Process process in Processes)
                {
                    if (process.MainWindowHandle != IntPtr.Zero)
                    {
                        // send the keydown signal
                        SendMessage(process.MainWindowHandle, WM_KEYDOWN, (IntPtr)key, IntPtr.Zero);

                        // give the process some time to "realize" the keystroke
                        System.Threading.Thread.Sleep(100);

                        // send the keyup signal
                        SendMessage(process.MainWindowHandle, WM_KEYUP, (IntPtr)key, IntPtr.Zero);
                    }
                }
            }
            catch (Exception e) //without the GetProcessesByName you'd get an exception.
            {
            }
        }

        public void SendKeys()
        {
            try
            {
                uint id = 1;
                IntPtr WindowToFind = Processes.First().MainWindowHandle;
                Debug.Assert(WindowToFind != IntPtr.Zero);
                SendMessage(WindowToFind, id, IntPtr.Zero, IntPtr.Zero);
            }
            catch (Exception e) //without the GetProcessesByName you'd get an exception.
            {
            }
        }
    }
}
