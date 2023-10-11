using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DBContext
{
    public class ApplicationDBContext:IdentityDbContext<IdentityUser>
    {
        public ApplicationDBContext()
        {

        }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Setting> Settings { get; set; }
        public DbSet<ContactNumber> ContactNumbers { get; set; }
        public DbSet<SiteMessages> SiteMessages { get; set; }
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<OurServices> OurServices { get; set; }
        public DbSet<NewsCategory> NewsCategories { get; set; }
        public DbSet<SingleValueTest> SingleValueTests { get; set; }
        public DbSet<MultipleValueText> MultipleValueTexts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=192.119.80.230;TrustServerCertificate=True;Initial Catalog=InvestingTest;User ID = 21techis; Password = techis@123#;Trusted_Connection=False;MultipleActiveResultSets=true");
            //optionsBuilder.UseMySQL("server=sharefunsd.com; port=3306; database=shareFundDB; user=ShareFund; password=Sudu@123#; Persist Security Info=True; Connect Timeout=300");

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}