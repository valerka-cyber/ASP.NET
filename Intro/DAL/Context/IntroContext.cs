using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Intro.DAL.Context
{
    public class IntroContext : DbContext
    {
        public DbSet<Entities.User> Users { get; set; }
         public IntroContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
        // этот метод вызывает когда создается модель
        //БД из кода. Здесь можна задать начальные настройки
        modelBuilder.ApplyConfiguration(new UsersConfiguration());
        } 
    }
}
