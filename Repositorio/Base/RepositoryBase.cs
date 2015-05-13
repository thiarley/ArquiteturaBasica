using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using System.ComponentModel;
using ArquiteturaBasica.Exceptions;
using ArquiteturaBasica.Dominio.Repositorio;
using ArquiteturaBasica.Dominio.Entidade.Base;
using ArquiteturaBasica.Pagina;


namespace ArquiteturaBasica.Concrete.Base
{
    internal static class RepositorioExtensions
    {
        internal static Order ToNHibernateOrder(this ArquiteturaBasica.Comuns.Utils.Enum.Order o)
        {
            return (o == ArquiteturaBasica.Comuns.Utils.Enum.Order.ASC) ? Order.Asc("Id") : Order.Desc("Id");
        }
    }

    /// <summary>
    /// Classe que representa a base para todos os repositorios
    /// </summary>
    /// <typeparam name="E"></typeparam>
    internal class RepositorioBase<E> : IRepositorio<E>
        where E : EntityBase<E>, INotifyPropertyChanged
    {
        private ISession Session
        {
            get
            {
                return ServiceLocator.Get<ISession>();
            }
        }


        #region SELECT

        /// <summary>
        /// Recupera uma entidade por ID
        /// </summary>
        /// <param name="pk">ID</param>
        /// <returns>Entidade</returns>
        public virtual E GetByID(int pk)
        {
            try
            {
                return Session.Get<E>(pk);
            }
            catch (Exception ex)
            {
                throw new AcessoDadosException(ex);
            }
        }

        /// <summary>
        /// Recupera todas as entidades por uma lista de ids
        /// </summary>
        /// <param name="pk">ID</param>
        /// <returns>Entidade</returns>
        public virtual IList<E> GetAllByIDs(params int[] pk)
        {
            try
            {
                ICriteria criteria = Session.CreateCriteria<E>();
                criteria.Add(NHibernate.Criterion.Expression.In(Projections.Id(), pk));
                return criteria.List<E>();
            }
            catch (Exception ex)
            {
                throw new AcessoDadosException(ex);
            }
        }

        /// <summary>
        /// Recupera todas as entidades
        /// </summary>
        /// <returns></returns>
        public virtual IList<E> GetAll()
        {
            try
            {

                ICriteria criteria = Session.CreateCriteria<E>();
                return criteria.List<E>();

            }
            catch (Exception ex)
            {
                throw new AcessoDadosException(ex);
            }
        }

        /// <summary>
        /// Recupera todas as entidades
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public virtual IList<E> GetAll(ArquiteturaBasica.Comuns.Utils.Enum.Order order)
        {
            try
            {

                ICriteria criteria = Session.CreateCriteria<E>();
                criteria.AddOrder(order.ToNHibernateOrder());
                return criteria.List<E>();

            }
            catch (Exception ex)
            {
                throw new AcessoDadosException(ex);
            }
        }

        /// <summary>
        /// Pega uma lista paginada por uma expressão LINQ
        /// </summary>
        /// <param name="query">Query em LINQ</param>
        /// <param name="pageIndex">Pagina</param>
        /// <param name="pageSize">Tamanho da página</param>
        /// <returns>Lista paginada</returns>
        public IListaPaginada<E> GetPagedBy(Expression<Func<E, bool>> query, int pageIndex, int pageSize)
        {
            try
            {

                var rowCount = Session.Query<E>().Where(query).Count();

                var results = Session.Query<E>().Where(query)
                                  .Skip((pageIndex - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToList<E>();

                return new ListaPaginada<E>(results, pageIndex, pageSize, rowCount);

            }
            catch (Exception ex)
            {
                throw new AcessoDadosException(ex);
            }
        }

        public IListaPaginada<E> GetByExemploPaged(E entity, int pageIndex, int pageSize)
        {
            try
            {

                ICriteria criteria = Session.CreateCriteria<E>();
                Example example = Example.Create(entity);

                // configuração 
                example.EnableLike(MatchMode.Anywhere);
                example.ExcludeNulls();
                example.ExcludeZeroes();

                criteria.Add(example);

                ICriteria countCriteria = CriteriaTransformer.Clone(criteria)
                        .SetProjection(Projections.RowCount());

                criteria
                    .SetMaxResults(pageSize)
                    .SetFirstResult((pageIndex - 1) * pageSize);

                var results = criteria.List<E>();

                return new ListaPaginada<E>(results, pageIndex, pageSize, (int)countCriteria.UniqueResult());


            }
            catch (Exception ex)
            {
                throw new AcessoDadosException(ex);
            }
        }

        /// <summary>
        /// Recupera uma lista por exemplo (Uma entidade com alguns valores)
        /// </summary>
        /// <param name="entity">Enidade com alguns valores preenchidos</param>
        /// <returns>Lista de entidades</returns>
        public IList<E> GetByExemplo(E entity)
        {
            try
            {

                ICriteria criteria = Session.CreateCriteria<E>();
                Example example = Example.Create(entity);

                // configuração 
                example.EnableLike(MatchMode.Anywhere);
                example.ExcludeNulls();
                example.ExcludeZeroes();

                criteria.Add(example);
                return criteria.List<E>();


            }
            catch (Exception ex)
            {
                throw new AcessoDadosException(ex);
            }
        }

        /// <summary>
        /// recupera entidade por uma expressão linq
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual E GetSingleBy(Expression<Func<E, bool>> query)
        {
            try
            {

                return Session.Query<E>().Where(query).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw new AcessoDadosException(ex);
            }
        }

        /// <summary>
        /// Recupera uma lista por uma expressão linq
        /// </summary>
        /// <param name="query">Expressão Linq</param>
        /// <returns></returns>
        public virtual IList<E> GetListBy(Expression<Func<E, bool>> query)
        {
            try
            {

                return Session.Query<E>().Where(query).ToList();

            }
            catch (Exception ex)
            {
                throw new AcessoDadosException(ex);
            }
        }

        #endregion

        /// <summary>
        /// Insere uma entidade
        /// </summary>
        /// <param name="entity">Entidade</param>
        public virtual void Insert(E entity)
        {
            try
            {
                Transact(() =>
                       Session.Save(entity)
                   );
            }
            catch (Exception ex)
            {
                throw new AcessoDadosException(ex);
            }
        }

        /// <summary>
        /// Atualiza uma entidade
        /// </summary>
        /// <param name="entity">Entidade</param>
        public virtual void Update(E entity)
        {
            try
            {
                Transact(() =>
                       Session.SaveOrUpdate(entity)
                      );
            }
            catch (Exception ex)
            {
                throw new AcessoDadosException(ex);
            }
        }

        /// <summary>
        /// Remove uma entidade
        /// </summary>
        /// <param name="entity">Entidade</param>
        public virtual void Delete(E entity)
        {
            try
            {
                Transact(() =>
                        Session.Delete(entity)
                  );
            }
            catch (Exception ex)
            {
                throw new AcessoDadosException(ex);
            }
        }

        private TResult Transact<TResult>(Func<TResult> func)
        {
            if (!Session.Transaction.IsActive)
            {
                // Wrap in transaction
                TResult result;
                using (var tx = Session.BeginTransaction())
                {
                    result = func.Invoke();
                    tx.Commit();
                }
                return result;
            }
            // Don't wrap;
            return func.Invoke();
        }

        private void Transact(Action action)
        {
            Transact<bool>(() =>
              {
                  action.Invoke();
                  return false;
              });
        }
    }
}

