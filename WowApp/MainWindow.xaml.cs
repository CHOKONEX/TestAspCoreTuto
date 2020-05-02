using System;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace WowApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AppWrapper appWrapper;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Interval = 1000 * 10;
            aTimer.Enabled = true;
            aTimer.Start();

            //System.Windows.Forms.SendKeys.Send("S");
            //appWrapper = new AppWrapper("notepad++", null);
            //appWrapper = new AppWrapper(21616);
            appWrapper = new AppWrapper("chrome", null);
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Hello World!");
            //appWrapper.SendKey((char)1);
            //appWrapper.SendKey((char)26);
            //appWrapper.SendKey((char)9);// tab
            //appWrapper.SendKey((char)13);//enter
            //appWrapper.SendKeys();

            string appName = "chrome";

            appWrapper.SendKey(appName, (char)49);
            appWrapper.SendKey(appName, (char)32);
            appWrapper.SendKey(appName, (char)65);
            appWrapper.SendKey(appName, (char)32);
            appWrapper.SendKey(appName, (char)97);

            appWrapper.SendKey(appName, (char)9);// tab
            appWrapper.SendKey(appName, (char)13);//enter

            //appWrapper.SendKey("chrome", Key.D1);
            //appWrapper.SendKey("chrome", Key.A);
            //appWrapper.SendKey("chrome", Key.Tab);// tab
            //appWrapper.SendKey("chrome", Key.Enter);//enter

            //Application.Current.Dispatcher.Invoke(() =>
            //{
            //    SendKeys.Send(Key.D1);
            //});
        }

        public static class SendKeys
        {
            /// <summary>
            ///   Sends the specified key.
            /// </summary>
            /// <param name="key">The key.</param>
            public static void Send(Key key)
            {
                if (Keyboard.PrimaryDevice != null)
                {
                    if (Keyboard.PrimaryDevice.ActiveSource != null)
                    {
                        var e1 = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, Key.Down)
                        {
                            RoutedEvent = Keyboard.KeyDownEvent
                        };
                        InputManager.Current.ProcessInput(e1);
                    }
                }
            }
        }
    }
}
