using System;
using CQRS.Logic.Queries;

namespace Economy.Logic.Queries
{
    public class CurrencyTypeGetAllQuery : BaseQuery
    {
        public static Guid Id = new Guid("26BD0207-9AC3-4EAD-8D3E-C7E1DE15F9E8");

        public override Guid QueryId
        {
            get { return Id; }
        }
    }
}