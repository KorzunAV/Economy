using System;
using CQRS.Common;

namespace Economy.Dtos
{
    public class BelinvestCourseArhiveDto : BaseDto
    {
        #region [ Property names ]
        
        public static readonly string PropDate = TypeHelper<BelinvestCourseArhiveDto>.PropertyName(x => x.RegDate);
        public static readonly string PropCurrency = TypeHelper<BelinvestCourseArhiveDto>.PropertyName(x => x.CurrencyTypeDto);
        public static readonly string PropBuy = TypeHelper<BelinvestCourseArhiveDto>.PropertyName(x => x.Buy);
        public static readonly string PropSel = TypeHelper<BelinvestCourseArhiveDto>.PropertyName(x => x.Sel);

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
        
        public bool Equals(BelinvestCourseArhiveDto obj)
        {
            return RegDate == obj.RegDate && CurrencyTypeDto.Id == obj.CurrencyTypeDto.Id;
        }
    }
}
