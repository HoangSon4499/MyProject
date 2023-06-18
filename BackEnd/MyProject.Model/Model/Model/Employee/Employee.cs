using MyProject.Model.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Model.Model
{
    [ConfigTable(tableName: "employee", isDeleted: true)]
    public class Employee : BaseModel
    {
        [Key]
        public int EmployeeID { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Họ tên
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Mã số thuế
        /// </summary>
        public string TaxCode { get; set; }
        /// <summary>
        /// Mã giới tính
        /// </summary>
        public int? GenderID { get; set; }
        /// <summary>
        /// tên giới tính
        /// </summary>
        public string GenderName { get; set; }
        /// <summary>
        /// ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// mail
        /// </summary>
        public string Email { get; set; }

    }
}
