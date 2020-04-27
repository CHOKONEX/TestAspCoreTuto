using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;

namespace WowForm
{
    public partial class Form1 : Form
    {
        AppWrapper appWrapper;
        public Form1()
        {
            InitializeComponent();

            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Interval = 1000 * 10;
            aTimer.Enabled = true;
            aTimer.Start();

            //System.Windows.Forms.SendKeys.Send("S");
            //appWrapper = new AppWrapper("notepad++", null);
            appWrapper = new AppWrapper(21616);
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Hello World!");
            appWrapper.SendKey('1');
            appWrapper.SendKey((char)9);// tab
            //appWrapper.SendKey((char)13);//enter
            appWrapper.SendKeys();

            InputSimulator sim = new InputSimulator();
            sim.Keyboard.KeyPress(VirtualKeyCode.VK_1);

        }

    }
}
