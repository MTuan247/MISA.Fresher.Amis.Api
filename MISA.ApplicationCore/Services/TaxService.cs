using MISA.ApplicationCore.Interfaces;
using MISA.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Services
{
    public class TaxService : BaseService<Tax>, ITaxService
    {
        #region Fields
        ITaxRepository _taxRepository;
        #endregion
        #region Constructor
        public TaxService(ITaxRepository taxRepository) : base(taxRepository)
        {
            _taxRepository = taxRepository;
        }

        #endregion
        #region Methods
        /// <summary>
        /// Hàm lấy dữ liệu đã được lọc
        /// </summary>
        /// <param name="taxFilter"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        /// Created by: NMTuan (23/09/2021)
        /// Modified by: NMTuan (23/09/2021)
        public EntitiesResponse<Tax> GetTaxsFiltered(string taxFilter, int year, int pageSize, int pageIndex)
        {
            var result = _taxRepository.GetTaxsFiltered(taxFilter, year, pageSize, pageIndex);

            return result;
        }
        /// <summary>
        /// Hàm lấy mã kỳ thuế mới
        /// </summary>
        /// <returns></returns>
        /// Created by: NMTuan (24/09/2021)
        /// Modified by: NMTuan (24/09/2021)
        public string GetNewCode()
        {
            return _taxRepository.GetNewTaxCode();
        }
        #endregion
    }
}
