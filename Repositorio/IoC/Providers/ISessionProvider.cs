using NHibernate;
using Ninject;
using Ninject.Activation;

namespace ArquiteturaBasica.Repositorio.IoC.Providers
{
    internal sealed class ISessionProvider : Provider<ISession>
    {

        private static ISession _session;

        /// <summary>
        /// Fábrica de ISession
        /// </summary>
        /// <param name="context">NInject context</param>
        /// <returns>A new instance of ISession</returns>
        protected override ISession CreateInstance(IContext context)
        {
            if (_session == null || !_session.IsOpen)
            {
                var sessionFactory = context.Kernel.Get<ISessionFactory>();
                _session = sessionFactory.OpenSession();
            }

            return _session;
        }
    }
}
