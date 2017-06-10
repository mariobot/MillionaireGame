using System;
using System.Collections.Generic;
using System.Web.Hosting;
using System.Web.Mvc;
using MillionaireGame.Repositories.Abstract;
using MillionaireGame.Repositories.Concrete;
using Ninject;

namespace MillionaireGame.Frontend.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<IQuestionRepository>().To<JsonQuestionRepository>()
                .WithConstructorArgument("filename", HostingEnvironment.MapPath("~/App_Data/Questions.json"));
        }
    }
}