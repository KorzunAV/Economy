using AutoMapper;
using Economy.DataAccess.NHibernate.Entities;
using Economy.Dtos.Commands;

namespace Economy.DataAccess.NHibernate.AutomapperMappings
{
    public class EntityDtoMappings
    {
        public EntityDtoMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CurrencyEntity, Currency>();
                cfg.CreateMap<Currency, CurrencyEntity>();
            });
        }
    }
}