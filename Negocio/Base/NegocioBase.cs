using System;
using System.Collections.Generic;
using Ninject;
using ArquiteturaBasica;
using System.ComponentModel;
using ArquiteturaBasica.Dominio.Entidade.Base;
using ArquiteturaBasica.Dominio.Repositorio;
using ArquiteturaBasica.Exceptions;
using ArquiteturaBasica.Pagina;
using ArquiteturaBasica.Dominio.Negocio;

namespace Business.Concrete.Base
{
    internal class NegocioBase<T, R> : INegocioCRUD<T>
        where T : EntityBase<T>, INotifyPropertyChanged
        where R : IRepositorio<T>
    {

        protected R _DAO;

        [Inject]
        public NegocioBase(R DAO)
        {
            _DAO = DAO;
        }

        public virtual void Insert(T entity)
        {
            try
            {
                ValidateInsert(entity);
                _DAO.Insert(entity);
            }
            catch (AcessoDadosException)
            {
                throw;
            }
            catch (NegocioException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new NegocioException("Erro inesperado, contate o administrador.", ex);
            }
        }
        
        public virtual void Update(T entity)
        {
            try
            {
                ValidateUpdate(entity);
                _DAO.Update(entity);
            }
            catch (AcessoDadosException)
            {
                throw;
            }
            catch (NegocioException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new NegocioException("Erro inesperado, contate o administrador.", ex);
            }
        }

        public virtual void Delete(T entity)
        {
            try
            {
                ValidateDelete(entity);
                _DAO.Delete(entity);
            }
            catch (AcessoDadosException)
            {
                throw;
            }
            catch (NegocioException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new NegocioException("Erro inesperado, contate o administrador.", ex);
            }
        }

        public virtual IList<T> GetAll()
        {
            try
            {
                return _DAO.GetAll();
            }
            catch (AcessoDadosException)
            {
                throw;
            }
            catch (NegocioException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new NegocioException("Erro inesperado, contate o administrador.", ex);
            }
        }

        public virtual T GetByID(int id)
        {
            try
            {
                return _DAO.GetByID(id);
            }
            catch (AcessoDadosException)
            {
                throw;
            }
            catch (NegocioException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new NegocioException("Erro inesperado, contate o administrador.", ex);
            }
        }

        protected virtual void ValidateInsert(T entity)
        {
            return;
        }

        protected virtual void ValidateUpdate(T entity)
        {
            return;
        }

        protected virtual void ValidateDelete(T entity)
        {
            return;
        }


        public IListaPaginada<T> GetPagedBy(System.Linq.Expressions.Expression<Func<T, bool>> query, int pageIndex, int pageSize)
        {
            try
            {
                return _DAO.GetPagedBy(query, pageIndex, pageSize);

            }
            catch (AcessoDadosException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new NegocioException("Erro inesperado, contate o administrador.", ex);
            }
        }

        public IListaPaginada<T> GetAllPaged(int pageIndex, int pageSize)
        {
            try
            {
                return _DAO.GetPagedBy(c => true, pageIndex, pageSize);

            }
            catch (AcessoDadosException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new NegocioException("Erro inesperado, contate o administrador.", ex);
            }
        }

        /// <summary>
        /// Recupera todas as entidades do modelo que possuam as mesmas características da entidade informada.
        /// Somente as propriedades não nulas serão consideradas.
        /// </summary>
        /// <param name="entity">entidade do modelo de exemplo</param>
        /// <returns>lista de todas as entidades do modelo que possuem as mesmas características da entidade de exemplo</returns>
        public IList<T> GetByExemplo(T entity)
        {
            try
            {
                return _DAO.GetByExemplo(entity);

            }
            catch (AcessoDadosException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new NegocioException("Erro inesperado, contate o administrador.", ex);
            }
        }

        public IListaPaginada<T> GetByExemploPaged(T entity, int pageIndex, int pageSize)
        {
            try
            {
                return _DAO.GetByExemploPaged(entity, pageIndex, pageSize);

            }
            catch (AcessoDadosException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new NegocioException("Erro inesperado, contate o administrador.", ex);
            }
        }

        public T GetSingleBy(System.Linq.Expressions.Expression<Func<T, bool>> query)
        {
            try
            {
                return _DAO.GetSingleBy(query);
            }
            catch (AcessoDadosException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new NegocioException("Erro inesperado, contate o administrador.", ex);
            }
        }

        public IList<T> GetListBy(System.Linq.Expressions.Expression<Func<T, bool>> query)
        {
            try
            {
                return _DAO.GetListBy(query);
            }
            catch (AcessoDadosException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new NegocioException("Erro inesperado, contate o administrador.", ex);
            }
        }
    }
}
