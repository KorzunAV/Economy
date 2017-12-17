using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Validation;

namespace Economy.DataAccess.BlToolkit.Entities
{
    internal abstract class BaseEntity
    {
        #region Id

        /// <summary>
        ///идентификатор
        /// </summary>
        [MapField("\"Id\""), Identity, PrimaryKey, Required]
        public virtual int Id { get; set; }

        #endregion Id


        #region Version

        /// <summary>
        ///Версия для оптимистической блокировки
        /// </summary>
        [MapField("\"Version\""), Required]
        public virtual int Version { get; set; }

        #endregion Version  

        public abstract void UpdateAssociations();
    }
}
