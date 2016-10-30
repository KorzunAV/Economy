using AutoMapper;
using Economy.Dtos;

namespace Economy.Logic.AutomapperMappings
{
    public class DtoMappings
    {
        public static void Initialize(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CurrencyTypeDto, CurrencyTypeDto>();
        }
    }
}