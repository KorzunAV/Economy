using AutoMapper;
using Economy.DataAccess.BlToolkit.Entities;
using Economy.Dtos;

namespace Economy.DataAccess.BlToolkit.AutomapperMappings
{
    public class EntityDtoMappings
    {
        public static void Initialize(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<BankDto, BankEntity>();
            cfg.CreateMap<BankEntity, BankDto>();
            cfg.CreateMap<CourseArhiveDto, CourseArhiveEntity>();
            cfg.CreateMap<CourseArhiveEntity, CourseArhiveDto>();
            cfg.CreateMap<CurrencyTypeDto, CurrencyTypeEntity>();
            cfg.CreateMap<CurrencyTypeEntity, CurrencyTypeDto>();
            cfg.CreateMap<MontlyReportDto, MontlyReportEntity>();
            cfg.CreateMap<MontlyReportEntity, MontlyReportDto>();
            cfg.CreateMap<SystemUserDto, SystemUserEntity>();
            cfg.CreateMap<SystemUserEntity, SystemUserDto>();
            cfg.CreateMap<TransactionDto, TransactionEntity>();
            cfg.CreateMap<TransactionEntity, TransactionDto>();
            cfg.CreateMap<WalletDto, WalletEntity>();
            cfg.CreateMap<WalletEntity, WalletDto>();

        }
    }
}