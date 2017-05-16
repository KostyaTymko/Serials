using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteSerials.Domain.Entities
{
    public class Serial
    {
        public int Id { get; set; }
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
        [Display(Name = "Название")]
        public string Serial_title { get; set; }
        [Display(Name = "Описание")]
        public string SerialDescription { get; set; }
        [Display(Name = "Категория")]
        public string Category { get; set; }
        [Display(Name = "Рейтинг")]
        public int Rating { get; set; }

        public virtual ICollection<Season> Seasons { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }

        public Serial()
        {
            this.Seasons = new HashSet<Season>();
            this.Genres = new HashSet<Genre>();
        }
    }
}
