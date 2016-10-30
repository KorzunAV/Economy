using System;
using Economy.DataAccess.BlToolkit.DbManagers;

namespace Economy.DataAccess.BlToolkit.Entities
{
    internal class MontlyReportEntity : MontlyReportBaseEntity
    {
        public override Guid WalletId
        {
            get
            {
                return base.WalletId == Guid.Empty && Wallet != null
                    ? Wallet.Id
                    : base.WalletId;
            }
            set { base.WalletId = value; }
        }
    }
}