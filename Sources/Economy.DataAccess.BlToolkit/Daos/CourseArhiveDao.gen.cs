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
    public partial class CourseArhiveDao : BaseDao
    {
		#region Commands

		public CourseArhiveDto Save(IBaseSessionManager manager, CourseArhiveDto dto)
        {
            return InsertOrSelect<CourseArhiveDto, CourseArhiveEntity>(manager, dto);
        }

		public List<CourseArhiveDto> SaveList(IBaseSessionManager manager, List<CourseArhiveDto> dtos)
        {
            return InsertOrSelectList<CourseArhiveDto, CourseArhiveEntity>(manager, dtos);
        }

		#endregion Commands

        #region Queries

		public static CourseArhiveDto GetById(IBaseSessionManager manager, int id)
        {
			var db = (EconomyDb)manager;
            var entity = db.CourseArhiveTable.SingleOrDefault(i=>i.Id == id);
			return Mapper.Map<CourseArhiveEntity,CourseArhiveDto>(entity);
        }

		#endregion Queries
    }
}