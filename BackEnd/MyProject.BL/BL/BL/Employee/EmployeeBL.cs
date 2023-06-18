using MyProject.BL.Interface;
using MyProject.BL.Interface.BaseInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyProject.BL.BL;
using MyProject.Model.Model.Base;

namespace MyProject.BL.BL
{
    public class EmployeeBL : BaseBL, IEmployee
    {
        /// <summary>
        /// khởi tạo
        /// </summary>
        /// <param name="baseServiceCollection"></param>
        public EmployeeBL(BaseServiceCollection baseServiceCollection) : base(baseServiceCollection)
        {

        }

        /// <summary>
        /// Hàm lấy danh sách nhân viên
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceReponse> GetEmployee()
        {
            var res = new ServiceReponse();
            var sql = "SELECT * FROM employee;";
            var param = new Dictionary<string, object>()
            {
                {
                    "@param", "nhson"
                }
            };
            var data = QueryUsingCommandTextAsync<EmployeeBL>(sql, param);

            res.Data = data;
            return res;
        }
    }
}
