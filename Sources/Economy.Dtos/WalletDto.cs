using System;

namespace Economy.Dtos
{
    public class WalletDto : BaseDto
    {
        private Guid _id = Guid.Empty;

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// первоначальный баланс счета
        /// </summary>
        public decimal StartBalance { get; set; }

        /// <summary>
        /// итоговое состояние счета
        /// </summary>
        public decimal Balance { get; set; }

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