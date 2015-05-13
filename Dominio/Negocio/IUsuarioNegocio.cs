
using ArquiteturaBasica.Dominio.Entidade;
using ArquiteturaBasica.Pagina;
namespace ArquiteturaBasica.Dominio.Negocio
{
    public interface IUsuarioNegocio : INegocioCRUD<Usuario>
    {
        IListaPaginada<Usuario> GetAllUsersByLogin(string login, int pageSize, int page);
        Usuario GetByLogin(string login);
        bool NormalUserExist(Usuario entity);
        void Update(Usuario entity, bool isPasswordDecrypted);
        bool ValidateLogin(Usuario entity);
        void ValidateUsuario(Usuario entidade);
    }
}
