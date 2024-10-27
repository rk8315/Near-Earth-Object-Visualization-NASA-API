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
            var neoService = new NeoApiService("API_KEY");
            var neos = await neoService.FetchNeoDataAsync("2023-10-01", "2023-10-07");
        }
    }
}