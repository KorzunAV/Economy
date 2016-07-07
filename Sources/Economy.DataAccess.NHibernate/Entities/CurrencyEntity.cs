
namespace Economy.DataAccess.NHibernate.Entities
{
    /// <summary>
    /// Валюта
    /// </summary>
    public class CurrencyEntity : BaseEntity
    {
        /// <summary>
        /// наименование валюты
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// трехбуквенное обозначение
        /// </summary>
        public virtual string ShortName { get; set; }
    }
}