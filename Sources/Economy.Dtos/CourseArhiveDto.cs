using CQRS.Common;

namespace Economy.Dtos
{
    public partial class CourseArhiveDto
    {
        #region [ Property names ]
        
        public static readonly string PropCurrency = TypeHelper<CourseArhiveDto>.PropertyName(x => x.CurrencyTypeDto);

        #endregion
   

        /// <summary>
        /// Валюта
        /// </summary>
        public CurrencyTypeDto CurrencyTypeDto { get; set; }


        
        public bool Equals(CourseArhiveDto obj)
        {
            return RegDate == obj.RegDate && CurrencyTypeDto.Id == obj.CurrencyTypeDto.Id;
        }
    }
}
