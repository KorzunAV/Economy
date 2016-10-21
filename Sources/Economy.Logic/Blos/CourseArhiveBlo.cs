using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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

        private ExecutionResult<bool> Save(BaseCommand command, IBaseSessionManager manager)
        {
            var dtos = ((CourseArhiveSaveCommand)command).Dtos;
            //Validate(period);
            CourseArhiveDao.Save(dtos, manager);
            return new ExecutionResult<bool> { Data = true };
        }

        private ExecutionResult<List<CourseArhiveDto>> GetAll(BaseQuery query, IBaseSessionManager manager)
        {
            var arhive = CourseArhiveDao.GetAll(manager);
            var cTDs = CurrencyTypeDao.GetAll(manager);
            foreach (var item in arhive)
            {
                var cdt = cTDs.FirstOrDefault(i => i.Id == item.CurrencyTypeDto.Id);
                if (cdt != null)
                    Mapper.Map(cdt, item.CurrencyTypeDto);
            }

            return new ExecutionResult<List<CourseArhiveDto>> { Data = arhive };
        }

        private ExecutionResult<CourseArhiveDto> GetLast(BaseQuery query, IBaseSessionManager manager)
        {
            var dto = CourseArhiveDao.GetLast(manager);
            return new ExecutionResult<CourseArhiveDto> { Data = dto };
        }

        public override void RegisterCommandsAndQueries(ICommandQueryRegistrator commandQueryRegistrator)
        {
            // RegisterCommands
            commandQueryRegistrator.RegisterCommand(CourseArhiveSaveCommand.Id, Save);
            // RegisterQueries
            commandQueryRegistrator.RegisterQuery(CourseArhiveGetAllQuery.Id, GetAll);
            commandQueryRegistrator.RegisterQuery(CourseArhiveGetLastQuery.Id, GetLast);
        }
    }
}