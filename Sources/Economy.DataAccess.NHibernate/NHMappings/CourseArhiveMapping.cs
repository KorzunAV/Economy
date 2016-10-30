using Economy.DataAccess.NHibernate.Entities;

namespace Economy.DataAccess.NHibernate.NHMappings
{
    public class CourseArhiveMapping : BaseMapping<CourseArhiveEntity>
    {
        public CourseArhiveMapping()
        {
            CompositeId()
                .KeyReference(x => x.CurrencyType, "CurrencyTypeId")
                .KeyReference(x => x.RegDate)
                .KeyReference(x => x.Bank, "BankId");
            Map(v => v.CurrencyTypeBaseId);
            References(v => v.CurrencyTypeBase).Column(Column(v => v.CurrencyTypeBaseId)).ReadOnly().LazyLoad();
            Map(v => v.Buy);
            Map(v => v.Sel);

        }
    }
}