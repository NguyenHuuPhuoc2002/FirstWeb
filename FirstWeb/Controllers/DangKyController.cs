using FirstWeb.Models;
using FirstWeb.Repositories;
using FirstWeb.Views;
using Microsoft.AspNetCore.Mvc;

namespace FirstWeb.Controllers
{
    public class DangKyController : Controller
    {
        private readonly ILoginRepository<Login> _loginRepository;
        private readonly IMajorRepository<Major> _majorRepository;
        public DangKyController(ILoginRepository<Login> loginRepository, IMajorRepository<Major> majorRepository)
        {
            this._loginRepository = loginRepository;
            this._majorRepository = majorRepository;
        }
        public async Task<IActionResult> DangKyView()
        {
            ViewBag.Majors = await _majorRepository.GetNganhAsync();
            return View();
        }
        [HttpPost, ActionName("DangKy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DangKy(string taiKhoan, string matKhau)
        {
            if(!string.IsNullOrEmpty(taiKhoan) && !string.IsNullOrEmpty(matKhau))
            {
                 await _loginRepository.registerUser(taiKhoan, matKhau); 
                return RedirectToAction("DangNhapView", "DangNhap");
            }
            else
            {
                TempData["ErrorMessageRegister"] = "Nhập đầy đủ thông tin !";
            }
            return RedirectToAction("DangKyView");
        }
    }
}
