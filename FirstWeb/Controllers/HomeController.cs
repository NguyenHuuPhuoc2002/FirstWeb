using FirstWeb.Models;
using FirstWeb.Views;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace FirstWeb.Controllers
{
    public class HomeController : Controller
    {
  
        private readonly IStudentRepository<Student> _studentRepository;
        private readonly IMajorRepository<Major> _majorRepository;

        public HomeController(IStudentRepository<Student> studentRepository, IMajorRepository<Major> majorRepository)
        {
            this._studentRepository = studentRepository;
            this._majorRepository = majorRepository;
        }

        public async Task<IActionResult> Index([Bind("maNganh, tenNganh")] string maNganh, string tenNganh, int index)
        {
            IEnumerable<Student> students;

            if (!string.IsNullOrEmpty(maNganh))
            {
                students = await _studentRepository.GetSearchMaNganhAsync(maNganh);
            }
            else if (!string.IsNullOrEmpty(tenNganh))
            {
                students = await _studentRepository.GetSearchNameAsync(tenNganh);
                ViewBag.Search = tenNganh;
            }
            else
            {
                var totalTask = _studentRepository.GetAllNumItemAsync();
                var total = await totalTask;
                var endPage = (total % 6 == 0) ? total / 6 : (total / 6) + 1;
                students = await _studentRepository.GetAllItemAsync(index);
                ViewBag.Majors = await _majorRepository.GetNganhAsync();
                TempData["endPage"] = endPage;
                var studentList = students.ToList();
                //students = await _studentRepository.GetAllAsync();
            }
            ViewBag.Majors = await _majorRepository.GetNganhAsync();
            return View(students);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
