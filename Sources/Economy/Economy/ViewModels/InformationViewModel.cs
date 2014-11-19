using System.Windows;
using Economy.Common;

namespace Economy.ViewModels
{
    public class InformationViewModel : ViewModelBase
    {
        private string _messageText;
        private Visibility _messageGroupBoxVisibility;

        public string MessageText
        {
            get { return _messageText; }
            set { SetProperty(ref _messageText, value); }
        }

        public Visibility MessageGroupBoxVisibility
        {
            get { return _messageGroupBoxVisibility; }
            set { SetProperty(ref _messageGroupBoxVisibility, value); }
        }

        public RelayCommand ShowErrorCommand { get; set; }

        public RelayCommand HideErrorCommand { get; set; }

        public InformationViewModel()
        {
            if (IsDesignTime)
            {
                MessageText = "DesignTime";
                MessageGroupBoxVisibility = Visibility.Visible;
            }
            else
            {
                MessageGroupBoxVisibility = Visibility.Hidden;
            }

            HideErrorCommand = new RelayCommand(Hide);
            ShowErrorCommand = new RelayCommand(Show);
        }

        private void Hide(object param)
        {
            MessageGroupBoxVisibility = Visibility.Hidden;
        }

        private void Show(object param)
        {
            MessageGroupBoxVisibility = Visibility.Visible;
        }
    }
}
