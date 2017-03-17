using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxSoftSol.MLS.Domain
{
    public class UnitOfWork : IUnitOfWork
    {
        private Dictionary<string, dynamic> _repositories;
        private ProMetrixContext _dataContext;
        private bool _disposed;
        private DbContextTransaction _transaction;


        public UnitOfWork()
        {
            _dataContext = new ProMetrixContext();
        }

        public UnitOfWork(ProMetrixContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, dynamic>();
            }

            var type = typeof(TEntity).Name;

            if (_repositories.ContainsKey(type))
            {
                return (IRepository<TEntity>)_repositories[type];
            }

            var repositoryType = typeof(Repository<>);
            _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dataContext));
            return _repositories[type];
        }


        public int SaveChanges()
        {
            try
            {
                return _dataContext.SaveChanges();
            }
            // Add Error Handling
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

        }


        //for now the isolationLevel is set to default Read_Committed_Snapshot
        public void BeginTransaction()//IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            _transaction = _dataContext.Database.BeginTransaction();
        }

        public bool Commit()
        {
            _transaction.Commit();
            return true;
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // free other managed objects that implement
                // IDisposable only

                try
                {
                    if (_dataContext != null)
                    {
                        _dataContext.Dispose();
                        _dataContext = null;
                    }
                }
                catch (ObjectDisposedException)
                {
                    // do nothing, the objectContext has already been disposed
                }
            }

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
        }
    }
}
