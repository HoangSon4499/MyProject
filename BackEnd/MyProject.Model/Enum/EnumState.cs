using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Model.Enum
{
    public enum EnumState
    {   
        /// <summary>
        /// Không có gì
        /// </summary>
        None = 0,
        /// <summary>
        /// Thêm
        /// </summary>
        Insert = 1, 
        /// <summary>
        /// Sửa
        /// </summary>
        Update = 2,
        /// <summary>
        /// Xóa
        /// </summary>
        Delete = 3,
        /// <summary>
        /// Nhân bản
        /// </summary>
        Duplicate = 4,
        /// <summary>
        /// Đồng bộ
        /// </summary>
        Sync = 5,
        /// <summary>
        /// Khôi phục
        /// </summary>
        Restore = 6
    }
}
