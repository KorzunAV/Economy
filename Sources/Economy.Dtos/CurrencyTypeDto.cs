namespace Economy.Dtos
{
    /// <summary>
    /// Валюта
    /// </summary>
    public partial class CurrencyTypeDto
    {
        public bool Equals(CurrencyTypeDto obj)
        {
            return Id == obj.Id;
        }

        public bool IsNew
        {
            get { return Id == 0; }
        }
    }
}