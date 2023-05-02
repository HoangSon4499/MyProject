using MyProject.BL.Interface;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BL.BL
{
    public class DataBaseServiceBL: IDataBaseService
    {

        private const string MASTERDB_CONNETION_STRING = "Master_DB"; // Key get connectionString từ appsetting
        private const string SQL_Get_Paging = "Proc_{0}_GetPaging"; 
        private const string SQL_Get_Total_Paging = "Proc_{0}_GetTotalPaging";
        private const string Proc_Insert = "Proc_{0}_Insert";
        private const string Proc_Update = "Proc_{0}_Update";
        private const string Proc_Delette = "Proc_{0}_Delete";
        private const string Proc_GetByID = "Proc_{0}_GetByID";
        private readonly IConfigService _configService;

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="configService"></param>
        public DataBaseServiceBL(IConfigService configService)
        {
            _configService = configService;
        }


        #region ========== KHU VỰC KẾT NỐI DB ==========
        /// <summary>
        /// Hàm xử lý lấy chuỗi connection với DB
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetConnectionStringAsync()
        {
            var connectionStringMasterDB = _configService.GetConnectString(MASTERDB_CONNETION_STRING);
            return connectionStringMasterDB;
        }

        /// <summary>
        /// Hàm xử lý lấy chuỗi connection với DB
        /// </summary>
        /// <returns></returns>
        private string GetConnectionString()
        {
            var connectionStringMasterDB = GetConnectionStringAsync().Result;
            return connectionStringMasterDB;
        }

        /// <summary>
        /// Hàm xử lý lấy kết nối DB theo connectionString
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public IDbConnection GetConnection(string connectionString)
        {
            var conBuilder = new MySqlConnectionStringBuilder(connectionString);
            if (connectionString != null)
            {
                if (connectionString.IndexOf("SslMode", StringComparison.OrdinalIgnoreCase) < 0)
                {
                    conBuilder.SslMode = MySqlSslMode.Disabled;
                }
                if (connectionString.IndexOf("proceducecachesize", StringComparison.OrdinalIgnoreCase) < 0)
                {
                    conBuilder.ProcedureCacheSize = 25;
                }
            }

            var con = new MySqlConnection(conBuilder.ToString());
            return con;
        }

        /// <summary>
        /// Hàm kết nối với DB
        /// </summary>
        /// <returns></returns>
        public async Task<IDbConnection> GetConnectionAsync()
        {
            var connectionString = await GetConnectionStringAsync();
            return GetConnection(connectionString);
        }

        /// <summary>
        /// Hàm kết nối với DB
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            var con = GetConnectionAsync().Result;
            return con;
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
        public async Task<IDbConnection> GetConnectionToServerAsync(string server,string dataBase, string userID, string passWord, int port = 3306)
        {
            var conBuilder = new MySqlConnectionStringBuilder()
            {
                Server = server,
                UserID = userID,
                Password = passWord,
                Port = (uint)port,
                AllowUserVariables = true,
                MaximumPoolSize = 100000,
                SslMode = MySqlSslMode.Disabled,
                ProcedureCacheSize = 25
            };

            if (!string.IsNullOrWhiteSpace(dataBase))
            {
                conBuilder.Database = dataBase;
            }

            var con = new MySqlConnection(conBuilder.ToString());
            return con;
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
            var con = GetConnectionToServerAsync(server, dataBase, userID, passWord, port).Result;
            return con;
        }
        #endregion
    }
}
