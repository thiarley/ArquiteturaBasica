using ArquiteturaBasica.Dominio.Entidade.Base;
using System.ComponentModel;

namespace ArquiteturaBasica.Dominio.Entidade
{
    public partial class Usuario : EntityBase<Usuario>, INotifyPropertyChanged
    {
        public Usuario()
        {

        }

        public virtual string Login
        {
            get;
            set;
        }

        public virtual string Senha
        {
            get;
            set;
        }

        public virtual bool Administrador
        {
            get;
            set;
        }

        public virtual bool Bloqueado
        {
            get;
            set;
        }

        public virtual string Nome
        {
            get;
            set;
        }

        public virtual string Email
        {
            get;
            set;
        }
    }
}
