using System.ComponentModel.DataAnnotations;
namespace AirlineReservationSystem.Models.Customer
{
    public class CustomerLoginView
    {
        [Required(ErrorMessage = "Please enter email!")]
        [Display(Name = "Enter email: ")]
        public string email { get; set; }

        [Required(ErrorMessage = "Please enter password!")]
        [Display(Name = "Enter password: ")]
        public string password { get; set; }
    }
}
