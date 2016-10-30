using System;
using Economy.DataAccess.NHibernate.Entities;
using Economy.DataAccess.NHibernate.NHibernate;
using Economy.Dtos;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace Economy.DataAccess.NHibernate.Daos
{
    public class MontlyReportDao : BaseDao
    {
        public MontlyReportDao(ISessionManager sessionManager) : base(sessionManager) { }

        public MontlyReportDto Save(MontlyReportDto dto)
        {
            return Save<MontlyReportDto, MontlyReportEntity>(dto);
        }

        public MontlyReportDto Get(Guid id)
        {
            MontlyReportEntity alias = null;
            var restrictions = Restrictions.Eq(Projections.Property(() => alias.Id), id);

            var dto = SessionManager
                .CurrentSession
                .QueryOver(() => alias)
                .Select(GetProjections())
                .And(restrictions)
                .TransformUsing(Transformers.AliasToBean<MontlyReportDto>())
                .SingleOrDefault<MontlyReportDto>();
            return dto;
        }

        private IProjection[] GetProjections()
        {
            MontlyReportEntity alias = null;

            return new[]
            {
                Projections.Alias(Projections.Property(() => alias.Id), "Id"),
                Projections.Alias(Projections.Property(() => alias.StartBalance), "StartBalance"),
                Projections.Alias(Projections.Property(() => alias.EndBalance), "EndBalance"),
                Projections.Alias(Projections.Property(() => alias.StartDate), "StartDate"),
                Projections.Alias(Projections.Property(() => alias.WalletId), "WalletId"),
                Projections.Alias(Projections.Property(() => alias.Version), "Version"),

            };
        }
    }
}