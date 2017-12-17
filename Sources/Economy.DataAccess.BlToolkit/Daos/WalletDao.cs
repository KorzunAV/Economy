using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Economy.DataAccess.BlToolkit.DbManagers;
using Economy.DataAccess.BlToolkit.Entities;
using Economy.Dtos;

namespace Economy.DataAccess.BlToolkit.Daos
{
    public partial class WalletDao
    {

        #region Queries

        internal static List<WalletDto> GetBySystemUserId(EconomyDb db, int systemUserId)
        {
            var walets = db.WalletTable.Where(i => i.SystemUserId == systemUserId).ToList();
            var dtos = Mapper.Map<List<WalletEntity>, List<WalletDto>>(walets);
            foreach (var dto in dtos)
            {
                if (dto.BankId.HasValue)
                    dto.Bank = BankDao.GetById(db, dto.BankId.Value);

                dto.CurrencyType = CurrencyTypeDao.GetById(db, dto.CurrencyTypeId);
            }
            return dtos;
        }

        #endregion Queries
    }
}