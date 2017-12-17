using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CQRS.Common;
using Economy.DataAccess.BlToolkit.DbManagers;
using Economy.DataAccess.BlToolkit.Entities;
using Economy.Dtos;

namespace Economy.DataAccess.BlToolkit.Daos
{
    public partial class SystemUserDao
    {
        #region Queries

        public SystemUserDto GetByLogin(IBaseSessionManager manager, string login)
        {
            var db = (EconomyDb)manager;
            var user = db.SystemUserTable.FirstOrDefault(i => i.Login == login);
            if (user != null)
            {
                var dto = Mapper.Map<SystemUserEntity, SystemUserDto>(user);
                dto.Wallets = WalletDao.GetBySystemUserId(db, user.Id);
                return dto;
            }
            return new SystemUserDto();
        }

        #endregion Queries
    }
}