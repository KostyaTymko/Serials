using Moq;
using Ninject;
using SiteSerials.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteSerials.Domain.Abstract;
using SiteSerials.Domain.Concrete;

namespace SiteSerials.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            //Serie a = new Serie { Title = "Парадигма тухлой рыбы" };
            //Serie b = new Serie { Title = "Евклид-авеню"};
            //Serie c = new Serie { Title = "	Камень, ножницы, бумага, ящерица, Спок" };
            //Season first = new Season { Season_title = "первый сезон  2007 ", Series = { a, b, c } };
            //Season second = new Season { Season_title = "второй сезон 2008" };
            //Season third = new Season { Season_title = "третий сезон 2009" };


            //// Здесь размещаются привязки
            //Mock<ISerialRepository> mock = new Mock<ISerialRepository>();
            //mock.Setup(m => m.Serials).Returns(new List<Serial>
            //{
            //    new Serial {Serial_title = "SimCity", Rating = 1499 , Seasons={first,second,third } },
            //    new Serial { Serial_title = "TITANFALL", Rating=2299 },
            //    new Serial { Serial_title = "Battlefield 4", Rating=899 }
            //});
            //kernel.Bind<ISerialRepository>().ToConstant(mock.Object);

            kernel.Bind<ISerialRepository>().To<EFSerialRepository>();
        }
    }
}