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
    public partial class CurrencyTypeBlo : BaseBlo
    {
	    private readonly CurrencyTypeDao _currencytypeDao;

        public CurrencyTypeBlo(ValidationManager validationManager, CurrencyTypeDao currencytypeDao)
            : base(validationManager)
        {
            _currencytypeDao = currencytypeDao;
        }	

		private ExecutionResult<CurrencyTypeDto> Save(IBaseSessionManager manager, BaseCommand command)
        {
            var dto = ((CurrencyTypeSaveCommand)command).Dto;
            //Validate(dto);
            var result = _currencytypeDao.Save(manager, dto);
            return new ExecutionResult<CurrencyTypeDto> { Data = result };
        }
		
		private ExecutionResult<List<CurrencyTypeDto>> SaveList(IBaseSessionManager manager, BaseCommand command)
        {
            var dtos = ((CurrencyTypeSaveListCommand)command).Dtos;
            //Validate(dto);
            var result = _currencytypeDao.SaveList(manager, dtos);
            return new ExecutionResult<List<CurrencyTypeDto>> { Data = result };
        }

        public override void RegisterCommandsAndQueries(ICommandQueryRegistrator commandQueryRegistrator)
        {
            // RegisterCommands
            commandQueryRegistrator.RegisterCommand(CurrencyTypeSaveCommand.Id, Save);
            commandQueryRegistrator.RegisterCommand(CurrencyTypeSaveListCommand.Id, SaveList);
            // RegisterQueries

			RegisterCommandsAndQueries2(commandQueryRegistrator);
        }
				
        partial void RegisterCommandsAndQueries2(ICommandQueryRegistrator commandQueryRegistrator);
    }
}