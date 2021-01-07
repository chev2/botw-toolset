using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BOTWToolset.Debugging
{
    static class BOTWConsole
    {
        private readonly static TextBox _console;
        static BOTWConsole()
        {
            var dashboard = Application.Current.Windows.OfType<Dashboard>().ToArray()[0];
            var tabControl = dashboard.tabTSCB;
            _console = tabControl.TSCBConsole;
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
    }
}
