//---------------------------------------------------------------------------------------------------
// auto-generated
//---------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using BLToolkit.Data;
using BLToolkit.Data.Linq;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Validation;
using CQRS.Common;
using Economy.DataAccess.BlToolkit.Entities;

namespace Economy.DataAccess.BlToolkit.Entities
{
    [TableName(Database = "economy", Owner = "public", Name = "\"CourseArhive\"")]
    internal class CourseArhiveEntity : BaseEntity
    {

        #region CurrencyTypeId

        int _currencytypeid;

        /// <summary>
        ///валюта
        /// </summary>
        [MapField("\"CurrencyTypeId\""), Required, Unique]
        public virtual int CurrencyTypeId
        {
            get
            {
                return  _currencytypeid == 0  && CurrencyType != null ? CurrencyType.Id : _currencytypeid;
            }
            set { _currencytypeid = value; }
        }

		[Association(ThisKey = "\"CurrencyTypeId\"", OtherKey = "\"Id\"", CanBeNull = false)]
        public virtual CurrencyTypeEntity CurrencyType { get; set; }

		#endregion CurrencyTypeId
		

		#region RegDate

        /// <summary>
        ///дата
        /// </summary>
        [MapField("\"RegDate\""), Required, Unique]
        public virtual DateTime RegDate { get; set; }

		#endregion RegDate
		

        #region BankId

        int _bankid;

        /// <summary>
        ///Банк
        /// </summary>
        [MapField("\"BankId\""), Required, Unique]
        public virtual int BankId
        {
            get
            {
                return  _bankid == 0  && Bank != null ? Bank.Id : _bankid;
            }
            set { _bankid = value; }
        }

		[Association(ThisKey = "\"BankId\"", OtherKey = "\"Id\"", CanBeNull = false)]
        public virtual BankEntity Bank { get; set; }

		#endregion BankId
		

		#region Buy

        /// <summary>
        ///цена покупки
        /// </summary>
        [MapField("\"Buy\""), Required]
        public virtual decimal Buy { get; set; }

		#endregion Buy
		

		#region Sel

        /// <summary>
        ///цена продажи
        /// </summary>
        [MapField("\"Sel\""), Required]
        public virtual decimal Sel { get; set; }

		#endregion Sel
		
        public override void UpdateAssociations()
        {
        }
  
    }
}