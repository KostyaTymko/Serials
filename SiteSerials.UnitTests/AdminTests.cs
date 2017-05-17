using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SiteSerials.Domain.Abstract;
using SiteSerials.Domain.Entities;
using SiteSerials.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteSerials.WebUI
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Serials()
        {
            // Организация - создание имитированного хранилища данных
            Mock<ISerialRepository> mock = new Mock<ISerialRepository>();
            mock.Setup(m => m.Serials).Returns(new List<Serial>
            {
                new Serial { Id = 1, Serial_title = "Игра1"},
                new Serial { Id = 2, Serial_title = "Игра2"},
                new Serial { Id = 3, Serial_title = "Игра3"},
                new Serial { Id = 4, Serial_title = "Игра4"},
                new Serial { Id = 5, Serial_title = "Игра5"}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            List<Serial> result = ((IEnumerable<Serial>)controller.Index().
                ViewData.Model).ToList();

            // Утверждение
            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual("Игра1", result[0].Serial_title);
            Assert.AreEqual("Игра2", result[1].Serial_title);
            Assert.AreEqual("Игра3", result[2].Serial_title);
        }
        [TestMethod]
        public void Can_Edit_Game()
        {
            // Организация - создание имитированного хранилища данных
            Mock<ISerialRepository> mock = new Mock<ISerialRepository>();
            mock.Setup(m => m.Serials).Returns(new List<Serial>
            {
                new Serial { Id = 1, Serial_title = "Игра1"},
                new Serial { Id = 2, Serial_title = "Игра2"},
                new Serial { Id = 3, Serial_title = "Игра3"},
                new Serial { Id = 4, Serial_title = "Игра4"},
                new Serial { Id = 5, Serial_title = "Игра5"}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            Serial serial1 = controller.Edit(1).ViewData.Model as Serial;
            Serial serial2 = controller.Edit(2).ViewData.Model as Serial;
            Serial serial3 = controller.Edit(3).ViewData.Model as Serial;

            // Assert
            Assert.AreEqual(1, serial1.Id);
            Assert.AreEqual(2, serial2.Id);
            Assert.AreEqual(3, serial3.Id);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Game()
        {
            // Организация - создание имитированного хранилища данных
            Mock<ISerialRepository> mock = new Mock<ISerialRepository>();
            mock.Setup(m => m.Serials).Returns(new List<Serial>
            {
                new Serial { Id = 1, Serial_title = "Игра1"},
                new Serial { Id = 2, Serial_title = "Игра2"},
                new Serial { Id = 3, Serial_title = "Игра3"},
                new Serial { Id = 4, Serial_title = "Игра4"},
                new Serial { Id = 5, Serial_title = "Игра5"}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            Serial result = controller.Edit(6).ViewData.Model as Serial;

            // Assert
        }
    }
}