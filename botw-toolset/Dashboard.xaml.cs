using BOTWToolset.IO.TSCB;
using System.Windows;
using BOTWToolset.Debugging;

namespace BOTWToolset
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public const string VERSION = "1.0.0-pre-alpha";

        public Dashboard()
        {
            InitializeComponent();

            BOTWConsole.Log($"Breath of the Wild Toolkit - Version {VERSION}");

            LabelVersion.Content = $"Version v{VERSION}";
        }
    }
}
