using CQRS.Logic;
using CQRS.Logic.Blos;
using CQRS.Logic.Validation;
using Economy.DataAccess.BlToolkit.Daos;

namespace Economy.Logic.Blos
{
    public class SystemUserBlo : BaseBlo
    {
        private SystemUserDao SystemUserDao { get; set; }

        public SystemUserBlo(ValidationManager validationManager, SystemUserDao systemUserDao)
            : base(validationManager)
        {
            SystemUserDao = systemUserDao;
        }

        public override void RegisterCommandsAndQueries(ICommandQueryRegistrator commandQueryRegistrator)
        {
            throw new System.NotImplementedException();
        }
    }
}