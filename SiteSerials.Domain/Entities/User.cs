using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteSerials.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Display(Name = "пароль")]
        public string Password { get; set; }

        public virtual ICollection<Serial> UserSerials { get; set; }
    }
}
