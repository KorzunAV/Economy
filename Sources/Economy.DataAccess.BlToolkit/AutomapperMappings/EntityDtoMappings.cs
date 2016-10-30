using AutoMapper;
using Economy.DataAccess.BlToolkit.Entities;
using Economy.Dtos;

namespace Economy.DataAccess.BlToolkit.AutomapperMappings
{
    public class EntityDtoMappings
    {
        public static void Initialize(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CurrencyTypeEntity, CurrencyTypeDto>();
            cfg.CreateMap<CurrencyTypeDto, CurrencyTypeEntity>();

            cfg.CreateMap<TransactionEntity, TransactionDto>();
            cfg.CreateMap<TransactionDto, TransactionEntity>();

            cfg.CreateMap<WalletEntity, WalletDto>();
            cfg.CreateMap<WalletDto, WalletEntity>();

            cfg.CreateMap<SystemUserEntity, SystemUserDto>();
            cfg.CreateMap<SystemUserDto, SystemUserEntity>();

            cfg.CreateMap<CourseArhiveEntity, CourseArhiveDto>()
                .ForMember(dto => dto.CurrencyType, ent => ent.MapFrom(src => new CurrencyTypeDto { Id = src.CurrencyTypeId }));
            cfg.CreateMap<CourseArhiveDto, CourseArhiveEntity>()
                 .ForMember(ent => ent.CurrencyTypeId, dto => dto.MapFrom(src => src.CurrencyType.Id));
        }
    }
}