using System.Collections.Generic;

namespace Economy.ViewModels
{
    /// <summary>
    /// Статус процесса
    /// </summary>
    public class StateViewModel : ViewModelBase
    {
        public enum Actions
        {
            MailConvert,
            None
        }

        private string _mainState;

        private readonly Dictionary<Actions, string> _actions;


        public string MainState
        {
            get { return _mainState; }
            set { SetProperty(ref _mainState, value); }
        }


        public StateViewModel()
        {
            _actions = new Dictionary<Actions, string>();

            if (IsDesignTime)
            {
                MainState = "DesignTime";
            }
        }

        public void CreateUpdateAction(Actions action, string message)
        {
            if (_actions.ContainsKey(action))
            {
                _actions[action] = message;
            }
            else
            {
                _actions.Add(action, message);
            }

            MainState = message;
        }
    }
}
