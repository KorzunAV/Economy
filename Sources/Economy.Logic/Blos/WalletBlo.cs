using CQRS.Logic;
using CQRS.Logic.Blos;
using CQRS.Logic.Validation;
using Economy.DataAccess.BlToolkit.Daos;

namespace Economy.Logic.Blos
{
    internal class WalletBlo : BaseBlo
    {
        private WalletDao WalletDao { get; set; }

        public WalletBlo(ValidationManager validationManager, WalletDao walletDao)
            : base(validationManager)
        {
            WalletDao = walletDao;
        }

        public override void RegisterCommandsAndQueries(ICommandQueryRegistrator commandQueryRegistrator)
        {
            //// RegisterCommands
            //commandQueryRegistrator.RegisterCommand(WalletSaveRangeCommand.Id, SaveRange);
            //// RegisterQueries
            //commandQueryRegistrator.RegisterQuery(WalletGetAllQuery.Id, GetAll);
        }
    }
}