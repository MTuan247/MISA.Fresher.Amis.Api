using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using System.Data;

using MISA.ApplicationCore.Models;
using MISA.ApplicationCore.Interfaces;
using MISA.Infrastructure;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Enums;

namespace MISA.Amis.Api.Controllers
{
    public class EmployeeController : BaseEntityController<Employee>
    {
        #region Fields
        IEmployeeService _employeeService;
        #endregion

        #region Constructors
        public EmployeeController(IEmployeeService employeeService) : base(employeeService)
        {
            _employeeService = employeeService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method get lấy danh sách nhân viên theo tiêu chí
        /// </summary>
        /// <param name="employeeFilter"></param>
        /// <param name="departmentId"></param>
        /// <param name="positionId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        [HttpGet("employeeFilter")]
        public IActionResult GetEmployeesFiltered(string employeeFilter, Guid? departmentId, int pageSize, int pageNumber)
        {
            try
            {
                var result = _employeeService.GetEntitiesFiltered(employeeFilter, departmentId, pageSize, pageNumber);
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
        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                var code = _employeeService.GetNewCode();
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
