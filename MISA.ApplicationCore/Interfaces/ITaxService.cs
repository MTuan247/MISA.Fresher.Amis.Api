using MISA.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces
{
    public interface ITaxService : IBaseService<Tax>
    {
        /// <summary>
        /// Hàm lấy dữ liệu đã được lọc
        /// </summary>
        /// <param name="taxFilter"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        /// Created by: NMTuan (23/09/2021)
        /// Modified by: NMTuan (23/09/2021)
        public EntitiesResponse<Tax> GetTaxsFiltered(string taxFilter, int year, int pageSize, int pageIndex);
        /// <summary>
        /// Hàm lấy mã kỳ thuế mới
        /// </summary>
        /// <returns></returns>
        /// Created by: NMTuan (24/09/2021)
        /// Modified by: NMTuan (24/09/2021)
        public string GetNewCode();
    }
}
