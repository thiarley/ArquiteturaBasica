using ArquiteturaBasica.Dominio.Negocio;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArquiteturaBasica.Dominio.Entidade;

namespace ArquiteturaBasica
{
    public partial class _Default : System.Web.UI.Page
    {
        
        IUsuarioNegocio _usuarioNegocio { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario us = new Usuario();
            us.Email = "thi7575try6ahgdasd75675arley@gmail.com";
            us.Login = "thityrhgarlasdey2";
            us.Nome = "thiatyrrhglasdey2";
            us.Senha = "pasytrythgdasw2";



            _usuarioNegocio = ServiceLocator.Get<IUsuarioNegocio>();

            _usuarioNegocio.Insert(us);

            _usuarioNegocio.GetAll();
            Usuario delete = _usuarioNegocio.GetByID(4);

            _usuarioNegocio.Delete(delete);
        }
    }
}