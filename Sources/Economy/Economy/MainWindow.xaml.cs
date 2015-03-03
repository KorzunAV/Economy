using System.Windows;
using Economy.ViewModels;

namespace Economy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
           ViewModelLocator.UpdateDataFiles();
        }
    }
}
