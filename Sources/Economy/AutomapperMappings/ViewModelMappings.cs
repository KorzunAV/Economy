using AutoMapper;
using Economy.Dtos;
using Economy.ViewModels;

namespace Economy.AutomapperMappings
{
    public class ViewModelMappings
    {
        public static void InitAutoMapper()
        {
            Mapper.CreateMap<TransactionDto, TransactionItemViewModel>();
            Mapper.CreateMap<TransactionItemViewModel, TransactionDto>();
        }
    }
}
