using System;
using Economy.DataAccess.BlToolkit.DbManagers;

namespace Economy.DataAccess.BlToolkit.Entities
{
    internal class TransactionEntity : TransactionBaseEntity
    {
        public override int CurrencyTypeId
        {
            get
            {
                return base.CurrencyTypeId == 0 && CurrencyType != null
                    ? CurrencyType.Id
                    : base.CurrencyTypeId;
            }
            set { base.CurrencyTypeId = value; }
        }

        public override Guid? FromWalletId
        {
            get
            {
                return (base.FromWalletId == null || base.FromWalletId == Guid.Empty) && FromWallet != null
                    ? FromWallet.Id
                    : base.FromWalletId;
            }
            set { base.FromWalletId = value; }
        }
        
        public override Guid ToWalletId
        {
            get
            {
                return base.ToWalletId == Guid.Empty && ToWallet != null
                    ? ToWallet.Id
                    : base.ToWalletId;
            }
            set { base.ToWalletId = value; }
        }

        public override Guid? MontlyReportId
        {
            get
            {
                return (base.MontlyReportId == null || base.MontlyReportId == Guid.Empty) && MontlyReport != null
                    ? MontlyReport.Id
                    : base.MontlyReportId;
            }
            set { base.MontlyReportId = value; }
        }
    }
}