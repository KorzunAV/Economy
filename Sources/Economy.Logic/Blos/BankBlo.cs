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
    public class BankBlo : BaseBlo
    {
        private BankDao BankDao { get; set; }
        private CurrencyTypeDao CurrencyTypeDao { get; set; }

        public BankBlo(ValidationManager validationManager, BankDao belinvestBankDao, CurrencyTypeDao currencyTypeDao)
            : base(validationManager)
        {
            BankDao = belinvestBankDao;
            CurrencyTypeDao = currencyTypeDao;
        }

        private ExecutionResult<int> Save(IBaseSessionManager manager, BaseCommand command)
        {
            var dtos = ((BankSaveCommand)command).Dtos;
            //Validate(period);
            var result = BankDao.Save(dtos);
            return new ExecutionResult<int> { Data = result.Id };
        }



        public override void RegisterCommandsAndQueries(ICommandQueryRegistrator commandQueryRegistrator)
        {
            // RegisterCommands
            commandQueryRegistrator.RegisterCommand(BankSaveCommand.Id, Save);
            // RegisterQueries
        }
    }
}