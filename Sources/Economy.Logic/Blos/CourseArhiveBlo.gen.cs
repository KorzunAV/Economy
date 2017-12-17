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
    public partial class CourseArhiveBlo : BaseBlo
    {
	    private readonly CourseArhiveDao _coursearhiveDao;

        public CourseArhiveBlo(ValidationManager validationManager, CourseArhiveDao coursearhiveDao)
            : base(validationManager)
        {
            _coursearhiveDao = coursearhiveDao;
        }	

		private ExecutionResult<CourseArhiveDto> Save(IBaseSessionManager manager, BaseCommand command)
        {
            var dto = ((CourseArhiveSaveCommand)command).Dto;
            //Validate(dto);
            var result = _coursearhiveDao.Save(manager, dto);
            return new ExecutionResult<CourseArhiveDto> { Data = result };
        }
		
		private ExecutionResult<List<CourseArhiveDto>> SaveList(IBaseSessionManager manager, BaseCommand command)
        {
            var dtos = ((CourseArhiveSaveListCommand)command).Dtos;
            //Validate(dto);
            var result = _coursearhiveDao.SaveList(manager, dtos);
            return new ExecutionResult<List<CourseArhiveDto>> { Data = result };
        }

        public override void RegisterCommandsAndQueries(ICommandQueryRegistrator commandQueryRegistrator)
        {
            // RegisterCommands
            commandQueryRegistrator.RegisterCommand(CourseArhiveSaveCommand.Id, Save);
            commandQueryRegistrator.RegisterCommand(CourseArhiveSaveListCommand.Id, SaveList);
            // RegisterQueries

			RegisterCommandsAndQueries2(commandQueryRegistrator);
        }
				
        partial void RegisterCommandsAndQueries2(ICommandQueryRegistrator commandQueryRegistrator);
    }
}