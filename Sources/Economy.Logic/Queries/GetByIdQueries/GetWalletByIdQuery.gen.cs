//---------------------------------------------------------------------------------------------------
// auto-generated
//---------------------------------------------------------------------------------------------------
using System;
using CQRS.Logic.Queries;
using Economy.Dtos;

namespace Economy.Logic.Queries.BaseQueries
{
    public class GetWalletByIdQuery : BaseQuery
    {
        public static readonly Guid Id = new Guid("8747FA8D-FEE5-43EE-B64D-99385C3A936A");

        public override Guid QueryId => Id;

        public int Id { get; set; }

		public GetWalletByIdQuery(int id)
        {
            Id = id;
        }
    }
}