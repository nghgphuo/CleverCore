using AutoMapper;
using CleverCore.Application.ViewModels;
using CleverCore.Data.Entities;

namespace CleverCore.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<ProductCategory, ProductCategoryViewModel>();
        }
    }
}
