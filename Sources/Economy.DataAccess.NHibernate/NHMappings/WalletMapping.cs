using Economy.DataAccess.NHibernate.Entities;

namespace Economy.DataAccess.NHibernate.NHMappings
{
    public class WalletMapping : BaseMapping<WalletEntity>
    {
        public WalletMapping()
        {
            Id(v => v.Id).GeneratedBy.Guid();
            Map(v => v.Name);
            Map(v => v.StartBalance);
            Map(v => v.Balance);
            Map(v => v.SystemUserId);
            References(v => v.SystemUser).Column(Column(v => v.SystemUserId)).ReadOnly().LazyLoad();
            Map(v => v.CurrencyTypeId);
            References(v => v.CurrencyType).Column(Column(v => v.CurrencyTypeId)).ReadOnly().LazyLoad();
            OptimisticLock.Version();
            Version(entity => entity.Version);

        }
    }
}