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
    [TableName(Database = "economy", Owner = "public", Name = "\"Transaction\"")]
    internal class TransactionEntity : BaseEntity
    {

		#region RegistrationDate

        /// <summary>
        ///дата регистрации транзакции в системе
        /// </summary>
        [MapField("\"RegistrationDate\""), Required]
        public virtual DateTime RegistrationDate { get; set; }

		#endregion RegistrationDate
		

		#region TransactionDate

        /// <summary>
        ///дата совершения транзакции
        /// </summary>
        [MapField("\"TransactionDate\""), Nullable]
        public virtual DateTime? TransactionDate { get; set; }

		#endregion TransactionDate
		

		#region Code

        /// <summary>
        ///код транзакции
        /// </summary>
        [MapField("\"Code\""), Nullable]
        public virtual string Code { get; set; }

		#endregion Code
		

		#region Description

        /// <summary>
        ///комментарий
        /// </summary>
        [MapField("\"Description\""), Nullable]
        public virtual string Description { get; set; }

		#endregion Description
		

        #region CurrencyTypeId

        int? _currencytypeid;

        /// <summary>
        ///валюта транзакции
        /// </summary>
        [MapField("\"CurrencyTypeId\""), Nullable]
        public virtual int? CurrencyTypeId
        {
            get
            {
                return (!_currencytypeid.HasValue || _currencytypeid == 0 ) && CurrencyType != null ? CurrencyType.Id : _currencytypeid;
            }
            set { _currencytypeid = value; }
        }

		[Association(ThisKey = "\"CurrencyTypeId\"", OtherKey = "\"Id\"", CanBeNull = false)]
        public virtual CurrencyTypeEntity CurrencyType { get; set; }

		#endregion CurrencyTypeId
		

		#region QuantityByTransaction

        /// <summary>
        ///сумма в валюте транзакции
        /// </summary>
        [MapField("\"QuantityByTransaction\""), Required]
        public virtual decimal QuantityByTransaction { get; set; }

		#endregion QuantityByTransaction
		

		#region QuantityByWallet

        /// <summary>
        ///сумма в валюте счета
        /// </summary>
        [MapField("\"QuantityByWallet\""), Nullable]
        public virtual decimal? QuantityByWallet { get; set; }

		#endregion QuantityByWallet
		

		#region Fee

        /// <summary>
        ///комиссия
        /// </summary>
        [MapField("\"Fee\""), Nullable]
        public virtual decimal? Fee { get; set; }

		#endregion Fee
		

        #region FromWalletId

        int? _fromwalletid;

        /// <summary>
        ///перевод с счета
        /// </summary>
        [MapField("\"FromWalletId\""), Nullable]
        public virtual int? FromWalletId
        {
            get
            {
                return (!_fromwalletid.HasValue || _fromwalletid == 0 ) && FromWallet != null ? FromWallet.Id : _fromwalletid;
            }
            set { _fromwalletid = value; }
        }

		[Association(ThisKey = "\"FromWalletId\"", OtherKey = "\"Id\"", CanBeNull = false)]
        public virtual WalletEntity FromWallet { get; set; }

		#endregion FromWalletId
		

        #region ToWalletId

        int? _towalletid;

        /// <summary>
        ///перевод на счет
        /// </summary>
        [MapField("\"ToWalletId\""), Nullable]
        public virtual int? ToWalletId
        {
            get
            {
                return (!_towalletid.HasValue || _towalletid == 0 ) && ToWallet != null ? ToWallet.Id : _towalletid;
            }
            set { _towalletid = value; }
        }

		[Association(ThisKey = "\"ToWalletId\"", OtherKey = "\"Id\"", CanBeNull = false)]
        public virtual WalletEntity ToWallet { get; set; }

		#endregion ToWalletId
		

        #region MontlyReportId

        int? _montlyreportid;

        /// <summary>
        ///
        /// </summary>
        [MapField("\"MontlyReportId\""), Nullable]
        public virtual int? MontlyReportId
        {
            get
            {
                return (!_montlyreportid.HasValue || _montlyreportid == 0 ) && MontlyReport != null ? MontlyReport.Id : _montlyreportid;
            }
            set { _montlyreportid = value; }
        }

		[Association(ThisKey = "\"MontlyReportId\"", OtherKey = "\"Id\"", CanBeNull = false)]
        public virtual MontlyReportEntity MontlyReport { get; set; }

		#endregion MontlyReportId
		
        public override void UpdateAssociations()
        {
        }
  
    }
}