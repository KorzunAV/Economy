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

        private ExecutionResult<bool> Save(BaseCommand command, IBaseSessionManager manager)
        {
            var dtos = ((BankSaveCommand)command).Dtos;
            //Validate(period);
            BankDao.Save(dtos, manager);
            return new ExecutionResult<bool> { Data = true };
        }

        private ExecutionResult<List<BankDto>> GetAll(BaseQuery query, IBaseSessionManager manager)
        {
            var dtos = BankDao.GetAll(manager);
            return new ExecutionResult<List<BankDto>> { Data = dtos };
        }


        public override void RegisterCommandsAndQueries(ICommandQueryRegistrator commandQueryRegistrator)
        {
            // RegisterCommands
            commandQueryRegistrator.RegisterCommand(BankSaveCommand.Id, Save);
            // RegisterQueries
            commandQueryRegistrator.RegisterQuery(BankGetAllQuery.Id, GetAll);
        }
    }
}