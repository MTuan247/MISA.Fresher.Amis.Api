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

        /// <summary>
        /// Hàm sửa nhiều bản ghi
        /// </summary>
        /// <param name="employees"></param>
        /// <returns></returns>
        /// Created by: NMTuan (31/08/2021)
        /// Modified by: NMTuan (01/09/2021)
        public int UpdateMultiple(Employee[] employees)
        {
            var properties = new String[] {"EmployeeId", "EmployeeCode", "EmployeeName", "DateOfBirth", "Gender",
                "DepartmentId", "IdentityNumber", "IdentityDate", "IdentityPlace", "EmployeePosition",
                "Address", "BankAccountNumber", "BankName", "BankBranchName", "BankProvinceName",
                "PhoneNumber", "TelephoneNumber", "Email", "ModifiedBy", "ModifiedDate" };
            DynamicParameters parameters = new DynamicParameters();
            var sql = $"INSERT INTO Employee" +
                $"(EmployeeId, EmployeeCode, EmployeeName, DateOfBirth, Gender, " +
                $"DepartmentId, IdentityNumber, IdentityDate, IdentityPlace, EmployeePosition, " +
                $"Address, BankAccountNumber, BankName, BankBranchName, BankProvinceName, " +
                $"PhoneNumber, TelephoneNumber, Email, ModifiedBy, ModifiedDate)" +
                $"VALUES ";

            for (var index = 0; index < employees.Length; index++)
            {
                sql += $"(@EmployeeId{index}, @EmployeeCode{index}, @EmployeeName{index}, @DateOfBirth{index}, @Gender{index}, " +
                $"@DepartmentId{index}, @IdentityNumber{index}, @IdentityDate{index}, @IdentityPlace{index}, @EmployeePosition{index}, " +
                $"@Address{index}, @BankAccountNumber{index}, @BankName{index}, @BankBranchName{index}, @BankProvinceName{index}, " +
                $"@PhoneNumber{index}, @TelephoneNumber{index}, @Email{index}, @ModifiedBy{index}, NOW())";
                if (index == employees.Length - 1)
                {
                    sql += $" ";
                } 
                else
                {
                    sql += $", ";
                }
                parameters = MappingObtype(employees[index], parameters, index);
            }

            sql += $"ON DUPLICATE KEY UPDATE ";
            for (var index = 0; index < properties.Length; index++)
            {
                var property = properties[index];
                sql += property + " = VALUES(" + property + ")";
                if (index == properties.Length - 1)
                {
                    sql += $";";
                }
                else
                {
                    sql += $", ";
                }
            }

            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                var res = _dbConnection.Execute(sql, parameters, transaction: transaction);

                transaction.Commit();

                return res;
            }
        }
        #endregion
    }
}
