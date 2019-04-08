using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using TourAgency.BLL.Interfaces;
using TourAgency.BLL.Services;
using TourAgency.DAL.Interfaces;
using TourAgency.DAL.Repositories;

namespace TourAgency.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>();
            Bind<ITourService>().To<TourService>();
            Bind<ICountryService>().To<CountryService>();
            Bind<IPictureService>().To<PictureService>();
            Bind<IDailyProgramService>().To<DailyProgramService>();
            Bind<ITransportService>().To<TransportService>();
            Bind<IOrderService>().To<OrderService>();
        }
    }
}
