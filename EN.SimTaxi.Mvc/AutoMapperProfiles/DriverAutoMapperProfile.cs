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
            CreateMap<CreateUpdateDriverViewModel, Driver>();

            CreateMap<Driver, CreateUpdateDriverViewModel>()
                .ForMember(createUpdateDriverViewModel => createUpdateDriverViewModel.CarIds,
                    opts => 
                        opts.MapFrom(driver => driver.Cars.Select(car => car.Id).ToList()));
                    
        }
    }
}
