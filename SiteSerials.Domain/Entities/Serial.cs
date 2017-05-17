using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SiteSerials.Domain.Entities
{
    public class Serial
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Пожалуйста, введите название игры")]
        public string Serial_title { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Пожалуйста, введите описание для игры")]
        public string SerialDescription { get; set; }

        [Display(Name = "Категория")]
        [Required(ErrorMessage = "Пожалуйста, укажите категорию для игры")]
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
