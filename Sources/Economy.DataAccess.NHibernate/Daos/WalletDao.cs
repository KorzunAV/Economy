using System;
using Economy.DataAccess.NHibernate.Entities;
using Economy.DataAccess.NHibernate.NHibernate;
using Economy.Dtos;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace Economy.DataAccess.NHibernate.Daos
{
    public class WalletDao : BaseDao
    {
        public WalletDao(ISessionManager sessionManager) : base(sessionManager) { }

        public WalletDto Save(WalletDto dto)
        {
            return Save<WalletDto, WalletEntity>(dto);
        }

        public WalletDto Get(Guid id)
        {
            WalletEntity alias = null;
            var restrictions = Restrictions.Eq(Projections.Property(() => alias.Id), id);

            var dto = SessionManager
                .CurrentSession
                .QueryOver(() => alias)
                .Select(GetProjections())
                .And(restrictions)
                .TransformUsing(Transformers.AliasToBean<WalletDto>())
                .SingleOrDefault<WalletDto>();
            return dto;
        }

        private IProjection[] GetProjections()
        {
            WalletEntity alias = null;

            return new[]
            {
                Projections.Alias(Projections.Property(() => alias.Id), "Id"),
                Projections.Alias(Projections.Property(() => alias.Name), "Name"),
                Projections.Alias(Projections.Property(() => alias.StartBalance), "StartBalance"),
                Projections.Alias(Projections.Property(() => alias.Balance), "Balance"),
                Projections.Alias(Projections.Property(() => alias.SystemUserId), "SystemUserId"),
                Projections.Alias(Projections.Property(() => alias.CurrencyTypeId), "CurrencyTypeId"),
                Projections.Alias(Projections.Property(() => alias.Version), "Version"),

            };
        }
    }
}