using FirstWeb.Models;
using FirstWeb.Views;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FirstWeb.Controllers
{
    public class HomeController : Controller
    {
  
        private readonly IStudentRepository<Student> _studentRepository;

        public HomeController(IStudentRepository<Student> studentRepository)
        {
            this._studentRepository = studentRepository;
        }

        public async Task<IActionResult> Index()
        {
           
            IEnumerable<Student> students = await _studentRepository.GetAllAsync();
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
