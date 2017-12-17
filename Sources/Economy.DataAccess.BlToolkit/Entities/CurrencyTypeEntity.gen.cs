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
    [TableName(Database = "economy", Owner = "public", Name = "\"CurrencyType\"")]
    internal class CurrencyTypeEntity : BaseEntity
    {

		#region Name

        /// <summary>
        ///наименование валюты
        /// </summary>
        [MapField("\"Name\""), Nullable]
        public virtual string Name { get; set; }

		#endregion Name
		

		#region ShortName

        /// <summary>
        ///трехбуквенное обозначение
        /// </summary>
        [MapField("\"ShortName\""), Required, Unique]
        public virtual string ShortName { get; set; }

		#endregion ShortName
		
        /// <summary>
        ///валюта
        /// </summary>
        [Association(ThisKey="CurrencyTypeId", OtherKey="Id", CanBeNull=true)]
        public virtual List<CourseArhiveEntity> CourseArhives { get; set; }

        /// <summary>
        ///валюта транзакции
        /// </summary>
        [Association(ThisKey="CurrencyTypeId", OtherKey="Id", CanBeNull=true)]
        public virtual List<TransactionEntity> Transactions { get; set; }

        /// <summary>
        ///валюта кошелька
        /// </summary>
        [Association(ThisKey="CurrencyTypeId", OtherKey="Id", CanBeNull=true)]
        public virtual List<WalletEntity> Wallets { get; set; }

        public override void UpdateAssociations()
        {
            foreach (var item in CourseArhives)
            {
                item.CurrencyTypeId = Id;
            }
            foreach (var item in Transactions)
            {
                item.CurrencyTypeId = Id;
            }
            foreach (var item in Wallets)
            {
                item.CurrencyTypeId = Id;
            }
        }
  
    }
}