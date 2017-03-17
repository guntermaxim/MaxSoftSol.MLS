using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace MaxSoftSol.MLS.Domain
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Insert(T entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        void Delete(object id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityToDelete"></param>
        void Delete(T entityToDelete);
        /// <summary>
        /// 
        /// </summary>
        /// 
        void Delete(IList<T> entitiesToDelete);

        /// <param name="entityToUpdate"></param>
        void Update(T entityToUpdate);
        void UpdateValues(T entityToUpdate, T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        DbSet<T> Query();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(object id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        DbRawSqlQuery<T> SqlQuery(string sql, params object[] parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IList<T> Find(Expression<Func<T, bool>> where);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="includeExpressions"></param>
        /// <returns></returns>
        IList<T> Find(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeExpressions);

        IList<T> FindAll(params Expression<Func<T, object>>[] includeExpressions);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IQueryable<T> FindQuery(Expression<Func<T, bool>> where);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        T Single(Expression<Func<T, bool>> where);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        T First(Expression<Func<T, bool>> where);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        T FirstOrDefault(Expression<Func<T, bool>> where);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="exps"></param>
        /// <returns></returns>
        T FirstOrDefault(Expression<Func<T, bool>> where, params Expression<Func<T, ICollection<object>>>[] exps);

        IList<T> FindByLinqExpression(Func<T, bool> where);
        IList<T> FindByLinqExpression(Func<T, bool> where, params Expression<Func<T, ICollection<object>>>[] exps);
    }
}