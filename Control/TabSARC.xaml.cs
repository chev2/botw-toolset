using BOTWToolset.Debugging;
using BOTWToolset.IO.SARC;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace BOTWToolset.Control
{
    /// <summary>
    /// Interaction logic for the SARC tab.
    /// </summary>
    public partial class TabSARC : UserControl
    {
        public static string fileLocation;
        public static SARC currentSARC;

        public TabSARC()
        {
            InitializeComponent();
        }

        public void SetEnabled(SARC s)
        {
            // SARC header
            SARCMagic.Text = s.Magic;
            SARCHeaderLength.Text = s.HeaderLength.ToString();
            SARCIsBigEndian.Text = s.IsBigEndian ? "Yes" : "No";
            SARCFileSize.Text = s.FileSize.ToString();
            SARCDataOffset.Text = s.DataOffset.ToString();
            SARCVersion.Text = s.Version.ToString();

            // SFAT header
            SFATMagic.Text = s.SFAT.Magic;
            SFATHeaderLength.Text = s.SFAT.HeaderLength.ToString();
            SFATNodeCount.Text = s.SFAT.NodeCount.ToString();
            SFATHashKey.Text = s.SFAT.HashKey.ToString();

            // SFNT header
            SFNTMagic.Text = s.SFNT.Magic;
            SFNTHeaderLength.Text = s.SFNT.HeaderLength.ToString();

            // Show file count
            FileCount.Content = $"SARC file count: {s.SFAT.NodeCount}";

            for (int i = 0; i < s.SFNT.FileNames.Length; i++)
            {
                string file_name = s.SFNT.FileNames[i];
                int file_size = s.Files[i].Length;

                Grid grid = new Grid
                {
                    Height = 112
                };

                Label title = new Label
                {
                    Height = 44,
                    Content = file_name,
                    FontSize = 20,
                    Margin = new Thickness(10, 10, 10, 0),
                    VerticalAlignment = VerticalAlignment.Top,
                    VerticalContentAlignment = VerticalAlignment.Center
                };

                Label desc = new Label
                {
                    Content = $"{file_size:n0} bytes",
                    Margin = new Thickness(10, 59, 10, 10),
                    FontSize = 16,
                    VerticalContentAlignment = VerticalAlignment.Center
                };

                grid.Children.Add(title);
                grid.Children.Add(desc);

                FileDisplay.Children.Add(grid);
            }
        }

        public void SetDisabled()
        {
            // SARC header
            SARCMagic.Clear();
            SARCHeaderLength.Clear();
            SARCIsBigEndian.Clear();
            SARCFileSize.Clear();
            SARCDataOffset.Clear();
            SARCVersion.Clear();

            // SFAT header
            SFATMagic.Clear();
            SFATHeaderLength.Clear();
            SFATNodeCount.Clear();
            SFATHashKey.Clear();

            // SFNT header
            SFNTMagic.Clear();
            SFNTHeaderLength.Clear();

            // SARC Files
            FileCount.Content = "";
            FileDisplay.Children.Clear();
        }

        private void Menu_FileOpen(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                RestoreDirectory = true,
                Title = "Select SARC file",
                DefaultExt = "sarc",
                Filter = "SARC files (*.arc;*.sarc;*.blarc;*.bgenv;*.genvb;*.pack;*.bars;*.stera)|*.arc;*.sarc;*.blarc;*.bgenv;*.genvb;*.pack;*.bars;*.stera|All files (*.*)|*.*",
                CheckFileExists = true
            };

            if ((bool)openFileDialog.ShowDialog())
            {
                SARC s = SARC.FromBytes(File.ReadAllBytes(openFileDialog.FileName));

                // Set the current file location to the chosen file's location
                fileLocation = openFileDialog.FileName;

                currentSARC = s;

                // Display info in the tab
                SetEnabled(s);

                // Enable file edits
                MenuFileClose.IsEnabled = true;
                MenuFileSave.IsEnabled = true;
                MenuFileSaveAs.IsEnabled = true;
                MenuFileExportFiles.IsEnabled = true;
            }
        }

        private void Menu_FileClose(object sender, RoutedEventArgs e)
        {
            fileLocation = null;

            // Set the current TSCB info to nothing
            currentSARC = null;

            // Set the tab as disabled
            SetDisabled();

            // Disable file edits
            MenuFileClose.IsEnabled = false;
            MenuFileSave.IsEnabled = false;
            MenuFileSaveAs.IsEnabled = false;
            MenuFileExportFiles.IsEnabled = false;
        }

        private void Menu_FileSave(object sender, RoutedEventArgs e)
        {
            BOTWConsole.LogStatus($"Saved SARC to {fileLocation}.");

            File.WriteAllBytes(fileLocation, SARC.ToBytes(currentSARC));
        }

        private void Menu_FileSaveAs(object sender, RoutedEventArgs e)
        {
            BOTWConsole.LogStatus($"Saving SARC as...");

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = @"C:\",
                RestoreDirectory = true,
                Title = "Save SARC file",
                Filter = "All files (*.*)|*.*"
            };

            if ((bool)saveFileDialog.ShowDialog())
            {
                File.WriteAllBytes(saveFileDialog.FileName, SARC.ToBytes(currentSARC));

                BOTWConsole.LogStatus($"Saved SARC to {saveFileDialog.FileName}.");
            }
        }

        private void Menu_FileExportFiles(object sender, RoutedEventArgs e)
        {
            BOTWConsole.LogStatus($"Exporting files to folder...");

            CommonOpenFileDialog openFolderDialog = new CommonOpenFileDialog
            {
                InitialDirectory = @"C:\",
                RestoreDirectory = true,
                Title = "Export files to folder",
                IsFolderPicker = true
            };

            if (openFolderDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                SARC.WriteFiles(currentSARC, openFolderDialog.FileName);

                BOTWConsole.LogStatus($"Exported SARC files to {openFolderDialog.FileName}.");
            }
        }
    }
}
