using System;
using CQRS.Logic.Queries;

namespace Economy.Logic.Queries
{
    public class BelinvestCourseArhiveGetAllQuery : BaseQuery
    {
        public static Guid Id = new Guid("1C9D5D14-7053-4728-BC77-D358C69F5A85");

        public override Guid QueryId
        {
            get { return Id; }
        }
    }
}