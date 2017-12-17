using System;
using System.Collections.Generic;
using CQRS.Common;
using CQRS.Logic;
using CQRS.Logic.Queries;
using Economy.Dtos;
using Economy.Logic.Queries;


namespace Economy.Logic.Blos
{
    public partial class CourseArhiveBlo
    {
        partial void RegisterCommandsAndQueries2(ICommandQueryRegistrator commandQueryRegistrator)
        {
            // RegisterCommands
            // RegisterQueries
            commandQueryRegistrator.RegisterQuery(CourseArhiveGetLastDataQuery.Id, GetLastData);
            commandQueryRegistrator.RegisterQuery(CourseArhiveGetAllQuery.Id, GetAll);
        }

        private ExecutionResult<DateTime?> GetLastData(IBaseSessionManager manager, BaseQuery command)
        {
            var dto = ((CourseArhiveGetLastDataQuery)command).Bank;
            //Validate(dto);
            var result = _coursearhiveDao.GetLastData(manager, dto);
            return new ExecutionResult<DateTime?> { Data = result };
        }

        private ExecutionResult<List<CourseArhiveDto>> GetAll(IBaseSessionManager manager, BaseQuery command)
        {
            var dto = ((CourseArhiveGetAllQuery)command).Bank;
            //Validate(dto);
            var result = _coursearhiveDao.GetAll(manager, dto);
            return new ExecutionResult<List<CourseArhiveDto>> { Data = result };
        }
    }
}