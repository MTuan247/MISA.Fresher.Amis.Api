using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Interfaces;
using MISA.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region Fields
        IEmployeeRepository _employeeRepository;
        #endregion

        #region Constructor
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hàm lấy nhân viên đã lọc
        /// </summary>
        /// <param name="employeeFilter"></param>
        /// <param name="departmentId"></param>
        /// <param name="positionId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        public EntitiesResponse<Employee> GetEntitiesFiltered(string employeeFilter, Guid? departmentId, int pageSize, int pageIndex)
        {
            var result = _employeeRepository.GetEmployeesFiltered(employeeFilter, departmentId, pageSize, pageIndex);

            return result;
        }

        /// <summary>
        /// Hàm update nhân viên
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (03/08/2021)
        public override ServiceResult Update(Guid employeeId, Employee employee)
        {
            // Kiểm tra id trong body và router có trùng
            if(employeeId != employee.EmployeeId || string.IsNullOrEmpty(employee.EmployeeId.ToString()))
            {
                serviceResult.StatusCode = 200;
                serviceResult.Success = false;
                serviceResult.UserMsg = Properties.Resources.ValidateError_EmployeeIdNotMatch;
                return serviceResult;
            }
            return base.Update(employeeId, employee);
        }

        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        public string GetNewCode()
        {
            return _employeeRepository.GetNewEmployeeCode();
        }

        #endregion
    }
}
