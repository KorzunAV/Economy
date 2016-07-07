using System.Collections.Generic;
using CQRS.Common;
using CQRS.Logic;
using CQRS.Logic.Blos;
using CQRS.Logic.Commands;
using CQRS.Logic.Queries;
using CQRS.Logic.Validation;
using Economy.DataAccess.BlToolkit.Daos;
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

        private ExecutionResult<int> SaveRange(BaseCommand command, IBaseSessionManager manager)
        {
            var dtos = ((TransactionSaveRangeCommand)command).Dtos;
            //Validate(period);
            var id = TransactionDao.SaveRange(dtos, manager);
            return new ExecutionResult<int> { Data = id };
        }

        private ExecutionResult<List<TransactionDto>> GetAll(BaseQuery query, IBaseSessionManager manager)
        {
            var dtos = TransactionDao.GetAll(manager);
            return new ExecutionResult<List<TransactionDto>> { Data = dtos };
        }

        public override void RegisterCommandsAndQueries(ICommandQueryRegistrator commandQueryRegistrator)
        {
            // RegisterCommands
            commandQueryRegistrator.RegisterCommand(TransactionSaveRangeCommand.Id, SaveRange);
            // RegisterQueries
            commandQueryRegistrator.RegisterQuery(TransactionGetAllQuery.Id, GetAll);
        }
    }
}