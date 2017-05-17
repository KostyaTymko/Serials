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
       UserDbContext context = new UserDbContext();

        public IEnumerable<User> Users
        {
            get { return context.Users; }
        }
    }

}
