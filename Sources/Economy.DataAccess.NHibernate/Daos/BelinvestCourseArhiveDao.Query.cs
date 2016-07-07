using System.Collections.Generic;
using Economy.DataAccess.NHibernate.Entities;
using Economy.Dtos.Commands;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace Economy.DataAccess.NHibernate.Daos
{
    public partial class BelinvestCourseArhiveDao
    {
        public IList<BelinvestCourseArhiveDto> GetAll()
        {
            BelinvestCourseArhiveEntity alias = null;
            //var restrictions = Restrictions.Eq(Projections.Property(() => alias.Id), id);

            var dto = SessionManager
                .CurrentSession
                .QueryOver(() => alias)
                .Select(GetProjections())
                //.And(restrictions)
                .TransformUsing(Transformers.AliasToBean<BelinvestCourseArhiveDto>())
                .List<BelinvestCourseArhiveDto>();
            return dto;
        }

        private IProjection[] GetProjections()
        {
            BelinvestCourseArhiveEntity alias = null;

            return new[]
            {
                Projections.Alias(Projections.Property(() => alias.Id), BaseDto.PropId),
                Projections.Alias(Projections.Property(() => alias.Date), BelinvestCourseArhiveDto.PropDate),
                Projections.Alias(Projections.Property(() => alias.Currency), BelinvestCourseArhiveDto.PropCurrency),
                Projections.Alias(Projections.Property(() => alias.Buy), BelinvestCourseArhiveDto.PropBuy),
                Projections.Alias(Projections.Property(() => alias.Sel), BelinvestCourseArhiveDto.PropSel)
            };
        }
    }
}