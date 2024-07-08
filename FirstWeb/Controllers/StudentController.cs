using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using FirstWeb.Models;
using FirstWeb.Repositories;
using FirstWeb.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstWeb
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository<Student> _studentRepository;
        private readonly IMajorRepository<Major> _majorRepository;
        public StudentController(IStudentRepository<Student> studentRepository, IMajorRepository<Major> majorRepository)
        {
            this._studentRepository = studentRepository;
            this._majorRepository = majorRepository;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Majors = await _majorRepository.GetNganh();
            return View();
        }

        // GET: HomeController1/GetStudents
        public async Task<ActionResult> GetStudents()
        {
            IEnumerable<Student> students = await _studentRepository.GetAllAsync();
            ViewBag.Majors = await _majorRepository.GetNganh();
            // Chuyển đổi IEnumerable<Student> sang List<Student>
            List<Student> studentList = students.ToList();

            // Trả về view với danh sách sinh viên
            return View(studentList);
        }

        // GET: HomeController1/Details/5
        public async Task<ActionResult> Details(string maSV)
        {
            ViewBag.Majors = await _majorRepository.GetNganh();
            if (string.IsNullOrEmpty(maSV))
            {
                return RedirectToAction("Index", "Home");
            }
            var student = await _studentRepository.GetByIdAsync(maSV);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // GET: HomeController1/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Majors = await _majorRepository.GetNganh();
            return View();
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("maSV,hoTen,gioiTinh,ngaySinh, soDiemCong, maNganh")] Student student)
        {
            if (ModelState.IsValid)
            {
                await _studentRepository.AddAsync(student);
                // Redirect to GetStudents action to show the updated list of students
                return RedirectToAction("Index", "Home");
            }
            return View(student);
        }

        // GET: HomeController1/Edit/5
        public async Task<ActionResult> Edit(string maSV)
        {
            ViewBag.Majors = await _majorRepository.GetNganh();
            if (maSV == null)
            {
                return NotFound();
            }

            var student = await _studentRepository.GetByIdAsync(maSV);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("maSV,hoTen,gioiTinh,ngaySinh,soDiemCong,maNganh")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _studentRepository.UpdateAsync(student);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            }
            return View();
        }


        // GET: HomeController1/Delete/5
        public async Task<IActionResult> Delete([Bind("maSV")] string maSV)
        {
            ViewBag.Majors = await _majorRepository.GetNganh();
            if (string.IsNullOrEmpty(maSV))
            {
                return RedirectToAction(nameof(Delete));
            }

            var student = await _studentRepository.GetByIdAsync(maSV);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: HomeController1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("maSV")] string maSV)
        {
            try
            {
                await _studentRepository.DeleteAsync(maSV);
                return RedirectToAction("Index", "Home");
            }
            catch
            {

                return View(maSV);
            }
        }
    }
}
