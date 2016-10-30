using System;
using Economy.DataAccess.NHibernate.Entities;
using Economy.DataAccess.NHibernate.NHibernate;
using Economy.Dtos;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace Economy.DataAccess.NHibernate.Daos
{
    public class CurrencyTypeDao : BaseDao
    {
        public CurrencyTypeDao(ISessionManager sessionManager) : base(sessionManager) { }

        public CurrencyTypeDto Save(CurrencyTypeDto dto)
        {
            return Save<CurrencyTypeDto, CurrencyTypeEntity>(dto);
        }

        public CurrencyTypeDto Get(int id)
        {
            CurrencyTypeEntity alias = null;
            var restrictions = Restrictions.Eq(Projections.Property(() => alias.Id), id);

            var dto = SessionManager
                .CurrentSession
                .QueryOver(() => alias)
                .Select(GetProjections())
                .And(restrictions)
                .TransformUsing(Transformers.AliasToBean<CurrencyTypeDto>())
                .SingleOrDefault<CurrencyTypeDto>();
            return dto;
        }

        private IProjection[] GetProjections()
        {
            CurrencyTypeEntity alias = null;

            return new[]
            {
                Projections.Alias(Projections.Property(() => alias.Id), "Id"),
                Projections.Alias(Projections.Property(() => alias.Name), "Name"),
                Projections.Alias(Projections.Property(() => alias.ShortName), "ShortName"),
                Projections.Alias(Projections.Property(() => alias.Version), "Version"),

            };
        }
    }
}