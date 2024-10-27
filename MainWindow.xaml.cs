using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NearEarthObjectVisualization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void FetchDataButton_Click(object sender, RoutedEventArgs e)
        {
            var neoService = new NeoApiService();
            var neos = await neoService.FetchNeoDataAsync("2015-09-07", "2015-09-08");
            DisplayNeoData(neos);
        }

        private void DisplayNeoData(List<NearEarthObject> neos)
        {
            OrbitalCanvas.Children.Clear();

            // earch is 12,742 km diameter
            var earth = new Ellipse { Width = 70, Height = 70, Fill = Brushes.Blue};

            Canvas.SetLeft(earth, OrbitalCanvas.Width / 2 - 10);
            Canvas.SetTop(earth, OrbitalCanvas.Height / 2 - 10);

            OrbitalCanvas.Children.Add(earth);

            foreach (var neo in neos)
            {
                if (true) 
                {
                    double scaledDistance = neo.MissDistanceKm / 100000;
                    double scaledSize = neo.EstimatedDiameterMeters / 10;

                    var neoEllipse = new Ellipse
                    {
                        Width = scaledSize,
                        Height = scaledSize,
                        StrokeThickness = 1,
                        Fill = (neo.IsPotentiallyDangerous) ? Brushes.Red : Brushes.White
                    };

                    // Put neos around the earth
                    double angle = new Random().NextDouble() * 2 * Math.PI;
                    double x = OrbitalCanvas.Width / 2 + scaledDistance * Math.Cos(angle) - scaledSize / 2;
                    double y = OrbitalCanvas.Width / 2 + scaledDistance * Math.Sin(angle) - scaledSize / 2;

                    Debug.WriteLine($"{neo.Name} is at position ({x}, {y}) with angle {angle} and scaled size of {scaledSize}");

                    Canvas.SetLeft(neoEllipse, x);
                    Canvas.SetTop(neoEllipse, y);
                    OrbitalCanvas.Children.Add(neoEllipse);
                }
            }
        }
    }
}