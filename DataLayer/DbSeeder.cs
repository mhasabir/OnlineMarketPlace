using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataLayer
{
    public class DbSeeder: DropCreateDatabaseIfModelChanges<OnlineDbContext>
    {
        protected override void Seed(OnlineDbContext context)
        {
            base.Seed(context);
            User d = new User()
            {
                Name ="Admin",
                User_Name = "admin",
                Password = "admin",
                IsAdmin = 1,
                Email = "admin@admin.com",
                Mobile = "0123456789",
                Location = "AIUB"
            };
            User e = new User()
            {
                Name = "User",
                User_Name = "user",
                Password = "user",
                IsAdmin = 0,
                Email = "user@user.com",
                Mobile = "9876543210",
                Location = "AIUB-Student"
            };
            User f = new User()
            {
                Name = "User2",
                User_Name = "user2",
                Password = "user2",
                IsAdmin = 2,
                Email = "user2@user.com",
                Mobile = "9876543222",
                Location = "AIUB-Student22"
            };
            context.Users.Add(d);
            context.Users.Add(e);
            context.Users.Add(f);
            context.SaveChanges();

        }

    }
}
