using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace BOTWToolset.Debugging
{
    /// <summary>
    /// Allows for printing debug info to an in-app console.
    /// </summary>
    static class BOTWConsole
    {
        private readonly static TextBox _console;
        private readonly static Label _status;

        private static DispatcherTimer ClearStatusTimer;

        static BOTWConsole()
        {
            var dashboard = Application.Current.Windows.OfType<Dashboard>().ToArray()[0];
            var tabControl = dashboard.tabTSCB;
            _console = tabControl.TSCBConsole;

            _status = dashboard.LabelStatus;
        }

        public static void Log(object text)
        {
            _console.Text += text.ToString() + "\n";
            _console.ScrollToEnd(); // After adding the new text, scroll to the end
        }

        public static void LogWarning(object text)
        {
            Log("[Warning]" + text);
        }

        public static void LogError(object text)
        {
            Log("[ERROR]" + text);
        }

        public static void LogStatus(object text)
        {
            if (ClearStatusTimer != null)
                ClearStatusTimer.Stop();

            _status.Content = text.ToString();

            ClearStatusTimer = new DispatcherTimer();
            ClearStatusTimer.Tick += ClearStatus;
            ClearStatusTimer.Interval = new TimeSpan(0, 0, 4);
            ClearStatusTimer.Start();
        }

        public static void ClearStatus(object src, EventArgs e)
        {
            _status.Content = "";
        }
    }
}
