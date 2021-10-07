using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Interfaces;
using MISA.ApplicationCore.Models;

namespace MISA.Infrastructure
{
    public class TaxRepository : BaseRepository<Tax>, ITaxRepository
    {
        #region Constructor
        public TaxRepository(IConfiguration iconfiguration) : base(iconfiguration)
        {
        }
        #endregion
        #region Methods
        /// <summary>
        /// Hàm lấy dữ liệu đã được lọc
        /// </summary>
        /// <param name="taxFilter"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        /// Created by: NMTuan (23/09/2021)
        /// Modified by: NMTuan (23/09/2021)
        public EntitiesResponse<Tax> GetTaxsFiltered(string taxFilter, int year, int pageSize, int pageIndex)
        {
            var sql = $"Proc_GetTaxsFilterPaging";

            var offset = pageIndex * pageSize;

            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@TaxFilter", taxFilter);
            parameters.Add("@Year", year);
            parameters.Add("@PageSize", pageSize);
            parameters.Add("@PageIndex", offset);

            var employees = _dbConnection.Query<Tax>(sql, parameters, commandType: CommandType.StoredProcedure);

            var totalPage = parameters.Get<int>("@TotalPage");
            var totalRecord = parameters.Get<int>("@TotalRecord");

            var result = new EntitiesResponse<Tax>
            {
                TotalPage = totalPage,
                TotalRecord = totalRecord,
                Data = employees
            };

            return result;
        }

        /// <summary>
        /// Hàm lấy thông tin kỳ thuế theo Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public override Tax GetEntityById(Guid entityId)
        {
            var sqlGetTax = $"SELECT * FROM Tax WHERE TaxId = @EntityId";
            var sqlGetDetail = $"SELECT t.TaxId, t.EmployeeId, t.Salary, " +
                $"t.Insurance, t.TaxableIncome, t.Tax, t.Description, e.EmployeeCode, e.EmployeeName " +
                $"FROM `tax-employee` t " +
                $"LEFT JOIN employee e " +
                $"ON t.EmployeeId = e.EmployeeId " +
                $"WHERE TaxId = @EntityId";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@EntityId", entityId);
            var entity = _dbConnection.QueryFirstOrDefault<Tax>(sqlGetTax, parameters);
            var detail = _dbConnection.Query<TaxDetail>(sqlGetDetail, parameters).ToArray();
            entity.Details = detail;
            return entity;
        }

        /// <summary>
        /// Hàm lấy mã kỳ thuế mới
        /// </summary>
        /// <returns></returns>
        /// Created by: NMTuan (24/09/2021)
        /// Modified by: NMTuan (24/09/2021)
        public string GetNewTaxCode()
        {
            var sql = $"Proc_GetNewTaxCode";

            var newCode = _dbConnection.QueryFirstOrDefault<string>(sql, commandType: CommandType.StoredProcedure);

            return newCode;
        }

        /// <summary>
        /// Cập nhật bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// CreatedBy: NMTuan (30/07/2021)
        /// ModifiedBy: NMTuan (02/08/2021)
        public override int Update(Tax entity)
        {

            var sql = $"UPDATE Tax t " +
                $"SET t.TaxCode = @TaxCode, t.TaxName = @TaxName, t.StartDate = @StartDate, t.EndDate = @EndDate, " +
                $"t.Description = @Description, t.ModifiedDate = NOW() " +
                $"WHERE t.TaxId = @TaxId; " +
                $"DELETE FROM `Tax-Employee` " +
                $"WHERE TaxId = @TaxId;";

            var properties = new String[] {"TaxId", "EmployeeId", "Salary", "Insurance", "TaxableIncome", "Tax", "Description" };
            var sqlUpdateDetails = $"INSERT INTO `Tax-Employee` (" +
                $"{string.Join(", ", properties)}" +
                $") VALUES ";
            DynamicParameters parametersUpdateDetails = new DynamicParameters();
            for (var index = 0; index < entity.Details.Length; index++)
            {
                sqlUpdateDetails += $"( @TaxId{index}, @EmployeeId{index}, @Salary{index}, @Insurance{index}, @TaxableIncome{index}, @Tax{index},  @Description{index} )";
                if (index == entity.Details.Length - 1)
                {
                    sqlUpdateDetails += $";";
                }
                else
                {
                    sqlUpdateDetails += $", ";
                }
                parametersUpdateDetails = MappingObtype(entity.Details[index], parametersUpdateDetails, index);
            }

            _dbConnection.Open();

            using (var transaction = _dbConnection.BeginTransaction())
            {
                DynamicParameters parameters = MappingObtype(entity);

                var res = _dbConnection.Execute(sql, parameters, transaction: transaction);

                _dbConnection.Execute(sqlUpdateDetails, parametersUpdateDetails, transaction: transaction);

                transaction.Commit();

                return res;
            }

        }

        /// <summary>
        /// Cập nhật bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// CreatedBy: NMTuan (30/07/2021)
        /// ModifiedBy: NMTuan (02/08/2021)
        public override int Add(Tax entity)
        {
            var uuid = Guid.NewGuid();

            var sql = $"INSERT INTO tax (TaxId, TaxCode, TaxName, StartDate, EndDate, Year, Description, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy)" +
                $"VALUES('{uuid}', @TaxCode, @TaxName, @StartDate, @EndDate, @Year, @Description, NOW(), @CreatedBy, @ModifiedDate, @ModifiedBy); ";

            var properties = new String[] { "TaxId", "EmployeeId", "Salary", "Insurance", "TaxableIncome", "Tax", "Description" };
            var sqlUpdateDetails = $"INSERT INTO `Tax-Employee` (" +
                $"{string.Join(", ", properties)}" +
                $") VALUES ";
            DynamicParameters parametersUpdateDetails = new DynamicParameters();
            for (var index = 0; index < entity.Details.Length; index++)
            {
                sqlUpdateDetails += $"( '{uuid}', @EmployeeId{index}, @Salary{index}, @Insurance{index}, @TaxableIncome{index}, @Tax{index},  @Description{index} )";
                if (index == entity.Details.Length - 1)
                {
                    sqlUpdateDetails += $";";
                }
                else
                {
                    sqlUpdateDetails += $", ";
                }
                parametersUpdateDetails = MappingObtype(entity.Details[index], parametersUpdateDetails, index);
            }

            _dbConnection.Open();

            using (var transaction = _dbConnection.BeginTransaction())
            {
                DynamicParameters parameters = MappingObtype(entity);

                parameters.Add("@TaxId", uuid);

                var res = _dbConnection.Execute(sql, parameters, transaction: transaction);

                _dbConnection.Execute(sqlUpdateDetails, parametersUpdateDetails, transaction: transaction);

                transaction.Commit();

                return res;
            }
        }

        /// <summary>
        /// Hàm map data theo object
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="parameters"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        protected DynamicParameters MappingObtype(TaxDetail entity, DynamicParameters parameters, int index)
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
