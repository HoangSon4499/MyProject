using Microsoft.AspNetCore.Mvc;
using MyProject.API.Controllers.Base;
using MyProject.BL.Interface;
using MyProject.Model.Model;
using MyProject.Model.Model.Base;

namespace MyProject.API.Controllers
{
    public class EmployeeController : BaseController
    {
        public EmployeeController(IEmployee employee)
        {
            this.BL = employee;
            this.currentModelType = typeof(Employee);
        }
        [HttpGet]
        public async Task<ServiceReponse> GetEmployee()
        {
            var res = new ServiceReponse();
            try
            {
                res = await (this.BL as IEmployee).GetEmployee();
            }
            catch (Exception ex)
            {
                res.onException(ex);
                throw;
            }

            return res;
        }
    }
}
