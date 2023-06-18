using MyProject.Model.Model;
using MyProject.Model.Model.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BL.Interface.BaseInterface
{
    public interface IBaseBL
    {
        /// <summary>
        /// Hàm xử lý lấy chuỗi connectionString từ file appsettings.json
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetConnectString(string key);

        /// <summary>
        /// Hàm xử lý lấy chuỗi connectionString từ file appsettings.json
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetAppSetting(string key, string valueDefault = null);

        #region ========== KHU VỰC KẾT NỐI DB ==========
        /// <summary>
        /// Hàm xử lý lấy kết nối DB theo connectionString
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        IDbConnection GetConnection(string connectionString);

        /// <summary>
        /// Hàm kết nối với DB
        /// </summary>
        /// <returns></returns>
        Task<IDbConnection> GetConnectionAsync();

        /// <summary>
        /// Hàm kết nối với DB
        /// </summary>
        /// <returns></returns>
        IDbConnection GetConnection();

        /// <summary>
        /// Hàm kết nối với DB khác
        /// </summary>
        /// <param name="server"></param>
        /// <param name="dataBase"></param>
        /// <param name="userID"></param>
        /// <param name="passWord"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        Task<IDbConnection> GetConnectionToServerAsync(string server, string dataBase, string userID, string passWord, int port = 3306);

        /// <summary>
        /// Hàm kết nối với DB khác
        /// </summary>
        /// <param name="server"></param>
        /// <param name="dataBase"></param>
        /// <param name="userID"></param>
        /// <param name="passWord"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        IDbConnection GetConnectionToServer(string server, string dataBase, string userID, string passWord, int port = 3306);
        #endregion

        #region ========== QUERY METHODS ===========
        #region ===== QueryUsingCommandText =====
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<List<T>> QueryUsingCommandTextAsync<T>(string commandText, Dictionary<string, object> dicParams);
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<List<T>> QueryUsingCommandTextAsync<T>(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams);
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<List<T>> QueryUsingCommandTextAsync<T>(IDbTransaction dbTransaction, string commandText, Dictionary<string, object> dicParams);
        #endregion

        #region ===== QueryUsingStoredProcedure =====
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<List<T>> QueryUsingStoredProceduceAsync<T>(string commandText, Dictionary<string, object> dicParams);
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<List<T>> QueryUsingStoredProceduceAsync<T>(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams);
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<List<T>> QueryUsingStoredProceduceAsync<T>(IDbTransaction dbTransaction, string commandText, Dictionary<string, object> dicParams);
        #endregion

        #region ===== QueryUsingCommandText Dynamic =====
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> QueryUsingCommandTextAsync(string commandText, Dictionary<string, object> dicParams);
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> QueryUsingCommandTextAsync(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams);
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> QueryUsingCommandTextAsync(IDbTransaction dbTransaction, string commandText, Dictionary<string, object> dicParams);
        #endregion

        #region ===== QueryUsingStoredProcedure Dynamic =====
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> QueryUsingStoredProceduceAsync(string commandText, Dictionary<string, object> dicParams);
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> QueryUsingStoredProceduceAsync(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams);
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> QueryUsingStoredProceduceAsync(IDbTransaction dbTransaction, string commandText, Dictionary<string, object> dicParams);
        #endregion

        #region ===== QueryUsingCommandText Multiple =====
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<List<List<object>>> QueryMultipleUsingCommandTextAsync(string commandText, Dictionary<string, object> dicParams, List<Type> types);
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<List<List<object>>> QueryMultipleUsingCommandTextAsync(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams, List<Type> types);
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<List<List<object>>> QueryMultipleUsingCommandTextAsync(IDbTransaction dbTransaction, string commandText, Dictionary<string, object> dicParams, List<Type> types);
        #endregion

        #region ===== QueryUsingStoredProcedure Multiple =====
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<List<List<object>>> QueryMultipleUsingStoredProceduceAsync(string commandText, Dictionary<string, object> dicParams, List<Type> types);
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<List<List<object>>> QueryMultipleUsingStoredProceduceAsync(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams, List<Type> types);
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<List<List<object>>> QueryMultipleUsingStoredProceduceAsync(IDbTransaction dbTransaction, string commandText, Dictionary<string, object> dicParams, List<Type> types);
        #endregion

        #region ===== QueryUsingCommandText Multiple Dynamic =====
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<List<List<object>>> QueryMultipleUsingCommandTextAsync(string commandText, Dictionary<string, object> dicParams);
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<List<List<object>>> QueryMultipleUsingCommandTextAsync(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams);
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<List<List<object>>> QueryMultipleUsingCommandTextAsync(IDbTransaction dbTransaction, string commandText, Dictionary<string, object> dicParams);
        #endregion

        #region ===== QueryUsingStoredProcedure Multiple Dynamic =====
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<List<List<object>>> QueryMultipleUsingStoredProceduceAsync(string commandText, Dictionary<string, object> dicParams);
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<List<List<object>>> QueryMultipleUsingStoredProceduceAsync(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams);
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<List<List<object>>> QueryMultipleUsingStoredProceduceAsync(IDbTransaction dbTransaction, string commandText, Dictionary<string, object> dicParams);
        #endregion

        #region ===== QueryUsingCommandText Multiple ListStringType =====
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<Dictionary<string, List<object>>> QueryMultipleUsingCommandTextAsync(string commandText, Dictionary<string, object> dicParams, List<string> types);
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<Dictionary<string, List<object>>> QueryMultipleUsingCommandTextAsync(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams, List<string> types);
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<Dictionary<string, List<object>>> QueryMultipleUsingCommandTextAsync(IDbTransaction dbTransaction, string commandText, Dictionary<string, object> dicParams, List<string> types);
        #endregion

        #region ===== QueryUsingStoredProcedure Multiple ListStringType =====
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<Dictionary<string, List<object>>> QueryMultipleUsingStoredProceduceAsync(string commandText, Dictionary<string, object> dicParams, List<string> types);
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<Dictionary<string, List<object>>> QueryMultipleUsingStoredProceduceAsync(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams, List<string> types);
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<Dictionary<string, List<object>>> QueryMultipleUsingStoredProceduceAsync(IDbTransaction dbTransaction, string commandText, Dictionary<string, object> dicParams, List<string> types);
        #endregion
        #endregion

        #region ========== EXECUTE METHODS =========
        #region ===== ExecuteUsingCommandText =====
        /// <summary>
        /// Hàm chạy command Text trả về thành công/thất bại
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<bool> ExecuteUsingCommandText(string commandText, Dictionary<string, object> dicParams);
        /// <summary>
        /// Hàm chạy command Text trả về thành công/thất bại
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<bool> ExecuteUsingCommandText(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams);
        /// <summary>
        /// Hàm chạy command Text trả về thành công/thất bại
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<bool> ExecuteUsingCommandText(IDbTransaction dbTransaction, string commandText, Dictionary<string, object> dicParams);
        #endregion

        #region ===== ExecuteUsingProcedure =====
        /// <summary>
        /// Hàm chạy command Text trả về thành công/thất bại
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<bool> ExecuteUsingProcedure(string commandText, Dictionary<string, object> dicParams);
        /// <summary>
        /// Hàm chạy command Text trả về thành công/thất bại
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<bool> ExecuteUsingProcedure(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams);
        /// <summary>
        /// Hàm chạy command Text trả về thành công/thất bại
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<bool> ExecuteUsingProcedure(IDbTransaction dbTransaction, string commandText, Dictionary<string, object> dicParams);
        #endregion

        #region ===== ExecuteScalarUsingCommandText =====
        /// <summary>
        /// Hàm chạy command Text trả về 1 giá trị theo model
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<T> ExecuteScalarUsingCommandText<T>(string commandText, Dictionary<string, object> dicParams);
        /// <summary>
        /// Hàm chạy command Text trả về thành công/thất bại
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<T> ExecuteScalarUsingCommandText<T>(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams);
        /// <summary>
        /// Hàm chạy command Text trả về thành công/thất bại
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<T> ExecuteScalarUsingCommandText<T>(IDbTransaction dbTransaction, string commandText, Dictionary<string, object> dicParams);
        #endregion

        #region ===== ExecuteScalarUsingProcedure =====
        /// <summary>
        /// Hàm chạy command Text trả về 1 giá trị theo model
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<T> ExecuteScalarUsingProcedure<T>(string commandText, Dictionary<string, object> dicParams);
        /// <summary>
        /// Hàm chạy command Text trả về thành công/thất bại
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<T> ExecuteScalarUsingProcedure<T>(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams);
        /// <summary>
        /// Hàm chạy command Text trả về thành công/thất bại
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        Task<T> ExecuteScalarUsingProcedure<T>(IDbTransaction dbTransaction, string commandText, Dictionary<string, object> dicParams);
        #endregion
        #endregion

        /// <summary>
        /// Hàm thêm dữ liệu
        /// </summary>
        /// <param name="baseModel"></param>
        /// <returns></returns>
        Task<ServiceReponse> SaveData(BaseModel baseModel);
    }
}
