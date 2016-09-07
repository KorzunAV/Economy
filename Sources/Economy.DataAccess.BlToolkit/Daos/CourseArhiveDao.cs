using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLToolkit.Data.Linq;
using CQRS.Common;
using Economy.DataAccess.BlToolkit.DbManagers;
using Economy.Dtos;

namespace Economy.DataAccess.BlToolkit.Daos
{
    public partial class CourseArhiveDao : BaseDao
    {
        #region Commands
        public void Save(List<CourseArhiveDto> dtos, IBaseSessionManager manager)
        {
            var db = (EconomyDb)manager;
            var entities = Mapper.Map<List<CourseArhiveDto>, List<CourseArhiveEntity>>(dtos);
            db.InsertBatch(entities);
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