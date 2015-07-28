using System.Windows.Media;

namespace Economy.ViewModels
{
    public class TransactionItemShortViewModel : ViewModelBase
    {
        #region Свойства
        public string Data { get; set; }
        public decimal Income { get; set; }
        public decimal Outcome { get; set; }

        public decimal IncomeWithoutLocal { get; set; }
        public decimal OutcomeWithoutLocal { get; set; }


        public decimal InOut
        {
            get { return Income + Outcome; }
        }


        public decimal Balance { get; set; }


        public bool IsIncome
        {
            get { return (Income + Outcome > 0); }
        }

        public Brush DataRowColor
        {
            get
            {
                var cl = IsIncome ? Colors.Green : Colors.Red;
                return new SolidColorBrush(cl);
            }
        }

        #endregion Свойства

    }
}
