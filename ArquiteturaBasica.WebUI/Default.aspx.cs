using ArquiteturaBasica.Dominio.Negocio;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArquiteturaBasica
{
    public partial class _Default : System.Web.UI.Page
    {
        
        IUsuarioNegocio _usuarioNegocio { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            _usuarioNegocio = ServiceLocator.Get<IUsuarioNegocio>();
            _usuarioNegocio.GetAll();
        }
    }
}