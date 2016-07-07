using BLToolkit.DataAccess;
using BLToolkit.Mapping;

namespace Economy.DataAccess.Entities
{
    /// <summary>
    /// Валюта
    /// </summary>
    [TableName(Name = "currency_type")]
    internal class CurrencyTypeEntity : BaseEntity
    {
        /// <summary>
        /// наименование валюты
        /// </summary>
        [MapField("name")]
        public string Name { get; set; }

        /// <summary>
        /// трехбуквенное обозначение
        /// </summary>
        [MapField("short_name")]
        public string ShortName { get; set; }
    }
}