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
    [TableName(Database = "economy", Owner = "public", Name = "\"SystemUser\"")]
    internal class SystemUserEntity : BaseEntity
    {

		#region Login

        /// <summary>
        ///
        /// </summary>
        [MapField("\"Login\""), Required, Unique]
        public virtual string Login { get; set; }

		#endregion Login
		

		#region Name

        /// <summary>
        ///
        /// </summary>
        [MapField("\"Name\""), Nullable]
        public virtual string Name { get; set; }

		#endregion Name
		
        /// <summary>
        ///пользователь
        /// </summary>
        [Association(ThisKey="SystemUserId", OtherKey="Id", CanBeNull=true)]
        public virtual List<WalletEntity> Wallets { get; set; }

        public override void UpdateAssociations()
        {
            foreach (var item in Wallets)
            {
                item.SystemUserId = Id;
            }
        }
  
    }
}