using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Models
{
    public class EntitiesResponse<TEntity>
    {
        /// <summary>
        /// Số trang
        /// </summary>
        public int TotalPage { get; set; }
        /// <summary>
        /// Số bản ghi
        /// </summary>
        public int TotalRecord { get; set; }
        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        public IEnumerable<TEntity> Data { get; set; }
    }
}
