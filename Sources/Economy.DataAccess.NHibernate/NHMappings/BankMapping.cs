using Economy.DataAccess.NHibernate.Entities;

namespace Economy.DataAccess.NHibernate.NHMappings
{
    public class BankMapping : BaseMapping<BankEntity>
    {
        public BankMapping()
        {
            Id(v => v.Id).GeneratedBy.Increment();
            Map(v => v.Name);
            OptimisticLock.Version();
            Version(entity => entity.Version);

        }
    }
}