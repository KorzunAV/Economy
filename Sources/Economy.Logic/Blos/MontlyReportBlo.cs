using System;
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
    public class MontlyReportBlo : BaseBlo
    {
        private MontlyReportDao MontlyReportDao { get; set; }

        public MontlyReportBlo(ValidationManager validationManager, MontlyReportDao montlyReportDao)
            : base(validationManager)
        {
            MontlyReportDao = montlyReportDao;
        }

        private ExecutionResult<int> SaveAll(BaseCommand command, IBaseSessionManager manager)
        {
            var dtos = ((MontlyReportSaveAllCommand)command).Dtos;
            //Validate(period);
            var id = MontlyReportDao.Save(dtos, manager);
            return new ExecutionResult<int> { Data = id };
        }
        
        private ExecutionResult<List<MontlyReportDto>> GetAll(BaseQuery query, IBaseSessionManager manager)
        {
            var dto = MontlyReportDao.GetAll(manager);
            return new ExecutionResult<List<MontlyReportDto>> { Data = dto };
        }

        public override void RegisterCommandsAndQueries(ICommandQueryRegistrator commandQueryRegistrator)
        {
            // RegisterCommands
            commandQueryRegistrator.RegisterCommand(MontlyReportSaveAllCommand.Id, SaveAll);
            // RegisterQueries
            commandQueryRegistrator.RegisterQuery(MontlyReportGetAllQuery.Id, GetAll);
        }
    }
}