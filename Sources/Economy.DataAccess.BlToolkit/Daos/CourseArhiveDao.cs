using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CQRS.Common;
using Economy.DataAccess.BlToolkit.DbManagers;
using Economy.DataAccess.BlToolkit.Entities;
using Economy.Dtos;

namespace Economy.DataAccess.BlToolkit.Daos
{
    public class CourseArhiveDao : BaseDao
    {
        #region Commands
        public void SaveAll(IBaseSessionManager manager, List<CourseArhiveDto> dtos)
        {
            foreach (var dto in dtos)
            {
                Save(manager, dto);
            }
        }

        public void Save(IBaseSessionManager manager, CourseArhiveDto dto)
        {
            Insert<CourseArhiveEntity, CourseArhiveDto>((EconomyDb)manager, dto);
        }
        #endregion

        #region Queries
        public List<CourseArhiveDto> GetAll(IBaseSessionManager manager)
        {
            var db = (EconomyDb)manager;
            var query = from c in db.CourseArhiveTable
                        select c;
            var entities = query.ToList();
            return Mapper.Map<List<CourseArhiveEntity>, List<CourseArhiveDto>>(entities);
        }

        public CourseArhiveDto GetLast(IBaseSessionManager manager)
        {
            var db = (EconomyDb)manager;

            var sortedRows = from b in db.CourseArhiveTable
                             orderby b.RegDate descending
                             select b;
            var lastest = sortedRows.Take(1);
            if (lastest.Any())
            {
                var value = lastest.FirstOrDefault();
                return Mapper.Map<CourseArhiveEntity, CourseArhiveDto>(value);
            }

            return null;
        }
        #endregion
    }
}