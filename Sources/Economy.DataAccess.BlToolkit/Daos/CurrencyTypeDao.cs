using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CQRS.Common;
using Economy.DataAccess.BlToolkit.DbManagers;
using Economy.DataAccess.BlToolkit.Entities;
using Economy.Dtos;

namespace Economy.DataAccess.BlToolkit.Daos
{
    public class CurrencyTypeDao : BaseDao
    {
        #region Commands
        public int Save(CurrencyTypeDto dto, IBaseSessionManager manager)
        {
            return Insert<CurrencyTypeEntity, CurrencyTypeDto>((EconomyDb)manager, dto);
        }
        #endregion

        #region Queries
        public List<CurrencyTypeDto> GetAll(IBaseSessionManager manager)
        {
            var db = (EconomyDb)manager;
            var all = db.CurrencyTypeTable.ToList();
            return Mapper.Map<List<CurrencyTypeEntity>, List<CurrencyTypeDto>>(all);
        }
        #endregion
    }
}