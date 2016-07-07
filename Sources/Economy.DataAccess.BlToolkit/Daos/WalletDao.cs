using System.Collections.Generic;
using CQRS.Common;
using Economy.Dtos;

namespace Economy.DataAccess.BlToolkit.Daos
{
    public class WalletDao
    {
        #region Commands
        public int SaveRange(List<WalletDto> dtos, IBaseSessionManager manager)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region Queries
        public List<WalletDto> GetAll(IBaseSessionManager manager)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}