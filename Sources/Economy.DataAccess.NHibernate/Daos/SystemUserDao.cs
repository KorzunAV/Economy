using System;
using Economy.DataAccess.NHibernate.Entities;
using Economy.DataAccess.NHibernate.NHibernate;
using Economy.Dtos;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace Economy.DataAccess.NHibernate.Daos
{
    public class SystemUserDao : BaseDao
    {
        public SystemUserDao(ISessionManager sessionManager) : base(sessionManager) { }

        public SystemUserDto Save(SystemUserDto dto)
        {
            return Save<SystemUserDto, SystemUserEntity>(dto);
        }

        public SystemUserDto Get(Guid id)
        {
            SystemUserEntity alias = null;
            var restrictions = Restrictions.Eq(Projections.Property(() => alias.Id), id);

            var dto = SessionManager
                .CurrentSession
                .QueryOver(() => alias)
                .Select(GetProjections())
                .And(restrictions)
                .TransformUsing(Transformers.AliasToBean<SystemUserDto>())
                .SingleOrDefault<SystemUserDto>();
            return dto;
        }

        private IProjection[] GetProjections()
        {
            SystemUserEntity alias = null;

            return new[]
            {
                Projections.Alias(Projections.Property(() => alias.Id), "Id"),
                Projections.Alias(Projections.Property(() => alias.Name), "Name"),
                Projections.Alias(Projections.Property(() => alias.Version), "Version"),

            };
        }
    }
}