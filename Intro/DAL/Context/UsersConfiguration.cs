using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Intro.DAL.Context
{
    public class UsersConfiguration  : IEntityTypeConfiguration<Entities.User>
    {
        public void Configure(EntityTypeBuilder<Entities.User> builder)
        {
            //Начальная конфигурация при построении модели
            //Сделегируется из Контекста. Здесь
            //можна задать начальные значения полей
            //поменять имя таблицы (по умолчанию - имя класса)
            // а так же задать начальные значения данные для таблицы
            //
            // (seed)
            builder.HasData(new Entities.User
            {
                Id                      = System.Guid.NewGuid(),
                RealName         = "Корневой администратор",
                Login                = "Admin",
                PassHash          = " ",
                Email                 = " ",
                PassSalt            = " ",
                RegMoment      = System.DateTime.Now,
                Avatar              = " "
            });






        }
    }
}
