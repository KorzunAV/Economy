using Economy.DataAccess.NHibernate.Entities;

namespace Economy.DataAccess.NHibernate.NHMappings
{
    public class CourseArhiveMapping : BaseMapping<CourseArhiveEntity>
    {
        public CourseArhiveMapping()
        {
            Id(v => v.Id, "\"Id\"").GeneratedBy.Increment();
            Map(v => v.CurrencyTypeId, "\"CurrencyTypeId\"");
            References(v => v.CurrencyType, "\"CurrencyTypeId\"").Column(Column(v => v.CurrencyTypeId)).ReadOnly().LazyLoad();
            Map(v => v.RegDate, "\"RegDate\"");
            Map(v => v.CurrencyTypeBaseId, "\"CurrencyTypeBaseId\"");
            References(v => v.CurrencyTypeBase, "\"CurrencyTypeBaseId\"").Column(Column(v => v.CurrencyTypeBaseId)).ReadOnly().LazyLoad();
            Map(v => v.BankId, "\"BankId\"");
            References(v => v.Bank, "\"BankId\"").Column(Column(v => v.BankId)).ReadOnly().LazyLoad();
            Map(v => v.Buy, "\"Buy\"");
            Map(v => v.Sel, "\"Sel\"");
            OptimisticLock.Version();
            Version(entity => entity.Version).Column("\"Version\"");

        }
    }
}