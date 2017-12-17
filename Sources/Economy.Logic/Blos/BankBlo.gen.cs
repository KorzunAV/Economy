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
    public partial class BankBlo : BaseBlo
    {
	    private readonly BankDao _bankDao;

        public BankBlo(ValidationManager validationManager, BankDao bankDao)
            : base(validationManager)
        {
            _bankDao = bankDao;
        }	

		private ExecutionResult<BankDto> Save(IBaseSessionManager manager, BaseCommand command)
        {
            var dto = ((BankSaveCommand)command).Dto;
            //Validate(dto);
            var result = _bankDao.Save(manager, dto);
            return new ExecutionResult<BankDto> { Data = result };
        }
		
		private ExecutionResult<List<BankDto>> SaveList(IBaseSessionManager manager, BaseCommand command)
        {
            var dtos = ((BankSaveListCommand)command).Dtos;
            //Validate(dto);
            var result = _bankDao.SaveList(manager, dtos);
            return new ExecutionResult<List<BankDto>> { Data = result };
        }

        public override void RegisterCommandsAndQueries(ICommandQueryRegistrator commandQueryRegistrator)
        {
            // RegisterCommands
            commandQueryRegistrator.RegisterCommand(BankSaveCommand.Id, Save);
            commandQueryRegistrator.RegisterCommand(BankSaveListCommand.Id, SaveList);
            // RegisterQueries

			RegisterCommandsAndQueries2(commandQueryRegistrator);
        }
				
        partial void RegisterCommandsAndQueries2(ICommandQueryRegistrator commandQueryRegistrator);
    }
}