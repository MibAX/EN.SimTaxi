using AutoMapper;
using EN.SimTaxi.Mvc.Entities.Bookings;
using EN.SimTaxi.Mvc.Models.Bookings;

namespace EN.SimTaxi.Mvc.AutoMapperProfiles
{
    public class BookingAutoMapperProfile : Profile
    {
        public BookingAutoMapperProfile()
        {
            CreateMap<Booking, BookingViewModel>();

            CreateMap<Booking, BookingDetailsViewModel>();

            CreateMap<CreateUpdateBookingViewModel, Booking>();
        }
    }
}
