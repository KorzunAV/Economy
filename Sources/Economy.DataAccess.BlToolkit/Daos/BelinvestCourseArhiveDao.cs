using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLToolkit.Data.Linq;
using CQRS.Common;
using Economy.DataAccess.BlToolkit.DbManagers;
using Economy.Dtos;

namespace Economy.DataAccess.BlToolkit.Daos
{
    public partial class BelinvestCourseArhiveDao : BaseDao
    {
        #region Commands
        public void Save(List<BelinvestCourseArhiveDto> dtos, IBaseSessionManager manager)
        {
            var db = (EconomyDb)manager;
            var entities = Mapper.Map<List<BelinvestCourseArhiveDto>, List<BelinvestCourseArhiveEntity>>(dtos);
            db.InsertBatch(entities);
        }
        #endregion

        #region Queries
        public List<BelinvestCourseArhiveDto> GetAll(IBaseSessionManager manager)
        {
            var db = (EconomyDb)manager;
            var query = from c in db.BelinvestCourseArhiveTable
                        select c;
            var entities = query.ToList();
            return Mapper.Map<List<BelinvestCourseArhiveEntity>, List<BelinvestCourseArhiveDto>>(entities);
        }

        public BelinvestCourseArhiveDto GetLast(IBaseSessionManager manager)
        {
            var db = (EconomyDb)manager;

            var sortedRows = from b in db.BelinvestCourseArhiveTable
                             orderby b.RegDate descending
                             select b;
            var lastest = sortedRows.Take(1);
            if (lastest.Any())
            {
                var value = lastest.FirstOrDefault();
                return Mapper.Map<BelinvestCourseArhiveEntity, BelinvestCourseArhiveDto>(value);
            }

            return null;
        }
        #endregion
    }
}