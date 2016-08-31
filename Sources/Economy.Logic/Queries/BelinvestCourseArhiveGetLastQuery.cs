using System;
using CQRS.Logic.Queries;

namespace Economy.Logic.Queries
{
    public class CourseArhiveGetLastQuery : BaseQuery
    {
        public static Guid Id = new Guid("D53E8CE3-E79E-4733-93EC-BCDC02365C3B");

        public override Guid QueryId
        {
            get { return Id; }
        }
    }
}