using System.ComponentModel.DataAnnotations;
using System.Globalization;
namespace FirstWeb.Models
{
    public class Student
    {
        [Display(Name = "Mã SV")]
        public string maSV { get; set; }
        [Display(Name = "Họ Tên")]
        public string hoTen { get; set; }
        [Display(Name = "Giới Tính")]
        public Gender gioiTinh { get; set; }
        [Display(Name = "Ngày Sinh")]
        public DateTime ngaySinh { get; set; }
        [Display(Name = "Số Điểm Cộng")]
        public int soDiemCong { get; set; }
        [Display(Name = "Mã Ngành")]
        public string maNganh { get; set; }
        public enum Gender
        {
            Male = 0,
            Female = 1
            // Các giá trị khác nếu có
        }
        public Student(string maSV, string hoTen, Gender gioiTinh, string ngaySinh, int soDiemCong, string maNganh)
        {
            this.maSV = maSV;
            this.hoTen = hoTen;
            this.gioiTinh = gioiTinh;
            this.ngaySinh = DateTime.ParseExact(ngaySinh, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            this.soDiemCong = soDiemCong;
            this.maNganh = maNganh;
        }
        public Student() { 
        }

  
    }
}
