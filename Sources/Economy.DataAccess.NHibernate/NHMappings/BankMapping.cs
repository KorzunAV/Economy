using Economy.DataAccess.NHibernate.Entities;

namespace Economy.DataAccess.NHibernate.NHMappings
{
    public class BankMapping : BaseMapping<BankEntity>
    {
        public BankMapping()
        {
            Id(v => v.Id, "\"Id\"").GeneratedBy.Increment();
            Map(v => v.Name, "\"Name\"");
            OptimisticLock.Version();
            Version(entity => entity.Version).Column("\"Version\"");

        }
    }
}