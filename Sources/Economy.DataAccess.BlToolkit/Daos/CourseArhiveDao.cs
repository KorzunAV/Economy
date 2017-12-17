using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CQRS.Common;
using Economy.DataAccess.BlToolkit.DbManagers;
using Economy.DataAccess.BlToolkit.Entities;
using Economy.Dtos;

namespace Economy.DataAccess.BlToolkit.Daos
{
    public partial class CourseArhiveDao
    {
        #region Commands



        #endregion Commands

        #region Queries
        public DateTime? GetLastData(IBaseSessionManager manager, BankDto dto)
        {
            if (dto.Id == 0)
            {
                dto = InsertOrSelect<BankDto, BankEntity>(manager, dto);
            }

            using (var db = new EconomyDb())
            {
                var rez = db.CourseArhiveTable.Where(e => e.BankId == dto.Id).OrderByDescending(e => e.RegDate).Select(e => e.RegDate).FirstOrDefault();
                return rez;
            }
        }

        public List<CourseArhiveDto> GetAll(IBaseSessionManager manager, BankDto dto)
        {
            if (dto.Id == 0)
            {
                dto = InsertOrSelect<BankDto, BankEntity>(manager, dto);
            }

            using (var db = new EconomyDb())
            {
                var rez = db.CourseArhiveTable.Where(e => e.BankId == dto.Id).OrderByDescending(e => e.RegDate).ToList();
                return Mapper.Map<List<CourseArhiveEntity>, List<CourseArhiveDto>>(rez);
            }
        }
        #endregion Queries


    }
}