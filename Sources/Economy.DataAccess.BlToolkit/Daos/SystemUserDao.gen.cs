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
    public partial class SystemUserDao : BaseDao
    {
		#region Commands

		public SystemUserDto Save(IBaseSessionManager manager, SystemUserDto dto)
        {
            return InsertOrSelect<SystemUserDto, SystemUserEntity>(manager, dto);
        }

		public List<SystemUserDto> SaveList(IBaseSessionManager manager, List<SystemUserDto> dtos)
        {
            return InsertOrSelectList<SystemUserDto, SystemUserEntity>(manager, dtos);
        }

		#endregion Commands

        #region Queries

		public static SystemUserDto GetById(IBaseSessionManager manager, int id)
        {
			var db = (EconomyDb)manager;
            var entity = db.SystemUserTable.SingleOrDefault(i=>i.Id == id);
			return Mapper.Map<SystemUserEntity,SystemUserDto>(entity);
        }

		#endregion Queries
    }
}