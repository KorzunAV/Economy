//---------------------------------------------------------------------------------------------------
// auto-generated
//---------------------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CQRS.Common;
using Economy.DataAccess.BlToolkit.DbManagers;
using Economy.DataAccess.BlToolkit.Entities;
using Economy.Dtos;

namespace Economy.DataAccess.BlToolkit.Daos
{
    public partial class WalletDao : BaseDao
    {
		#region Commands

		public WalletDto Save(IBaseSessionManager manager, WalletDto dto)
        {
            return InsertOrSelect<WalletDto, WalletEntity>(manager, dto);
        }

		public List<WalletDto> SaveList(IBaseSessionManager manager, List<WalletDto> dtos)
        {
            return InsertOrSelectList<WalletDto, WalletEntity>(manager, dtos);
        }

		#endregion Commands

        #region Queries

		public static WalletDto GetById(IBaseSessionManager manager, int id)
        {
			var db = (EconomyDb)manager;
            var entity = db.WalletTable.SingleOrDefault(i=>i.Id == id);
			return Mapper.Map<WalletEntity,WalletDto>(entity);
        }

		#endregion Queries
    }
}