using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Model.Attribute
{
    public class ConfigTableAttribute : System.Attribute
    {
        /// <summary>
        /// Tên bảng trong DB
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// Tên view 
        /// </summary>
        public string ViewName { get; set; }
        /// <summary>
        /// store lấy dữ liệu
        /// </summary>
        public string ProcGetPaging { get; set; }
        /// <summary>
        /// store lấy tổng số bản ghi
        /// </summary>
        public string ProcGetTotalPaging { get; set; }
        /// <summary>
        /// store thêm dữ liệu
        /// </summary>
        public string ProcInsert { get; set; }
        /// <summary>
        /// store update dữ liệu
        /// </summary>
        public string ProcUpdate { get; set; }
        /// <summary>
        /// trong model có trường đã xóa không
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// trong model có trường editversion không
        /// </summary>
        public bool IsEditVersion { get; set; }

        /// <summary>
        /// Khởi tạo
        /// </summary>
        public ConfigTableAttribute(string tableName, string viewName = "", string procGetPaging = "", string procGetTotalPaging = "", string procInsert = "", string procUpdate = "", bool isDeleted = false, bool isEditVersion = false )
        {
            TableName   = tableName; 
            ViewName = viewName; 
            ProcGetPaging = procGetPaging; 
            ProcGetTotalPaging = procGetTotalPaging; 
            ProcInsert = procInsert; 
            ProcUpdate = procUpdate;
            IsDeleted = isDeleted; 
            IsEditVersion = isEditVersion;
        }
    }
}
