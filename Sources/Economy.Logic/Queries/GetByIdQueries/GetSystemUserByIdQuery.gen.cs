//---------------------------------------------------------------------------------------------------
// auto-generated
//---------------------------------------------------------------------------------------------------
using System;
using CQRS.Logic.Queries;
using Economy.Dtos;

namespace Economy.Logic.Queries.BaseQueries
{
    public class GetSystemUserByIdQuery : BaseQuery
    {
        public static readonly Guid Id = new Guid("32938626-A1B4-4E02-819F-4E270B350689");

        public override Guid QueryId => Id;

        public int Id { get; set; }

		public GetSystemUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}