using BOTWToolset.Resources.BOTW;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BOTWToolset.Control
{
    /// <summary>
    /// Interaction logic for TabMapGuide.xaml
    /// </summary>
    public partial class TabMapGuide : UserControl
    {
        public TabMapGuide()
        {
            InitializeComponent();
        }

        private void SwitchView(object sender, RoutedEventArgs e)
        {
            ClearPins();

            var sender_button = (Button)sender;
            switch (sender_button.Name)
            {
                case "ViewMajorToS":
                    foreach (int[] coords in PinLocations.MajorToSLocations)
                    {
                        AddPin(coords, 16, @"Resources/Icons/hollow-circle.png", Color.FromRgb(255, 255, 255));
                    }
                    break;
                case "ViewYigaDeadzone":
                    break;
                case "ViewDragonPaths":
                    break;
            }
        }

        private void AddPin(int[] coords, int pin_size, string pin_image, Color color)
        {
            var img_src = new BitmapImage(new Uri(@$"pack://application:,,,/{pin_image}"));

            // Initialize new image control, which will represent the map marker
            MapPin pin = new MapPin();

            // Set image properties
            pin.Source = img_src;
            pin.Width = pin_size;
            pin.Height = pin_size;
            pin.VerticalAlignment = VerticalAlignment.Top;
            pin.HorizontalAlignment = HorizontalAlignment.Left;

            // Use proper aliasing
            RenderOptions.SetBitmapScalingMode(pin, BitmapScalingMode.HighQuality);

            pin.X = coords[0];
            pin.Z = coords[1];

            int[] pin_coords = FromBOTWCoordinate(pin.X, pin.Z);

            Canvas.SetLeft(pin, pin_coords[0] - (pin.Width / 2));
            Canvas.SetTop(pin, pin_coords[1] - (pin.Width / 2));

            // Add the image control to the map area
            MapArea.Children.Add(pin);
        }

        private void ClearPins()
        {
            //Remove all map area children, excluding the map image itself
            //MapArea.Children.RemoveRange(1, MapArea.Children.Count - 1);
            MapArea.Children.Clear();
        }

        private int[] FromBOTWCoordinate(int x, int z)
        {
            // Difference between canvas and canvas background - used due to uniform bg resizing
            double difference_width = (MapArea.ActualWidth - MapImage.ActualWidth) / 2;
            double difference_height = (MapArea.ActualHeight - MapImage.ActualHeight) / 2;

            // -6000, -5000 top left, 6000, 5000 bottom right
            double botw_img_ratio = 12000 / MapImage.ActualWidth;

            return new int[] { (int)(((x + 6000) / botw_img_ratio) + difference_width), (int)(((z + 5000) / botw_img_ratio) + difference_height) };
        }

        private void MapSizeChanged(object sender, SizeChangedEventArgs e)
        {
            // If the control doesn't have proper sizes yet, to prevent errors
            if (e.PreviousSize.Width == 0 || e.PreviousSize.Height == 0)
                return;

            foreach (var child in MapArea.Children)
            {
                var child_elem = (MapPin)child;

                int[] img_coords = FromBOTWCoordinate(child_elem.X, child_elem.Z);
                Canvas.SetLeft(child_elem, img_coords[0] - (child_elem.Width / 2));
                Canvas.SetTop(child_elem, img_coords[1] - (child_elem.Width / 2));
            }
        }
    }
}
