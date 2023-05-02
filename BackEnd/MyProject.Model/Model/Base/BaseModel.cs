using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Model.Model
{
    public class BaseModel
    {
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// Người sửa
        /// </summary>
        public string ModifyBy { get; set; }
    }
}
