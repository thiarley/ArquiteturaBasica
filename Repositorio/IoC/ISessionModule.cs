using ArquiteturaBasica.Repositorio.IoC.Providers;
using NHibernate;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;


namespace ArquiteturaBasica.Repositorio.IoC
{
    public class ISessionModule : NinjectModule
    {
        /// <summary>
        /// Modulo do Ninject que representa todas as factories da camada
        /// </summary>
        public override void Load()
        {
            Bind<ISessionFactory>().ToProvider(new ISessionFactoryProvider()).InSingletonScope();
            Bind<ISession>().ToMethod(context => context.Kernel.Get<ISessionFactory>().OpenSession()).InRequestScope();
        }
    }
}
