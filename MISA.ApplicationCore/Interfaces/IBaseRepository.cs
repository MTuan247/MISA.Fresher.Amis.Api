using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// Lấy danh sách entities
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetEntities();
        /// <summary>
        /// Lấy entities theo phân trang
        /// </summary>
        /// <param name="EntityCode"></param>
        /// <param name="PageNumber"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        IEnumerable<TEntity> GetEntitiesFiltered(string EntityCode, int PageNumber, int PageSize);
        /// <summary>
        /// Lấy entity theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        TEntity GetEntityById(Guid entityId);
        /// <summary>
        /// Lấy entity theo code
        /// </summary>
        /// <param name="entityCode"></param>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        TEntity GetEntityByCode(string entityCode, TEntity entity);
        /// <summary>
        /// Hàm lấy entities theo property
        /// </summary>
        /// <param name="property"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetEntitiesByProperty(string property, TEntity entity);
        /// <summary>
        /// Thêm entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        int Add(TEntity entity);
        /// <summary>
        /// Cập nhật entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        int Update(TEntity entity);
        /// <summary>
        /// Xóa entity
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        int Delete(Guid entityId);

        int DeleteMultiple(Guid[] entityIds);
    }
}
