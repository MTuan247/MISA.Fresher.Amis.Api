using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Interfaces;
using MISA.ApplicationCore.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.Amis.Api.Controllers
{
    public class DepartmentController : BaseEntityController<Department>
    {
        public DepartmentController(IBaseService<Department> baseService) : base(baseService)
        {
        }
    }
}
