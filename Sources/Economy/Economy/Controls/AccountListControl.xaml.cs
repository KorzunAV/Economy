using System.Windows;

namespace Economy.Controls
{
    /// <summary>
    /// Interaction logic for AccountListControl.xaml
    /// </summary>
    public partial class AccountListControl
    {
        public event RoutedEventHandler ShowErrorEvent;

        public AccountListControl()
        {
            InitializeComponent();
        }

        private void ShowError(object sender, RoutedEventArgs e)
        {
            if (ShowErrorEvent != null)
                ShowErrorEvent(sender, e);
        }
    }
}
