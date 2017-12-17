//---------------------------------------------------------------------------------------------------
// auto-generated
//---------------------------------------------------------------------------------------------------
using System;
using CQRS.Logic.Queries;
using Economy.Dtos;

namespace Economy.Logic.Queries.BaseQueries
{
    public class GetMontlyReportByIdQuery : BaseQuery
    {
        public static readonly Guid Id = new Guid("6C5667A9-AC2D-4931-B6AC-F1CD1EC9B784");

        public override Guid QueryId => Id;

        public int Id { get; set; }

		public GetMontlyReportByIdQuery(int id)
        {
            Id = id;
        }
    }
}