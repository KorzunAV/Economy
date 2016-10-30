using System;
using Economy.DataAccess.NHibernate.Entities;
using Economy.DataAccess.NHibernate.NHibernate;
using Economy.Dtos;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace Economy.DataAccess.NHibernate.Daos
{
    public class CourseArhiveDao : BaseDao
    {
        public CourseArhiveDao(ISessionManager sessionManager) : base(sessionManager) { }

        public CourseArhiveDto Save(CourseArhiveDto dto)
        {
            return Save<CourseArhiveDto, CourseArhiveEntity>(dto);
        }

        //public CourseArhiveDto Get(int id)
        //{
        //    CourseArhiveEntity alias = null;
        //    var restrictions = Restrictions.Eq(Projections.Property(() => alias.Id), id);

        //    var dto = SessionManager
        //        .CurrentSession
        //        .QueryOver(() => alias)
        //        .Select(GetProjections())
        //        .And(restrictions)
        //        .TransformUsing(Transformers.AliasToBean<CourseArhiveDto>())
        //        .SingleOrDefault<CourseArhiveDto>();
        //    return dto;
        //}

        private IProjection[] GetProjections()
        {
            CourseArhiveEntity alias = null;

            return new[]
            {
                Projections.Alias(Projections.Property(() => alias.CurrencyTypeId), "CurrencyTypeId"),
                Projections.Alias(Projections.Property(() => alias.RegDate), "RegDate"),
                Projections.Alias(Projections.Property(() => alias.CurrencyTypeBaseId), "CurrencyTypeBaseId"),
                Projections.Alias(Projections.Property(() => alias.BankId), "BankId"),
                Projections.Alias(Projections.Property(() => alias.Buy), "Buy"),
                Projections.Alias(Projections.Property(() => alias.Sel), "Sel"),

            };
        }
    }
}