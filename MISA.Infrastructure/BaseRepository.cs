using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Interfaces;
using MISA.ApplicationCore.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region Fields
        string _connectionString;
        string className;
        protected IDbConnection _dbConnection;
        IConfiguration _iconfiguration;
        #endregion

        #region Constructor
        public BaseRepository(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
            _connectionString = _iconfiguration.GetConnectionString("MISAAmisLocalConnectionString");
            _dbConnection = new MySqlConnection(_connectionString);
            className = typeof(TEntity).Name;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hàm lấy danh sách entities
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: NMTuan (30/07/2021)
        /// ModifiedBy: NMTuan (30/07/2021)
        public IEnumerable<TEntity> GetEntities()
        {
            /*var sql = $"SELECT * FROM {className}";
            var entities = _dbConnection.Query<Entity>(sql);*/

            var sql = $"Proc_Get{className}s";

            var entities = _dbConnection.Query<TEntity>(sql, commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Hàm lấy danh sách theo tiêu chí
        /// </summary>
        /// <param name="EntityCode"></param>
        /// <param name="PageNumber"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        /// CreatedBy: NMTuan (30/07/2021)
        /// ModifiedBy: NMTuan (30/07/2021)
        public IEnumerable<TEntity> GetEntitiesFiltered(string EntityCode, int PageNumber, int PageSize)
        {
            var sqlCommand = $"Select * from {className} " +
               $"where {className}Code like @{className}Code " +
               $"ORDER BY {className}Code " +
               $"LiMIT @Limit " +
               $"OFFSET @Offset";

            DynamicParameters parameters = new DynamicParameters();

            parameters.Add($"@{className}Code", '%' + EntityCode + '%');
            parameters.Add("@Limit", PageSize);
            parameters.Add("@Offset", (PageNumber - 1) * PageSize);

            var entities = _dbConnection.Query<TEntity>(sqlCommand, parameters);

            return entities;
        }

        /// <summary>
        /// Lấy bản ghi theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// CreatedBy: NMTuan (30/07/2021)
        /// ModifiedBy: NMTuan (02/08/2021)
        public virtual TEntity GetEntityById(Guid entityId)
        {
            //var sql = $"SELECT * FROM {className} WHERE {className}Id = @{className}Id";

            var sql = $"Proc_Get{className}ById";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{className}Id", entityId);
            var entity = _dbConnection.QueryFirstOrDefault<TEntity>(sql, parameters, commandType: CommandType.StoredProcedure);
            return entity;
        }

        /// <summary>
        /// Hàm lấy bản ghi theo danh sách các id
        /// </summary>
        /// <param name="entityIds"></param>
        /// <returns></returns>
        /// CreatedBy: NMTuan (21/09/2021)
        /// ModifiedBy: NMTuan (21/09/2021)
        public IEnumerable<TEntity> GetEntitiesByIds(Guid[] entityIds)
        {
            var sql = $"SELECT * FROM {className} " +
               $"WHERE {className}Id in @EntityIds";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@EntityIds", entityIds);
            var entities = _dbConnection.Query<TEntity>(sql, parameters);


            return entities;
        }

        /// <summary>
        /// Lấy bản ghi theo mã
        /// </summary>
        /// <param name="entityCode"></param>
        /// <returns></returns>
        /// CreatedBy: NMTuan (30/07/2021)
        /// ModifiedBy: NMTuan (02/08/2021)
        public TEntity GetEntityByCode(string entityCode, TEntity entity)
        {
            var entityState = entity.EntityState;

            var entityId = entity.GetType().GetProperty($"{className}Id").GetValue(entity);

            var sql = string.Empty;
            if (entityState == ApplicationCore.Enums.EntityState.AddNew)
            {
                sql = $"Select * from {className} Where {className}Code = @{className}Code";
            }
            else if (entityState == ApplicationCore.Enums.EntityState.Update)
            {
                sql = $"Select * from {className} Where {className}Code = @{className}Code and {className}Id <> @EntityId";
            }
            else
            {
                return null;
            }


            DynamicParameters parameters = new DynamicParameters();

            parameters.Add($"@{className}Code", entityCode);
            parameters.Add($"@EntityId", entityId);

            var result = _dbConnection.QueryFirstOrDefault<TEntity>(sql, parameters);

            return result;
        }

        /// <summary>
        /// Hàm lấy bản ghi theo property
        /// </summary>
        /// <param name="property"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// CreatedBy: NMTuan (05/08/2021)
        /// ModifiedBy: NMTuan (05/08/2021)
        public IEnumerable<TEntity> GetEntitiesByProperty(string property, TEntity entity)
        {
            var entityState = entity.EntityState;

            var entityId = entity.GetType().GetProperty($"{className}Id").GetValue(entity);

            var sql = string.Empty;
            if (entityState == ApplicationCore.Enums.EntityState.AddNew)
            {
                sql = $"Select * from {className} Where {property} = @Property";
            }
            else if (entityState == ApplicationCore.Enums.EntityState.Update)
            {
                sql = $"Select * from {className} Where {property} = @Property and {className}Id <> @EntityId";
            }
            else
            {
                return null;
            }


            DynamicParameters parameters = new DynamicParameters();

            var propertyValue = entity.GetType().GetProperty(property).GetValue(entity);

            parameters.Add($"@Property", propertyValue);
            parameters.Add($"@EntityId", entityId);

            var result = _dbConnection.Query<TEntity>(sql, parameters);

            return result;
        }

        /// <summary>
        /// Thêm bản ghi mới
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// CreatedBy: NMTuan (30/07/2021)
        /// ModifiedBy: NMTuan (02/08/2021)
        public virtual int Add(TEntity entity)
        {
            var sql = $"Proc_Insert{className}";

            DynamicParameters parameters = MappingObtype(entity);

            var res = _dbConnection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

            return res;
        }

        /// <summary>
        /// Cập nhật bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// CreatedBy: NMTuan (30/07/2021)
        /// ModifiedBy: NMTuan (02/08/2021)
        public virtual int Update(TEntity entity)
        {
            var sql = $"Proc_Update{className}";

            DynamicParameters parameters = MappingObtype(entity);

            var res = _dbConnection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

            return res;
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// CreatedBy: NMTuan (30/07/2021)
        /// ModifiedBy: NMTuan (02/08/2021)
        public int Delete(Guid entityId)
        {
            //var sqlCommand = $"DELETE FROM {className} WHERE {className}Id= @{className}Id ";

            var sqlCommand = $"Proc_Delete{className}ById";

            DynamicParameters parameters = new DynamicParameters();

            parameters.Add($"@{className}Id", entityId);

            var res = _dbConnection.Execute(sqlCommand, parameters, commandType: CommandType.StoredProcedure);

            return res;
        }

        /// <summary>
        /// Hàm xóa nhiều
        /// </summary>
        /// <param name="entityIds"></param>
        /// <returns></returns>
        public int DeleteMultiple(Guid[] entityIds)
        {

            var sqlCommand = $"DELETE FROM {className} WHERE {className}Id in @EntityIds";

            _dbConnection.Open();

            using (var transaction = _dbConnection.BeginTransaction())
            {

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@EntityIds", entityIds);

                var res = _dbConnection.Execute(sqlCommand, parameters, transaction: transaction);

                transaction.Commit();

                return res;
            }
        }

        /// <summary>
        /// Hàm map data theo object
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// CreatedBy: NMTuan (30/07/2021)
        /// ModifiedBy: NMTuan (02/08/2021)
        protected virtual DynamicParameters MappingObtype(TEntity entity)
        {
            var properties = entity.GetType().GetProperties();
            var parameters = new DynamicParameters();
            foreach (var property in properties)
            {
                // Kiểm tra prop có thuộc tính bỏ qua khi map
                var propOver = property.GetCustomAttributes(typeof(MISAOverMapping), true);
                if (propOver.Length > 0)
                {
                    continue;
                }
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                var propertyType = property.PropertyType;
                if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                {
                    parameters.Add($"@{propertyName}", propertyValue, DbType.String);
                }
                else
                {
                    parameters.Add($"@{propertyName}", propertyValue);
                }
            }
            return parameters;
        }

        /// <summary>
        /// Hàm map data theo object
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="parameters"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        protected DynamicParameters MappingObtype(TEntity entity, DynamicParameters parameters, int index)
        {
            var properties = entity.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Kiểm tra prop có thuộc tính bỏ qua khi map
                var propOver = property.GetCustomAttributes(typeof(MISAOverMapping), true);
                if (propOver.Length > 0)
                {
                    continue;
                }
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                var propertyType = property.PropertyType;
                if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                {
                    parameters.Add($"@{propertyName}{index}", propertyValue, DbType.String);
                }
                else
                {
                    parameters.Add($"@{propertyName}{index}", propertyValue);
                }
            }
            return parameters;
        } 
        #endregion
    }
}
