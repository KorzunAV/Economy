//---------------------------------------------------------------------------------------------------
// auto-generated
//---------------------------------------------------------------------------------------------------
using System;
using CQRS.Logic.Queries;
using Economy.Dtos;

namespace Economy.Logic.Queries.BaseQueries
{
    public class GetCourseArhiveByIdQuery : BaseQuery
    {
        public static readonly Guid Id = new Guid("1AD0F966-EC48-4B0F-B6F7-3C704B0913B8");

        public override Guid QueryId => Id;

        public int Id { get; set; }

		public GetCourseArhiveByIdQuery(int id)
        {
            Id = id;
        }
    }
}