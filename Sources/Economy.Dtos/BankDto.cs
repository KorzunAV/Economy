using System;
using System.Collections.Generic;
using CQRS.Common;
using CQRS.Dtos;

namespace Economy.Dtos
{
    public class BankDto : BaseDto
    {
        /// <summary>
        ///идентификатор
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///Версия для оптимистической блокировки
        /// </summary>
        public virtual int Version { get; set; }

        /// <summary>
        ///Банк
        /// </summary>
        public virtual List<CourseArhiveDto> CourseArhives { get; set; }

    
    }
}