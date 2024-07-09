using FirstWeb.Models;
using FirstWeb.Views;
using Microsoft.AspNetCore.Mvc;

namespace FirstWeb.Controllers
{
    public class LichSuController : Controller
    {
        private readonly IHistoryRepository<Student> _studentRepository;
        private readonly IMajorRepository<Major> _majorRepository;

        public LichSuController(IHistoryRepository<Student> studentRepository, IMajorRepository<Major> majorRepository)
        {
            this._studentRepository = studentRepository;
            this._majorRepository = majorRepository;
        }

        public async Task<IActionResult> LichSuView()
        {
            IEnumerable<Student> students = await _studentRepository.GetAllAsync();
            HttpContext.Session.Remove("Students");
            ViewBag.Majors = await _majorRepository.GetNganhAsync();
            return View(students);
        }
        [HttpPost, ActionName("AddHistory")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddHistory([Bind("maSV,hoTen,gioiTinh,ngaySinh, soDiemCong, maNganh")] Student student)
        {
            List<Student> students = HttpContext.Session.GetObjectFromJson<List<Student>>("Students") ?? new List<Student>();
            foreach (var item in students)
            {
                await _studentRepository.AddAsync(item);
            }

            return RedirectToAction("LichSuView");
        }

    }
}
