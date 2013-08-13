using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using NidTid.Domain.Concrete;
using NidTid.Domain.Abstract;
using NidTid.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using Moq;

namespace NidTid.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory {
        private IKernel ninjectKernel;

        public NinjectControllerFactory() {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType) {
            
            return controllerType == null
                    ? null
                    : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings() {
            
            ninjectKernel.Bind<ICustomerRepository>().To<EFCustomerRepository>();
            ninjectKernel.Bind<IProjectRepository>().To<EFProjectRepository>();
        }
    }
}