using Economy.DataAccess.NHibernate.Entities;

namespace Economy.DataAccess.NHibernate.NHMappings
{
    public class TransactionMapping : BaseMapping<TransactionEntity>
    {
        public TransactionMapping()
        {
            Id(v => v.Id, "\"Id\"").GeneratedBy.Guid();
            Map(v => v.RegistrationDate, "\"RegistrationDate\"");
            Map(v => v.TransactionDate, "\"TransactionDate\"");
            Map(v => v.Code, "\"Code\"");
            Map(v => v.Description, "\"Description\"");
            Map(v => v.CurrencyTypeId, "\"CurrencyTypeId\"");
            References(v => v.CurrencyType, "\"CurrencyTypeId\"").Column(Column(v => v.CurrencyTypeId)).ReadOnly().LazyLoad();
            Map(v => v.QuantityByTransaction, "\"QuantityByTransaction\"");
            Map(v => v.QuantityByWallet, "\"QuantityByWallet\"");
            Map(v => v.Commission, "\"Commission\"");
            Map(v => v.FromWalletId, "\"FromWalletId\"");
            References(v => v.FromWallet, "\"FromWalletId\"").Column(Column(v => v.FromWalletId)).ReadOnly().LazyLoad();
            Map(v => v.ToWalletId, "\"ToWalletId\"");
            References(v => v.ToWallet, "\"ToWalletId\"").Column(Column(v => v.ToWalletId)).ReadOnly().LazyLoad();
            Map(v => v.MontlyReportId, "\"MontlyReportId\"");
            References(v => v.MontlyReport, "\"MontlyReportId\"").Column(Column(v => v.MontlyReportId)).ReadOnly().LazyLoad();
            OptimisticLock.Version();
            Version(entity => entity.Version).Column("\"Version\"");

        }
    }
}