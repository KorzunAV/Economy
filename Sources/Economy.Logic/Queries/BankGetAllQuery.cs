using System;
using CQRS.Logic.Queries;

namespace Economy.Logic.Queries
{
    public class BankGetAllQuery : BaseQuery
    {
        public static Guid Id = new Guid("2BC00CED-54A3-436B-9BC3-F5754A580F6E");

        public override Guid QueryId
        {
            get { return Id; }
        }
    }
}