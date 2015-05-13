using ArquiteturaPadrao.Repositorio;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Ninject.Activation;
using Repository.Mapping;

namespace ArquiteturaBasica.Repositorio.IoC.Providers
{
    internal sealed class ISessionFactoryProvider : Provider<ISessionFactory>
    {

        /// <summary>
        /// Fabrica de ISessionFactory
        /// </summary>
        /// <param name="context">Ninject context</param>
        /// <returns>A new instance of ISessionFactory</returns>
        protected override ISessionFactory CreateInstance(IContext context)
        {

            ISessionFactory factory = null;

            factory = Fluently.Configure().Database(MySQLConfiguration.Standard
                                       .ConnectionString(c => c.FromConnectionStringWithKey("EntityModelHB")))
                                       .Mappings(n => n.FluentMappings.AddFromAssemblyOf<UsuarioMap>())
                                       .ExposeConfiguration(c => DatabaseConfiguration.Inicialize(c))
                                       .BuildSessionFactory();

            return factory;
        }
    }
}
