using MISA.ApplicationCore.Interfaces;
using MISA.ApplicationCore.Models;
using MISA.ApplicationCore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        #region Fields
        IBaseRepository<TEntity> _baseRepository;
        protected ServiceResult serviceResult;
        #endregion

        #region Constructors
        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            serviceResult = new ServiceResult();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy toàn bộ bản ghi
        /// </summary>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        public virtual IEnumerable<TEntity> GetEntities()
        {
            return _baseRepository.GetEntities();
        }

        /// <summary>
        /// Lấy thông tin bản ghi theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        public virtual TEntity GetEntityById(Guid entityId)
        {

            return _baseRepository.GetEntityById(entityId);

        }

        /// <summary>
        /// Thêm bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        public virtual ServiceResult Add(TEntity entity)
        {
            entity.EntityState = Enums.EntityState.AddNew;

            if (!Validate(entity))
            {
                return serviceResult;
            }
            var res = _baseRepository.Add(entity);

            if (res > 0)
            {
                serviceResult.StatusCode = 201;
                serviceResult.UserMsg = Properties.Resources.CreateSuccess;
                serviceResult.Success = true;
            }
            else
            {
                serviceResult.StatusCode = 500;
                serviceResult.UserMsg = Properties.Resources.ExceptionError;
                serviceResult.Success = false;
            }
            //serviceResult.StatusCode = 201;
            //serviceResult.UserMsg = Properties.Resources.CreateSuccess;
            //serviceResult.Success = true;
            return serviceResult;
        }

        /// <summary>
        /// Hàm update bản ghi
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        public virtual ServiceResult Update(Guid id, TEntity entity)
        {
            entity.EntityState = Enums.EntityState.Update;

            if (!Validate(entity))
            {
                return serviceResult;
            }
            var res = _baseRepository.Update(entity);

            if (res > 0)
            {
                serviceResult.StatusCode = 200;
                serviceResult.Success = true;
                serviceResult.UserMsg = Properties.Resources.UpdateSuccess;
            }
            else
            {
                serviceResult.StatusCode = 200;
                serviceResult.UserMsg = Properties.Resources.ExceptionError;
                serviceResult.Success = false;
            }
            return serviceResult;
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        public virtual ServiceResult Delete(Guid entityId)
        {
            var res = _baseRepository.Delete(entityId);
            if (res > 0)
            {
                serviceResult.StatusCode = 200;
                serviceResult.Success = true;
                serviceResult.UserMsg = Properties.Resources.DeleteSuccess;
            }
            else
            {
                serviceResult.StatusCode = 200;
                serviceResult.Success = false;
                serviceResult.UserMsg = Properties.Resources.DeleteError;
            }
            return serviceResult;
        }

        public ServiceResult DeleteMultiple(Guid[] entityIds)
        {
            var res = _baseRepository.DeleteMultiple(entityIds);

            if (res > 0)
            {
                serviceResult.StatusCode = 200;
                serviceResult.Success = true;
                serviceResult.UserMsg = Properties.Resources.DeleteSuccess;
            }
            else
            {
                serviceResult.StatusCode = 500;
                serviceResult.Success = false;
                serviceResult.UserMsg = Properties.Resources.DeleteError;
            }

            return serviceResult;
        }

        /// <summary>
        /// Hàm kiểm tra trung code
        /// </summary>
        /// <param name="entityCode"></param>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        public bool CheckDuplicateCode(string entityCode, TEntity entity)
        {
            var isDuplicate = false;

            var res = _baseRepository.GetEntityByCode(entityCode, entity);

            if (res != null)
            {
                isDuplicate = true;
            }

            return isDuplicate;
        }

        /// <summary>
        /// Hàm check string có phải guid
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        public bool IsGuid(string value)
        {
            Guid x;
            return Guid.TryParse(value, out x);
        }

        /// <summary>
        /// Hàm validate
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private bool Validate(TEntity entity)
        {
            var isValidate = true;
            // Đọc các Property
            var properties = entity.GetType().GetProperties();

            foreach (var property in properties)
            {
                //Lấy các trường bắt buộc
                var propRequired = property.GetCustomAttributes(typeof(MISARequired), true);
                //Lấy display name của các trường
                var propDisplayName = property.GetCustomAttributes(typeof(MISAPropertyName), true);
                //Lấy các trường không được trùng
                var propUnique = property.GetCustomAttributes(typeof(MISAUnique), true);
                // Lấy các truòng giới hạn độ dài
                var propMaxLength = property.GetCustomAttributes(typeof(MISAMaxLength), true);
                // Lấy giá trị
                var propertyValue = property.GetValue(entity);

                //Lấy tên trường
                var propName = property.Name;
                var displayName = string.Empty;

                if (propDisplayName.Length > 0)
                {
                    displayName = (propDisplayName[0] as MISAPropertyName).Name;
                }
                else
                {
                    displayName = propName;
                }

                //Kiểm tra trường có attribute required
                if (propRequired.Length > 0)
                {

                    // Kiểm tra trường có trống
                    if (propertyValue == null || string.IsNullOrEmpty(propertyValue.ToString()))
                    {
                        serviceResult.Success = false;
                        serviceResult.StatusCode = 200;
                        serviceResult.UserMsg = displayName + Properties.Resources.ValidateError_FieldEmpty;
                        serviceResult.Data = propName;
                        isValidate = false;
                        return false;
                    }
                } 
                else
                {
                    if (propertyValue == null || propertyValue.ToString() == string.Empty)
                    {
                        return true;
                    }
                }

                //Kiểm tra trường không trùng
                if (propUnique.Length > 0)
                {
                    //Lấy bản ghi từ database từ các trường
                    var entitiesDuplicate = _baseRepository.GetEntitiesByProperty(propName, entity);
                    //Tồn tại bản ghi trả false
                    if (entitiesDuplicate.Count() > 0)
                    {
                        serviceResult.Success = false;
                        serviceResult.StatusCode = 200;
                        serviceResult.UserMsg = string.Format(Properties.Resources.ValidateError_FieldDuplicate, displayName, propertyValue);
                        serviceResult.Data = propName;
                        return false;
                    }
                }

                //Kiểm tra trường giới hạn độ dài
                if (propMaxLength.Length > 0 && property.GetValue(entity) != null)
                {
                    //Lấy giá trị
                    var propertyMaxLength = (propMaxLength[0] as MISAMaxLength).Max;
                    //Check email
                    if (propertyValue.ToString().Length > propertyMaxLength)
                    {
                        serviceResult.Success = false;
                        serviceResult.StatusCode = 200;
                        serviceResult.UserMsg = string.Format(Properties.Resources.ValidateError_FieldMaxLength, displayName, propertyMaxLength);
                        serviceResult.Data = propName;
                        return false;
                    }
                }

            }

            if (!ValidateCustom())
            {
                isValidate = false;
            }

            return isValidate;

        }

        /// <summary>
        /// Hàm validate custom
        /// </summary>
        /// <returns></returns>
        public virtual bool ValidateCustom()
        {
            return true;
        }

        #endregion

    }
}
