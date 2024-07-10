using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
namespace FirstWeb.Models
{
    public class Student
    {
        [Display(Name = "Ảnh")]
        public string image { get; set; }
        [Display(Name = "Mã SV")]
        public string maSV { get; set; }
        [Display(Name = "Họ Tên")]
        public string hoTen { get; set; }
        [Display(Name = "Giới Tính")]
        public Gender gioiTinh { get; set; }
        [Display(Name = "Ngày Sinh")]
        [DataType(DataType.Date)]
        public DateTime ngaySinh { get; set; }
        [Display(Name = "Số Điểm Cộng")]
        public int soDiemCong { get; set; }
        [Display(Name = "Mã Ngành")]
        public string maNganh { get; set; }
        [NotMapped]
        public IFormFile ImageUpload { get; set; }
        public enum Gender
        {
            Male = 0,
            Female = 1
        }
        public Student(string image, string maSV, string hoTen, Gender gioiTinh, DateTime ngaySinh, int soDiemCong, string maNganh)
        {
            this.image = image;
            this.maSV = maSV;
            this.hoTen = hoTen;
            this.gioiTinh = gioiTinh;
            this.ngaySinh = ngaySinh;
            this.soDiemCong = soDiemCong;
            this.maNganh = maNganh;
        }
        public Student() {
            image = "~/anh/anh.png";
            ngaySinh = DateTime.Today;
        }

  
    }
}
