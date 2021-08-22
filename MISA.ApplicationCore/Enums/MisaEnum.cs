using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Enums
{
    class MisaEnum
    {

    }

    /// <summary>
    /// Xác định giới tính
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// Nam
        /// </summary>
        Male = 0,

        /// <summary>
        /// Nữ
        /// </summary>
        Female = 1,
        /// <summary>
        /// Khác
        /// </summary>
        Other = 2,
    }

/// <summary>
/// Xác định tình trạng công việc
/// </summary>
public enum WorkStatus
{
    /// <summary>
    /// Đang làm việc
    /// </summary>
    Working = 1,

    /// <summary>
    /// Đã nghỉ việc
    /// </summary>
    Retired = 0,
    /// <summary>
    /// Đang học việc
    /// </summary>
    Apprenticing = 2,
    /// <summary>
    /// Không xác định
    /// </summary>
    Other = 3,
}

/// <summary>
/// Xác định trang thái entity: Update, Add, Delete
/// </summary>
public enum EntityState
{
    /// <summary>
    /// Thêm mới
    /// </summary>
    AddNew = 1,
    /// <summary>
    /// Cập nhật
    /// </summary>
    Update = 2,
    /// <summary>
    /// Xóa
    /// </summary>
    Delete = 3,
}
}
