using MISA.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces
{
    public interface IEmployeeService : IBaseService<Employee>
    {
        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        public string GetNewCode();
        /// <summary>
        /// Hàm lấy danh sách nhân viên theo tiêu chí
        /// </summary>
        /// <param name="employeeFilter"></param>
        /// <param name="departmentId"></param>
        /// <param name="positionId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        public EntitiesResponse<Employee> GetEntitiesFiltered(string employeeFilter, Guid? departmentId, int pageSize, int pageIndex);
        /// <summary>
        /// Hàm sửa nhiều bản ghi
        /// </summary>
        /// <param name="employees"></param>
        /// <returns></returns>
        /// Created by: NMTuan (31/08/2021)
        public ServiceResult Update(Employee[] employees);
    }
}
