using Economy.DataAccess.NHibernate.Entities;

namespace Economy.DataAccess.NHibernate.NHMappings
{
    public class MontlyReportMapping : BaseMapping<MontlyReportEntity>
    {
        public MontlyReportMapping()
        {
            Id(v => v.Id).GeneratedBy.Guid();
            Map(v => v.StartBalance);
            Map(v => v.EndBalance);
            Map(v => v.StartDate);
            Map(v => v.WalletId);
            References(v => v.Wallet).Column(Column(v => v.WalletId)).ReadOnly().LazyLoad();
            OptimisticLock.Version();
            Version(entity => entity.Version);

        }
    }
}