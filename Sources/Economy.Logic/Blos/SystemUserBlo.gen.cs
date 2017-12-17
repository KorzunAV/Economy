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
    public partial class SystemUserBlo : BaseBlo
    {
	    private readonly SystemUserDao _systemuserDao;

        public SystemUserBlo(ValidationManager validationManager, SystemUserDao systemuserDao)
            : base(validationManager)
        {
            _systemuserDao = systemuserDao;
        }	

		private ExecutionResult<SystemUserDto> Save(IBaseSessionManager manager, BaseCommand command)
        {
            var dto = ((SystemUserSaveCommand)command).Dto;
            //Validate(dto);
            var result = _systemuserDao.Save(manager, dto);
            return new ExecutionResult<SystemUserDto> { Data = result };
        }
		
		private ExecutionResult<List<SystemUserDto>> SaveList(IBaseSessionManager manager, BaseCommand command)
        {
            var dtos = ((SystemUserSaveListCommand)command).Dtos;
            //Validate(dto);
            var result = _systemuserDao.SaveList(manager, dtos);
            return new ExecutionResult<List<SystemUserDto>> { Data = result };
        }

        public override void RegisterCommandsAndQueries(ICommandQueryRegistrator commandQueryRegistrator)
        {
            // RegisterCommands
            commandQueryRegistrator.RegisterCommand(SystemUserSaveCommand.Id, Save);
            commandQueryRegistrator.RegisterCommand(SystemUserSaveListCommand.Id, SaveList);
            // RegisterQueries

			RegisterCommandsAndQueries2(commandQueryRegistrator);
        }
				
        partial void RegisterCommandsAndQueries2(ICommandQueryRegistrator commandQueryRegistrator);
    }
}