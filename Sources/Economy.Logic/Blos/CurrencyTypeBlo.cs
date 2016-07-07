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
    public class CurrencyTypeBlo : BaseBlo
    {
        private CurrencyTypeDao CurrencyTypeDao { get; set; }

        public CurrencyTypeBlo(ValidationManager validationManager, CurrencyTypeDao currencyTypeDao)
            : base(validationManager)
        {
            CurrencyTypeDao = currencyTypeDao;
        }

        private ExecutionResult<int> Save(BaseCommand command, IBaseSessionManager manager)
        {
            var dto = ((CurrencyTypeSaveCommand)command).Dto;
            //Validate(period);
            var id = CurrencyTypeDao.Save(dto, manager);
            return new ExecutionResult<int> { Data = id };
        }

        private ExecutionResult<List<CurrencyTypeDto>> GetAll(BaseQuery query, IBaseSessionManager manager)
        {
            var dto = CurrencyTypeDao.GetAll(manager);
            return new ExecutionResult<List<CurrencyTypeDto>> { Data = dto };
        }

        public override void RegisterCommandsAndQueries(ICommandQueryRegistrator commandQueryRegistrator)
        {
            // RegisterCommands
            commandQueryRegistrator.RegisterCommand(CurrencyTypeSaveCommand.Id, Save);
            // RegisterQueries
            commandQueryRegistrator.RegisterQuery(CurrencyTypeGetAllQuery.Id, GetAll);
        }

        public static int SelectOrSave(CurrencyTypeDto currencyType)
        {
            throw new System.NotImplementedException();
        }
    }
}