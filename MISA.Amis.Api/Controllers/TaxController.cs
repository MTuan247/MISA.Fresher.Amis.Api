using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Interfaces;
using MISA.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Amis.Api.Controllers
{
    public class TaxController : BaseEntityController<Tax>
    {
        #region Fields
        ITaxService _taxService;
        #endregion
        #region Constructor
        public TaxController(ITaxService taxService) : base(taxService)
        {
            _taxService = taxService;
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
        [HttpGet("taxFilter")]
        public IActionResult GetEmployeesFiltered(string taxFilter, int year, int pageSize, int pageNumber)
        {
            try
            {
                var result = _taxService.GetTaxsFiltered(taxFilter, year, pageSize, pageNumber);
                if (result.Data.Count() > 0 && result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(204);
                }
            }
            catch (Exception ex)
            {
                serviceResult.ExceptionHandle(ex);
                return StatusCode(500, serviceResult);
            }

        }
        
        /// <summary>
        /// Method get lấy mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        [HttpGet("newCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                var code = _taxService.GetNewCode();
                return Ok(code);
            }
            catch (Exception ex)
            {
                serviceResult.ExceptionHandle(ex);
                return StatusCode(500, serviceResult);
            }

        }
        #endregion
    }
}
