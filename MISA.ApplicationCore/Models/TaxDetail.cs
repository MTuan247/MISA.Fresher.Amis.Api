using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Models
{
    /// <summary>
    /// Chi tiết thông tin của nhân viên trong kỳ thuế
    /// </summary>
    public class TaxDetail
    {
        /// <summary>
        /// Khóa chính của kỳ thuế
        /// </summary>
        public Guid TaxId { get; set; }
        /// <summary>
        /// Khóa chính của nhân viên
        /// </summary>
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// Lương của nhân viên
        /// </summary>
        public int Salary { get; set; }
        /// <summary>
        /// Tiền bảo hiểm
        /// </summary>
        public int Insurance { get; set; }
        /// <summary>
        /// Thu nhập đóng thuế
        /// </summary>
        public int TaxableIncome { get; set; }
        /// <summary>
        /// Thuế phải đóng
        /// </summary>
        public int Tax { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Description { get; set; }
    }
}
