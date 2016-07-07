using System;
using CQRS.Logic.Queries;

namespace Economy.Logic.Queries
{
    public class TransactionGetAllQuery : BaseQuery
    {
        public static Guid Id = new Guid("D91CA193-6787-40FC-87A2-3A027E6C7827");

        public override Guid QueryId
        {
            get { return Id; }
        }
    }
}