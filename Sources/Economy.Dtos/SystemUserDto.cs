using System;
using System.Collections.Generic;
using CQRS.Common;
using CQRS.Dtos;

namespace Economy.Dtos
{
    public class SystemUserDto : BaseDto
    {
        /// <summary>
        ///идентификатор пользователя
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///Версия для оптимистической блокировки
        /// </summary>
        public virtual int Version { get; set; }

        /// <summary>
        ///пользователь
        /// </summary>
        public virtual List<WalletDto> Wallets { get; set; }

    
    }
}