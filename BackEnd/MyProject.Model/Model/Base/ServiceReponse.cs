using MyProject.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Model.Model.Base
{
    public class ServiceReponse
    {
        /// <summary>
        /// Thành công hay thất bại
        /// </summary>
        public bool Success { get; set; } = true;
        /// <summary>
        /// Mã của respon
        /// </summary>
        public EnumServiceReponse Code { get; set; } = EnumServiceReponse.Success;
        /// <summary>
        /// mã lỗi phụ, phân biệt với mã mỗi chính và chi tiết hơn
        /// </summary>
        public int SubCode { get; set; }
        /// <summary>
        /// thông báo lỗi đối với người dùng
        /// </summary>
        public string UserMassage { get; set; }
        /// <summary>
        /// Thông báo lỗi của hệ thống
        /// </summary>
        public string SystemMessage { get; set; }
        /// <summary>
        /// Thời gian respon trả về
        /// </summary>
        public DateTime ServerTime { get; set; } = DateTime.Now;
        /// <summary>
        /// dữ liệu trả về
        /// </summary>
        public object Data { get; set; }

        #region ===== METHODS =====
        /// <summary>
        /// khi respon thành công
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ServiceReponse onSuccess(object data = null)
        {
            this.Data = data;
            return this;
        }
        /// <summary>
        /// Khi respon bị lỗi
        /// </summary>
        /// <param name="code"></param>
        /// <param name="subCode"></param>
        /// <param name="userMessage"></param>
        /// <param name="systemMessage"></param>
        /// <returns></returns>
        public ServiceReponse onError(EnumServiceReponse code = EnumServiceReponse.Error, int subCode = 0, string userMessage = "Có lỗi xảy ra", string systemMessage = "Có lỗi xảy ra")
        {
            this.Success = false;
            this.Code = code;
            this.SubCode = subCode;
            this.UserMassage = userMessage;
            this.SystemMessage = systemMessage;
            return this;
        }
        /// <summary>
        /// khi respon gặp exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public ServiceReponse onException(Exception ex)
        {
            this.Success = false;
            this.Code = EnumServiceReponse.ErrorSystem;
            this.SubCode = 0;
            this.UserMassage = "Có lỗi xảy ra";
            this.SystemMessage = ex.Message;
            return this;
        }
        #endregion
    }
}
