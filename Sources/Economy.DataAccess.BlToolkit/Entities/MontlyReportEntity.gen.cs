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
    [TableName(Database = "economy", Owner = "public", Name = "\"MontlyReport\"")]
    internal class MontlyReportEntity : BaseEntity
    {

		#region StartBalance

        /// <summary>
        ///баланс на начало месяца
        /// </summary>
        [MapField("\"StartBalance\""), Required]
        public virtual decimal StartBalance { get; set; }

		#endregion StartBalance
		

		#region EndBalance

        /// <summary>
        ///баланс на конец месяца
        /// </summary>
        [MapField("\"EndBalance\""), Required]
        public virtual decimal EndBalance { get; set; }

		#endregion EndBalance
		

		#region StartDate

        /// <summary>
        ///Период действия (год месяц)
        /// </summary>
        [MapField("\"StartDate\""), Required, Unique]
        public virtual DateTime StartDate { get; set; }

		#endregion StartDate
		

        #region WalletId

        int _walletid;

        /// <summary>
        ///Идентификатор кошелька
        /// </summary>
        [MapField("\"WalletId\""), Required, Unique]
        public virtual int WalletId
        {
            get
            {
                return  _walletid == 0  && Wallet != null ? Wallet.Id : _walletid;
            }
            set { _walletid = value; }
        }

		[Association(ThisKey = "\"WalletId\"", OtherKey = "\"Id\"", CanBeNull = false)]
        public virtual WalletEntity Wallet { get; set; }

		#endregion WalletId
		
        /// <summary>
        ///
        /// </summary>
        [Association(ThisKey="MontlyReportId", OtherKey="Id", CanBeNull=true)]
        public virtual List<TransactionEntity> Transactions { get; set; }

        public override void UpdateAssociations()
        {
            foreach (var item in Transactions)
            {
                item.MontlyReportId = Id;
            }
        }
  
    }
}