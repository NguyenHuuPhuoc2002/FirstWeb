namespace FirstWeb.Models
{
    public class Login
    {
        public string taiKhoan { get; set; }
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
