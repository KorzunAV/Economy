using System;
using CQRS.Logic.Queries;

namespace Economy.Logic.Queries
{
    public class MontlyReportGetAllQuery : BaseQuery
    {
        public static Guid Id = new Guid("DDF52922-61D0-47AD-BFAE-669360E3993C");

        public override Guid QueryId
        {
            get { return Id; }
        }
    }
}