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
    public class BelinvestCourseArhiveBlo : BaseBlo
    {
        private BelinvestCourseArhiveDao BelinvestCourseArhiveDao { get; set; }
        private CurrencyTypeDao CurrencyTypeDao { get; set; }

        public BelinvestCourseArhiveBlo(ValidationManager validationManager, BelinvestCourseArhiveDao belinvestCourseArhiveDao, CurrencyTypeDao currencyTypeDao)
            : base(validationManager)
        {
            BelinvestCourseArhiveDao = belinvestCourseArhiveDao;
            CurrencyTypeDao = currencyTypeDao;
        }

        private ExecutionResult<bool> Save(BaseCommand command, IBaseSessionManager manager)
        {
            var dtos = ((BelinvestCourseArhiveSaveCommand)command).Dtos;
            //Validate(period);
            BelinvestCourseArhiveDao.Save(dtos, manager);
            return new ExecutionResult<bool> { Data = true };
        }

        private ExecutionResult<List<BelinvestCourseArhiveDto>> GetAll(BaseQuery query, IBaseSessionManager manager)
        {
            var arhive = BelinvestCourseArhiveDao.GetAll(manager);
            var cTDs = CurrencyTypeDao.GetAll(manager);
            foreach (var item in arhive)
            {
                var cdt = cTDs.FirstOrDefault(i => i.Id == item.CurrencyTypeDto.Id);
                if (cdt != null)
                    Mapper.Map(cdt, item.CurrencyTypeDto);
            }

            return new ExecutionResult<List<BelinvestCourseArhiveDto>> { Data = arhive };
        }

        private ExecutionResult<BelinvestCourseArhiveDto> GetLast(BaseQuery query, IBaseSessionManager manager)
        {
            var dto = BelinvestCourseArhiveDao.GetLast(manager);
            return new ExecutionResult<BelinvestCourseArhiveDto> { Data = dto };
        }

        public override void RegisterCommandsAndQueries(ICommandQueryRegistrator commandQueryRegistrator)
        {
            // RegisterCommands
            commandQueryRegistrator.RegisterCommand(BelinvestCourseArhiveSaveCommand.Id, Save);
            // RegisterQueries
            commandQueryRegistrator.RegisterQuery(BelinvestCourseArhiveGetAllQuery.Id, GetAll);
            commandQueryRegistrator.RegisterQuery(BelinvestCourseArhiveGetLastQuery.Id, GetLast);
        }
    }
}