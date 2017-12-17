using System.Collections.Generic;
using CQRS.Dtos;
using Economy.Dtos;

namespace Economy.Models
{
    public class SystemUserModel : BaseDto
    {
        /// <summary>
        ///идентификатор
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        ///Версия для оптимистической блокировки
        /// </summary>
        public virtual int Version { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual string Login { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///пользователь
        /// </summary>
        public virtual List<WalletModel> Wallets { get; set; }

    
    }
}