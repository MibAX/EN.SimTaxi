using AutoMapper;
using EN.SimTaxi.Mvc.Entities.Cars;
using EN.SimTaxi.Mvc.Models.Cars;

namespace EN.SimTaxi.Mvc.AutoMapperProfiles
{
    public class CarAutoMapperProfile : Profile
    {
        public CarAutoMapperProfile()
        {
            CreateMap<Car, CarViewModel>();

            CreateMap<CreateUpdateCarViewModel, Car>().ReverseMap();

            CreateMap<Car, CarDetailsViewModel>();
        }
    }
}
