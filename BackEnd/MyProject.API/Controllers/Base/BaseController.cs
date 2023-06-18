using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.BL.Interface.BaseInterface;
using MyProject.BL.Utility;
using MyProject.Model.Model;
using MyProject.Model.Model.Base;

namespace MyProject.API.Controllers.Base
{
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// khởi tạo
        /// </summary>
        public BaseController()
        {

        }

        private IBaseBL _BL;
        protected IBaseBL BL
        {
            get
            {
                if (_BL == null)
                {
                    throw new NotImplementedException("Chưa gán property 'BL' cho controller");
                }
                return _BL;
            }
            set { _BL = value; }
        } 
        
        private Type _currentModelType;
        protected Type currentModelType
        {
            get
            {
                if (_currentModelType == null)
                {
                    throw new NotImplementedException("Chưa gán property 'BL' cho controller");
                }
                return _currentModelType;
            }
            set { _currentModelType = value; }
        }


        [HttpPost("save")]
        public async Task<ServiceReponse> SaveData([FromBody] object data)
        {
            var res = new ServiceReponse();
            try
            {
                var baseModel = (BaseModel) ConverterUtility.Deserialize(data.ToString(), this.currentModelType);
                res.Data = await this.BL.SaveData(baseModel);
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
