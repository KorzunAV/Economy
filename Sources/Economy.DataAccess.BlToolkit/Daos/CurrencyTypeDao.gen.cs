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
    public partial class CurrencyTypeDao : BaseDao
    {
		#region Commands

		public CurrencyTypeDto Save(IBaseSessionManager manager, CurrencyTypeDto dto)
        {
            return InsertOrSelect<CurrencyTypeDto, CurrencyTypeEntity>(manager, dto);
        }

		public List<CurrencyTypeDto> SaveList(IBaseSessionManager manager, List<CurrencyTypeDto> dtos)
        {
            return InsertOrSelectList<CurrencyTypeDto, CurrencyTypeEntity>(manager, dtos);
        }

		#endregion Commands

        #region Queries

		public static CurrencyTypeDto GetById(IBaseSessionManager manager, int id)
        {
			var db = (EconomyDb)manager;
            var entity = db.CurrencyTypeTable.SingleOrDefault(i=>i.Id == id);
			return Mapper.Map<CurrencyTypeEntity,CurrencyTypeDto>(entity);
        }

		#endregion Queries
    }
}