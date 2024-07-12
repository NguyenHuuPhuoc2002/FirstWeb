using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace FirstWeb.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Tên đăng nhập không được trống!")]
        [Display(Name = "Tên đăng nhập")]
        public string taiKhoan { get; set; }
        [Required(ErrorMessage = "Mật Khẩu không được trống!")]
        [Display(Name = "Mật Khẩu")]
        public string matKhau { get; set; }
        public bool role { get; set; }

        public Login(string taiKhoan, string matKhau, bool role)
        {
            this.taiKhoan = taiKhoan;
            this.matKhau = matKhau;
            this.role = role;
        }
        public Login()
        {
            
        }
    }
}
