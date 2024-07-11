using EN.SimTaxi.Mvc.Enums;
using System.ComponentModel.DataAnnotations;

namespace EN.SimTaxi.Mvc.Models.Cars
{
    public class CreateUpdateCarViewModel
    {
        public int Id { get; set; } 
        public string Model { get; set; } 
        public string Color { get; set; } 
        public DateTime Year { get; set; }


        [Display(Name = "Plate Number")]
        public string PlateNumber { get; set; }

        [Display(Name = "Power Type")]
        public PowerType PowerType { get; set; }

        [Display(Name = "Car Type")]
        public CarType CarType { get; set; }
    }
}
