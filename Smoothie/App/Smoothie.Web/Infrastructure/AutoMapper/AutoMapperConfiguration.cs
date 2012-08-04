using AutoMapper;

namespace Smoothie.Web.Infrastructure.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x=>x.AddProfile<ViewModelProfile>());


        }
    }
}