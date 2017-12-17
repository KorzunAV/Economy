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
    public partial class MontlyReportDao : BaseDao
    {
		#region Commands

		public MontlyReportDto Save(IBaseSessionManager manager, MontlyReportDto dto)
        {
            return InsertOrSelect<MontlyReportDto, MontlyReportEntity>(manager, dto);
        }

		public List<MontlyReportDto> SaveList(IBaseSessionManager manager, List<MontlyReportDto> dtos)
        {
            return InsertOrSelectList<MontlyReportDto, MontlyReportEntity>(manager, dtos);
        }

		#endregion Commands

        #region Queries

		public static MontlyReportDto GetById(IBaseSessionManager manager, int id)
        {
			var db = (EconomyDb)manager;
            var entity = db.MontlyReportTable.SingleOrDefault(i=>i.Id == id);
			return Mapper.Map<MontlyReportEntity,MontlyReportDto>(entity);
        }

		#endregion Queries
    }
}