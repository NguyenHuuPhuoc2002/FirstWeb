namespace FirstWeb.Models
{
    public class Major
    {
        public string maNganh { get; set; }
        public string tenNganh { get; set; }

        public Major(string maNganh, string tenNganh)
        {
            this.maNganh = maNganh;
            this.tenNganh = tenNganh;
        }
        public Major()
        {
        }
    }
}
