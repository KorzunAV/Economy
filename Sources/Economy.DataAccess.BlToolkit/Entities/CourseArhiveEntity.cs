using Economy.DataAccess.BlToolkit.DbManagers;

namespace Economy.DataAccess.BlToolkit.Entities
{
    internal class CourseArhiveEntity : CourseArhiveBaseEntity
    {
        public override int CurrencyTypeId
        {
            get
            {
                return base.CurrencyTypeId == 0 && CurrencyType != null
                    ? CurrencyType.Id
                    : base.CurrencyTypeId;
            }
            set { base.CurrencyTypeId = value; }
        }

        public override int CurrencyTypeBaseId
        {
            get
            {
                return base.CurrencyTypeBaseId == 0 && CurrencyTypeBase != null
                    ? CurrencyTypeBase.Id
                    : base.CurrencyTypeBaseId;
            }
            set { base.CurrencyTypeBaseId = value; }
        }
        
        public override int BankId
        {
            get
            {
                return base.BankId == 0 && Bank != null
                    ? Bank.Id
                    : base.BankId;
            }
            set { base.BankId = value; }
        }
    }
}