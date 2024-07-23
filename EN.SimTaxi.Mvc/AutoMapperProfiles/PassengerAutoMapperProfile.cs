using AutoMapper;
using EN.SimTaxi.Mvc.Entities.Passengers;
using EN.SimTaxi.Mvc.Models.Passengers;

namespace EN.SimTaxi.Mvc.AutoMapperProfiles
{
    public class PassengerAutoMapperProfile : Profile
    {
        public PassengerAutoMapperProfile()
        {
            CreateMap<Passenger, PassengerViewModel>();
            CreateMap<Passenger, PassengerDetailsViewModel>();
            CreateMap<Passenger, CreateUpdatePassengerViewModel>().ReverseMap();
        }
    }
}
