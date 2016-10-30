using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLToolkit.Data.Linq;
using CQRS.Common;
using Economy.DataAccess.BlToolkit.DbManagers;
using Economy.DataAccess.BlToolkit.Entities;
using Economy.Dtos;

namespace Economy.DataAccess.BlToolkit.Daos
{
    public class BankDao : BaseDao
    {
        #region Commands
        public void Save(List<BankDto> dtos, IBaseSessionManager manager)
        {
            var db = (EconomyDb)manager;
            var entities = Mapper.Map<List<BankDto>, List<BankEntity>>(dtos);
            db.InsertBatch(entities);
        }
        #endregion

        #region Queries

        public List<BankDto> GetAll(IBaseSessionManager manager)        
        {
            var db = (EconomyDb)manager;
            var query = from c in db.BankTable
                        select c;
            var entities = query.ToList();
            return Mapper.Map<List<BankEntity>, List<BankDto>>(entities);
        }
       
        #endregion
    }
}