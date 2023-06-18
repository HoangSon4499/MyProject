using MyProject.BL.Interface;
using MyProject.BL.Interface.BaseInterface;
using MyProject.Model.Model;
using MyProject.Model.Model.Base;
using System;
using System.Collections.Generic;
using System.Configuration.Internal;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BL.BL
{
    public partial class BaseBL : IBaseBL
    {
        private readonly BaseServiceCollection _services;
        /// <summary>
        /// khởi tạo
        /// </summary>
        /// <param name="baseServiceCollection"></param>
        public BaseBL(BaseServiceCollection baseServiceCollection)
        {
            _services = baseServiceCollection;
        }

        protected IConfigService _configService { get => _services.GetConfigService();}

        protected IDataBaseService _dataBaseService { get => _services.GetDataBaseService();}

        #region =========== METHODS SAVEDATA ==========
        /// <summary>
        /// Hàm xử lý lưu dữ liệu
        /// </summary>
        /// <param name="baseModel"></param>
        /// <returns></returns>
        public async Task<ServiceReponse> SaveData(BaseModel baseModel)
        {
            ServiceReponse serviceReponse = new ServiceReponse();
            IDbConnection connection = null;
            IDbTransaction transaction = null;
            try
            {
                // Validate dữ liệu
                var inValid = ValidateSaveData(baseModel);
                if (!inValid)
                {
                    serviceReponse.Success = false;
                    return serviceReponse;
                }

                // xử lý dữ liệu trước khi lưu
                BeforeSaveData(baseModel);

                connection = this.GetConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                // thực hiện save dữ liệu và trả về save thành công hay thất bại
                var result = DoSaveData(baseModel, transaction);
                // sau khi lưu thành công thì xử lý nghiẹp vụ tiếp theo nhưng vẫn giữ transaction
                if (result)
                {
                    AfterSaveData(baseModel, transaction);
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                    serviceReponse.Success = false;
                }
            }
            catch (Exception)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                throw;
            }
            finally
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            if (serviceReponse.Success)
            {
                AfterSaveDataCommit(baseModel, transaction);
            }

            return serviceReponse;
        }

        /// <summary>
        /// Hàm xử lý validate dữ liệu trước khi lưu
        /// </summary>
        /// <param name="baseModel"></param>
        /// <returns></returns>
        public virtual bool ValidateSaveData(BaseModel baseModel)
        {
            return true;
        }
        /// <summary>
        /// Hàm xử lý trước khi lưu
        /// </summary>
        /// <param name="baseModel"></param>
        public virtual void BeforeSaveData(BaseModel baseModel)
        {
            if (baseModel.State == Model.Enum.EnumState.Insert || baseModel.State == Model.Enum.EnumState.Duplicate)
            {
                baseModel.CreatedBy = "admin";
                baseModel.CreatedDate = DateTime.Now;
            }

            baseModel.ModifyBy = "admin";
            baseModel.ModifyDate = DateTime.Now;
        }
        /// <summary>
        /// Hàm thực hiện save Data
        /// </summary>
        /// <param name="baseModel"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public virtual bool DoSaveData(BaseModel baseModel, IDbTransaction transaction)
        {
            return true;
        }
        /// <summary>
        /// Hàm xử lý sau khi lưu nhưng vẫn giữ transaction
        /// </summary>
        /// <param name="baseModel"></param>
        public virtual void AfterSaveData(BaseModel baseModel, IDbTransaction transaction)
        {
            // override lại để xử lý nghiệp vụ ở đây
        }
        /// <summary>
        /// Hàm xử lý sau khi lưu thành công và không có transaction
        /// </summary>
        /// <param name="baseModel"></param>
        public virtual void AfterSaveDataCommit(BaseModel baseModel, IDbTransaction transaction)
        {
            // override lại để xử lý nghiệp vụ ở đây
        }
        #endregion
    }
}
