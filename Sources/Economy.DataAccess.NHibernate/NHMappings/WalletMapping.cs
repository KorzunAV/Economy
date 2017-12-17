using Economy.DataAccess.NHibernate.Entities;

namespace Economy.DataAccess.NHibernate.NHMappings
{
    public class WalletMapping : BaseMapping<WalletEntity>
    {
        public WalletMapping()
        {
            Id(v => v.Id, "\"Id\"").GeneratedBy.Guid();
            Map(v => v.Name, "\"Name\"");
            Map(v => v.StartBalance, "\"StartBalance\"");
            Map(v => v.Balance, "\"Balance\"");
            Map(v => v.SystemUserId, "\"SystemUserId\"");
            References(v => v.SystemUser, "\"SystemUserId\"").Column(Column(v => v.SystemUserId)).ReadOnly().LazyLoad();
            Map(v => v.CurrencyTypeId, "\"CurrencyTypeId\"");
            References(v => v.CurrencyType, "\"CurrencyTypeId\"").Column(Column(v => v.CurrencyTypeId)).ReadOnly().LazyLoad();
            OptimisticLock.Version();
            Version(entity => entity.Version).Column("\"Version\"");

        }
    }
}