using System;
using CQRS.Logic.Queries;
using Economy.Dtos;

namespace Economy.Logic.Queries
{
    public class CourseArhiveGetLastDataQuery : BaseQuery
    {
        public static Guid Id = new Guid("D53E8CE3-E79E-4733-93EC-BCDC02365C3B");

        public override Guid QueryId
        {
            get { return Id; }
        }

        public BankDto Bank { get; set; }


        public CourseArhiveGetLastDataQuery(BankDto bank)
        {
            Bank = bank;
        }
    }
}