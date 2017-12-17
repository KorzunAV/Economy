using Economy.DataAccess.NHibernate.Entities;

namespace Economy.DataAccess.NHibernate.NHMappings
{
    public class MontlyReportMapping : BaseMapping<MontlyReportEntity>
    {
        public MontlyReportMapping()
        {
            Id(v => v.Id, "\"Id\"").GeneratedBy.Guid();
            Map(v => v.StartBalance, "\"StartBalance\"");
            Map(v => v.EndBalance, "\"EndBalance\"");
            Map(v => v.StartDate, "\"StartDate\"");
            Map(v => v.WalletId, "\"WalletId\"");
            References(v => v.Wallet, "\"WalletId\"").Column(Column(v => v.WalletId)).ReadOnly().LazyLoad();
            OptimisticLock.Version();
            Version(entity => entity.Version).Column("\"Version\"");

        }
    }
}