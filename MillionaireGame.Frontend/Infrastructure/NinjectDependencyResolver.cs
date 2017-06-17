using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;
using MillionaireGame.Repositories.Abstract;
using MillionaireGame.Repositories.Concrete;
using Ninject;
using MillionaireGame.BusinessLogic.Abstraction;
using MillionaireGame.BusinessLogic.Concrete;
// ReSharper disable AssignNullToNotNullAttribute

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
                .WithConstructorArgument("filename", HostingEnvironment.MapPath(ConfigurationManager.AppSettings["QuestionsPath"]));
            _kernel.Bind<IGameStepRepository>().To<JsonGameStepRepository>()
                .WithConstructorArgument("filename", HostingEnvironment.MapPath(ConfigurationManager.AppSettings["GameStepsPath"]));
            _kernel.Bind<IGameHint>().To<GameHint>();
        }
    }
}