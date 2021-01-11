using BOTWToolset.Debugging;
using BOTWToolset.IO;
using BOTWToolset.IO.EXTM;
using BOTWToolset.IO.SARC;
using BOTWToolset.IO.TSCB;
using BOTWToolset.IO.Yaz0;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BOTWToolset.Control
{
    /// <summary>
    /// Control tab for TSCB management
    /// </summary>
    public partial class TabTSCB : UserControl
    {
        public static string fileLocation;
        public static TSCB currentTSCB;

        public static WriteableBitmap writeableBitmap; //used for pixel-like display

        public TabTSCB()
        {
            InitializeComponent();

            RenderOptions.SetBitmapScalingMode(PixelView, BitmapScalingMode.NearestNeighbor); // Nearest-neighbor sampling
            //RenderOptions.SetEdgeMode(i, EdgeMode.Unspecified);

            //Placeholder image
            writeableBitmap = new WriteableBitmap(4, 4, 16, 16, PixelFormats.Rgb24, null);

            PixelView.Source = writeableBitmap;
            PixelView.Stretch = Stretch.Uniform;

            PixelView.MouseLeftButtonDown += new MouseButtonEventHandler(PixelView_MouseLeft);
            //PixelViewBorder.MouseWheel += new MouseWheelEventHandler(PixelViewBorder_MouseWheel);
        }

        private void ClearBitmap()
        {
            PixelView.Source = null;
            writeableBitmap = null;
            GC.Collect();
        }

        private void DrawPixel(MouseEventArgs e)
        {

        }

        private void ErasePixel(MouseEventArgs e)
        {

        }

        private void PixelView_MouseLeft(object sender, MouseButtonEventArgs e)
        {
            DrawPixel(e);
        }

        public void PixelViewBorder_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Matrix m = PixelView.RenderTransform.Value;

            if (e.Delta > 0)
            {
                m.ScaleAt(1.5f, 1.5f, e.GetPosition(PixelViewBorder).X, e.GetPosition(PixelViewBorder).Y);
            }
            else
            {
                m.ScaleAt(2 / 3, 2 / 3, e.GetPosition(PixelViewBorder).X, e.GetPosition(PixelViewBorder).Y);
            }

            PixelView.RenderTransform = new MatrixTransform(m);
        }

        private void PixelView_UpdateView(object sender, RoutedEventArgs e)
        {
            byte zoom = (byte)SliderZoomLevel.Value;

            // Get the parent directory of the files directory
            DirectoryInfo parent = Directory.GetParent(fileLocation);
            string main_field_dir = Path.Combine(parent.FullName, "MainField");

            Button sender_button = (Button)sender;

            if (Directory.Exists(main_field_dir))
            {
                switch (sender_button.Name)
                {
                    case "PixelViewMATE":
                        {
                            string extension = ".mate.sstera";
                            int img_size = (int)Math.Pow(2, 8 + zoom);

                            ClearBitmap();
                            writeableBitmap = new WriteableBitmap(img_size, img_size, 16, 16, PixelFormats.Rgb24, null);
                            PixelView.Source = writeableBitmap;

                            PixelView_SetMATE(main_field_dir, extension, zoom, 256);
                        }
                        break;
                    case "PixelViewHGHT":
                        {
                            string extension = ".hght.sstera";
                            int img_size = (int)Math.Pow(2, 8 + zoom);

                            ClearBitmap();
                            writeableBitmap = new WriteableBitmap(img_size, img_size, 16, 16, PixelFormats.Gray8, null);
                            PixelView.Source = writeableBitmap;

                            PixelView_SetHGHT(main_field_dir, extension, zoom, 256);
                        }
                        break;
                    case "PixelViewGrassEXTM":
                        {
                            string extension = ".grass.extm.sstera";
                            int img_size = (int)Math.Pow(2, 6 + zoom);

                            ClearBitmap();
                            writeableBitmap = new WriteableBitmap(img_size, img_size, 16, 16, PixelFormats.Rgb24, null);
                            PixelView.Source = writeableBitmap;

                            PixelView_SetGrassEXTM(main_field_dir, extension, zoom, 64);
                        }
                        break;
                    case "PixelViewWaterEXTM":
                        {
                            string extension = ".water.extm.sstera";
                            int img_size = (int)Math.Pow(2, 6 + zoom);

                            ClearBitmap();
                            writeableBitmap = new WriteableBitmap(img_size, img_size, 16, 16, PixelFormats.Rgb24, null);
                            PixelView.Source = writeableBitmap;

                            PixelView_SetWaterEXTM(main_field_dir, extension, zoom, 64);
                        }
                        break;
                }
            }
        }

        private void PixelView_SetMATE(string folder, string extension, byte zoom_level, int grid_size)
        {
            BOTWConsole.Log("Setting view to texture");
            string[] ext_files = Directory.GetFiles(folder, $"5{zoom_level}*{extension}");

            foreach (string ext_file in ext_files)
            {
                byte[] yaz0_bytes = File.ReadAllBytes(Path.Combine(folder, ext_file));
                byte[] yaz0_decomp = Yaz0.Decompress(yaz0_bytes);
                SARC s = SARC.FromBytes(new MemoryStream(yaz0_decomp));

                for (int i = 0; i < s.Files.Length; i++)
                {
                    // Get grid index from filename
                    string grid_index_str = s.SFNT.FileNames[i].Replace($"5{zoom_level}", "").Replace(extension.Replace(".sstera", ""), "").Replace(folder + "\\", "");
                    int grid_index = int.Parse(grid_index_str, System.Globalization.NumberStyles.HexNumber);
                    int[] xyoffs = GridConverter.ZCurveToXY(grid_index);
                    MATE[] mats = MATE.FromBytes(s.Files[i]);

                    for (int j = 0; j < mats.Length; j++)
                    {
                        MATE m = mats[j];

                        int x = j % grid_size;
                        int y = j / grid_size;

                        // Draw pixel at the correct X, Y coordinate
                        Int32Rect rect = new Int32Rect(x + (xyoffs[0] * grid_size), y + (xyoffs[1] * grid_size), 1, 1);
                        // Get this water's color based off its material index/water type
                        System.Drawing.Color c = GridColors.MaterialColors[m.Material0];
                        // Use water type color + brightness adjust
                        byte[] color = new byte[] { c.R, c.G, c.B };

                        writeableBitmap.WritePixels(rect, color, writeableBitmap.BackBufferStride, 0);
                    }
                }
            }
        }

        private void PixelView_SetHGHT(string folder, string extension, byte zoom_level, int grid_size)
        {
            BOTWConsole.Log("Setting view to heightmap");
            string[] ext_files = Directory.GetFiles(folder, $"5{zoom_level}*{extension}");

            foreach (string ext_file in ext_files)
            {
                byte[] yaz0_bytes = File.ReadAllBytes(Path.Combine(folder, ext_file));
                byte[] yaz0_decomp = Yaz0.Decompress(yaz0_bytes);
                SARC s = SARC.FromBytes(new MemoryStream(yaz0_decomp));

                for (int i = 0; i < s.Files.Length; i++)
                {
                    // Get grid index from filename
                    string grid_index_str = s.SFNT.FileNames[i].Replace($"5{zoom_level}", "").Replace(extension.Replace(".sstera", ""), "").Replace(folder + "\\", "");
                    int grid_index = int.Parse(grid_index_str, System.Globalization.NumberStyles.HexNumber);
                    int[] xyoffs = GridConverter.ZCurveToXY(grid_index);
                    HGHT h = HGHT.FromBytes(s.Files[i]);

                    for (int j = 0; j < h.Heights.Length; j++)
                    {
                        ushort height = h.Heights[j];

                        int x = j % grid_size;
                        int y = j / grid_size;

                        // Draw pixel at the correct X, Y coordinate
                        Int32Rect rect = new Int32Rect(x + (xyoffs[0] * grid_size), y + (xyoffs[1] * grid_size), 1, 1);
                        // Brightness
                        byte brightness = (byte)(height / 65535f * 255f);
                        // Use water type color + brightness adjust
                        byte[] color = new byte[] { brightness };

                        writeableBitmap.WritePixels(rect, color, writeableBitmap.BackBufferStride, 0);
                    }
                }
            }
        }

        private void PixelView_SetGrassEXTM(string folder, string extension, byte zoom_level, int grid_size)
        {
            BOTWConsole.Log("Setting view to grass");
            string[] ext_files = Directory.GetFiles(folder, $"5{zoom_level}*{extension}");

            foreach (string ext_file in ext_files)
            {
                byte[] yaz0_bytes = File.ReadAllBytes(Path.Combine(folder, ext_file));
                byte[] yaz0_decomp = Yaz0.Decompress(yaz0_bytes);
                SARC s = SARC.FromBytes(new MemoryStream(yaz0_decomp));

                for (int i = 0; i < s.Files.Length; i++)
                {
                    // Get grid index from filename
                    string grid_index_str = s.SFNT.FileNames[i].Replace($"5{zoom_level}", "").Replace(extension.Replace(".sstera", ""), "").Replace(folder + "\\", "");
                    int grid_index = int.Parse(grid_index_str, System.Globalization.NumberStyles.HexNumber);
                    int[] xyoffs = GridConverter.ZCurveToXY(grid_index);
                    Grass[] grasses = Grass.FromBytes(s.Files[i]);

                    for (int j = 0; j < grasses.Length; j++)
                    {
                        Grass g = grasses[j];

                        int x = j % grid_size;
                        int y = j / grid_size;

                        // Draw pixel at the correct X, Y coordinate
                        Int32Rect rect = new Int32Rect(x + (xyoffs[0] * grid_size), y + (xyoffs[1] * grid_size), 1, 1);
                        // Brightness - clamp values so colors don't become completely muted
                        float brightness = (g.Height / 255f);
                        // Use water type color + brightness adjust
                        byte[] color = new byte[] { (byte)(g.R * brightness), (byte)(g.G * brightness), (byte)(g.B * brightness) };

                        writeableBitmap.WritePixels(rect, color, writeableBitmap.BackBufferStride, 0);
                    }
                }
            }
        }

        private void PixelView_SetWaterEXTM(string folder, string extension, byte zoom_level, int grid_size)
        {
            BOTWConsole.Log("Setting view to water");
            string[] ext_files = Directory.GetFiles(folder, $"5{zoom_level}*{extension}");

            foreach (string ext_file in ext_files)
            {
                byte[] yaz0_bytes = File.ReadAllBytes(Path.Combine(folder, ext_file));
                byte[] yaz0_decomp = Yaz0.Decompress(yaz0_bytes);
                SARC s = SARC.FromBytes(new MemoryStream(yaz0_decomp));

                for (int i = 0; i < s.Files.Length; i++)
                {
                    // Get grid index from filename
                    string grid_index_str = s.SFNT.FileNames[i].Replace($"5{zoom_level}", "").Replace(extension.Replace(".sstera", ""), "").Replace(folder + "\\", "");
                    int grid_index = int.Parse(grid_index_str, System.Globalization.NumberStyles.HexNumber);
                    int[] xyoffs = GridConverter.ZCurveToXY(grid_index);
                    Water[] waters = Water.FromBytes(s.Files[i]);

                    for (int j = 0; j < waters.Length; j++)
                    {
                        Water w = waters[j];

                        int x = j % grid_size;
                        int y = j / grid_size;

                        // Draw pixel at the correct X, Y coordinate
                        Int32Rect rect = new Int32Rect(x + (xyoffs[0] * grid_size), y + (xyoffs[1] * grid_size), 1, 1);
                        // Brightness - clamp values so colors don't become completely muted
                        float brightness = (w.Height / 65535f).Clamp(0.2f, 0.8f);
                        // Get this water's color based off its material index/water type
                        System.Drawing.Color c = GridColors.WaterColors[w.MaterialIndex];
                        // Use water type color + brightness adjust
                        byte[] color = new byte[] { (byte)(c.R * brightness), (byte)(c.G * brightness), (byte)(c.B * brightness) };

                        writeableBitmap.WritePixels(rect, color, writeableBitmap.BackBufferStride, 0);
                    }
                }
            }
        }

        public void SetEnabled(TSCB t)
        {
            Signature.Text = t.Signature;
            Version.Text = t.Version.ToString() + ".0.0.0";
            FileBaseOffset.Text = t.FileBaseOffset.ToString();
            WorldScale.Text = t.WorldScale.ToString();
            TerrainMaxHeight.Text = t.TerrainMaxHeight.ToString();
            MaterialInfoLength.Text = t.MaterialInfoLength.ToString();
            AreaArrayLength.Text = t.AreaArrayLength.ToString();
            TileSize.Text = t.TileSize.ToString();

            foreach (var child in PixelViewTypes.Children)
            {
                if (child.GetType() == typeof(Button))
                {
                    ((Button)child).IsEnabled = true;
                }
                else if (child.GetType() == typeof(TextBox))
                {
                    ((TextBox)child).IsEnabled = true;
                }
            }
        }

        public void SetDisabled()
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

            // Clear pixel view
            ClearBitmap();

            // Disable controls
            foreach (var child in PixelViewTypes.Children)
            {
                if (child.GetType() == typeof(Button))
                {
                    ((Button)child).IsEnabled = false;
                }
                else if (child.GetType() == typeof(TextBox))
                {
                    ((TextBox)child).IsEnabled = false;
                }
            }
        }

        private void Menu_FileOpen(object sender, RoutedEventArgs e)
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
                // This is to ensure that opening a file when one is already open resets everything in the tab
                SetDisabled();

                BOTWConsole.Log("Opening file");

                TSCB t = TSCB.ReadFile(openFileDialog.FileName);

                // Set the current file location to the chosen file's location
                fileLocation = openFileDialog.FileName;

                // Set the current TCSBInfo to the new TSCBInfo
                currentTSCB = t;

                // Set UI sidebar to have header info, enable controls
                SetEnabled(t);

                // Allow the file to be saved
                MenuFileClose.IsEnabled = true;
                MenuFileSave.IsEnabled = true;

                // Really laggy, creating 9,000+ controls isn't necessarily a fantastic idea
                // Maybe having a filter would help?
                /*oreach (var area in t.AreaInfo)
                {
                    Control.TSCBAreaExpander ae = new Control.TSCBAreaExpander();
                    ae.AreaExpander.Header = $"({area.PositionX}, {area.PositionZ})";
                    ae.AreaExpander.IsExpanded = false;

                    TSCBAreaViewer.Children.Add(ae);
                }*/
            }
        }

        private void Menu_FileClose(object sender, RoutedEventArgs e)
        {
            BOTWConsole.Log("Clicked File -> Close button");

            // Set the current TSCB info to nothing
            currentTSCB = null;

            // Set the tab as disabled
            SetDisabled();

            // Since there's no file open, don't allow saving
            MenuFileClose.IsEnabled = false;
            MenuFileSave.IsEnabled = false;
        }

        // TODO: split this into "save" and "save as"
        private void Menu_FileSave(object sender, RoutedEventArgs e)
        {
            BOTWConsole.Log("Clicked File -> Save button");

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = @"C;\",
                RestoreDirectory = true,
                Title = "Save TSCB file",
                DefaultExt = "tscb",
                Filter = "TSCB files (*.tscb)|*.tscb"
            };

            if ((bool)saveFileDialog.ShowDialog())
            {
                File.WriteAllBytes(saveFileDialog.FileName, TSCB.GetBytes(currentTSCB));
            }
        }

        private void OverrideKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                TextBox textSender = (TextBox)sender;

                if (float.TryParse(textSender.Text, out float overrideValue))
                {
                    // Clamp value between 0 and 1
                    overrideValue = MathExt.Clamp(overrideValue, 0.0f, 1.0f);

                    BOTWConsole.Log($"Overriding {textSender.Name} with value {overrideValue}");

                    switch (textSender.Name)
                    {
                        case "OverrideMinTerrainHeight":
                            foreach (var area in currentTSCB.AreaInfo)
                                area.MinTerrainHeight = overrideValue;
                            break;
                        case "OverrideMaxTerrainHeight":
                            foreach (var area in currentTSCB.AreaInfo)
                                area.MaxTerrainHeight = overrideValue;
                            break;
                        case "OverrideMinWaterHeight":
                            foreach (var area in currentTSCB.AreaInfo)
                                area.MinWaterHeight = overrideValue;
                            break;
                        case "OverrideMaxWaterHeight":
                            foreach (var area in currentTSCB.AreaInfo)
                                area.MaxWaterHeight = overrideValue;
                            break;
                    }
                }
            }
        }
    }
}
