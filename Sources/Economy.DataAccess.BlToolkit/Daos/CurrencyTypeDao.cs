using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLToolkit.Data.Linq;
using CQRS.Common;
using Economy.DataAccess.BlToolkit.DbManagers;
using Economy.Dtos;

namespace Economy.DataAccess.BlToolkit.Daos
{
    public partial class CurrencyTypeDao : BaseDao
    {
        #region Commands
        public int Save(CurrencyTypeDto dto, IBaseSessionManager manager)
        {
            var db = (EconomyDb)manager;
            var entiy = Mapper.Map<CurrencyTypeDto, CurrencyTypeEntity>(dto);
            var entityId = (int)db.CurrencyTypeTable.InsertWithIdentity(() => entiy);
            return entityId;
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