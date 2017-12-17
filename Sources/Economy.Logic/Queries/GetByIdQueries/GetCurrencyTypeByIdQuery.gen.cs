//---------------------------------------------------------------------------------------------------
// auto-generated
//---------------------------------------------------------------------------------------------------
using System;
using CQRS.Logic.Queries;
using Economy.Dtos;

namespace Economy.Logic.Queries.BaseQueries
{
    public class GetCurrencyTypeByIdQuery : BaseQuery
    {
        public static readonly Guid Id = new Guid("EAC2E9D4-E3BD-43BA-B9E2-536672525B4B");

        public override Guid QueryId => Id;

        public int Id { get; set; }

		public GetCurrencyTypeByIdQuery(int id)
        {
            Id = id;
        }
    }
}