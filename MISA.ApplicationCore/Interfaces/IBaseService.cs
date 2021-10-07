using MISA.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces
{
    public interface IBaseService<TEntity>
    {
        /// <summary>
        /// Hàm lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        IEnumerable<TEntity> GetEntities();
        /// <summary>
        /// Hàm lấy dữ liệu theo Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        TEntity GetEntityById(Guid entityId);
        /// <summary>
        /// Hàm lấy bản ghi theo danh sách các id
        /// </summary>
        /// <param name="entityIds"></param>
        /// <returns></returns>
        /// CreatedBy: NMTuan (21/09/2021)
        /// ModifiedBy: NMTuan (21/09/2021)
        IEnumerable<TEntity> GetEntitiesByIds(Guid[] entitiesIds);
        /// <summary>
        /// Hàm thêm dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        ServiceResult Add(TEntity entity);
        /// <summary>
        /// Hàm sửa dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        ServiceResult Update(Guid id, TEntity entity);
        /// <summary>
        /// Hàm xóa dữ liệu
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        ServiceResult Delete(Guid entityId);
        /// <summary>
        /// Hàm xóa nhiều bản ghi
        /// </summary>
        /// <param name="entityIds"></param>
        /// <returns></returns>
        /// Created by: NMTuan (17/08/2021)
        /// Modified by: NMTuan (17/08/2021)
        ServiceResult DeleteMultiple(Guid[] entityIds);
    }
}
