using System;
using CQRS.Logic.Commands;
using Economy.Dtos;

namespace Economy.Logic.Commands
{
    public class TransactionSaveCommand : BaseCommand
    {
        public static readonly Guid Id = new Guid("65C303FD-73C8-4D6B-8482-6480888A4859");

        public override Guid CommandId
        {
            get { return Id; }
        }

        public TransactionDto Dto { get; set; }
    }
}