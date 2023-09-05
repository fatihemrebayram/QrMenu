using System.ComponentModel.DataAnnotations;

namespace HotelAndTours.WebUI.Dtos.AppUserDto
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Şifre boş bırakılmaz")]
        public string password { get; set; }

        public bool RememberMe { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı boş bırakılmaz")]
        public string UserName { get; set; }
    }
}