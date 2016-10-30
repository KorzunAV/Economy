using System;
using Economy.DataAccess.NHibernate.Entities;
using Economy.DataAccess.NHibernate.NHibernate;
using Economy.Dtos;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace Economy.DataAccess.NHibernate.Daos
{
    public class TransactionDao : BaseDao
    {
        public TransactionDao(ISessionManager sessionManager) : base(sessionManager) { }

        public TransactionDto Save(TransactionDto dto)
        {
            return Save<TransactionDto, TransactionEntity>(dto);
        }

        public TransactionDto Get(Guid id)
        {
            TransactionEntity alias = null;
            var restrictions = Restrictions.Eq(Projections.Property(() => alias.Id), id);

            var dto = SessionManager
                .CurrentSession
                .QueryOver(() => alias)
                .Select(GetProjections())
                .And(restrictions)
                .TransformUsing(Transformers.AliasToBean<TransactionDto>())
                .SingleOrDefault<TransactionDto>();
            return dto;
        }

        private IProjection[] GetProjections()
        {
            TransactionEntity alias = null;

            return new[]
            {
                Projections.Alias(Projections.Property(() => alias.Id), "Id"),
                Projections.Alias(Projections.Property(() => alias.RegistrationDate), "RegistrationDate"),
                Projections.Alias(Projections.Property(() => alias.TransactionDate), "TransactionDate"),
                Projections.Alias(Projections.Property(() => alias.Code), "Code"),
                Projections.Alias(Projections.Property(() => alias.Description), "Description"),
                Projections.Alias(Projections.Property(() => alias.CurrencyTypeId), "CurrencyTypeId"),
                Projections.Alias(Projections.Property(() => alias.QuantityByTransaction), "QuantityByTransaction"),
                Projections.Alias(Projections.Property(() => alias.QuantityByWallet), "QuantityByWallet"),
                Projections.Alias(Projections.Property(() => alias.Commission), "Commission"),
                Projections.Alias(Projections.Property(() => alias.FromWalletId), "FromWalletId"),
                Projections.Alias(Projections.Property(() => alias.ToWalletId), "ToWalletId"),
                Projections.Alias(Projections.Property(() => alias.MontlyReportId), "MontlyReportId"),
                Projections.Alias(Projections.Property(() => alias.Version), "Version"),

            };
        }
    }
}