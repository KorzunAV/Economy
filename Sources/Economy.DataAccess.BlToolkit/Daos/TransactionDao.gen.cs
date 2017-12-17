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
    public partial class TransactionDao : BaseDao
    {
		#region Commands

		public TransactionDto Save(IBaseSessionManager manager, TransactionDto dto)
        {
            return InsertOrSelect<TransactionDto, TransactionEntity>(manager, dto);
        }

		public List<TransactionDto> SaveList(IBaseSessionManager manager, List<TransactionDto> dtos)
        {
            return InsertOrSelectList<TransactionDto, TransactionEntity>(manager, dtos);
        }

		#endregion Commands

        #region Queries

		public static TransactionDto GetById(IBaseSessionManager manager, int id)
        {
			var db = (EconomyDb)manager;
            var entity = db.TransactionTable.SingleOrDefault(i=>i.Id == id);
			return Mapper.Map<TransactionEntity,TransactionDto>(entity);
        }

		#endregion Queries
    }
}