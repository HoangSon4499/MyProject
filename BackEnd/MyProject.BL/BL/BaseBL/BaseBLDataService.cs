using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BL.BL
{
    public partial class BaseBL
    {
        #region ========== KHU VỰC KẾT NỐI DB ==========
        /// <summary>
        /// Hàm xử lý lấy kết nối DB theo connectionString
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public IDbConnection GetConnection(string connectionString)
        {
            return _dataBaseService.GetConnection(connectionString);
        }

        /// <summary>
        /// Hàm kết nối với DB
        /// </summary>
        /// <returns></returns>
        public async Task<IDbConnection> GetConnectionAsync()
        {
            return await _dataBaseService.GetConnectionAsync();
        }

        /// <summary>
        /// Hàm kết nối với DB
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            return _dataBaseService.GetConnection();
        }

        /// <summary>
        /// Hàm kết nối với DB khác
        /// </summary>
        /// <param name="server"></param>
        /// <param name="dataBase"></param>
        /// <param name="userID"></param>
        /// <param name="passWord"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public async Task<IDbConnection> GetConnectionToServerAsync(string server, string dataBase, string userID, string passWord, int port = 3306)
        {
            return await _dataBaseService.GetConnectionToServerAsync(server, dataBase, userID, passWord, port);
        }

        /// <summary>
        /// Hàm kết nối với DB khác
        /// </summary>
        /// <param name="server"></param>
        /// <param name="dataBase"></param>
        /// <param name="userID"></param>
        /// <param name="passWord"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public IDbConnection GetConnectionToServer(string server, string dataBase, string userID, string passWord, int port = 3306)
        {
            return _dataBaseService.GetConnectionToServer(server, dataBase, userID, passWord, port);
        }
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
        public async Task<List<T>> QueryUsingCommandTextAsync<T>(string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.QueryUsingCommandTextAsync<T>(commandText, dicParams);
        }
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<List<T>> QueryUsingCommandTextAsync<T>(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.QueryUsingCommandTextAsync<T>(dbConnection, commandText, dicParams);
        }
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<List<T>> QueryUsingCommandTextAsync<T>(IDbTransaction dbTransaction, string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.QueryUsingCommandTextAsync<T>(dbTransaction, commandText, dicParams);
        }
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
        public async Task<List<T>> QueryUsingStoredProceduceAsync<T>(string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.QueryUsingStoredProceduceAsync<T>(commandText, dicParams);
        }
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<List<T>> QueryUsingStoredProceduceAsync<T>(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.QueryUsingStoredProceduceAsync<T>(dbConnection, commandText, dicParams);
        }
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<List<T>> QueryUsingStoredProceduceAsync<T>(IDbTransaction dbTransaction, string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.QueryUsingStoredProceduceAsync<T>(dbTransaction, commandText, dicParams);
        }
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
        public async Task<IEnumerable<dynamic>> QueryUsingCommandTextAsync(string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.QueryUsingCommandTextAsync(commandText, dicParams);
        }
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<IEnumerable<dynamic>> QueryUsingCommandTextAsync(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.QueryUsingCommandTextAsync(dbConnection, commandText, dicParams);
        }
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<IEnumerable<dynamic>> QueryUsingCommandTextAsync(IDbTransaction dbTransaction, string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.QueryUsingCommandTextAsync(dbTransaction, commandText, dicParams);
        }
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
        public async Task<IEnumerable<dynamic>> QueryUsingStoredProceduceAsync(string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.QueryUsingStoredProceduceAsync(commandText, dicParams);
        }
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<IEnumerable<dynamic>> QueryUsingStoredProceduceAsync(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.QueryUsingStoredProceduceAsync(dbConnection, commandText, dicParams);
        }
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<IEnumerable<dynamic>> QueryUsingStoredProceduceAsync(IDbTransaction dbTransaction, string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.QueryUsingStoredProceduceAsync(dbTransaction, commandText, dicParams);
        }
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
        public async Task<List<List<object>>> QueryMultipleUsingCommandTextAsync(string commandText, Dictionary<string, object> dicParams, List<Type> types)
        {
            return await _dataBaseService.QueryMultipleUsingCommandTextAsync(commandText.Trim(), dicParams, types);
        }
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<List<List<object>>> QueryMultipleUsingCommandTextAsync(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams, List<Type> types)
        {
            return await _dataBaseService.QueryMultipleUsingCommandTextAsync(dbConnection, commandText, dicParams, types);
        }
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<List<List<object>>> QueryMultipleUsingCommandTextAsync(IDbTransaction dbTransaction, string commandText, Dictionary<string, object> dicParams, List<Type> types)
        {
            return await _dataBaseService.QueryMultipleUsingCommandTextAsync(dbTransaction, commandText, dicParams, types);
        }
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
        public async Task<List<List<object>>> QueryMultipleUsingStoredProceduceAsync(string commandText, Dictionary<string, object> dicParams, List<Type> types)
        {
            return await _dataBaseService.QueryMultipleUsingStoredProceduceAsync(commandText, dicParams, types);
        }
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<List<List<object>>> QueryMultipleUsingStoredProceduceAsync(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams, List<Type> types)
        {
            return await _dataBaseService.QueryMultipleUsingStoredProceduceAsync(dbConnection, commandText, dicParams, types);
        }
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<List<List<object>>> QueryMultipleUsingStoredProceduceAsync(IDbTransaction dbTransaction, string commandText, Dictionary<string, object> dicParams, List<Type> types)
        {
            return await _dataBaseService.QueryMultipleUsingStoredProceduceAsync(dbTransaction, commandText, dicParams, types);
        }
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
        public async Task<List<List<object>>> QueryMultipleUsingCommandTextAsync(string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.QueryMultipleUsingCommandTextAsync(commandText, dicParams);
        }
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<List<List<object>>> QueryMultipleUsingCommandTextAsync(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.QueryMultipleUsingCommandTextAsync(dbConnection, commandText, dicParams);
        }
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<List<List<object>>> QueryMultipleUsingCommandTextAsync(IDbTransaction dbTransaction, string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.QueryMultipleUsingCommandTextAsync(dbTransaction, commandText, dicParams);
        }
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
        public async Task<List<List<object>>> QueryMultipleUsingStoredProceduceAsync(string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.QueryMultipleUsingStoredProceduceAsync(commandText, dicParams);
        }
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<List<List<object>>> QueryMultipleUsingStoredProceduceAsync(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.QueryMultipleUsingStoredProceduceAsync(dbConnection, commandText, dicParams);
        }
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<List<List<object>>> QueryMultipleUsingStoredProceduceAsync(IDbTransaction dbTransaction, string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.QueryMultipleUsingStoredProceduceAsync(dbTransaction, commandText, dicParams);
        }
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
        public async Task<Dictionary<string, List<object>>> QueryMultipleUsingCommandTextAsync(string commandText, Dictionary<string, object> dicParams, List<string> types)
        {
            return await _dataBaseService.QueryMultipleUsingCommandTextAsync(commandText, dicParams, types);
        }
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, List<object>>> QueryMultipleUsingCommandTextAsync(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams, List<string> types)
        {
            return await _dataBaseService.QueryMultipleUsingCommandTextAsync(dbConnection, commandText, dicParams, types);
        }
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, List<object>>> QueryMultipleUsingCommandTextAsync(IDbTransaction dbTransaction, string commandText, Dictionary<string, object> dicParams, List<string> types)
        {
            return await _dataBaseService.QueryMultipleUsingCommandTextAsync(dbTransaction, commandText, dicParams, types);
        }
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
        public async Task<Dictionary<string, List<object>>> QueryMultipleUsingStoredProceduceAsync(string commandText, Dictionary<string, object> dicParams, List<string> types)
        {
            return await _dataBaseService.QueryMultipleUsingStoredProceduceAsync(commandText, dicParams, types);
        }
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, List<object>>> QueryMultipleUsingStoredProceduceAsync(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams, List<string> types)
        {
            return await _dataBaseService.QueryMultipleUsingStoredProceduceAsync(dbConnection, commandText, dicParams, types);
        }
        /// <summary>
        /// Hàm xử lý query Using commandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, List<object>>> QueryMultipleUsingStoredProceduceAsync(IDbTransaction dbTransaction, string commandText, Dictionary<string, object> dicParams, List<string> types)
        {
            return await _dataBaseService.QueryMultipleUsingStoredProceduceAsync(dbTransaction, commandText, dicParams, types);
        }
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
        public async Task<bool> ExecuteUsingCommandText(string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.ExecuteUsingCommandText(commandText, dicParams);
        }
        /// <summary>
        /// Hàm chạy command Text trả về thành công/thất bại
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<bool> ExecuteUsingCommandText(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.ExecuteUsingCommandText(dbConnection, commandText, dicParams);
        }
        /// <summary>
        /// Hàm chạy command Text trả về thành công/thất bại
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<bool> ExecuteUsingCommandText(IDbTransaction dbTransaction, string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.ExecuteUsingCommandText(dbTransaction, commandText, dicParams);
        }
        #endregion

        #region ===== ExecuteUsingProcedure =====
        /// <summary>
        /// Hàm chạy command Text trả về thành công/thất bại
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<bool> ExecuteUsingProcedure(string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.ExecuteUsingProcedure(commandText, dicParams);
        }
        /// <summary>
        /// Hàm chạy command Text trả về thành công/thất bại
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<bool> ExecuteUsingProcedure(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.ExecuteUsingProcedure(dbConnection, commandText, dicParams);
        }
        /// <summary>
        /// Hàm chạy command Text trả về thành công/thất bại
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<bool> ExecuteUsingProcedure(IDbTransaction dbTransaction, string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.ExecuteUsingProcedure(dbTransaction, commandText, dicParams);
        }
        #endregion

        #region ===== ExecuteScalarUsingCommandText =====
        /// <summary>
        /// Hàm chạy command Text trả về 1 giá trị theo model
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<T> ExecuteScalarUsingCommandText<T>(string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.ExecuteScalarUsingCommandText<T>(commandText, dicParams);
        }
        /// <summary>
        /// Hàm chạy command Text trả về thành công/thất bại
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<T> ExecuteScalarUsingCommandText<T>(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.ExecuteScalarUsingCommandText<T>(dbConnection, commandText, dicParams);
        }
        /// <summary>
        /// Hàm chạy command Text trả về thành công/thất bại
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<T> ExecuteScalarUsingCommandText<T>(IDbTransaction dbTransaction, string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.ExecuteScalarUsingCommandText<T>(dbTransaction, commandText, dicParams);
        }
        #endregion

        #region ===== ExecuteScalarUsingProcedure =====
        /// <summary>
        /// Hàm chạy command Text trả về 1 giá trị theo model
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<T> ExecuteScalarUsingProcedure<T>(string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.ExecuteScalarUsingProcedure<T>(commandText, dicParams);
        }
        /// <summary>
        /// Hàm chạy command Text trả về thành công/thất bại
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<T> ExecuteScalarUsingProcedure<T>(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.ExecuteScalarUsingProcedure<T>(dbConnection, commandText, dicParams);
        }
        /// <summary>
        /// Hàm chạy command Text trả về thành công/thất bại
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public async Task<T> ExecuteScalarUsingProcedure<T>(IDbTransaction dbTransaction, string commandText, Dictionary<string, object> dicParams)
        {
            return await _dataBaseService.ExecuteScalarUsingProcedure<T>(dbTransaction, commandText, dicParams);
        }
        #endregion
        #endregion
    }
}
