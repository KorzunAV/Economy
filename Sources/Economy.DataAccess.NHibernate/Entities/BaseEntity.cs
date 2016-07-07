namespace Economy.DataAccess.NHibernate.Entities
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// Identifier of an entity
        /// </summary>
        public virtual long Id { get; set; }
    }
}