using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;
using MillionaireGame.BusinessLogic.Abstract;
using MillionaireGame.Repositories.Abstract;
using MillionaireGame.Repositories.Concrete;
using Ninject;
using MillionaireGame.BusinessLogic.Concrete;
using Ninject.Web.Mvc.FilterBindingSyntax;
using MillionaireGame.Frontend.Filters;
using MillionaireGame.Entities;
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
            _kernel.Bind<IQuestionRepository>().To<DbQuestionRepository>()
                .WithConstructorArgument("context", new LoggerContext());
            _kernel.Bind<IGameStepRepository>().To<JsonGameStepRepository>()
                .WithConstructorArgument("filename", HostingEnvironment.MapPath(ConfigurationManager.AppSettings["GameStepsPath"]));

            _kernel.Bind<IEncryptionService>().To<AESEncryptionService>().WithConstructorArgument("encryptionKey", "abc123");
            _kernel.Bind<IMessageService>().To<EmailClient>().WithConstructorArgument("encryptionService", _kernel.Get<IEncryptionService>());

            _kernel.Bind<IQuestionStatisticRepository>().To<DbQuestionStatisticRepository>()
                .WithConstructorArgument("context", new LoggerContext());
            _kernel.Bind<IGameHint>().To<GameHint>().WithConstructorArgument("messageService", _kernel.Get<IMessageService>())
                .WithConstructorArgument("questionStatisticRepository", _kernel.Get<IQuestionStatisticRepository>());

            _kernel.Bind<IExceptionDetailRepository>().To<DbExceptionDetailRepository>().WithConstructorArgument("context",
                new LoggerContext());

            _kernel.BindFilter<ExceptionLoggerFilter>(FilterScope.Controller, 0)
                .WhenControllerHas<ExceptionLoggerAttribute>()
                .WithConstructorArgument("repository", _kernel.Get<IExceptionDetailRepository>()); //logger injection
            _kernel.BindFilter<ActionLoggerFilter>(FilterScope.Action, 0)
                .WhenActionMethodHas<ActionLoggerAttribute>()
                .WithConstructorArgument("repository", _kernel.Get<IQuestionStatisticRepository>());
        }
    }
}
