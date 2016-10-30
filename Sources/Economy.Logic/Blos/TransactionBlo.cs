using System;
using System.Collections.Generic;
using CQRS.Common;
using CQRS.Logic;
using CQRS.Logic.Blos;
using CQRS.Logic.Commands;
using CQRS.Logic.Queries;
using CQRS.Logic.Validation;
using Economy.DataAccess.NHibernate.Daos;
using Economy.Dtos;
using Economy.Logic.Commands;
using Economy.Logic.Queries;

namespace Economy.Logic.Blos
{
    public class TransactionBlo : BaseBlo
    {
        private TransactionDao TransactionDao { get; set; }

        public TransactionBlo(ValidationManager validationManager, TransactionDao transactionDao)
            : base(validationManager)
        {
            TransactionDao = transactionDao;
        }

        private ExecutionResult<Guid> Save(IBaseSessionManager manager, BaseCommand command)
        {
            var dto = ((TransactionSaveCommand)command).Dto;
            Validate(dto);
            var rez = TransactionDao.Save(dto);
            return new ExecutionResult<Guid> { Data = rez.Id };
        }
        
        public override void RegisterCommandsAndQueries(ICommandQueryRegistrator commandQueryRegistrator)
        {
            // RegisterCommands
            commandQueryRegistrator.RegisterCommand(TransactionSaveCommand.Id, Save);
            // RegisterQueries
        }
    }
}