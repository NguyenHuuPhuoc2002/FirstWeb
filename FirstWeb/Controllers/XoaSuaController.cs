using FirstWeb.Models;
using FirstWeb.Views;
using Microsoft.AspNetCore.Mvc;

namespace FirstWeb.Controllers
{
    public class XoaSuaController : Controller
    {
        private readonly IStudentRepository<Student> _studentRepository;
        private readonly IMajorRepository<Major> _majorRepository;

        public XoaSuaController(IStudentRepository<Student> studentRepository, IMajorRepository<Major> majorRepository)
        {
            this._studentRepository = studentRepository;
            this._majorRepository = majorRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UpdatePoint()
        {
            return View();
        }

        [HttpPost, ActionName("XoaSua")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> XoaSua(string Sua, string Xoa, string xoachon)
        {
            List<Student> students = HttpContext.Session.GetObjectFromJson<List<Student>>("Students") ?? new List<Student>();
            var ds = Request.Form["c1"].ToList();
            if (!string.IsNullOrEmpty(Sua))
            {
                string maSV = Sua;
                int soDiemCong;
                bool isValid = int.TryParse(Request.Form["soDiemCong"], out soDiemCong);

                var student = students.FirstOrDefault(s => s.maSV == maSV);
                if (student != null && isValid)
                {
                    student.soDiemCong = soDiemCong;
                    HttpContext.Session.SetObjectAsJson("Students", students);
                }
            }
            else if (!string.IsNullOrEmpty(Xoa))
            {
                string maSV = Xoa;

                var student = students.FirstOrDefault(s => s.maSV == maSV);
                if (student != null)
                {
                    students.Remove(student);
                    HttpContext.Session.SetObjectAsJson("Students", students);
                }
            }
            else if (ds != null)
            {

                foreach (var item in ds)
                {
                    if (string.IsNullOrEmpty(xoachon))
                    {
                        var studentRemove = students.FirstOrDefault(s => s.maSV == item);
                        if (studentRemove != null)
                        {
                            students.Remove(studentRemove);
                        }
                    }
                }
                HttpContext.Session.SetObjectAsJson("Students", students);
            }

            return RedirectToAction("GioHangView", "GioHang");
        }


    }
}
