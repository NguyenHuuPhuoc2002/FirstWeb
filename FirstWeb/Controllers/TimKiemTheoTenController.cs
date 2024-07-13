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
        public async Task<IActionResult> TimKiemTheoTenView([Bind("ten, index")] string ten, int index)
        {
            IEnumerable<Student> studentList;
            
            if (!string.IsNullOrEmpty(ten))
            {
                HttpContext.Session.SetObjectAsJson("Name", ten);
                //xử lí phân trang
                var totalTask = _studentRepository.GetTotalNameAsync(ten);
                var total = await totalTask;
                var endPage = (total % 4 == 0) ? total / 4 : (total / 4) + 1;
                var students = await _studentRepository.GetAllNameItemAsync(ten, index); ;
                studentList = students.ToList();
                //truyền số trang
                TempData["endPageName"] = endPage;
                ViewBag.Search = ten;
                
            }
            else
            {
                if(index > 0)
                {
                    var newTen = HttpContext.Session.GetObjectFromJson<string>("Name");
                    //xử lí phân trang
                    var totalTask = _studentRepository.GetTotalNameAsync(newTen);
                    var total = await totalTask;
                    var endPage = (total % 4 == 0) ? total / 4 : (total / 4) + 1;
                    var students = await _studentRepository.GetAllNameItemAsync(newTen, index); ;
                    studentList = students.ToList();
                    //truyền số trang
                    TempData["endPageName"] = endPage;
                    ViewBag.Search = newTen;
                }
                else
                {
                    ViewBag.Majors = await _majorRepository.GetNganhAsync();
                    HttpContext.Session.Remove("Name");
                    return RedirectToAction("Index", "Home");
                }
               
                
            }
            ViewBag.Majors = await _majorRepository.GetNganhAsync();
            return View(studentList);
        }

    }
}
