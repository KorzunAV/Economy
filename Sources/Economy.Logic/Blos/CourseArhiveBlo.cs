using System.Collections.Generic;
using AutoMapper;
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
    public class CourseArhiveBlo : BaseBlo
    {
        private CourseArhiveDao CourseArhiveDao { get; set; }
        private CurrencyTypeDao CurrencyTypeDao { get; set; }

        public CourseArhiveBlo(ValidationManager validationManager, CourseArhiveDao belinvestCourseArhiveDao, CurrencyTypeDao currencyTypeDao)
            : base(validationManager)
        {
            CourseArhiveDao = belinvestCourseArhiveDao;
            CurrencyTypeDao = currencyTypeDao;
        }

        private ExecutionResult<bool> Save(IBaseSessionManager manager, BaseCommand command)
        {
            var dto = ((CourseArhiveSaveCommand)command).Dto;
            //Validate(period);
            CourseArhiveDao.Save(dto);
            return new ExecutionResult<bool> { Data = true };
        }

        
      
        public override void RegisterCommandsAndQueries(ICommandQueryRegistrator commandQueryRegistrator)
        {
            // RegisterCommands
            commandQueryRegistrator.RegisterCommand(CourseArhiveSaveCommand.Id, Save);
            // RegisterQueries
        }
    }
}