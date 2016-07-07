using System.Collections.Generic;
using CQRS.Common;
using Economy.Dtos;

namespace Economy.DataAccess.BlToolkit.Daos
{
    public class MontlyReportDao : BaseDao
    {
        #region Commands
        public int Save(List<MontlyReportDto> dtos, IBaseSessionManager manager)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region Queries
        public List<MontlyReportDto> GetAll(IBaseSessionManager manager)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}