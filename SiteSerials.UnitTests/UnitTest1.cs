using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SiteSerials.Domain.Abstract;
using System.Collections.Generic;
using SiteSerials.Domain.Entities;
using SiteSerials.WebUI.Controllers;
using System.Linq;
using System.Web.Mvc;
using SiteSerials.WebUI.Models;
using SiteSerials.WebUI.HtmlHelpers;

namespace SiteSerials.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            // Организация (arrange)
            Mock<ISerialRepository> mock = new Mock<ISerialRepository>();
            mock.Setup(m => m.Serials).Returns(new List<Serial>
            {
                new Serial { Id = 1, Serial_title = "Сериал1", Seasons= {  new Season { Season_title = "второй сезон 2008" }} },
                new Serial { Id = 2, Serial_title = "Сериал2"},
                new Serial { Id = 3, Serial_title = "Сериал3"},
                new Serial { Id = 4, Serial_title = "Сериал4"},
                new Serial { Id = 5, Serial_title = "Сериал5"}
            });
            SerialController controller = new SerialController(mock.Object);
            controller.pageSize = 3;

            // Действие (act)
            SerialsListViewModel result = (SerialsListViewModel)controller.List(null,2).Model;

            // Утверждение
            List<Serial> serials = result.Serials.ToList();
            Assert.IsTrue(serials.Count == 2);
            Assert.AreEqual(serials[0].Serial_title, "Сериал4");
            Assert.AreEqual(serials[1].Serial_title, "Сериал5");
        }

        [TestMethod]
        public void Can_Create_User()
        {
            // Организация (arrange)
            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            mock.Setup(m => m.Users).Returns(new List<User>
            {
                new User { Id = 1, UserName = "Сериал1"},
                new User { Id = 2, UserName = "Сериал2"},
                new User { Id = 3, UserName = "Сериал3"},
                new User { Id = 4, UserName = "Сериал4"},
                new User { Id = 5, UserName = "Сериал5"}
            });
            UserController controller = new UserController(mock.Object);
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {

            // Организация - определение вспомогательного метода HTML - это необходимо
            // для применения расширяющего метода
            HtmlHelper myHelper = null;

            // Организация - создание объекта PagingInfo
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // Организация - настройка делегата с помощью лямбда-выражения
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Действие
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Утверждение
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }


        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            // Организация (arrange)
            Mock<ISerialRepository> mock = new Mock<ISerialRepository>();
            mock.Setup(m => m.Serials).Returns(new List<Serial>
            {
                new Serial { Id = 1, Serial_title = "Сериал1", Seasons= {  new Season { Season_title = "второй сезон 2008" }} },
                new Serial { Id = 2, Serial_title = "Сериал2"},
                new Serial { Id = 3, Serial_title = "Сериал3"},
                new Serial { Id = 4, Serial_title = "Сериал4"},
                new Serial { Id = 5, Serial_title = "Сериал5"}
            });
            SerialController controller = new SerialController(mock.Object);
            controller.pageSize = 3;

            // Act
            SerialsListViewModel result
                = (SerialsListViewModel)controller.List(null,2).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }
        [TestMethod]
        public void Can_Filter_Games()
        {
            // Организация (arrange)
            Mock<ISerialRepository> mock = new Mock<ISerialRepository>();
            mock.Setup(m => m.Serials).Returns(new List<Serial>
            {
                new Serial { Id = 1, Serial_title = "Сериал1", Category="Cat1"},
                new Serial { Id = 2, Serial_title = "Сериал2", Category="Cat2"},
                new Serial { Id = 3, Serial_title = "Сериал3", Category="Cat1"},
                new Serial { Id = 4, Serial_title = "Сериал4", Category="Cat2"},
                new Serial { Id = 5, Serial_title = "Сериал5", Category="Cat3"}
            });
            SerialController controller = new SerialController(mock.Object);
            controller.pageSize = 3;

            // Action
            List<Serial> result = ((SerialsListViewModel)controller.List("Cat2", 1).Model)
                .Serials.ToList();

            // Assert
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].Serial_title == "Сериал2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Serial_title == "Сериал4" && result[1].Category == "Cat2");
        }
        [TestMethod]
        public void Can_Create_Categories()
        {
            // Организация - создание имитированного хранилища
            Mock<ISerialRepository> mock = new Mock<ISerialRepository>();
            mock.Setup(m => m.Serials).Returns(new List<Serial> {
                new Serial { Id = 1, Serial_title = "Сериал1", Category="Симулятор"},
                new Serial { Id = 2, Serial_title = "Сериал2", Category="Симулятор"},
                new Serial { Id = 3, Serial_title = "Сериал3", Category="Шутер"},
                new Serial { Id = 4, Serial_title = "Сериал4", Category="RPG"},
            });

            // Организация - создание контроллера
            NavigationController target = new NavigationController(mock.Object);

            // Действие - получение набора категорий
            List<string> results = ((IEnumerable<string>)target.Menu().Model).ToList();

            // Утверждение
            Assert.AreEqual(results.Count(), 3);
            Assert.AreEqual(results[0], "RPG");
            Assert.AreEqual(results[1], "Симулятор");
            Assert.AreEqual(results[2], "Шутер");
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            // Организация - создание имитированного хранилища
            Mock<ISerialRepository> mock = new Mock<ISerialRepository>();
            mock.Setup(m => m.Serials).Returns(new Serial[] {
                new Serial { Id = 1, Serial_title = "Сериал1", Category="Симулятор"},
                new Serial { Id = 2, Serial_title = "Сериал2", Category="Шутер"}
            });

            // Организация - создание контроллера
            NavigationController target = new NavigationController(mock.Object);

            // Организация - определение выбранной категории
            string categoryToSelect = "Шутер";

            // Действие
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            // Утверждение
            Assert.AreEqual(categoryToSelect, result);
        }
        [TestMethod]
        public void Generate_Category_Specific_Game_Count()
        {
            /// Организация (arrange)
            Mock<ISerialRepository> mock = new Mock<ISerialRepository>();
            mock.Setup(m => m.Serials).Returns(new List<Serial>
            {
                new Serial { Id = 1, Serial_title = "Игра1", Category="Cat1"},
                new Serial { Id = 2, Serial_title = "Игра2", Category="Cat2"},
                new Serial { Id = 3, Serial_title = "Игра3", Category="Cat1"},
                new Serial { Id = 4, Serial_title = "Игра4", Category="Cat2"},
                new Serial { Id = 5, Serial_title = "Игра5", Category="Cat3"}
            });
            SerialController controller = new SerialController(mock.Object);
            controller.pageSize = 3;

            // Действие - тестирование счетчиков товаров для различных категорий
            int res1 = ((SerialsListViewModel)controller.List("Cat1").Model).PagingInfo.TotalItems;
            int res2 = ((SerialsListViewModel)controller.List("Cat2").Model).PagingInfo.TotalItems;
            int res3 = ((SerialsListViewModel)controller.List("Cat3").Model).PagingInfo.TotalItems;
            int resAll = ((SerialsListViewModel)controller.List(null).Model).PagingInfo.TotalItems;

            // Утверждение
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }
    }
}
