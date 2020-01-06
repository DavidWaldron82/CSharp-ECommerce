using System.ComponentModel.DataAnnotations;
namespace ECommerce.Models {
    public class LoginUser {
        [Required(ErrorMessage=" is required")]
        public string Email {get;set;}
        [Required(ErrorMessage=" is required")]
        [DataType(DataType.Password)]
        public string Password {get;set;}
    }
}