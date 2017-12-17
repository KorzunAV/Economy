//---------------------------------------------------------------------------------------------------
// auto-generated
//---------------------------------------------------------------------------------------------------
using BLToolkit.Data.Linq;
using Economy.DataAccess.BlToolkit.Entities;

namespace Economy.DataAccess.BlToolkit.DbManagers
{
    internal partial class EconomyDb
    {
        internal Table<BankEntity> BankTable => GetTable<BankEntity>();
        internal Table<CourseArhiveEntity> CourseArhiveTable => GetTable<CourseArhiveEntity>();
        internal Table<CurrencyTypeEntity> CurrencyTypeTable => GetTable<CurrencyTypeEntity>();
        internal Table<MontlyReportEntity> MontlyReportTable => GetTable<MontlyReportEntity>();
        internal Table<SystemUserEntity> SystemUserTable => GetTable<SystemUserEntity>();
        internal Table<TransactionEntity> TransactionTable => GetTable<TransactionEntity>();
        internal Table<WalletEntity> WalletTable => GetTable<WalletEntity>();

    }
}