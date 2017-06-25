using MillionaireGame.Entities;
using MillionaireGame.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MillionaireGame.Frontend.Filters
{
    public class ExceptionLoggerAttribute : Attribute
    {

    }

    public class ExceptionLoggerFilter : IExceptionFilter
    {
        private IExceptionDetailRepository _exceptionDetailRepository;

        public ExceptionLoggerFilter(IExceptionDetailRepository repository)
        {
            _exceptionDetailRepository = repository;
        }

        public void OnException(ExceptionContext filterContext)
        {
            ExceptionDetail exceptionDetail = new ExceptionDetail()
            {
                ExceptionMessage = filterContext.Exception.Message,
                StackTrace = filterContext.Exception.StackTrace,
                ControllerName = filterContext.RouteData.Values["controller"].ToString(),
                ActionName = filterContext.RouteData.Values["action"].ToString(),
                Date = DateTime.Now
            };

            _exceptionDetailRepository.Add(exceptionDetail);
            _exceptionDetailRepository.SaveChanges();
        }
    }
}