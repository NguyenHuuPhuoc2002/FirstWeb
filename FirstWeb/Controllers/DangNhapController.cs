using Azure.Core;
using FirstWeb.Models;
using FirstWeb.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FirstWeb.Controllers
{
    public class DangNhapController : Controller
    {
        private readonly ILoginRepository<Login> _loginRepository;
        private readonly IMajorRepository<Major> _majorRepository;
        public DangNhapController(ILoginRepository<Login> loginRepository, IMajorRepository<Major> majorRepository)
        {
            this._loginRepository = loginRepository;
            this._majorRepository = majorRepository;
        }
        public async Task<IActionResult> DangNhapView()
        {
            ViewBag.Majors = await _majorRepository.GetNganhAsync();
            return View();
        }

        [HttpPost, ActionName("CheckLogin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckLogin(string taiKhoan, string matKhau)
        {
            if (!string.IsNullOrEmpty(taiKhoan) && !string.IsNullOrEmpty(matKhau))
            {
                var loginResult = await _loginRepository.checkLogin(taiKhoan, matKhau);

                if (loginResult != null)
                {
                    HttpContext.Session.SetObjectAsJson("User", loginResult);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = "Đăng nhập không thành công!";
                }
            }
            
            return RedirectToAction("DangNhapView");
        }
        


    }
}
