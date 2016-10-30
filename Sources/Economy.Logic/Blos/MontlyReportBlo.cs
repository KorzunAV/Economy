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
    public class MontlyReportBlo : BaseBlo
    {
        private MontlyReportDao MontlyReportDao { get; set; }

        public MontlyReportBlo(ValidationManager validationManager, MontlyReportDao montlyReportDao)
            : base(validationManager)
        {
            MontlyReportDao = montlyReportDao;
        }

        private ExecutionResult<Guid> Save(IBaseSessionManager manager, BaseCommand command)
        {
            var dto = ((MontlyReportSaveCommand)command).Dto;
            //Validate(period);
            var rez = MontlyReportDao.Save(dto);
            return new ExecutionResult<Guid> { Data = rez.Id };
        }
        
        public override void RegisterCommandsAndQueries(ICommandQueryRegistrator commandQueryRegistrator)
        {
            // RegisterCommands
            commandQueryRegistrator.RegisterCommand(MontlyReportSaveCommand.Id, Save);
            // RegisterQueries
        }
    }
}