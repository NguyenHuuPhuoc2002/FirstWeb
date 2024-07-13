using FirstWeb.Models;
using FirstWeb.Views;
using Microsoft.AspNetCore.Mvc;

namespace FirstWeb.Controllers
{
    public class TimKiemTheoTenController : Controller
    {
        private readonly IStudentRepository<Student> _studentRepository;
        private readonly IMajorRepository<Major> _majorRepository;

        public TimKiemTheoTenController(IStudentRepository<Student> studentRepository, IMajorRepository<Major> majorRepository)
        {
            this._studentRepository = studentRepository;
            this._majorRepository = majorRepository;
        }
        public async Task<IActionResult> TimKiemTheoTenView([Bind("tenNganh, index")] string tenNganh, int index)
        {
            IEnumerable<Student> studentList;
            
            if (!string.IsNullOrEmpty(tenNganh))
            {
                ViewBag.Majors = await _majorRepository.GetNganhAsync();
                return View(studentList);
            }

    }
}
