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
            #region Вывод пользователей на экран
            foreach (var user in _introContext.Users)
            {
                ViewData["user"] = user.RealName;           
                
            }
            #endregion
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
                if (UserData.Avatar != null)  // есть переданный файл
                {
                    // файл нужно сохранить в нужном месте.
                    // для простоты доступа - в папку wwwroot/img
                    // !! крайне НЕ рекомендуется сохранять имя переданного файла
                    //  - возможен конфликт, если разные пользователи -- одно имя
                    //  - уязвимость - если в имени файла есть ../

                    // Д.З. Сформировать новое имя файла, сохранить расширение,
                    //   убедиться, что файл не стирает к-то другой файл

                    UserData.Avatar.CopyToAsync(
                        new FileStream(
                            "./wwwroot/img/" + UserData.Avatar.FileName,
                            FileMode.Create));
                }
                // если валидация пройдена, то добавляем пользователя в БД
                // валидация успешна если нет сообщений об ошибках
                bool isValid = true;
                foreach (string error in err)
                {
                    if (!String.IsNullOrEmpty(error)) isValid = false;
                }
                if (isValid)   // валидация успешна
                {
                    var user = new DAL.Entities.User();
                    // крипто-соль - это случайное число (в сроковом виде)
                    user.PassSalt = _hasher.Hash(DateTime.Now.ToString());
                    user.PassHash = _hasher.Hash(
                        UserData.Password1 + user.PassSalt);  // соль "смешивается" с паролем
                    user.Avatar = UserData.Avatar.FileName;   // заменить по результатам ДЗ
                    user.Email = UserData.Email;
                    user.RealName = UserData.RealName;
                    user.Login = UserData.Login;
                    user.RegMoment = DateTime.Now;

                    // добавляем в БД (контекст)
                    _introContext.Users.Add(user);

                    // сохраняем изменения
                    _introContext.SaveChanges();
                }
            }
            ViewData["err"] = err;
            ViewData["UserData"] = UserData;
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