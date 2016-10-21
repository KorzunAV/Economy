using System;

namespace Economy.Dtos
{
    public partial class WalletDto
    {
        /// <summary>
        /// пользователь
        /// </summary>
        public SystemUserDto User { get; set; }

        /// <summary>
        /// валюта кошелька
        /// </summary>
        public CurrencyTypeDto CurrencyType { get; set; }


        public bool IsNew
        {
            get { return Id == Guid.Empty || User.IsNew || CurrencyType.IsNew; }
        }
    }
}