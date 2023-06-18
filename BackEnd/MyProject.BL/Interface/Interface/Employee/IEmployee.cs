using MyProject.BL.Interface.BaseInterface;
using MyProject.Model.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BL.Interface
{
    public interface IEmployee : IBaseBL
    {
        /// <summary>
        /// Hàm thực hiện lấy danh sách nhân viên
        /// </summary>
        /// <returns></returns>
        Task<ServiceReponse> GetEmployee();
    }
}
