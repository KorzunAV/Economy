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
    public partial class BankDao : BaseDao
    {
		#region Commands

		public BankDto Save(IBaseSessionManager manager, BankDto dto)
        {
            return InsertOrSelect<BankDto, BankEntity>(manager, dto);
        }

		public List<BankDto> SaveList(IBaseSessionManager manager, List<BankDto> dtos)
        {
            return InsertOrSelectList<BankDto, BankEntity>(manager, dtos);
        }

		#endregion Commands

        #region Queries

		public static BankDto GetById(IBaseSessionManager manager, int id)
        {
			var db = (EconomyDb)manager;
            var entity = db.BankTable.SingleOrDefault(i=>i.Id == id);
			return Mapper.Map<BankEntity,BankDto>(entity);
        }

		#endregion Queries
    }
}