using System.Collections.Generic;
using Economy.DataAccess.NHibernate.Entities;
using Economy.Dtos.Commands;

namespace Economy.DataAccess.NHibernate.Daos
{
    public partial class BelinvestCourseArhiveDao
    {
        public void Save(List<BelinvestCourseArhiveDto> dtos)
        {
            foreach (var dto in dtos)
            {
                Save(dto);
            }
        }

        public long Save(BelinvestCourseArhiveDto dto)
        {
            return Save<BelinvestCourseArhiveDto, BelinvestCourseArhiveEntity>(dto);
        }
    }
}