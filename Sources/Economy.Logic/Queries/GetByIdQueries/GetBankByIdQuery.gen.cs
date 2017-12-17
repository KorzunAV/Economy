//---------------------------------------------------------------------------------------------------
// auto-generated
//---------------------------------------------------------------------------------------------------
using System;
using CQRS.Logic.Queries;
using Economy.Dtos;

namespace Economy.Logic.Queries.BaseQueries
{
    public class GetBankByIdQuery : BaseQuery
    {
        public static readonly Guid Id = new Guid("938AD0CE-431D-4C79-A5C0-7C3ED8020102");

        public override Guid QueryId => Id;

        public int Id { get; set; }

		public GetBankByIdQuery(int id)
        {
            Id = id;
        }
    }
}