using BOTWToolset.Debugging;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace BOTWToolset
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public static string VERSION = System.Diagnostics.FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;

        public static string UserDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public Dashboard()
        {
            InitializeComponent();

            BOTWConsole.Log($"Breath of the Wild Toolkit - Version {VERSION}");

            LabelVersion.Content = $"Version v{VERSION}";
        }

        private void TabSelect(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var sender_border = (Border)sender;

            switch (sender_border.Name)
            {
                case "DashboardButtonTSCB": tabItemTSCB.IsSelected = true; break;
                case "DashboardButtonYaz0": tabItemYaz0.IsSelected = true; break;
                case "DashboardButtonSARC": tabItemSARC.IsSelected = true; break;
                case "DashboardButtonRSTB": tabItemRSTB.IsSelected = true; break;
            }
        }
    }
}
