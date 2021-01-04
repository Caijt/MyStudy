using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
//using MySql.Data.MySqlClient;

namespace ItSys.Api
{
    public class SqlHelper
    {
        private IDataBaseObject _dataBaseObject;
        private static SqlHelper _dbHelper;
        private SqlHelper()
        {
            if (_dataBaseObject == null)
            {
                string DbTypeString = ConfigurationManager.AppSettings["DataBaseType"];
                CreateDataBaseFactory df = new CreateDataBaseFactory();
                _dataBaseObject = df.CreateDataBase(DbTypeString);
            }
        }
        public static SqlHelper GetInstance()
        {
            if (_dbHelper == null)
            {
                _dbHelper = new SqlHelper();
            }
            return _dbHelper;
        }


        #region 字典转换成SQL参数
        private void addCommandParamterFromDictionary(Dictionary<string, object> parameters, IDbCommand dataCommand)
        {
            if (parameters != null)
            {
                foreach (string parameterName in parameters.Keys)
                {
                    IDbDataParameter parameter = dataCommand.CreateParameter();
                    parameter.ParameterName = parameterName;
                    parameter.Value = parameters[parameterName];
                    dataCommand.Parameters.Add(parameter);
                }
            }
        }
        #endregion
        #region 用Adapter查询数据
        public DataSet GetDataSetWithAdapter(string cmdStr)
        {
            return GetDataSetWithAdapter(cmdStr, null);
        }
        public DataSet GetDataSetWithAdapter(string cmdStr, Dictionary<string, object> parameters)
        {
            using (IDbConnection dataConnection = _dataBaseObject.CreateConnection())
            {
                using (IDbCommand dataCommand = dataConnection.CreateCommand())
                {
                    dataCommand.CommandText = cmdStr;
                    addCommandParamterFromDictionary(parameters, dataCommand);
                    using (DbDataAdapter dataAdapter = _dataBaseObject.CreateDataAdapter())
                    {
                        dataAdapter.SelectCommand = (DbCommand)dataCommand;
                        DataSet ds = new DataSet();
                        dataAdapter.Fill(ds);
                        return ds;
                    }
                }
            }
        }
        #endregion

        #region 用Reader查询数据
        public IDataReader GetDataSetWithReader(string cmdStr)
        {
            return GetDataSetWithReader(cmdStr, null);
        }
        public IDataReader GetDataSetWithReader(string cmdStr, Dictionary<string, object> parameters)
        {
            using (IDbConnection dataConnection = _dataBaseObject.CreateConnection())
            {
                dataConnection.Open();
                using (IDbCommand dataCommand = dataConnection.CreateCommand())
                {
                    dataCommand.CommandText = cmdStr;
                    addCommandParamterFromDictionary(parameters, dataCommand);
                    return dataCommand.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
        }

        public IList<IDictionary<string, object>> ExecuteReader(string cmdStr)
        {
            return this.ExecuteReader(cmdStr, null);
        }

        public IList<IDictionary<string, object>> ExecuteReader(string cmdStr, Dictionary<string, object> param)
        {
            using (IDbConnection dataConnection = _dataBaseObject.CreateConnection())
            {
                dataConnection.Open();
                using (IDbCommand com = dataConnection.CreateCommand())
                {
                    com.CommandText = cmdStr;
                    addCommandParamterFromDictionary(param, com);
                    using (IDataReader reader = com.ExecuteReader())
                    {
                        IList<IDictionary<string, object>> list = new List<IDictionary<string, object>>();
                        while (reader.Read())
                        {
                            Dictionary<string, object> dict = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string name = reader.GetName(i);
                                object value = reader.GetValue(i);
                                dict.Add(name, value);
                            }

                            list.Add(dict);
                        }
                        return list;
                    }
                }
            }
        }
        #endregion



        #region 执行sql语句并返回首行首列数据
        public object ExecuteWithReturn(string cmdStr)
        {
            return ExecuteWithReturn(cmdStr, null);
        }
        public object ExecuteWithReturn(string cmdStr, Dictionary<string, object> parameters)
        {
            using (IDbConnection dataConnection = _dataBaseObject.CreateConnection())
            {
                dataConnection.Open();
                using (IDbCommand dataCommand = dataConnection.CreateCommand())
                {
                    dataCommand.CommandText = cmdStr;
                    addCommandParamterFromDictionary(parameters, dataCommand);
                    return dataCommand.ExecuteScalar();
                }
            }
        }
        #endregion

        #region 执行sql语句并返回影响行数
        public int Execute(string cmdStr)
        {
            return Execute(cmdStr, null);
        }
        public int Execute(string cmdStr, Dictionary<string, object> parameters)
        {
            using (IDbConnection dataConnection = _dataBaseObject.CreateConnection())
            {
                dataConnection.Open();
                using (IDbCommand dataCommand = dataConnection.CreateCommand())
                {
                    dataCommand.CommandText = cmdStr;
                    addCommandParamterFromDictionary(parameters, dataCommand);
                    return dataCommand.ExecuteNonQuery();
                }
            }
        }
        #endregion
    }
    #region 数据库生产工厂类
    class CreateDataBaseFactory
    {
        public IDataBaseObject CreateDataBase(string dataBaseType)
        {
            IDataBaseObject dbObject;
            dataBaseType = dataBaseType.ToLower();
            switch (dataBaseType)
            {
                case "mssql":
                    dbObject = new MsSqlDataObject();
                    break;
                //case "mysql":
                //      dbObject = new MySqlDataObject();
                //    break;
                case "access":
                    dbObject = new AccessDataObject();
                    break;
                default:
                    throw new Exception("找不到数据库类型");

            }
            string connectionString = ConfigurationManager.ConnectionStrings[dataBaseType].ConnectionString;
            dbObject.connectionString = connectionString;
            return dbObject;
        }
    }
    #endregion
    #region 数据库对象接口
    interface IDataBaseObject
    {
        string connectionString { get; set; }
        IDbConnection CreateConnection();
        DbDataAdapter CreateDataAdapter();
    }
    #endregion
    #region MsSql数据库对象
    class MsSqlDataObject : IDataBaseObject
    {
        public string connectionString { get; set; }
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(connectionString);
        }
        public DbDataAdapter CreateDataAdapter()
        {
            return new SqlDataAdapter();
        }
    }
    #endregion

    #region Access数据库对象
    class AccessDataObject : IDataBaseObject
    {
        public string connectionString { get; set; }
        public IDbConnection CreateConnection()
        {
            return new OleDbConnection(connectionString);
        }
        public DbDataAdapter CreateDataAdapter()
        {
            return new OleDbDataAdapter();
        }
    }
    #endregion

    //class MySqlDataObject : IDataBaseObject
    //{
    //    public string connectionString { get; set; }
    //    public IDbConnection CreateConnection()
    //    {
    //        return new MySqlConnection(connectionString);
    //    }
    //    public DbDataAdapter CreateDataAdapter()
    //    {
    //        return new MySqlDataAdapter();
    //    }
    //}

}
