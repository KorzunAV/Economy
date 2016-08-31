using AutoMapper;
using Economy.DataAccess.BlToolkit.DbManagers;
using Economy.Dtos;

namespace Economy.DataAccess.BlToolkit.AutomapperMappings
{
    public class EntityDtoMappings
    {
        public EntityDtoMappings()
        {
            Mapper.CreateMap<CurrencyTypeEntity, CurrencyTypeDto>();
            Mapper.CreateMap<CurrencyTypeDto, CurrencyTypeEntity>();

            Mapper.CreateMap<TransactionEntity, TransactionDto>();
            Mapper.CreateMap<TransactionDto, TransactionEntity>();

            Mapper.CreateMap<WalletEntity, WalletDto>();
            Mapper.CreateMap<WalletDto, WalletEntity>();

            Mapper.CreateMap<SystemUserEntity, SystemUserDto>();
            Mapper.CreateMap<SystemUserDto, SystemUserEntity>();

            Mapper.CreateMap<CourseArhiveEntity, CourseArhiveDto>()
                .ForMember(dto => dto.CurrencyTypeDto, ent => ent.MapFrom(src => new CurrencyTypeDto { Id = src.CurrencyTypeId }));
            Mapper.CreateMap<CourseArhiveDto, CourseArhiveEntity>()
                 .ForMember(ent => ent.CurrencyTypeId, dto => dto.MapFrom(src => src.CurrencyTypeDto.Id));



        }
    }
}