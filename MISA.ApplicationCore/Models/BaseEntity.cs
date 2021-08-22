using MISA.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Models
{
    #region Attributes
    /// <summary>
    /// Attribute trường bắt buộc
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MISARequired : Attribute
    {

    }

    /// <summary>
    /// Attribute khóa chính
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKey : Attribute
    {

    }

    /// <summary>
    /// Attribute Tên hiển thị
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MISAPropertyName : Attribute
    {
        public string Name = string.Empty;
        public MISAPropertyName(string propertyName)
        {
            Name = propertyName;
        }
    }

    /// <summary>
    /// Attribute Giới hạn độ dài
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MISAMaxLength : Attribute
    {
        public int Max = int.MaxValue;
        public MISAMaxLength(int maxLength)
        {
            Max = maxLength;
        }
    }

    /// <summary>
    /// Attribute Không được trùng
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MISAUnique : Attribute
    {

    }

    /// <summary>
    /// Attribute Email
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MISAEmail : Attribute
    {

    }

    /// <summary>
    /// Attribute Số điện thoại
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MISAPhoneNumber : Attribute
    {

    }
    #endregion

    public class BaseEntity
    {
        #region Properties
        /// <summary>
        /// Trạng thái entity : add, update, delete
        /// </summary>
        public EntityState EntityState { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Ngày chỉnh sửa
        /// </summary>
        public DateTime ModifiedDate { get; set; }
        /// <summary>
        /// Người chỉnh sửa
        /// </summary>
        public string ModifiedBy { get; set; }
        #endregion

    }
}
