using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Enums
{
    class StatusCodeEnum
    {
    }

    /// <summary>
    /// Xác định StatusCode
    /// </summary>
    public enum StatusCode
    {
        /// <summary>
        /// Mã thành công
        /// </summary>
        SuccessCode = 200,
        /// <summary>
        /// Lấy dữ liệu thành công
        /// </summary>
        GetSuccess = 200,
        /// <summary>
        /// Không có dữ liệu
        /// </summary>
        NoContent = 204,
        /// <summary>
        /// Lỗi Validate
        /// </summary>
        ValidateCode = 200,
        /// <summary>
        /// BadRequest
        /// </summary>
        BadRequest = 400,
        /// <summary>
        /// Exception
        /// </summary>
        Exception = 500,
        /// <summary>
        /// Cập nhật thành công
        /// </summary>
        UpdateSuccess = 200,
        /// <summary>
        /// Cập nhật thất bại
        /// </summary>
        UpdateFail = 200,
        /// <summary>
        /// Thêm thành công
        /// </summary>
        AddSuccess = 201,
        /// <summary>
        /// Thêm thất bại
        /// </summary>
        AddFail = 200,
        /// <summary>
        /// Xóa thành công
        /// </summary>
        DeleteSuccess = 200,
        /// <summary>
        /// Xóa thất bại
        /// </summary>
        DeleteFail = 200,
    }
}
