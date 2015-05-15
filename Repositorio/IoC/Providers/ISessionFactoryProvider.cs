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

            factory = Fluently.Configure().Database(MsSqlConfiguration.MsSql2012
                                       .ConnectionString(c => c.FromConnectionStringWithKey("TESTE")))
                                       .Mappings(n => n.FluentMappings.AddFromAssemblyOf<UsuarioMap>())
                                       .ExposeConfiguration(c => DatabaseConfiguration.Inicialize(c))
                                       .BuildSessionFactory();

            return factory;
        }
    }
}
