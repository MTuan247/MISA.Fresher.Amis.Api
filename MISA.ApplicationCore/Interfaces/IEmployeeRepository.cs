using MISA.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        public string GetNewEmployeeCode();

        /// <summary>
        /// Hàm lấy dữ liệu đã được lọc
        /// </summary>
        /// <param name="employeeFilter">Từ khóa tìm kiếm</param>
        /// <param name="departmentId">Id phòng ban</param>
        /// <param name="positionId">Id vị trí</param>
        /// <param name="pageSize">Số bản ghi trả về</param>
        /// <param name="pageIndex">Số trang</param>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        public EntitiesResponse<Employee> GetEmployeesFiltered(string employeeFilter, Guid? departmentId, int pageSize, int pageIndex);
    }
}
