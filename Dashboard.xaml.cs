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
        // Get the app version from the assembly info
        public static readonly string VERSION = System.Diagnostics.FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;

        // Get the user's desktop path
        public static readonly string UserDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        // All tabs used in the toolset
        // Adding a new tool to the program requires its tab control to be added here
        public static readonly UserControl[] toolsetTabs = new UserControl[]
        {
            new Control.TabTSCB(),
            new Control.TabYaz0(),
            new Control.TabSARC(),
            new Control.TabRSTB()
        };

        public Dashboard()
        {
            InitializeComponent();

            LabelVersion.Content = $"Version v{VERSION}";

            // Initialize and add all tool tabs to the tab controller
            foreach (UserControl tab in toolsetTabs)
            {
                // Initialize the new tab
                TabItem tabItem = new TabItem
                {
                    // Set the tab's content to whatever the tool's control is
                    Content = tab,
                    // Give the header (tab name) a proper name based off its class name
                    Header = tab.GetType().Name.Replace("Tab", "")
                };

                TabController.Items.Add(tabItem);
            }
        }

        /// <summary>
        /// Selects the tab depending on what control was clicked on.
        /// </summary>
        private void TabSelect(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var sender_border = (Border)sender;

            // Get the name of the tab from the dashboard button's name
            string tab_name = sender_border.Name.Replace("DashboardButton", "");

            foreach (TabItem tab in TabController.Items)
            {
                string tab_control_name = tab.Content.GetType().Name.Replace("Tab", "");

                if (tab_name == tab_control_name)
                {
                    tab.IsSelected = true;
                    break;
                }
            }
        }
    }
}
