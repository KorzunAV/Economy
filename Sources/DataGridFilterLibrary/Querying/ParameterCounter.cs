
namespace DataGridFilterLibrary.Querying
{
    public class ParameterCounter
    {
        private int _count;

        public int ParameterNumber { get { return _count - 1; } }
        
        public void Increment()
        {
            _count++;
        }

        public void Decrement()
        {
            _count--;
        }

        public ParameterCounter()
        {
        }

        public ParameterCounter(int count)
        {
            _count = count;
        }

        public override string ToString()
        {
            return ParameterNumber.ToString();
        }
    }
}
