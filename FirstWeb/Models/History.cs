using System.ComponentModel.DataAnnotations;
using System.Globalization;
namespace FirstWeb.Models
{
    public class History
    {
        [Display(Name = "Mã SV")]
        public string maSV { get; set; }
        [Display(Name = "Họ Tên")]
        public string hoTen { get; set; }
        [Display(Name = "Giới Tính")]
        public Gender gioiTinh { get; set; }
        [Display(Name = "Ngày Sinh")]
        [DataType(DataType.Date)]
        public DateTime ngaySinh { get; set; }

        [Display(Name = "Mã Ngành")]
        public string maNganh { get; set; }
        public enum Gender
        {
            Male = 0,
            Female = 1
            // Các giá trị khác nếu có
        }
        public History(string maSV, string hoTen, Gender gioiTinh, DateTime ngaySinh, int soDiemCong, string maNganh)
        {
            this.maSV = maSV;
            this.hoTen = hoTen;
            this.gioiTinh = gioiTinh;
            this.ngaySinh = ngaySinh;
            this.maNganh = maNganh;
        }
        public History()
        {
        }


    }
}
