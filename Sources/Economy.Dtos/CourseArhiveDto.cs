using System;
using CQRS.Common;

namespace Economy.Dtos
{
    public class CourseArhiveDto : BaseDto
    {
        #region [ Property names ]
        
        public static readonly string PropDate = TypeHelper<CourseArhiveDto>.PropertyName(x => x.RegDate);
        public static readonly string PropCurrency = TypeHelper<CourseArhiveDto>.PropertyName(x => x.CurrencyTypeDto);
        public static readonly string PropBuy = TypeHelper<CourseArhiveDto>.PropertyName(x => x.Buy);
        public static readonly string PropSel = TypeHelper<CourseArhiveDto>.PropertyName(x => x.Sel);

        #endregion
        
        /// <summary>
        /// Дата записи
        /// </summary>
        public DateTime RegDate { get; set; }

        /// <summary>
        /// Валюта
        /// </summary>
        public CurrencyTypeDto CurrencyTypeDto { get; set; }

        /// <summary>
        /// Цена покупки
        /// </summary>
        public decimal Buy { get; set; }

        /// <summary>
        /// Цена продажи
        /// </summary>
        public decimal Sel { get; set; }
        
        public bool Equals(CourseArhiveDto obj)
        {
            return RegDate == obj.RegDate && CurrencyTypeDto.Id == obj.CurrencyTypeDto.Id;
        }
    }
}
