using System.Data;
using System.Data.SqlClient;
using Ninject.Infrastructure.Language;

namespace Incoding.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    public class EntityFrameworkRepository : IRepository
    {
        readonly DbContext session;
        public EntityFrameworkRepository(DbContext session)
        {
            this.session = session;
        }

        public void ExecuteSql(string sql)
        {
            session.Database.ExecuteSqlCommand(sql);
        }

        public object[] ExecuteSp(string storedProcedureName, params object[] parameters)
        {
            using (var connection = new SqlConnection(session.Database.GetDbConnection().ConnectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = storedProcedureName;
                cmd.CommandType = CommandType.StoredProcedure;
                parameters.Map(r =>
                {
                    var par = cmd.CreateParameter();
                    par.Value = r;
                    cmd.Parameters.Add(par);
                    par.ParameterName = null;
                });
                connection.Open();
                cmd.ExecuteNonQuery();
                return parameters;
            }
        }

        public TProvider GetProvider<TProvider>() where TProvider : class
        {
            return session as TProvider;
        }

        public void Save<TEntity>(TEntity entity) where TEntity : class, IEntity, new()
        {
            session.Set<TEntity>().Add(entity);
        }

        public void Saves<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity, new()
        {
            foreach (var entity in entities)
                Save(entity);
        }

        public void Flush()
        {
            session.SaveChanges();
        }

        public void SaveOrUpdate<TEntity>(TEntity entity) where TEntity : class, IEntity, new()
        {
            if (session.Entry(entity).State == EntityState.Detached)
                session.Set<TEntity>().Add(entity);
        }

        public void Delete<TEntity>(int id) where TEntity : class, IEntity, new()
        {
            Delete(GetById<TEntity>(id));
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity, new()
        {
            session.Set<TEntity>().Remove(entity);
        }



        public TEntity GetById<TEntity>(int id) where TEntity : class, IEntity, new()
        {
            return session.Set<TEntity>().FirstOrDefault(r => r.Id == id);
        }

        public TEntity LoadById<TEntity>(int id) where TEntity : class, IEntity, new()
        {
            return session.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> Query<TEntity>(OrderSpecification<TEntity> orderSpecification = null, Specification<TEntity> whereSpecification = null, FetchSpecification<TEntity> fetchSpecification = null, PaginatedSpecification paginatedSpecification = null) where TEntity : class, IEntity, new()
        {
            return session.Set<TEntity>().Query(orderSpecification, whereSpecification, fetchSpecification, paginatedSpecification);
        }

        public IncPaginatedResult<TEntity> Paginated<TEntity>(PaginatedSpecification paginatedSpecification, OrderSpecification<TEntity> orderSpecification = null, Specification<TEntity> whereSpecification = null, FetchSpecification<TEntity> fetchSpecification = null) where TEntity : class, IEntity, new()
        {
            return session.Set<TEntity>().Paginated(orderSpecification, whereSpecification, fetchSpecification, paginatedSpecification);
        }

        public void Clear() { }
    }
}