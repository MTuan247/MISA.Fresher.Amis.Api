using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Models
{
    public class ServiceResult
    {
        #region Properties
        /// <summary>
        /// Giá trị xác định thành công hay không
        /// </summary>
        public bool Success { get; set; } = true;
        /// <summary>
        /// Thông báo trả về cho người dùng
        /// </summary>
        public string UserMsg { get; set; }
        /// <summary>
        /// Thông báo trả về cho dev
        /// </summary>
        public string DevMsg { get; set; }
        /// <summary>
        /// StatusCode trả về
        /// </summary>
        public StatusCode StatusCode { get; set; }
        /// <summary>
        /// Mã lỗi trả về
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// Dữ liệu lỗi muốn trả về
        /// </summary>
        public string Data { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Hàm xử lý khi xảy ra exception
        /// </summary>
        /// <param name="ex"></param>
        /// Created by: NMTuan (09/08/2021)
        public void ExceptionHandle(Exception ex)
        {
            this.Success = false;
            this.StatusCode = StatusCode.Exception;
            this.DevMsg = ex.Message;
            this.UserMsg = Resources.ExceptionError;
        }
        #endregion
    }
}
