using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Model.Enum
{
    public enum EnumServiceReponse
    {
        /// <summary>
        /// thành công
        /// </summary>
        Success = 0,
        /// <summary>
        /// không có quyền
        /// </summary>
        NotPermission = 1,
        /// <summary>
        /// lỗi hệ thống
        /// </summary>
        ErrorSystem = 2,
        /// <summary>
        /// Không có dữ liệu
        /// </summary>
        NotFound = 3,
        /// <summary>
        /// Gặp lỗi
        /// </summary>
        Error = 99
    }
}
