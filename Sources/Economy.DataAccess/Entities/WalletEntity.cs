using BLToolkit.Mapping;

namespace Economy.DataAccess.Entities
{
    internal class WalletEntity : BaseEntity
    {
        /// <summary>
        /// Дата регистрации
        /// </summary>
        [MapField("name")]
        public string Name { get; set; }

        /// <summary>
        /// первоначальный баланс счета
        /// </summary>
        [MapField("start_balance")]
        public decimal StartBalance { get; set; }

        /// <summary>
        /// итоговое состояние счета
        /// </summary>
        [MapField("balance")]
        public decimal Balance { get; set; }

        /// <summary>
        /// пользователь
        /// </summary>
        [MapField("system_user")]
        public int SystemUserId { get; set; }

        public SystemUserEntity User { get; set; }

        /// <summary>
        /// валюта кошелька
        /// </summary>
        [MapField("currency_type")]
        public int CurrencyTypeId { get; set; }

        public CurrencyTypeEntity CurrencyType { get; set; }
    }
}