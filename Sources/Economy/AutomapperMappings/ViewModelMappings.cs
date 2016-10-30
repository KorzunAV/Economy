using AutoMapper;
using Economy.Dtos;
using Economy.ViewModels;

namespace Economy.AutomapperMappings
{
    public class ViewModelMappings
    {
        public static void Initialize(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<TransactionDto, TransactionItemViewModel>();
            cfg.CreateMap<TransactionItemViewModel, TransactionDto>();
        }
    }
}
