using FirstWeb.Models;
using FirstWeb.Views;
using Microsoft.AspNetCore.Mvc;

namespace FirstWeb.Controllers
{
    public class TimKiemTheoMaController : Controller
    {
        private readonly IStudentRepository<Student> _studentRepository;
        private readonly IMajorRepository<Major> _majorRepository;

        public TimKiemTheoMaController(IStudentRepository<Student> studentRepository, IMajorRepository<Major> majorRepository)
        {
            this._studentRepository = studentRepository;
            this._majorRepository = majorRepository;
        }
        public async Task<IActionResult> TimKiemTheoMaView([Bind("maNganh, index")] string maNganh, int index)
        {
            IEnumerable<Student> studentList;
            
            if (!string.IsNullOrEmpty(maNganh))
            {
                HttpContext.Session.SetObjectAsJson("Major", maNganh);
                //sử lí tổng trang 
                var totalTask = _studentRepository.GetNumMarjorItemAsync(maNganh);
                var total = await totalTask;
                var endPage = (total % 4 == 0) ? total / 4 : (total / 4) + 1;
                var students = await _studentRepository.GetAllMajorAsync(maNganh, index);
                studentList = students.ToList();
                //truyền dữ liệu
                TempData["endPage2"] = endPage;
                //students = await _studentRepository.GetSearchMaNganhAsync(maNganh);
            }
            else 
            {
                var major = HttpContext.Session.GetObjectFromJson<String>("Major");
                var totalTask = _studentRepository.GetNumMarjorItemAsync(major);
                var total = await totalTask;
                var endPage = (total % 4 == 0) ? total / 4 : (total / 4) + 1;
                var students = await _studentRepository.GetAllMajorAsync(major, index);
                studentList = students.ToList();
                //truyền dữ liệu
                TempData["endPage2"] = endPage;
            }
           
            ViewBag.Majors = await _majorRepository.GetNganhAsync();
            return View(studentList);
        }

    }
}
