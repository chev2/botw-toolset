using BOTWToolset.Debugging;
using BOTWToolset.IO.TSCB;
using Microsoft.Win32;
using System.Windows.Controls;

namespace BOTWToolset
{
    /// <summary>
    /// Control tab for TSCB management
    /// </summary>
    public partial class TabTSCB : UserControl
    {
        public static string fileLocation;
        public static TSCBInfo currentTSCBInfo;

        public TabTSCB()
        {
            InitializeComponent();
        }

        public void SetSidebarHeaderInfo(TSCBInfo t)
        {
            Signature.Text = t.Signature;
            Version.Text = t.Version.ToString() + ".0.0.0"; // TODO: An actual version based off bytes should be done later
            FileBaseOffset.Text = t.FileBaseOffset.ToString();
            WorldScale.Text = t.WorldScale.ToString();
            TerrainMaxHeight.Text = t.TerrainMaxHeight.ToString();
            MaterialInfoLength.Text = t.MaterialInfoLength.ToString();
            AreaArrayLength.Text = t.AreaArrayLength.ToString();
            TileSize.Text = t.TileSize.ToString();
        }

        public void ClearAllTabInfo()
        {
            // Clear sidebar header info
            Signature.Clear();
            Version.Clear();
            FileBaseOffset.Clear();
            WorldScale.Clear();
            TerrainMaxHeight.Clear();
            MaterialInfoLength.Clear();
            AreaArrayLength.Clear();
            TileSize.Clear();

            // Clear area display stack panel
            TSCBAreaViewer.Children.Clear();
        }

        private void Menu_OpenFile(object sender, System.Windows.RoutedEventArgs e)
        {
            BOTWConsole.Log("Clicked File -> Open button");

            var openFileDialog = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                RestoreDirectory = true,
                Title = "Select TSCB file",
                DefaultExt = "tscb",
                Filter = "TSCB files (*.tscb)|*.tscb",
                CheckFileExists = true
            };

            if ((bool)openFileDialog.ShowDialog())
            {
                BOTWConsole.Log("Opening file");

                TSCBInfo t = TSCB.ReadFile(openFileDialog.FileName);

                // Set the current file location to the chosen file's location
                fileLocation = openFileDialog.FileName;

                // Set the current TCSBInfo to the new TSCBInfo
                currentTSCBInfo = t;

                // Set UI sidebar to have header info
                SetSidebarHeaderInfo(t);

                // Allow the file to be saved
                MenuFileSave.IsEnabled = true;
            }
        }

        private void Menu_CloseFile(object sender, System.Windows.RoutedEventArgs e)
        {
            BOTWConsole.Log("Clicked File -> Close button");

            // Set the current TSCB info to nothing
            currentTSCBInfo = null;

            // Clear sidebar header info
            ClearAllTabInfo();

            // Since there's no file open, don't allow saving
            MenuFileSave.IsEnabled = false;
        }

        private void Menu_SaveFile(object sender, System.Windows.RoutedEventArgs e)
        {
            BOTWConsole.Log("Clicked File -> Save button");
        }
    }
}
