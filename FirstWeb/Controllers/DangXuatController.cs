using Microsoft.AspNetCore.Mvc;

namespace FirstWeb.Controllers
{
    public class DangXuatController : Controller
    {
        public IActionResult DangXuat()
        {
            HttpContext.Session.Remove("User");
            return RedirectToAction("DangNhapView", "DangNhap");
        }
    }
}
