using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace ArquiteturaBasica
{
    public class Global : NinjectHttpApplication
    {

        protected override Ninject.IKernel CreateKernel()
        {
           
            ServiceLocator.Initialize("ArquiteturaBasica.Negocio", "ArquiteturaBasica.Repositorio");
            return ServiceLocator.Kernel;
        }
    }
}