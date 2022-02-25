using System.ComponentModel.DataAnnotations;

namespace ShopAPI
{

    public class LoginModel
     {
         [Required]
         public string email { get; set; }
         [Required]
         public string password { get; set; }
     }
}