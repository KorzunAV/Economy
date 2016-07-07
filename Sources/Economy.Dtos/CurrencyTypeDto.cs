namespace Economy.Dtos
{
    /// <summary>
    /// Валюта
    /// </summary>
    public class CurrencyTypeDto : BaseDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// наименование валюты
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// трехбуквенное обозначение
        /// </summary>
        public string ShortName { get; set; }

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