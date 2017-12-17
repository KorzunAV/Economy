//---------------------------------------------------------------------------------------------------
// auto-generated
//---------------------------------------------------------------------------------------------------
using System;
using CQRS.Logic.Queries;
using Economy.Dtos;

namespace Economy.Logic.Queries.BaseQueries
{
    public class GetTransactionByIdQuery : BaseQuery
    {
        public static readonly Guid Id = new Guid("043C2F81-8675-495B-A165-A12973188B5F");

        public override Guid QueryId => Id;

        public int Id { get; set; }

		public GetTransactionByIdQuery(int id)
        {
            Id = id;
        }
    }
}