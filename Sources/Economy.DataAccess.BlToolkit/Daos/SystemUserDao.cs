using System;
using System.Linq;
using AutoMapper;
using BLToolkit.Data.Linq;
using CQRS.Common;
using Economy.DataAccess.BlToolkit.DbManagers;
using Economy.DataAccess.BlToolkit.Entities;
using Economy.Dtos;

namespace Economy.DataAccess.BlToolkit.Daos
{
    public class SystemUserDao
    {
        #region Commands

        public Guid Save(SystemUserDto user, IBaseSessionManager manager)
        {
            var db = (EconomyDb)manager;
            var entity = Mapper.Map<SystemUserDto, SystemUserEntity>(user);
            var result = db.SystemUserTable.InsertWithIdentity(() => entity);
            return (Guid)result;
        }
        
        #endregion

        #region Queries

        public Guid GetUserIdByName(string name, IBaseSessionManager manager)
        {
            var db = (EconomyDb)manager;

            var itm = db.SystemUserTable.Where(i => i.Name == name).Select(i => i.Id).FirstOrDefault();
            return itm;

        }
        #endregion
    }
}