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
            SerialsListViewModel result = (SerialsListViewModel)controller.List(2).Model;

            // Утверждение
            List<Serial> games = result.Serials.ToList();
            Assert.IsTrue(games.Count == 2);
            Assert.AreEqual(games[0].Serial_title, "Сериал4");
            Assert.AreEqual(games[1].Serial_title, "Сериал5");
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
                = (SerialsListViewModel)controller.List(2).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }
    }
}
