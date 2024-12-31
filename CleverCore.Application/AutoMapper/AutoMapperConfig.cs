using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace CleverCore.Application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings(IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<DomainToViewModelMappingProfile>();
                cfg.AddProfile<ViewModelToDomainMappingProfile>();
            });
        }
    }
}
