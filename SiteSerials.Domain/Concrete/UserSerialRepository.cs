using SiteSerials.Domain.Abstract;
using SiteSerials.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteSerials.Domain.Concrete
{
    public class UserSerialRepository:IUserRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<User> Users
        {
            get { return context.Users; }
        }

        public void SaveUser(User user)
        {
            if (user.Id == 0)
                context.Users.Add(user);
            else
            {
                User dbEntry = context.Users.Find(user.Id);
                if (dbEntry != null)
                {
                    dbEntry.UserName= user.UserName;
                    dbEntry.Login = user.Login;
                    dbEntry.Password = user.Password;
                    dbEntry.UserSerials = user.UserSerials;
                }
            }
            context.SaveChanges();
        }
    }

}
