using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MaxSoftSol.MLS.Domain
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public Repository()
        {
        }

        public Repository(ProMetrixContext db)
        {
            Db = db;
        }

        public ProMetrixContext Db { get; set; }


        public virtual IList<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = Db.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }


        public virtual T GetById(object id)
        {
            return Db.Set<T>().Find(id);

        }


        public DbSet<T> Query()
        {
            return Db.Set<T>();
        }


        public DbRawSqlQuery<T> SqlQuery(string sql, params object[] parameters)
        {
            return Db.Database.SqlQuery<T>(sql, parameters);
        }


        public IQueryable<T> FindQuery(Expression<Func<T, bool>> where)
        {
            return Db.Set<T>().Where(where);
        }


        public IList<T> Find(Expression<Func<T, bool>> where)
        {
            return Db.Set<T>().Where(where).ToList();
        }


        public IList<T> Find(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> query = Db.Set<T>();

            foreach (var includeExpression in includeExpressions)
            {
                query = query.Include(includeExpression);
            }

            return query.Where(where).ToList();
        }


        public IList<T> FindAll(params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> query = Db.Set<T>();

            foreach (var includeExpression in includeExpressions)
            {
                query = query.Include(includeExpression);
            }

            return query.ToList();
        }


        public virtual T FirstOrDefault(Expression<Func<T, bool>> where)
        {
            var entity = Db.Set<T>().FirstOrDefault(@where);
            return entity;
        }

        public virtual T FirstOrDefault(Expression<Func<T, bool>> where, params Expression<Func<T, ICollection<object>>>[] exps)
        {
            IQueryable<T> query = Db.Set<T>();

            foreach (var includeExpression in exps)
            {
                query = query.Include(includeExpression);
            }

            return query.FirstOrDefault(@where);
        }


        public void Load<TElement>(T parent, params Expression<Func<T, ICollection<TElement>>>[] includeExpressions) where TElement : class
        {
            foreach (var includeExpression in includeExpressions)
            {
                Db.Entry(parent).Collection(includeExpression).Load();
            }
        }

        public T Single(Expression<Func<T, bool>> where)
        {
            return Db.Set<T>().Single(where);
        }

        public T First(Expression<Func<T, bool>> where)
        {
            return Db.Set<T>().First(where);
        }


        public virtual void Insert(T entity)
        {
            Db.Set<T>().Add(entity);
        }

        public virtual void Update(T entityToUpdate)
        {
            Db.Set<T>().Attach(entityToUpdate);
            Db.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void UpdateValues(T entityToUpdate, T entity)
        {

            Db.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            Db.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Delete(object id)
        {
            T entityToDelete = Db.Set<T>().Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (Db.Entry(entityToDelete).State == EntityState.Detached)
            {
                Db.Set<T>().Attach(entityToDelete);
            }

            Db.Set<T>().Remove(entityToDelete);
        }

        public virtual void Delete(IList<T> entitiesToDelete)
        {
            foreach (var entityToDelete in entitiesToDelete)
            {
                if (Db.Entry(entityToDelete).State == EntityState.Detached)
                {
                    Db.Set<T>().Attach(entityToDelete);
                }
            }
            Db.Set<T>().RemoveRange(entitiesToDelete);
        }
        public virtual IList<T> FindByLinqExpression(Func<T, bool> where)
        {
            return Db.Set<T>().Where(where).ToList();
        }

        public virtual IList<T> FindByLinqExpression(Func<T, bool> where, params Expression<Func<T, ICollection<object>>>[] exps)
        {
            IQueryable<T> query = Db.Set<T>();

            foreach (var includeExpression in exps)
            {
                query = query.Include(includeExpression);
            }
            return query.Where(@where).ToList();
        }
    }
}
