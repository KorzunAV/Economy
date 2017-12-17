using AutoMapper;
using Economy.Dtos;
using Economy.Models;
using Economy.ViewModels;

namespace Economy.AutomapperMappings
{
    public class ViewModelMappings
    {
        public static void Initialize(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<TransactionDto, TransactionItemViewModel>();
            cfg.CreateMap<TransactionItemViewModel, TransactionDto>();
            cfg.CreateMap<MontlyReportDto, MontlyReport>();
        }
    }
}
