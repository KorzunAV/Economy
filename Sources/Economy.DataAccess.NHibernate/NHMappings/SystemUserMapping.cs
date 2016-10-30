using Economy.DataAccess.NHibernate.Entities;

namespace Economy.DataAccess.NHibernate.NHMappings
{
    public class SystemUserMapping : BaseMapping<SystemUserEntity>
    {
        public SystemUserMapping()
        {
            Id(v => v.Id).GeneratedBy.Guid();
            Map(v => v.Name);
            OptimisticLock.Version();
            Version(entity => entity.Version);

        }
    }
}