using Economy.DataAccess.NHibernate.Daos;

namespace Economy.DataAccess.NHibernate.IoC
{
    public partial class DataAccessModule
    {
        private void BindDaos()
        {
            Bind<BankDao>().ToSelf().InSingletonScope();
            Bind<CourseArhiveDao>().ToSelf().InSingletonScope();
            Bind<CurrencyTypeDao>().ToSelf().InSingletonScope();
            Bind<MontlyReportDao>().ToSelf().InSingletonScope();
            Bind<SystemUserDao>().ToSelf().InSingletonScope();
            Bind<TransactionDao>().ToSelf().InSingletonScope();
            Bind<WalletDao>().ToSelf().InSingletonScope();

        }
    }
}