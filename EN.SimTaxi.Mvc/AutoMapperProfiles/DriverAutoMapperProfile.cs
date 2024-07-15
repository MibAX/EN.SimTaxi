using AutoMapper;
using EN.SimTaxi.Mvc.Entities.Drivers;
using EN.SimTaxi.Mvc.Models.Drivers;

namespace EN.SimTaxi.Mvc.AutoMapperProfiles
{
    public class DriverAutoMapperProfile : Profile
    {
        public DriverAutoMapperProfile()
        {
            CreateMap<Driver, DriverViewModel>();
            CreateMap<Driver, DriverDetailsViewModel>();
            CreateMap<CreateUpdateDriverViewModel, Driver>().ReverseMap();
        }
    }
}
