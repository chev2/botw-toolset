using BOTWToolset.Debugging;
using BOTWToolset.Exceptions;
using BOTWToolset.IO.Yaz0;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace BOTWToolset.Control
{
    /// <summary>
    /// Interaction logic for the Yaz0 tab.
    /// </summary>
    public partial class TabYaz0 : UserControl
    {
        public TabYaz0()
        {
            InitializeComponent();
        }

        private void Menu_FileDecode(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Dashboard.UserDesktop,
                RestoreDirectory = true,
                Title = "Select Yaz0 file",
                DefaultExt = "yaz0",
                Filter = "All Files (*.*)|*.*",
                CheckFileExists = true
            };

            if ((bool)openFileDialog.ShowDialog())
            {
                try
                {
                    byte[] decoded = Yaz0.Decompress(File.ReadAllBytes(openFileDialog.FileName));

                    // If decoding at source is checked, don't open a file save picker
                    if ((bool)Yaz0DecodeAtSource.IsChecked)
                    {
                        string decoded_filename = openFileDialog.FileName;
                        string ext = Path.GetExtension(openFileDialog.FileName);

                        // Replace extension when decoding
                        if (ext.StartsWith(".s"))
                        {
                            ext = ext.Replace(".s", ".");
                            decoded_filename = Path.ChangeExtension(decoded_filename, ext);
                        }

                        File.WriteAllBytes(decoded_filename, decoded);
                    }
                    else
                    {
                        var saveFileDialog = new SaveFileDialog
                        {
                            InitialDirectory = Dashboard.UserDesktop,
                            RestoreDirectory = true,
                            Title = "Save decoded Yaz0 file",
                            Filter = "All Files (*.*)|*.*"
                        };

                        if ((bool)saveFileDialog.ShowDialog())
                        {
                            File.WriteAllBytes(saveFileDialog.FileName, decoded);
                        }
                    }

                    BOTWConsole.LogStatus("Yaz0 file successfully decoded.");
                }
                catch (InvalidMagicException err)
                {
                    BOTWConsole.LogStatus(err.Message);
                }
            }
        }

        private void Menu_FileEncode(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Dashboard.UserDesktop,
                RestoreDirectory = true,
                Title = "Select file to Yaz0-encode",
                Filter = "All Files (*.*)|*.*",
                CheckFileExists = true
            };

            if ((bool)openFileDialog.ShowDialog())
            {
                try
                {
                    byte[] encoded = Yaz0.Compress(File.ReadAllBytes(openFileDialog.FileName));

                    // If decoding at source is checked, don't open a file save picker
                    if ((bool)Yaz0DecodeAtSource.IsChecked)
                    {
                        // Replace extension when encoding
                        string ext = Path.GetExtension(openFileDialog.FileName).Replace(".", ".s");
                        string encoded_filename = Path.ChangeExtension(openFileDialog.FileName, ext);
                        File.WriteAllBytes(encoded_filename, encoded);
                    }
                    else
                    {
                        var saveFileDialog = new SaveFileDialog
                        {
                            InitialDirectory = Dashboard.UserDesktop,
                            RestoreDirectory = true,
                            Title = "Save encoded Yaz0 file",
                            Filter = "All Files (*.*)|*.*"
                        };

                        if ((bool)saveFileDialog.ShowDialog())
                        {
                            File.WriteAllBytes(saveFileDialog.FileName, encoded);
                        }
                    }

                    BOTWConsole.LogStatus("Yaz0 file successfully encoded.");
                }
                catch (InvalidMagicException err)
                {
                    BOTWConsole.LogStatus(err.Message);
                }
            }
        }

        /*private void Menu_FileOpen(object sender, RoutedEventArgs e)
        {
            BOTWConsole.Log("Clicked File -> Open button");

            var openFileDialog = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                RestoreDirectory = true,
                Title = "Select Yaz0 file",
                DefaultExt = "yaz0",
                Filter = "All Files (*.*)|*.*",
                CheckFileExists = true
            };

            if ((bool)openFileDialog.ShowDialog())
            {
                // This is to ensure that opening a file when one is already open resets everything in the tab
                SetDisabled();

                BOTWConsole.Log("Opening file");

                Yaz0 y = Yaz0.ReadFile(openFileDialog.FileName);

                // Set the current file location to the chosen file's location
                fileLocation = openFileDialog.FileName;

                currentYaz0 = y;

                // Set UI sidebar to have header info, enable controls
                SetEnabled(y);

                // Allow the file to be saved
                MenuFileClose.IsEnabled = true;
                MenuFileSave.IsEnabled = true;
                MenuFileSaveAs.IsEnabled = true;
            }
        }*/
    }
}
