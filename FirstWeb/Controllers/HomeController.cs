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

        public async Task<IActionResult> Index(int index)
        {
            IEnumerable<Student> studentList;

            //sử lí tổng trang 
            var totalTask = _studentRepository.GetNumItemAsync();
            var total = await totalTask;
            var endPage = (total % 4 == 0) ? total / 4 : (total / 4) + 1;
            var students = await _studentRepository.GetAllItemAsync(index);
            studentList = students.ToList();
            //truyền dữ liệu
            TempData["endPage"] = endPage;
            ViewBag.Majors = await _majorRepository.GetNganhAsync();
            return View(studentList);
        }

        /*public async Task<IActionResult> Index([Bind("maNganh, tenNganh, index")] string maNganh, string tenNganh, int index)
        {
            IEnumerable<Student> studentList;

            if (!string.IsNullOrEmpty(maNganh))
            {
                //sử lí tổng trang 
                var totalTask = _studentRepository.GetNumMarjorItemAsync(maNganh);
                var total = await totalTask;
                var endPage = (total % 4 == 0) ? total / 4 : (total / 4) + 1;
                var students = await _studentRepository.GetAllMajorAsync(maNganh, index);
                studentList = students.ToList();
                //truyền dữ liệu
                TempData["endPage"] = endPage;
                //students = await _studentRepository.GetSearchMaNganhAsync(maNganh);
            }
            else if (!string.IsNullOrEmpty(tenNganh))
            {
                studentList = await _studentRepository.GetSearchNameAsync(tenNganh);
                ViewBag.Search = tenNganh;
            }
            else
            {
                //sử lí tổng trang 
                var totalTask = _studentRepository.GetNumItemAsync();
                var total = await totalTask;
                var endPage = (total % 4 == 0) ? total / 4 : (total / 4) + 1;
                var students = await _studentRepository.GetAllItemAsync(index);
                studentList = students.ToList();
                //truyền dữ liệu
                //ViewBag.Majors = await _majorRepository.GetNganhAsync();
                TempData["endPage"] = endPage;
                //students = await _studentRepository.GetAllAsync();
            }
            ViewBag.Majors = await _majorRepository.GetNganhAsync();
            return View(studentList);
        }*/
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
