using CQRS.Common;
using CQRS.Logic;
using CQRS.Logic.Queries;
using Economy.Dtos;
using Economy.Logic.Queries;


namespace Economy.Logic.Blos
{
    public partial class SystemUserBlo
    {
        partial void RegisterCommandsAndQueries2(ICommandQueryRegistrator commandQueryRegistrator)
        {
            // RegisterQueries
            commandQueryRegistrator.RegisterQuery(GetSystemUserByLoginQuery.Id, GetByLogin);
        }

        private ExecutionResult<SystemUserDto> GetByLogin(IBaseSessionManager manager, BaseQuery query)
        {
            var login = ((GetSystemUserByLoginQuery)query).Login;
            //Validate(dto);
            var result = _systemuserDao.GetByLogin(manager, login);
            return new ExecutionResult<SystemUserDto> { Data = result };
        }
    }
}