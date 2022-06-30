using Intro.Models;
using Intro.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Intro.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RandomService _randomService;
        private readonly IHasher _hasher;
        private readonly DAL.Context.IntroContext _introContext;

        public HomeController(ILogger<HomeController> logger, 
            RandomService randomService, IHasher hasher,
            DAL.Context.IntroContext introContext)
        {
            _logger = logger;
            _randomService =randomService;
            _hasher =hasher;
            _introContext =introContext;
        }

        public IActionResult Index()
        {
            ViewData["rnd"] ="<b>" +  _randomService.Integer + "</b>";
            ViewBag.hash = _hasher.Hash("123");
            ViewData["UsersCount"] = _introContext.Users.Count();
            //ViewData["UsersName"] = _introContext.Users.Select(x=>x.RealName);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            var model = new AboutModel
            {
                Data = "The Model Data"
            };
            return View(model);
        }

        public IActionResult Contacts()
        {
            var model = new ContactsModel
            {
                Name = "Valeriia",
                numberPhohe = "+380983383672",
                adress = "st.Primorskaya"
            };
            return View(model);
        }
        public ViewResult Register()
        {
            return View();
        }
        [HttpPost] //следующий метод срабатывает на пост запрос
        
        //Метод может автоматически собрать все переданные данные в модель
        // по совпадению имен
        public IActionResult RegUser(Models.RegUserModel UserData)
        {
            // return Json(UserData);  // способ проверить передачу данных
            String[] err = new String[6];
            String data =" ";

            if (UserData == null)
            {
                err[0] = "Некорректный вызов (нет данных)";
            }
            else
            {
                if (String.IsNullOrEmpty(UserData.RealName))
                {
                    err[1] = "Имя не может быть пустым";
                }
                else
                {
                    data = UserData.RealName;
                }

                if (String.IsNullOrEmpty(UserData.Login))
                {
                    err[2] = "Логин не может быть пустым";
                }
                else
                {
                    data = UserData.Login;
                }
                if (String.IsNullOrEmpty(UserData.Password1))
                {
                    err[3] = "Пароль не может быть пустым";
                }
                if (String.IsNullOrEmpty(UserData.Password2))
                {
                    err[4] = " Повторний пароль не может быть пустым";
                }
                if (String.IsNullOrEmpty(UserData.Email))
                {
                    err[5] = "Email не может быть пустым";
                }
                else
                {
                    data = UserData.Email;
                }
            }
            ViewData["err"] = err;
            ViewData["data"] = data;
            return View("Register");

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}