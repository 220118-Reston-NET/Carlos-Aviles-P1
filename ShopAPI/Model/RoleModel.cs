using System.ComponentModel.DataAnnotations;

namespace ShopAPI
{

    public class RoleModel
     {
         [Required]
         public string email { get; set; }
     }
}