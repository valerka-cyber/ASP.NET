namespace Intro.Models
{
    //модель для сбора регистрации пользователя
    //имена свойств должны совпадать (до регистра с именами)
    //полей форм
    public class RegUserModel
    {
        public string RealName { get; set; }
        public string Login { get; set; }
        public string Password1 { get; set; }
        public string Password2 { get; set; }
        public string Email { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
