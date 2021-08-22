using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Interfaces;
using MISA.ApplicationCore.Models;
using MISA.ApplicationCore.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.Amis.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseEntityController<TEntity> : ControllerBase
    {
        #region Fields
        protected IBaseService<TEntity> _baseService;
        protected ServiceResult serviceResult;
        #endregion

        #region Constructors
        public BaseEntityController(IBaseService<TEntity> baseService)
        {
            _baseService = baseService;
            serviceResult = new ServiceResult();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method get lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        [HttpGet]
        public virtual IActionResult Get()
        {
            try
            {
                var entities = _baseService.GetEntities();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                serviceResult.ExceptionHandle(ex);
                return StatusCode(500, serviceResult);
            }

        }

        /// <summary>
        /// Method get lấy dữ liệu theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        [HttpGet("{id}")]
        public virtual IActionResult Get(Guid id)
        {
            try
            {
                var entity = _baseService.GetEntityById(id);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                serviceResult.ExceptionHandle(ex);
                return StatusCode(500, serviceResult);
            }
        }

        /// <summary>
        /// Method post thêm dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        [HttpPost]
        public virtual IActionResult Post([FromBody] TEntity entity)
        {
            try
            {
                var res = _baseService.Add(entity);
                return StatusCode(res.StatusCode, res);
            }
            catch (Exception ex)
            {
                serviceResult.ExceptionHandle(ex);
                return StatusCode(500, serviceResult);
            }
        }

        /// <summary>
        /// Method put sửa dữ liệu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        [HttpPut("{id}")]
        public virtual IActionResult Put(Guid id, [FromBody] TEntity entity)
        {
            try
            {
                var res = _baseService.Update(id, entity);
                return StatusCode(res.StatusCode, res);
            }
            catch (Exception ex)
            {
                serviceResult.ExceptionHandle(ex);
                return StatusCode(500, serviceResult);
            }

        }

        /// <summary>
        /// Method delete xóa dữ liệu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Created by: NMTuan (02/08/2021)
        /// Modified by: NMTuan (02/08/2021)
        [HttpDelete("{id}")]
        public virtual IActionResult Delete(Guid id)
        {
            try
            {
                var result = _baseService.Delete(id);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {

                serviceResult.ExceptionHandle(ex);
                return StatusCode(500, serviceResult);
            }

        }

        [HttpDelete("multiple")]
        public virtual IActionResult DeleteMultiple([FromBody] Guid[] entityIds)
        {
            try
            {
                var result = _baseService.DeleteMultiple(entityIds);
                return StatusCode(result.StatusCode, result);
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
