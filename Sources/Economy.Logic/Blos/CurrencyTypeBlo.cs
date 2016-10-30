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
    public class CurrencyTypeBlo : BaseBlo
    {
        private CurrencyTypeDao CurrencyTypeDao { get; set; }

        public CurrencyTypeBlo(ValidationManager validationManager, CurrencyTypeDao currencyTypeDao)
            : base(validationManager)
        {
            CurrencyTypeDao = currencyTypeDao;
        }

        private ExecutionResult<int> Save(IBaseSessionManager manager, BaseCommand command)
        {
            var dto = ((CurrencyTypeSaveCommand)command).Dto;
            //Validate(period);
            var rez = CurrencyTypeDao.Save(dto);
            return new ExecutionResult<int> { Data = rez.Id };
        }

        

        public override void RegisterCommandsAndQueries(ICommandQueryRegistrator commandQueryRegistrator)
        {
            // RegisterCommands
            commandQueryRegistrator.RegisterCommand(CurrencyTypeSaveCommand.Id, Save);
            // RegisterQueries
        }

        public static int SelectOrSave(CurrencyTypeDto currencyType)
        {
            throw new System.NotImplementedException();
        }
    }
}