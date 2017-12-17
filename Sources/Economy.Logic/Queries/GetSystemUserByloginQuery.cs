using System;
using CQRS.Logic.Queries;

namespace Economy.Logic.Queries
{
    public class GetSystemUserByLoginQuery : BaseQuery
    {
        public static readonly Guid Id = new Guid("C62BAB78-1A7E-499B-8079-0490B40EFD22");

        public override Guid QueryId => Id;

        public string Login { get; set; }

		public GetSystemUserByLoginQuery(string login)
        {
            Login = login;
        }
    }
}