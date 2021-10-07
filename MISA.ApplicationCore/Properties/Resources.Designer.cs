﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MISA.ApplicationCore.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MISA.ApplicationCore.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Thêm mới dữ liệu thành công.
        /// </summary>
        public static string CreateSuccess {
            get {
                return ResourceManager.GetString("CreateSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Xóa thất bại!.
        /// </summary>
        public static string DeleteError {
            get {
                return ResourceManager.GetString("DeleteError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Xóa thành công!.
        /// </summary>
        public static string DeleteSuccess {
            get {
                return ResourceManager.GetString("DeleteSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Có lỗi xảy ra! Vui lòng liên hệ Misa.
        /// </summary>
        public static string ExceptionError {
            get {
                return ResourceManager.GetString("ExceptionError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nữ.
        /// </summary>
        public static string Female {
            get {
                return ResourceManager.GetString("Female", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nam.
        /// </summary>
        public static string Male {
            get {
                return ResourceManager.GetString("Male", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Không có dữ liệu trả về!.
        /// </summary>
        public static string NoContent {
            get {
                return ResourceManager.GetString("NoContent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Khác.
        /// </summary>
        public static string Other {
            get {
                return ResourceManager.GetString("Other", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cập nhật thông tin thành công.
        /// </summary>
        public static string UpdateSuccess {
            get {
                return ResourceManager.GetString("UpdateSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mã nhân viên đã tồn tại!.
        /// </summary>
        public static string ValidateError_EmployeeCodeDuplicate {
            get {
                return ResourceManager.GetString("ValidateError_EmployeeCodeDuplicate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mã nhân viên không được phép để trống!.
        /// </summary>
        public static string ValidateError_EmployeeCodeEmpty {
            get {
                return ResourceManager.GetString("ValidateError_EmployeeCodeEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ID nhân viên không trùng khớp.
        /// </summary>
        public static string ValidateError_EmployeeIdNotMatch {
            get {
                return ResourceManager.GetString("ValidateError_EmployeeIdNotMatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} &amp;lt;{1}&amp;gt; đã tồn tại trên hệ thống, vui lòng kiểm tra lại..
        /// </summary>
        public static string ValidateError_FieldDuplicate {
            get {
                return ResourceManager.GetString("ValidateError_FieldDuplicate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to  không được phép để trống!.
        /// </summary>
        public static string ValidateError_FieldEmpty {
            get {
                return ResourceManager.GetString("ValidateError_FieldEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} không được vượt quá {1} kí tự!.
        /// </summary>
        public static string ValidateError_FieldMaxLength {
            get {
                return ResourceManager.GetString("ValidateError_FieldMaxLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} không đúng định dạng!.
        /// </summary>
        public static string ValidateError_FormatValidate {
            get {
                return ResourceManager.GetString("ValidateError_FormatValidate", resourceCulture);
            }
        }
    }
}
