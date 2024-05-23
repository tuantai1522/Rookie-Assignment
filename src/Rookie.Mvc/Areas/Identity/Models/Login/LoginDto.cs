using System.ComponentModel.DataAnnotations;

namespace Rookie.Mvc.Areas.Identity.Models.Login
{
    public class LoginDto
    {
        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "PassWord is required.")]
        public string PassWord { get; set; }
    }
}