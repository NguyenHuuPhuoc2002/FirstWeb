namespace FirstWeb.Models
{
    public class Login
    {
        public string taiKhoan { get; set; }
        public string matKhau { get; set; }

        public Login(string taiKhoan, string matKhau)
        {
            this.taiKhoan = taiKhoan;
            this.matKhau = matKhau;
        }
        public Login()
        {
            
        }
    }
}
