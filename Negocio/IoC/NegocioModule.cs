using Ninject.Modules;

using Business.Concrete;
using ArquiteturaBasica.Dominio.Negocio;
using ArquiteturaBasica.Negocio;

namespace ArquiteturaBasica.Negocio.IoC
{
    public class NegocioModule : NinjectModule
    {
        /// <summary>
        /// Loads this instance.
        /// </summary>
        public override void Load()
        {         
            Bind<IUsuarioNegocio>().To<UsuarioNegocio>();           
        }
    }
}
