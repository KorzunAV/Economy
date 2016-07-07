using AutoMapper;
using Economy.Dtos;

namespace Economy.Logic.AutomapperMappings
{
    public class DtoMappings
    {
        public DtoMappings()
        {
            Mapper.CreateMap<CurrencyTypeDto, CurrencyTypeDto>();
        }
    }
}