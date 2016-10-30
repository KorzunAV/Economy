using System;
using Economy.DataAccess.BlToolkit.DbManagers;

namespace Economy.DataAccess.BlToolkit.Entities
{
    internal class WalletEntity : WalletBaseEntity
    {
        public override Guid? SystemUserId
        {
            get
            {
                return (base.SystemUserId == null || base.SystemUserId == Guid.Empty) && SystemUser != null
                    ? SystemUser.Id
                    : base.SystemUserId;
            }
            set { base.SystemUserId = value; }
        }
        
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
    }
}