using System.Collections.Generic;
using CQRS.Common;
using Economy.Dtos;

namespace Economy.DataAccess.BlToolkit.Daos
{
    public class TransactionDao
    {
        #region Commands
        public int SaveRange(List<TransactionDto> dtos, IBaseSessionManager manager)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region Queries
         public List<TransactionDto> GetAll(IBaseSessionManager manager)
        {
            throw new System.NotImplementedException();
        }
        #endregion
        
    }
}