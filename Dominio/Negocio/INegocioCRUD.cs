using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.ComponentModel;
using ArquiteturaBasica.Dominio.Entidade.Base;
using ArquiteturaBasica.Pagina;

namespace ArquiteturaBasica.Dominio.Negocio
{
    public interface INegocioCRUD<E>
        where E : EntityBase<E>, INotifyPropertyChanged
    {
        /// <summary>
        /// Recupera uma entidade do modelo a partir da sua chave primária.
        /// </summary>
        /// <param name="pk">chave primária da entidade do modelo</param>
        /// <returns>entidade do modelo identificada pela chave primária</returns>
        E GetByID(int id);

        /// <summary>
        /// Recupera todas as entidades do modelo.
        /// </summary>
        /// <returns>lista com todas as entidades do modelo</returns>
        IList<E> GetAll();

        /// <summary>
        /// Recupera todos paginados
        /// </summary>
        /// <returns></returns>
        IListaPaginada<E> GetAllPaged(int pageIndex, int pageSize);  

        /// <summary>
        /// Recupera todas as entidades do modelo que possuam as mesmas características da entidade informada.
        /// Somente as propriedades não nulas serão consideradas.
        /// </summary>
        /// <param name="entity">entidade do modelo de exemplo</param>
        /// <returns>lista de todas as entidades do modelo que possuem as mesmas características da entidade de exemplo</returns>
        IList<E> GetByExemplo(E entity);

        /// <summary>
        /// Recupera todas as entidades do modelo que possuam as mesmas características da entidade informada.
        /// Somente as propriedades não nulas serão consideradas.
        /// </summary>
        /// <param name="entity">entidade do modelo de exemplo</param>
        /// <returns>lista de todas as entidades do modelo que possuem as mesmas características da entidade de exemplo</returns>
        IListaPaginada<E> GetByExemploPaged(E entity, int pageIndex, int pageSize);

        /// <summary>
        /// Recupera uma lista paginada
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IListaPaginada<E> GetPagedBy(Expression<Func<E, bool>> query, int pageIndex, int pageSize);

        /// <summary>
        /// Salva no repositório a entidade do modelo ainda não salva.
        /// </summary>
        /// <param name="entity">entidade do modelo ainda não salva</param>
        void Insert(E entity);

        ///// <summary>
        ///// Salva no repositório a(s) entidade(s) do modelo ainda não salva(s).
        ///// </summary>
        ///// <param name="entity">entidade(s) do modelo a ser(em) salva(s)</param>
        ///// <returns>lista com a(s) chave(s) primária(s) da(s) entidade(s) do modelo indicada(s)</returns>
        //void Insert(params E[] entity);

        /// <summary>
        /// Salva ou atualiza a entidade do modelo.
        /// </summary>
        /// <param name="entity">entidade a ser salva</param>
        void Update(E entity);

        ///// <summary>
        ///// Salva ou atualiza a(s) entidade(s) do modelo.
        ///// </summary>
        ///// <param name="entity">entidade(s) a ser(em) salva(s)</param>
        //void Update(params E[] entity);

        /// <summary>
        /// Remove do repositório a entidade do modelo indicada.
        /// </summary>
        /// <param name="entity">entidade a ser removida</param>
        void Delete(E entity);

        /// <summary>
        /// Pega um unico elemento por uma expression
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        E GetSingleBy(Expression<Func<E, bool>> query);

        /// <summary>
        /// Pega uma lista por uma expression
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<E> GetListBy(Expression<Func<E, bool>> query);
    }
}
