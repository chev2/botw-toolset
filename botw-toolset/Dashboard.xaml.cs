using BOTWToolset.Debugging;
using System;
using System.Windows;

namespace BOTWToolset
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public const string VERSION = "1.0.0-pre-alpha";
        public static string UserDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public Dashboard()
        {
            InitializeComponent();

            BOTWConsole.Log($"Breath of the Wild Toolkit - Version {VERSION}");

            LabelVersion.Content = $"Version v{VERSION}";
        }

        private void TabSelect_TSCB(object sender, System.Windows.Input.MouseButtonEventArgs e) => tabItemTSCB.IsSelected = true;

        private void TabSelect_Yaz0(object sender, System.Windows.Input.MouseButtonEventArgs e) => tabItemYaz0.IsSelected = true;

        private void TabSelect_SARC(object sender, System.Windows.Input.MouseButtonEventArgs e) => tabItemSARC.IsSelected = true;

        private void TabSelect_RSTB(object sender, System.Windows.Input.MouseButtonEventArgs e) => tabItemRSTB.IsSelected = true;
    }
}
