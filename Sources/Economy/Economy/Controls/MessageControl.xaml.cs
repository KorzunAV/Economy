using System.Windows;

namespace Economy.Controls
{
    /// <summary>
    /// Просто область с текстом и кнопкой закрытия
    /// </summary>
    public partial class MessageControl
    {
        public MessageControl()
        {
            InitializeComponent();
        }

        private void CloseMessageBox(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
        }
    }
}
