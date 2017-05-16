using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Модель представления не является частью модели предметной области.Это всего лишь удобный класс для передачи данных 
//между контроллером и представлением.Чтобы подчеркнуть указанное обстоятельство, мы поместили класс PagingInfo в проект
//GameStore.WebUI для сохранения его отдельно от классов модели предметной области.

namespace SiteSerials.WebUI.Models
{
    public class PagingInfo
    {
        // Кол-во товаров
        public int TotalItems { get; set; }

        // Кол-во товаров на одной странице
        public int ItemsPerPage { get; set; }

        // Номер текущей страницы
        public int CurrentPage { get; set; }

        // Общее кол-во страниц
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }
}