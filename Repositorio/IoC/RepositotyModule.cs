using ArquiteturaBasica.Concrete;
using ArquiteturaBasica.Dominio.Repositorio;
using ArquiteturaBasica.Repositorio;
using Ninject.Modules;


namespace ArquiteturaBasica.Repositorio.IoC
{
    public class RepositoryModule : NinjectModule
    {
        /// <summary>
        /// Faz o mapeamento das Interfaces com suas implementações
        /// </summary>
        public override void Load()
        {       
            Bind<IUsuarioRepo>().To<UsuarioRepo>();           
        }
    }
}
