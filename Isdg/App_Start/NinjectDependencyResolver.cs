using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Isdg.Services.Information;
using Isdg.Services.Messages;
using Isdg.Data;
using Isdg.Core;
using Isdg.Core.Data;
using Isdg.Models;

namespace Isdg.App_Start
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
            kernel.Bind(typeof(IRepository<>)).To(typeof(EfRepository<>));
            kernel.Bind(typeof(IPagedList<>)).To(typeof(PagedList<>));
            kernel.Bind<IDbContext>().To<IsdgObjectContext>().WithConstructorArgument("nameOrConnectionString", "IsdgObjectContext");            
            kernel.Bind<INewsService>().To<NewsService>();
            kernel.Bind<IMeetingService>().To<MeetingService>();
            kernel.Bind<IEmailAccountService>().To<EmailAccountService>();
            kernel.Bind<IEmailSender>().To<EmailSender>();   
        }
    }
}