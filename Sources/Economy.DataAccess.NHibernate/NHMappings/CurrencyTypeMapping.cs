using Economy.DataAccess.NHibernate.Entities;

namespace Economy.DataAccess.NHibernate.NHMappings
{
    public class CurrencyTypeMapping : BaseMapping<CurrencyTypeEntity>
    {
        public CurrencyTypeMapping()
        {
            Id(v => v.Id, "\"Id\"").GeneratedBy.Increment();
            Map(v => v.Name, "\"Name\"");
            Map(v => v.ShortName, "\"ShortName\"");
            OptimisticLock.Version();
            Version(entity => entity.Version).Column("\"Version\"");

        }
    }
}