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
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region Constructors
        public EmployeeRepository(IConfiguration iconfiguration) : base(iconfiguration)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hàm lấy danh sách nhân viên đã lọc
        /// </summary>
        /// <param name="employeeFilter"></param>
        /// <param name="departmentId"></param>
        /// <param name="positionId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        public EntitiesResponse<Employee> GetEmployeesFiltered(string employeeFilter, Guid? departmentId, int pageSize, int pageIndex)
        {
            var sql = $"Proc_GetEmployeesFilterPaging";

            var offset = pageIndex * pageSize;

            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@EmployeeFilter", employeeFilter);
            parameters.Add("@DepartmentId", departmentId);
            parameters.Add("@PageSize", pageSize);
            parameters.Add("@PageIndex", offset);

            var employees = _dbConnection.Query<Employee>(sql, parameters, commandType: CommandType.StoredProcedure);

            var totalPage = parameters.Get<int>("@TotalPage");
            var totalRecord = parameters.Get<int>("@TotalRecord");

            var result = new EntitiesResponse<Employee>
            {
                TotalPage = totalPage,
                TotalRecord = totalRecord,
                Data = employees
            };

            return result;
        }

        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        public string GetNewEmployeeCode()
        {
            var sql = $"Proc_GetNewEmployeeCode";

            var newCode = _dbConnection.QueryFirstOrDefault<string>(sql, commandType: CommandType.StoredProcedure);

            return newCode;
        }
        #endregion
    }
}
