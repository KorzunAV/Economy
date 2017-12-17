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
    public partial class WalletBlo : BaseBlo
    {
	    private readonly WalletDao _walletDao;

        public WalletBlo(ValidationManager validationManager, WalletDao walletDao)
            : base(validationManager)
        {
            _walletDao = walletDao;
        }	

		private ExecutionResult<WalletDto> Save(IBaseSessionManager manager, BaseCommand command)
        {
            var dto = ((WalletSaveCommand)command).Dto;
            //Validate(dto);
            var result = _walletDao.Save(manager, dto);
            return new ExecutionResult<WalletDto> { Data = result };
        }
		
		private ExecutionResult<List<WalletDto>> SaveList(IBaseSessionManager manager, BaseCommand command)
        {
            var dtos = ((WalletSaveListCommand)command).Dtos;
            //Validate(dto);
            var result = _walletDao.SaveList(manager, dtos);
            return new ExecutionResult<List<WalletDto>> { Data = result };
        }

        public override void RegisterCommandsAndQueries(ICommandQueryRegistrator commandQueryRegistrator)
        {
            // RegisterCommands
            commandQueryRegistrator.RegisterCommand(WalletSaveCommand.Id, Save);
            commandQueryRegistrator.RegisterCommand(WalletSaveListCommand.Id, SaveList);
            // RegisterQueries

			RegisterCommandsAndQueries2(commandQueryRegistrator);
        }
				
        partial void RegisterCommandsAndQueries2(ICommandQueryRegistrator commandQueryRegistrator);
    }
}