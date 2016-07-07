using System.Diagnostics;
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
        
        private void OpenHistory(object sender, RoutedEventArgs e)
        {
            Process.Start(ViewModelLocator.DirHistoryPath);
        }
    }
}