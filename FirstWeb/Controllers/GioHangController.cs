using FirstWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FirstWeb.Views;
using FirstWeb.Repositories;

namespace FirstWeb.Controllers
{
    public class GioHangController : Controller
    {
        private readonly IStudentRepository<Student> _studentRepository;
        private readonly IMajorRepository<Major> _majorRepository;

        public GioHangController(IStudentRepository<Student> studentRepository, IMajorRepository<Major> majorRepository)
        {
            this._studentRepository = studentRepository;
            this._majorRepository = majorRepository;
        }

        public async Task<IActionResult> GioHangView()
        {
            // Lấy danh sách sinh viên từ session
            var students = HttpContext.Session.GetObjectFromJson<List<Student>>("Students") ?? new List<Student>();
            ViewBag.Majors = await _majorRepository.GetNganhAsync();
            return View(students);
        }

        // GET: GioHang/Add
        public IActionResult Add()
        {
            return View();
        }

        // POST: GioHang/Add
        [HttpPost, ActionName("Add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("maSV")] string maSV)
        {
            // Lấy sinh viên từ repository bằng id
            Student student = await _studentRepository.GetByIdAsync(maSV);
            if (student == null)
            {
                return NotFound();
            }

            // Lấy danh sách sinh viên từ session
            List<Student> students = HttpContext.Session.GetObjectFromJson<List<Student>>("Students") ?? new List<Student>();

            // Kiểm tra xem sinh viên đã có trong danh sách session chưa
            if (!students.Any(s => s.maSV == maSV))
            {
                student.soDiemCong = 1;
                students.Add(student);
                HttpContext.Session.SetObjectAsJson("Students", students);
            }
            else
            {
                var studentFirst = students.FirstOrDefault(s => s.maSV == maSV);
                if (studentFirst != null)
                {
                    studentFirst.soDiemCong += 1; // Đổi tên thành "A"
                    HttpContext.Session.SetObjectAsJson("Students", students);
                }
            }

            // Chuyển hướng về action Index của GioHangController
            return RedirectToAction("GioHangView");
        }

        [HttpPost, ActionName("ClearAll")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClearAll()
        {
            HttpContext.Session.Remove("Students"); 
            return RedirectToAction("GioHangView");
        }

       
    }
}
