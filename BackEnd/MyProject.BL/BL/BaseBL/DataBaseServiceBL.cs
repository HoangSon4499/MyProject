using Dapper;
using MyProject.BL.Interface;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BL.BL
{
    public class DataBaseServiceBL : IDataBaseService
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
        public async Task<IDbConnection> GetConnectionToServerAsync(string server, string dataBase, string userID, string passWord, int port = 3306)
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
        public async Task<List<T>> QueryUsingCommandTextAsync<T>(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams)
        {
            return await DoQueryUsingCommandTextAsync<T>(commandText, dicParams, dbConnection: dbConnection);
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
            return await DoQueryUsingCommandTextAsync<T>(commandText, dicParams, dbTransaction: dbTransaction);
        }
        /// <summary>
        /// Hàm thực hiện chạy Query Using CommandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="dbConnection"></param>
        /// <returns></returns>
        public async Task<List<T>> DoQueryUsingCommandTextAsync<T>(string commandText, Dictionary<string, object> dicParams, IDbTransaction dbTransaction = null, IDbConnection dbConnection = null)
        {
            var cd = new CommandDefinition();
            try
            {
                List<T> result = new List<T>();
                var con = dbTransaction != null ? dbTransaction.Connection : dbConnection;
                if (con != null)
                {
                    var commandDefinitionInfo = new CommandDefinitionInfo()
                    {
                        Transaction = dbTransaction,
                        Connection = dbConnection
                    };
                    cd = await BuildCommandDefinition(commandText, dicParams, commandDefinitionInfo, CommandType.Text);
                    var query = await con.QueryAsync<T>(cd);
                    result = query.ToList();
                }
                else
                {
                    using (var conn = await GetConnectionAsync())
                    {
                        var commandDefinitionInfo = new CommandDefinitionInfo()
                        {
                            Transaction = dbTransaction,
                            Connection = dbConnection
                        };
                        cd = await BuildCommandDefinition(commandText, dicParams, commandDefinitionInfo, CommandType.Text);
                        var query = await conn.QueryAsync<T>(cd);
                        result = query.ToList();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        public async Task<List<T>> QueryUsingStoredProceduceAsync<T>(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams)
        {
            return await DoQueryUsingStoredProceduceAsync<T>(commandText, dicParams, dbConnection: dbConnection);
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
            return await DoQueryUsingStoredProceduceAsync<T>(commandText, dicParams, dbTransaction: dbTransaction);
        }
        /// <summary>
        /// Hàm thực hiện chạy Query Using CommandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="dbConnection"></param>
        /// <returns></returns>
        public async Task<List<T>> DoQueryUsingStoredProceduceAsync<T>(string commandText, Dictionary<string, object> dicParams, IDbTransaction dbTransaction = null, IDbConnection dbConnection = null)
        {
            var cd = new CommandDefinition();
            try
            {
                List<T> result = new List<T>();
                var con = dbTransaction != null ? dbTransaction.Connection : dbConnection;
                if (con != null)
                {
                    var commandDefinitionInfo = new CommandDefinitionInfo()
                    {
                        Transaction = dbTransaction,
                        Connection = dbConnection
                    };
                    cd = await BuildCommandDefinition(commandText, dicParams, commandDefinitionInfo, CommandType.StoredProcedure);
                    var query = await con.QueryAsync<T>(cd);
                    result = query.ToList();
                }
                else
                {
                    using (var conn = await GetConnectionAsync())
                    {
                        var commandDefinitionInfo = new CommandDefinitionInfo()
                        {
                            Transaction = dbTransaction,
                            Connection = dbConnection
                        };
                        cd = await BuildCommandDefinition(commandText, dicParams, commandDefinitionInfo, CommandType.Text);
                        var query = await conn.QueryAsync<T>(cd);
                        result = query.ToList();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        public async Task<IEnumerable<dynamic>> QueryUsingCommandTextAsync(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams)
        {
            return await DoQueryUsingCommandTextAsync(commandText, dicParams, dbConnection: dbConnection);
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
            return await DoQueryUsingCommandTextAsync(commandText, dicParams, dbTransaction: dbTransaction);
        }
        /// <summary>
        /// Hàm thực hiện chạy Query Using CommandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="dbConnection"></param>
        /// <returns></returns>
        public async Task<IEnumerable<dynamic>> DoQueryUsingCommandTextAsync(string commandText, Dictionary<string, object> dicParams, IDbTransaction dbTransaction = null, IDbConnection dbConnection = null)
        {
            var cd = new CommandDefinition();
            try
            {
                IEnumerable<dynamic> result = null;
                var con = dbTransaction != null ? dbTransaction.Connection : dbConnection;
                if (con != null)
                {
                    var commandDefinitionInfo = new CommandDefinitionInfo()
                    {
                        Transaction = dbTransaction,
                        Connection = dbConnection
                    };
                    cd = await BuildCommandDefinition(commandText, dicParams, commandDefinitionInfo, CommandType.Text);
                    result = await con.QueryAsync(cd);
                }
                else
                {
                    using (var conn = await GetConnectionAsync())
                    {
                        var commandDefinitionInfo = new CommandDefinitionInfo()
                        {
                            Transaction = dbTransaction,
                            Connection = dbConnection
                        };
                        cd = await BuildCommandDefinition(commandText, dicParams, commandDefinitionInfo, CommandType.Text);
                        result = await conn.QueryAsync(cd);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        public async Task<IEnumerable<dynamic>> QueryUsingStoredProceduceAsync(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams)
        {
            return await DoQueryUsingStoredProceduceAsync(commandText, dicParams, dbConnection: dbConnection);
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
            return await DoQueryUsingStoredProceduceAsync(commandText, dicParams, dbTransaction: dbTransaction);
        }
        /// <summary>
        /// Hàm thực hiện chạy Query Using CommandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="dbConnection"></param>
        /// <returns></returns>
        public async Task<IEnumerable<dynamic>> DoQueryUsingStoredProceduceAsync(string commandText, Dictionary<string, object> dicParams, IDbTransaction dbTransaction = null, IDbConnection dbConnection = null)
        {
            var cd = new CommandDefinition();
            try
            {
                IEnumerable<dynamic> result = null;
                var con = dbTransaction != null ? dbTransaction.Connection : dbConnection;
                if (con != null)
                {
                    var commandDefinitionInfo = new CommandDefinitionInfo()
                    {
                        Transaction = dbTransaction,
                        Connection = dbConnection
                    };
                    cd = await BuildCommandDefinition(commandText, dicParams, commandDefinitionInfo, CommandType.StoredProcedure);
                    result = await con.QueryAsync(cd);
                }
                else
                {
                    using (var conn = await GetConnectionAsync())
                    {
                        var commandDefinitionInfo = new CommandDefinitionInfo()
                        {
                            Transaction = dbTransaction,
                            Connection = dbConnection
                        };
                        cd = await BuildCommandDefinition(commandText, dicParams, commandDefinitionInfo, CommandType.Text);
                        result = await conn.QueryAsync(cd);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        public async Task<List<List<object>>> QueryMultipleUsingCommandTextAsync(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams, List<Type> types)
        {
            return await DoQueryMultipleUsingCommandTextAsync(commandText, dicParams, dbConnection: dbConnection, types: types);
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
            return await DoQueryMultipleUsingCommandTextAsync(commandText, dicParams, dbTransaction: dbTransaction, types: types);
        }
        /// <summary>
        /// Hàm thực hiện chạy Query Using CommandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="dbConnection"></param>
        /// <returns></returns>
        public async Task<List<List<object>>> DoQueryMultipleUsingCommandTextAsync(string commandText, Dictionary<string, object> dicParams, List<Type> types, IDbTransaction dbTransaction = null, IDbConnection dbConnection = null)
        {
            var cd = new CommandDefinition();
            try
            {
                List<List<object>> result = new List<List<object>>();
                var con = dbTransaction != null ? dbTransaction.Connection : dbConnection;
                if (con != null)
                {
                    var commandDefinitionInfo = new CommandDefinitionInfo()
                    {
                        Transaction = dbTransaction,
                        Connection = dbConnection
                    };
                    cd = await BuildCommandDefinition(commandText, dicParams, commandDefinitionInfo, CommandType.Text);
                    using (var queryMultiple = await con.QueryMultipleAsync(cd))
                    {
                        var index = 0;
                        do
                        {
                            var queryResult = await queryMultiple.ReadAsync(types[index]);
                            result.Add(queryResult.ToList());
                            index++;
                        } while (!queryMultiple.IsConsumed);
                    }
                }
                else
                {
                    using (var conn = await GetConnectionAsync())
                    {
                        var commandDefinitionInfo = new CommandDefinitionInfo()
                        {
                            Transaction = dbTransaction,
                            Connection = dbConnection
                        };
                        cd = await BuildCommandDefinition(commandText, dicParams, commandDefinitionInfo, CommandType.Text);
                        using (var queryMultiple = await con.QueryMultipleAsync(cd))
                        {
                            var index = 0;
                            do
                            {
                                var queryResult = await queryMultiple.ReadAsync(types[index]);
                                result.Add(queryResult.ToList());
                                index++;
                            } while (!queryMultiple.IsConsumed);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        public async Task<List<List<object>>> QueryMultipleUsingStoredProceduceAsync(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams, List<Type> types)
        {
            return await DoQueryMultipleUsingStoredProceduceAsync(commandText, dicParams, dbConnection: dbConnection, types: types);
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
            return await DoQueryMultipleUsingStoredProceduceAsync(commandText, dicParams, dbTransaction: dbTransaction, types: types);
        }
        /// <summary>
        /// Hàm thực hiện chạy Query Using CommandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="dbConnection"></param>
        /// <returns></returns>
        public async Task<List<List<object>>> DoQueryMultipleUsingStoredProceduceAsync(string commandText, Dictionary<string, object> dicParams, List<Type> types, IDbTransaction dbTransaction = null, IDbConnection dbConnection = null)
        {
            var cd = new CommandDefinition();
            try
            {
                List<List<object>> result = new List<List<object>>();
                var con = dbTransaction != null ? dbTransaction.Connection : dbConnection;
                if (con != null)
                {
                    var commandDefinitionInfo = new CommandDefinitionInfo()
                    {
                        Transaction = dbTransaction,
                        Connection = dbConnection
                    };
                    cd = await BuildCommandDefinition(commandText, dicParams, commandDefinitionInfo, CommandType.StoredProcedure);
                    using (var queryMultiple = await con.QueryMultipleAsync(cd))
                    {
                        var index = 0;
                        do
                        {
                            var queryResult = await queryMultiple.ReadAsync(types[index]);
                            result.Add(queryResult.ToList());
                            index++;
                        } while (!queryMultiple.IsConsumed);
                    }
                }
                else
                {
                    using (var conn = await GetConnectionAsync())
                    {
                        var commandDefinitionInfo = new CommandDefinitionInfo()
                        {
                            Transaction = dbTransaction,
                            Connection = dbConnection
                        };
                        cd = await BuildCommandDefinition(commandText, dicParams, commandDefinitionInfo, CommandType.Text);
                        using (var queryMultiple = await con.QueryMultipleAsync(cd))
                        {
                            var index = 0;
                            do
                            {
                                var queryResult = await queryMultiple.ReadAsync(types[index]);
                                result.Add(queryResult.ToList());
                                index++;
                            } while (!queryMultiple.IsConsumed);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        public async Task<List<List<object>>> QueryMultipleUsingCommandTextAsync(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams)
        {
            return await DoQueryMultipleUsingCommandTextAsync(commandText, dicParams, dbConnection: dbConnection);
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
            return await DoQueryMultipleUsingCommandTextAsync(commandText, dicParams, dbTransaction: dbTransaction);
        }
        /// <summary>
        /// Hàm thực hiện chạy Query Using CommandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="dbConnection"></param>
        /// <returns></returns>
        public async Task<List<List<object>>> DoQueryMultipleUsingCommandTextAsync(string commandText, Dictionary<string, object> dicParams, IDbTransaction dbTransaction = null, IDbConnection dbConnection = null)
        {
            var cd = new CommandDefinition();
            try
            {
                List<List<object>> result = new List<List<object>>();
                var con = dbTransaction != null ? dbTransaction.Connection : dbConnection;
                if (con != null)
                {
                    var commandDefinitionInfo = new CommandDefinitionInfo()
                    {
                        Transaction = dbTransaction,
                        Connection = dbConnection
                    };
                    cd = await BuildCommandDefinition(commandText, dicParams, commandDefinitionInfo, CommandType.Text);
                    using (var queryMultiple = await con.QueryMultipleAsync(cd))
                    {
                        var index = 0;
                        do
                        {
                            var queryResult = await queryMultiple.ReadAsync<dynamic>();
                            result.Add(queryResult.ToList());
                            index++;
                        } while (!queryMultiple.IsConsumed);
                    }
                }
                else
                {
                    using (var conn = await GetConnectionAsync())
                    {
                        var commandDefinitionInfo = new CommandDefinitionInfo()
                        {
                            Transaction = dbTransaction,
                            Connection = dbConnection
                        };
                        cd = await BuildCommandDefinition(commandText, dicParams, commandDefinitionInfo, CommandType.Text);
                        using (var queryMultiple = await con.QueryMultipleAsync(cd))
                        {
                            var index = 0;
                            do
                            {
                                var queryResult = await queryMultiple.ReadAsync<dynamic>();
                                result.Add(queryResult.ToList());
                                index++;
                            } while (!queryMultiple.IsConsumed);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        public async Task<List<List<object>>> QueryMultipleUsingStoredProceduceAsync(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams)
        {
            return await DoQueryMultipleUsingStoredProceduceAsync(commandText, dicParams, dbConnection: dbConnection);
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
            return await DoQueryMultipleUsingStoredProceduceAsync(commandText, dicParams, dbTransaction: dbTransaction);
        }
        /// <summary>
        /// Hàm thực hiện chạy Query Using CommandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="dbConnection"></param>
        /// <returns></returns>
        public async Task<List<List<object>>> DoQueryMultipleUsingStoredProceduceAsync(string commandText, Dictionary<string, object> dicParams, IDbTransaction dbTransaction = null, IDbConnection dbConnection = null)
        {
            var cd = new CommandDefinition();
            try
            {
                List<List<object>> result = new List<List<object>>();
                var con = dbTransaction != null ? dbTransaction.Connection : dbConnection;
                if (con != null)
                {
                    var commandDefinitionInfo = new CommandDefinitionInfo()
                    {
                        Transaction = dbTransaction,
                        Connection = dbConnection
                    };
                    cd = await BuildCommandDefinition(commandText, dicParams, commandDefinitionInfo, CommandType.StoredProcedure);
                    using (var queryMultiple = await con.QueryMultipleAsync(cd))
                    {
                        var index = 0;
                        do
                        {
                            var queryResult = await queryMultiple.ReadAsync<dynamic>();
                            result.Add(queryResult.ToList());
                            index++;
                        } while (!queryMultiple.IsConsumed);
                    }
                }
                else
                {
                    using (var conn = await GetConnectionAsync())
                    {
                        var commandDefinitionInfo = new CommandDefinitionInfo()
                        {
                            Transaction = dbTransaction,
                            Connection = dbConnection
                        };
                        cd = await BuildCommandDefinition(commandText, dicParams, commandDefinitionInfo, CommandType.Text);
                        using (var queryMultiple = await con.QueryMultipleAsync(cd))
                        {
                            var index = 0;
                            do
                            {
                                var queryResult = await queryMultiple.ReadAsync<dynamic>();
                                result.Add(queryResult.ToList());
                                index++;
                            } while (!queryMultiple.IsConsumed);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        public async Task<Dictionary<string, List<object>>> QueryMultipleUsingCommandTextAsync(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams, List<string> types)
        {
            return await DoQueryMultipleUsingCommandTextAsync(commandText, dicParams, dbConnection: dbConnection, types: types);
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
            return await DoQueryMultipleUsingCommandTextAsync(commandText, dicParams, dbTransaction: dbTransaction, types: types);
        }
        /// <summary>
        /// Hàm thực hiện chạy Query Using CommandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="dbConnection"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, List<object>>> DoQueryMultipleUsingCommandTextAsync(string commandText, Dictionary<string, object> dicParams, List<string> types, IDbTransaction dbTransaction = null, IDbConnection dbConnection = null)
        {
            var cd = new CommandDefinition();
            try
            {
                Dictionary<string, List<object>> result = new Dictionary<string, List<object>>();
                var queryResult = await DoQueryMultipleUsingCommandTextAsync(commandText, dicParams, dbTransaction, dbConnection);
                for (int i = 0; i < types.Count; i++)
                {
                    result.Add(types[i], queryResult[i]);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        public async Task<Dictionary<string, List<object>>> QueryMultipleUsingStoredProceduceAsync(IDbConnection dbConnection, string commandText, Dictionary<string, object> dicParams, List<string> types)
        {
            return await DoQueryMultipleUsingStoredProceduceAsync(commandText, dicParams, dbConnection: dbConnection, types:types);
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
            return await DoQueryMultipleUsingStoredProceduceAsync(commandText, dicParams, dbTransaction: dbTransaction, types: types);
        }
        /// <summary>
        /// Hàm thực hiện chạy Query Using CommandText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <param name="dicParams"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="dbConnection"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, List<object>>> DoQueryMultipleUsingStoredProceduceAsync(string commandText, Dictionary<string, object> dicParams, List<string> types, IDbTransaction dbTransaction = null, IDbConnection dbConnection = null)
        {
            var cd = new CommandDefinition();
            try
            {
                Dictionary<string, List<object>> result = new Dictionary<string, List<object>>();
                var queryResult = await DoQueryMultipleUsingStoredProceduceAsync(commandText, dicParams, dbTransaction, dbConnection);
                for( var i = 0; i < types.Count; i++)
                {
                    result.Add(types[i], queryResult[i]);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion

        #region ========== GET DATA METHODS =========
        #endregion

        #region ========== COMMON METHODS ==========
        /// <summary>
        /// model của CommandDefinitionInfo
        /// </summary>
        private class CommandDefinitionInfo
        {
            /// <summary>
            /// Transaction
            /// </summary>
            public IDbTransaction Transaction { get; set; }
            /// <summary>
            /// Connection
            /// </summary>
            public IDbConnection Connection { get; set; }
            /// <summary>
            /// khởi tạo
            /// </summary>
            /// <param name="dbConnection"></param>
            /// <param name="dbTransaction"></param>
            public CommandDefinitionInfo(IDbConnection dbConnection = null, IDbTransaction dbTransaction = null)
            {
                this.Transaction = dbTransaction;
                this.Connection = dbConnection;
            }
        }
        /// <summary>
        /// Hàm thực hiện build CommandText để thực hiện chạy
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dicParams"></param>
        /// <param name="commandDefinitionInfo"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<CommandDefinition> BuildCommandDefinition(string sql, Dictionary<string, object> dicParams, CommandDefinitionInfo commandDefinitionInfo, CommandType commandType)
        {
            if (dicParams.Count == 0)
            {
                throw new Exception($"Yêu cần truyền param cho sql: {sql}");
            }
            switch (commandType)
            {
                case CommandType.Text:
                    {
                        var commandDefinition = new CommandDefinition(sql, dicParams, commandDefinitionInfo?.Transaction, commandType: CommandType.Text);
                        return commandDefinition;
                    }
                case CommandType.StoredProcedure:
                    {
                        var commandDefinition = new CommandDefinition(sql, dicParams, commandDefinitionInfo?.Transaction, commandType: CommandType.StoredProcedure);
                        return commandDefinition;
                    }
                default:
                    {
                        throw new Exception($"Truyền Đúng CommantType cho sql: {sql}");
                    }
            }
            
        }
        #endregion
    }
}
