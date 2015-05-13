using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Business.Concrete.Base;
using Ninject;
using ArquiteturaBasica;
using ArquiteturaBasica.Dominio.Entidade;
using ArquiteturaBasica.Exceptions;
using ArquiteturaBasica.Dominio.Repositorio;
using ArquiteturaBasica.Dominio.Negocio;
using ArquiteturaBasica.Comuns.Utils.Enum;
using ArquiteturaBasica.Pagina;
using ArquiteturaBasica.Comuns.Seguranca;

namespace ArquiteturaBasica.Negocio
{
    /// <summary>
    /// DAO que representa as regras de negócio relacionadas com Usuários
    /// </summary>
    internal class UsuarioNegocio : NegocioBase<Usuario, IUsuarioRepo>, IUsuarioNegocio
    {
        [Inject]
        public UsuarioNegocio(IUsuarioRepo dao)
            : base(dao)
        { }

        private readonly Regex _emailRegex = new Regex(@"^(([^<>()[\]\\.,;:\s@\""]+"
        + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
        + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
        + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
        + @"[a-zA-Z]{2,}))$");

        /// <summary>
        /// Login do administrador.
        /// </summary>
        private const string AdministratorLogin = "adm_dabi";

        protected override void ValidateInsert(Usuario entity)
        {
            ValidateUsuario(entity);
        }

        protected override void ValidateUpdate(Usuario entity)
        {
            ValidateUsuario(entity);
        }

        /// <summary>
        /// Método responsável por validar um Usuário.
        /// </summary>
        /// <param name="entity">Usuário e senha a ser logado</param>
        public bool ValidateLogin(Usuario entity)
        {
            List<object> listErrors = new List<object>();
            Usuario user = this.GetByLogin(entity.Login);

            // se o usuário estiver bloqueado
            if (user != null && user.Bloqueado == true)
            {
                listErrors.Add(ArquiteturaBasica.Comuns.Utils.Enum.PessoaErrors.UserBlocked);
            }

            ValidadeFieldsLoginAndPassword(entity, listErrors);

            if (listErrors.Count > 0)
            {
                throw new ValidacaoException(listErrors);
            }

            string senha = StringCryptography.Encrypt(entity.Senha);

            return user != null && user.Senha.Equals(senha);
        }

        /// <summary>
        /// Valida as informações do Usuário.
        /// </summary>
        /// <param name="entidade"></param>
        public void ValidateUsuario(Usuario entidade)
        {
            List<object> lstErr = new List<object>();

            if (string.IsNullOrEmpty(entidade.Email) || entidade.Email.Trim() == String.Empty || entidade.Email.Length > 300 || !_emailRegex.IsMatch(entidade.Email))
            {
                lstErr.Add(UsuarioErrors.EmailNullOrInvalid);
            }

            ValidadeFieldsLoginAndPassword(entidade, lstErr);

            Usuario testUsuarioLogin = GetByLogin(entidade.Login);
            if (entidade.Login != null && (testUsuarioLogin != null && entidade.ID != testUsuarioLogin.ID))
            {
                lstErr.Add(UsuarioErrors.LoginDuplicated);
            }

            if (string.IsNullOrEmpty(entidade.Nome) || entidade.Nome.Trim() == String.Empty || entidade.Nome.Length > 300)
            {
                lstErr.Add(UsuarioErrors.NameNullOrInvalid);
            }

            if (lstErr.Count > 0)
            {
                throw new ValidacaoException(lstErr);
            }
        }

        /// <summary>
        /// Valida o login e a senha se estão vazias ou inválidas
        /// </summary>
        /// <param name="entidade"></param>
        /// <param name="lstErr"></param>
        private void ValidadeFieldsLoginAndPassword(Usuario entidade, List<object> lstErr)
        {
            if (string.IsNullOrEmpty(entidade.Login) || entidade.Login.Trim() == String.Empty || entidade.Login.Length > 45)
            {
                lstErr.Add(UsuarioErrors.LoginNullOrInvalid);
            }

            if (string.IsNullOrEmpty(entidade.Senha) || entidade.Senha.Trim() == String.Empty || entidade.Senha.Length > 256)
            {
                lstErr.Add(UsuarioErrors.PasswordNullOrInvalid);
            }
        }

        /// <summary>
        ///  Verificando se o usuário existe
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>bool</returns>
        public bool NormalUserExist(Usuario entity)
        {
            return _DAO.GetAll().Any(p => p.Login == entity.Login && p.Administrador == false);
        }

        /// <summary>
        /// Método que retorna o usuário por Login
        /// </summary>
        /// <param name="login">Login do usuário</param>
        /// <returns>Usuario</returns>
        public Usuario GetByLogin(string login)
        {
            return _DAO.GetSingleBy(p => p.Login == login);
        }

        /// <summary>
        /// Atualizando usuário
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isPasswordDecrypted"></param>
        public void Update(Usuario entity, bool isPasswordDecrypted)
        {
            if (isPasswordDecrypted == true)
            {
                entity.Senha = StringCryptography.Encrypt(entity.Senha);
            }

            base.Update(entity);
        }

        /// <summary>
        /// Insere usuário
        /// </summary>
        /// <param name="entity"></param>
        public override void Insert(Usuario entity)
        {
            entity.Senha = StringCryptography.Encrypt(entity.Senha);
            base.Insert(entity);
        }

        /// <summary>
        /// Retorna os usuários pelo login utilizando paginação. 
        /// ATENÇÃO: O usuário administrador do sistema não é retornado nessa colsulta para evitar alterações indesejadas.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="amountRows"></param>
        /// <param name="startingAtRow"></param>
        /// <param name="totalRowsInDatabase"></param>
        /// <returns></returns>
        public IListaPaginada<Usuario> GetAllUsersByLogin(string login, int pageSize, int page)
        {
            IListaPaginada<Usuario> lst = null;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(login.Trim()))
            {
                lst = _DAO.GetPagedBy(c => true, page, pageSize);
            }
            else
            {
                lst = _DAO.GetPagedBy(c => c.Login == login && c.Login != AdministratorLogin, page, pageSize);
            }

            return lst;
        }
    }

}
