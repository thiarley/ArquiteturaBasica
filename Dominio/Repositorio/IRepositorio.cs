using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.ComponentModel;
using ArquiteturaBasica.Dominio.Entidade.Base;
using ArquiteturaBasica.Comuns.Utils.Enum;
using ArquiteturaBasica.Pagina;


namespace ArquiteturaBasica.Dominio.Repositorio
{
    public interface IRepositorio<E>
        where E : EntityBase<E>, INotifyPropertyChanged
    {
        /// <summary>
        /// Recupera uma entidade do modelo a partir da sua chave primária.
        /// </summary>
        /// <param name="pk">chave primária da entidade do modelo</param>
        /// <returns>entidade do modelo identificada pela chave primária</returns>
        E GetByID(int pk);

        /// <summary>
        /// Recupera a lista das entidades do modelo identificadas por cada chave primária indicada.
        /// </summary>
        /// <param name="pk">chaves primárias das entidades do modelo</param>
        /// <returns>entidades do modelo identificadas pelas chaves primárias</returns>
        IList<E> GetAllByIDs(params int[] pk);

        /// <summary>
        /// Recupera todas as entidades do modelo.
        /// </summary>
        /// <returns>lista com todas as entidades do modelo</returns>
        IList<E> GetAll();

        /// <summary>
        /// Recupera todas as entidades do modelo ordenadas
        /// </summary>
        /// <param name="order">Ordenação</param>
        /// <returns>lista com todas as entidades do modelo</returns>
        IList<E> GetAll(Order order);

        /// <summary>
        /// Recupera todas as entidades do modelo que possuam as mesmas características da entidade informada.
        /// Somente as propriedades não nulas serão consideradas.
        /// </summary>
        /// <param name="entity">entidade do modelo de exemplo</param>
        /// <returns>lista de todas as entidades do modelo que possuem as mesmas características da entidade de exemplo</returns>
        IList<E> GetByExemplo(E entity);

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

        /// <summary>
        /// Salva ou atualiza a entidade do modelo.
        /// </summary>
        /// <param name="entity">entidade a ser salva</param>
        void Update(E entity);       

        /// <summary>
        /// Remove do repositório a entidade do modelo indicada.
        /// </summary>
        /// <param name="entity">entidade a ser removida</param>
        void Delete(E entity);

        E GetSingleBy(Expression<Func<E, bool>> query);

        IList<E> GetListBy(Expression<Func<E, bool>> query);
    }
}
