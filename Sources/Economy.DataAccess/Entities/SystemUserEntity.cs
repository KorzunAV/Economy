using BLToolkit.Mapping;

namespace Economy.DataAccess.Entities
{
    internal class SystemUserEntity : BaseEntity
    {
        /// <summary>
        /// Дата регистрации
        /// </summary>
        [MapField("name")]
        public string Name { get; set; }
    }
}