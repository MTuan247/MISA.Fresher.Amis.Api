using MISA.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Models
{
    /// <summary>
    /// Nhân viên
    /// </summary>
    public class Employee : BaseEntity
    {

        #region Properties
        /// <summary>
        /// Khóa chính
        /// </summary>
        [PrimaryKey]
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [MISARequired]
        [MISAUnique]
        [MISAPropertyName("Mã nhân viên")]
        [MISAMaxLength(20)]
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Họ và tên
        /// </summary>
        [MISARequired]
        [MISAPropertyName("Họ và tên")]
        [MISAMaxLength(100)]
        public string EmployeeName { get; set; }
        #nullable enable
        /// <summary>
        /// Giới tính
        /// </summary>
        [MISAPropertyName("Giới tính")]
        public Gender? Gender { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public string? GenderName {
            get
            {
                var name = string.Empty;
                switch (this.Gender)
                {
                    case Enums.Gender.Male:
                        name = "Nam";
                        break;
                    case Enums.Gender.Female:
                        name = "Nữ";
                        break;
                    case Enums.Gender.Other:
                        name = "Không xác định";
                        break;
                    default:
                        break;
                }
                return name;
            }
            set { }
        }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        [MISAPhoneNumber]
        [MISAPropertyName("Số điện thoại")]
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        public string? TelephoneNumber { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        [MISAEmail]
        [MISAMaxLength(100)]
        public string? Email { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        [MISAMaxLength(255)]
        public string? Address { get; set; }
        /// <summary>
        /// CMTND
        /// </summary>
        public string? IdentityNumber { get; set; }
        /// <summary>
        /// Nơi cấp CMT
        /// </summary>
        public DateTime? IdentityDate { get; set; }
        /// <summary>
        /// Ngày cấp CMT
        /// </summary>
        public string? IdentityPlace { get; set; }
        /// <summary>
        /// ID phòng ban
        /// </summary>
        [MISARequired]
        [MISAPropertyName("Phòng ban")]
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? DepartmentName { get; set; }
        /// <summary>
        /// Vị trí công việc nhân viên
        /// </summary>
        public string? EmployeePosition { get; set; }
        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        public string? BankAccountNumber { get; set; }
        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }
        /// <summary>
        /// Tên chi nhánh ngân hàng
        /// </summary>
        public string? BankBranchName { get; set; }
        /// <summary>
        /// Tên tỉnh của ngân hàng
        /// </summary>
        public string? BankProvinceName { get; set; }

        #endregion

        #region Methods

        #endregion
    }
}
