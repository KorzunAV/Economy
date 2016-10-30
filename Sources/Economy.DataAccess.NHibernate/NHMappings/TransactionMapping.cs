using Economy.DataAccess.NHibernate.Entities;

namespace Economy.DataAccess.NHibernate.NHMappings
{
    public class TransactionMapping : BaseMapping<TransactionEntity>
    {
        public TransactionMapping()
        {
            Id(v => v.Id).GeneratedBy.Guid();
            Map(v => v.RegistrationDate);
            Map(v => v.TransactionDate);
            Map(v => v.Code);
            Map(v => v.Description);
            Map(v => v.CurrencyTypeId);
            References(v => v.CurrencyType).Column(Column(v => v.CurrencyTypeId)).ReadOnly().LazyLoad();
            Map(v => v.QuantityByTransaction);
            Map(v => v.QuantityByWallet);
            Map(v => v.Commission);
            Map(v => v.FromWalletId);
            References(v => v.FromWallet).Column(Column(v => v.FromWalletId)).ReadOnly().LazyLoad();
            Map(v => v.ToWalletId);
            References(v => v.ToWallet).Column(Column(v => v.ToWalletId)).ReadOnly().LazyLoad();
            Map(v => v.MontlyReportId);
            References(v => v.MontlyReport).Column(Column(v => v.MontlyReportId)).ReadOnly().LazyLoad();
            OptimisticLock.Version();
            Version(entity => entity.Version);

        }
    }
}