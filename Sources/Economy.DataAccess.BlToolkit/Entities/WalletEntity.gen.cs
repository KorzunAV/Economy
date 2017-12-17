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
    [TableName(Database = "economy", Owner = "public", Name = "\"Wallet\"")]
    internal class WalletEntity : BaseEntity
    {

		#region Name

        /// <summary>
        ///
        /// </summary>
        [MapField("\"Name\""), Nullable]
        public virtual string Name { get; set; }

		#endregion Name
		

		#region StartBalance

        /// <summary>
        ///первоначальный баланс счета
        /// </summary>
        [MapField("\"StartBalance\""), Nullable]
        public virtual decimal? StartBalance { get; set; }

		#endregion StartBalance
		

		#region Balance

        /// <summary>
        ///итоговое состояние счета
        /// </summary>
        [MapField("\"Balance\""), Nullable]
        public virtual decimal? Balance { get; set; }

		#endregion Balance
		

        #region SystemUserId

        int? _systemuserid;

        /// <summary>
        ///пользователь
        /// </summary>
        [MapField("\"SystemUserId\""), Nullable]
        public virtual int? SystemUserId
        {
            get
            {
                return (!_systemuserid.HasValue || _systemuserid == 0 ) && SystemUser != null ? SystemUser.Id : _systemuserid;
            }
            set { _systemuserid = value; }
        }

		[Association(ThisKey = "\"SystemUserId\"", OtherKey = "\"Id\"", CanBeNull = false)]
        public virtual SystemUserEntity SystemUser { get; set; }

		#endregion SystemUserId
		

        #region CurrencyTypeId

        int _currencytypeid;

        /// <summary>
        ///валюта кошелька
        /// </summary>
        [MapField("\"CurrencyTypeId\""), Required]
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
		

        #region BankId

        int? _bankid;

        /// <summary>
        ///--Привязка к банку
        /// </summary>
        [MapField("\"BankId\""), Nullable]
        public virtual int? BankId
        {
            get
            {
                return (!_bankid.HasValue || _bankid == 0 ) && Bank != null ? Bank.Id : _bankid;
            }
            set { _bankid = value; }
        }

		[Association(ThisKey = "\"BankId\"", OtherKey = "\"Id\"", CanBeNull = false)]
        public virtual BankEntity Bank { get; set; }

		#endregion BankId
		
        /// <summary>
        ///Идентификатор кошелька
        /// </summary>
        [Association(ThisKey="WalletId", OtherKey="Id", CanBeNull=true)]
        public virtual List<MontlyReportEntity> MontlyReports { get; set; }

        public override void UpdateAssociations()
        {
            foreach (var item in MontlyReports)
            {
                item.WalletId = Id;
            }
        }
  
    }
}