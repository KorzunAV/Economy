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
    [TableName(Database = "economy", Owner = "public", Name = "\"Bank\"")]
    internal class BankEntity : BaseEntity
    {

		#region Name

        /// <summary>
        ///
        /// </summary>
        [MapField("\"Name\""), Nullable, Unique]
        public virtual string Name { get; set; }

		#endregion Name
		
        /// <summary>
        ///Банк
        /// </summary>
        [Association(ThisKey="BankId", OtherKey="Id", CanBeNull=true)]
        public virtual List<CourseArhiveEntity> CourseArhives { get; set; }

        /// <summary>
        ///--Привязка к банку
        /// </summary>
        [Association(ThisKey="BankId", OtherKey="Id", CanBeNull=true)]
        public virtual List<WalletEntity> Wallets { get; set; }

        public override void UpdateAssociations()
        {
            foreach (var item in CourseArhives)
            {
                item.BankId = Id;
            }
            foreach (var item in Wallets)
            {
                item.BankId = Id;
            }
        }
  
    }
}