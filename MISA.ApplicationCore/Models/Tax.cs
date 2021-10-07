using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Models
{
    /// <summary>
    /// Kỳ thuế
    /// </summary>
    public class Tax : BaseEntity
    {
        #region Properties
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid TaxId { get; set; }
        /// <summary>
        /// Mã kỳ
        /// </summary>
        [MISAUnique]
        [MISAPropertyName("Mã kỳ")]
        public string TaxCode { get; set; }
        /// <summary>
        /// Tên kỳ
        /// </summary>
        public string TaxName { get; set; }
        /// <summary>
        /// Ngày bắt đầu
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// Ngày kết thúc
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// Năm
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Tổng thuế trong kỳ
        /// </summary>
        public int SummaryTax { get; set; }
        /// <summary>
        /// Tổng số nhân viên trong kỳ
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Chi tiết thông tin nhân viên trong kỳ thuế
        /// </summary>
        [MISAOverMapping]
        public TaxDetail[] Details { get; set; }

        #endregion
    }
}
