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
        private readonly IHistoryRepository<Student> _historyRepository;
        private readonly IWebHostEnvironment _environment;
        public StudentController(IWebHostEnvironment environment, IStudentRepository<Student> studentRepository, IMajorRepository<Major> majorRepository, IHistoryRepository<Student> historyRepository)
        {
            this._studentRepository = studentRepository;
            this._majorRepository = majorRepository;
            this._historyRepository = historyRepository;
            this._environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Majors = await _majorRepository.GetNganhAsync();
            return View();
        }

        // GET: HomeController1/GetStudents
        public async Task<ActionResult> GetStudents()
        {
            IEnumerable<Student> students = await _studentRepository.GetAllAsync();
            ViewBag.Majors = await _majorRepository.GetNganhAsync();
            // Chuyển đổi IEnumerable<Student> sang List<Student>
            List<Student> studentList = students.ToList();

            // Trả về view với danh sách sinh viên
            return View(studentList);
        }

        // GET: HomeController1/Details/5
        public async Task<ActionResult> Details(string maSV)
        {
            ViewBag.Majors = await _majorRepository.GetNganhAsync();
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
            Student student = new Student();
            ViewBag.Majors = await _majorRepository.GetNganhAsync();
            return View(student);
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                if (student.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(student.ImageUpload.FileName);
                    string extension = Path.GetExtension(student.ImageUpload.FileName);
                    fileName = fileName + extension;

                    // Tạo đường dẫn tuyệt đối tới thư mục lưu file trên server
                    string filePath = Path.Combine(_environment.WebRootPath, "anh", fileName);

                    // Lưu file lên server
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await student.ImageUpload.CopyToAsync(stream);
                    }

                    // Lưu đường dẫn tương đối của file vào thuộc tính Image của student
                    student.image = "anh/" + fileName;

                }
                await _studentRepository.AddAsync(student);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // GET: HomeController1/Edit/5
        public async Task<ActionResult> Edit(string maSV)
        {
            ViewBag.Majors = await _majorRepository.GetNganhAsync();
            if (maSV == null)
            {
                return NotFound();
            }

            var student = await _studentRepository.GetByIdAsync(maSV);
            if (student == null)
            {
                return NotFound();
            }
            ViewBag.Majors = await _majorRepository.GetNganhAsync();
            return View(student);
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                if (student.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(student.ImageUpload.FileName);
                    string extension = Path.GetExtension(student.ImageUpload.FileName);
                    fileName = fileName + extension;

                    // Tạo đường dẫn tuyệt đối tới thư mục lưu file trên server
                    string filePath = Path.Combine(_environment.WebRootPath, "anh", fileName);

                    // Lưu file lên server
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await student.ImageUpload.CopyToAsync(stream);
                    }

                    // Lưu đường dẫn tương đối của file vào thuộc tính Image của student
                    student.image = "anh/" + fileName;

                }
                await _studentRepository.UpdateAsync(student);
                return RedirectToAction("Index", "Home");
            }  
            return View();
        }


        // GET: HomeController1/Delete/5
        public async Task<IActionResult> Delete([Bind("maSV")] string maSV)
        {
            ViewBag.Majors = await _majorRepository.GetNganhAsync();
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
        public async Task<IActionResult> DeleteConfirmed(string maSV)
        {
            await _studentRepository.DeleteAsync(maSV);
            return RedirectToAction("Index", "Home");
        }


    }
}
