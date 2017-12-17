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
    public partial class MontlyReportBlo : BaseBlo
    {
	    private readonly MontlyReportDao _montlyreportDao;

        public MontlyReportBlo(ValidationManager validationManager, MontlyReportDao montlyreportDao)
            : base(validationManager)
        {
            _montlyreportDao = montlyreportDao;
        }	

		private ExecutionResult<MontlyReportDto> Save(IBaseSessionManager manager, BaseCommand command)
        {
            var dto = ((MontlyReportSaveCommand)command).Dto;
            //Validate(dto);
            var result = _montlyreportDao.Save(manager, dto);
            return new ExecutionResult<MontlyReportDto> { Data = result };
        }
		
		private ExecutionResult<List<MontlyReportDto>> SaveList(IBaseSessionManager manager, BaseCommand command)
        {
            var dtos = ((MontlyReportSaveListCommand)command).Dtos;
            //Validate(dto);
            var result = _montlyreportDao.SaveList(manager, dtos);
            return new ExecutionResult<List<MontlyReportDto>> { Data = result };
        }

        public override void RegisterCommandsAndQueries(ICommandQueryRegistrator commandQueryRegistrator)
        {
            // RegisterCommands
            commandQueryRegistrator.RegisterCommand(MontlyReportSaveCommand.Id, Save);
            commandQueryRegistrator.RegisterCommand(MontlyReportSaveListCommand.Id, SaveList);
            // RegisterQueries

			RegisterCommandsAndQueries2(commandQueryRegistrator);
        }
				
        partial void RegisterCommandsAndQueries2(ICommandQueryRegistrator commandQueryRegistrator);
    }
}