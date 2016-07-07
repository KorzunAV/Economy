using BLToolkit.Mapping;

namespace Economy.DataAccess.Entities
{
    internal class BaseEntity
    {
        [MapField("id")]
        public int Id { get; set; }
    }
}