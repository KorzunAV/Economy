//---------------------------------------------------------------------------------------------------
// auto-generated
//---------------------------------------------------------------------------------------------------
using System.Collections.Generic;
using CQRS.Common;
using CQRS.Logic;
using CQRS.Logic.Blos;
using CQRS.Logic.Commands;
using CQRS.Logic.Queries;
using CQRS.Logic.Validation;
using Economy.DataAccess.BlToolkit.Daos;
using Economy.Dtos;
using Economy.Logic.Commands.SaveCommands;
using Economy.Logic.Commands.SaveListCommands;
using Economy.Logic.Queries;


namespace Economy.Logic.Blos
{
    public partial class TransactionBlo : BaseBlo
    {
	    private readonly TransactionDao _transactionDao;

        public TransactionBlo(ValidationManager validationManager, TransactionDao transactionDao)
            : base(validationManager)
        {
            _transactionDao = transactionDao;
        }	

		private ExecutionResult<TransactionDto> Save(IBaseSessionManager manager, BaseCommand command)
        {
            var dto = ((TransactionSaveCommand)command).Dto;
            //Validate(dto);
            var result = _transactionDao.Save(manager, dto);
            return new ExecutionResult<TransactionDto> { Data = result };
        }
		
		private ExecutionResult<List<TransactionDto>> SaveList(IBaseSessionManager manager, BaseCommand command)
        {
            var dtos = ((TransactionSaveListCommand)command).Dtos;
            //Validate(dto);
            var result = _transactionDao.SaveList(manager, dtos);
            return new ExecutionResult<List<TransactionDto>> { Data = result };
        }

        public override void RegisterCommandsAndQueries(ICommandQueryRegistrator commandQueryRegistrator)
        {
            // RegisterCommands
            commandQueryRegistrator.RegisterCommand(TransactionSaveCommand.Id, Save);
            commandQueryRegistrator.RegisterCommand(TransactionSaveListCommand.Id, SaveList);
            // RegisterQueries

			RegisterCommandsAndQueries2(commandQueryRegistrator);
        }
				
        partial void RegisterCommandsAndQueries2(ICommandQueryRegistrator commandQueryRegistrator);
    }
}