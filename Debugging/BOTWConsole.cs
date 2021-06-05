using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BOTWToolset.Debugging
{
    /// <summary>
    /// Allows for printing debug info to an in-app console.
    /// </summary>
    static class BOTWConsole
    {
        private static readonly Label _status;

        // Timer which automatically clears the status bar - currently unused
        //private static DispatcherTimer ClearStatusTimer;

        static BOTWConsole()
        {
            var dashboard = Application.Current.Windows.OfType<Dashboard>().ToArray()[0];

            _status = dashboard.LabelStatus;
        }

        /// <summary>
        /// Sets the status bar text.
        /// </summary>
        /// <param name="text">The text to display.</param>
        public static void LogStatus(object text)
        {
            /*if (ClearStatusTimer != null)
                ClearStatusTimer.Stop();*/

            _status.Content = text.ToString();

            /*ClearStatusTimer = new DispatcherTimer();
            ClearStatusTimer.Tick += (src, e) => { ClearStatus(); };
            ClearStatusTimer.Interval = new TimeSpan(0, 0, 4);
            ClearStatusTimer.Start();*/
        }
        public static void LogWarning(object text)
        {
            LogStatus("[Warning]" + text);
        }

        public static void LogError(object text)
        {
            LogStatus("[ERROR]" + text);
        }

        /// <summary>
        /// Clears the status bar text.
        /// </summary>
        public static void ClearStatus()
        {
            _status.Content = "";
        }
    }
}
