using System;
using System.Data;
using System.Data.Odbc;

public class OdbcHelper
{
    private OdbcConnection _Connection = new OdbcConnection();
    private OdbcCommand _Command = null;
    private OdbcDataAdapter _DataAdapter = null;

    /// <summary>
    /// 设定连接字串
    /// </summary>
    /// <param name="connectionString">连接字串</param>
    private void InitConnection(string connectionString)
    {
        if (connectionString == null || "".Equals(connectionString.Trim())) throw new Exception("连接字串为空，操作失败.");
        //连接字串不同时才更新
        if (!_Connection.ConnectionString.Equals(connectionString))
        {
            if (_Connection.State != ConnectionState.Closed) _Connection.Close();
            _Connection.ConnectionString = connectionString;
        }
    }

    /// <summary>
    /// 执行增删改指令(DML)，需处理执行异常
    /// </summary>
    /// <param name="sql">DML指令</param>
    /// <param name="connectionString">连接字串</param>
    /// <returns>返回成功执行笔数</returns>
    public int ExecuteNonQuery(string sql, string connectionString)
    {
        if (sql == null || sql.Trim().Length == 0) throw new Exception("参数sql为空，放弃操作.");
        int result = 0;
        try
        {
            InitConnection(connectionString);
            _Command = new OdbcCommand(sql, _Connection);
            if (_Connection.State != ConnectionState.Open) _Connection.Open();
            result = _Command.ExecuteNonQuery();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            _Command.Dispose();
            _Command = null;
            _Connection.Close();
        }
        return result;
    }

    /// <summary>
    /// 查询并返回结果的首行首列值(DQL)，需处理执行异常
    /// </summary>
    /// <param name="sql">DQL指令</param>
    /// <param name="connectionString">ODBC连接字串名</param>
    /// <param name="erpquery">True:Informix资料库，False:其它资料库</param>
    /// <returns>返回查询结果，指令错误返回null</returns>
    public object ExecuteScalar(string sql, string connectionString, bool erpquery)
    {
        if (sql == null || sql.Trim().Length == 0) throw new Exception("参数sql为空，放弃操作.");
        object result = null;
        _Command = new OdbcCommand();
        try
        {
            InitConnection(connectionString);
            _Command.Connection = _Connection;
            if (_Connection.State != ConnectionState.Open) _Connection.Open();
            if (erpquery)
            {
                _Command.CommandText = "set isolation to dirty read";
                _Command.ExecuteNonQuery();
            }
            _Command.CommandText = sql;
            result = _Command.ExecuteScalar();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            _Command.Dispose();
            _Command = null;
            _Connection.Close();
            _Connection = null;
        }
        return result;
    }

    /// <summary>
    /// 查询并返回结果的首行首列值(DQL)，需处理执行异常
    /// </summary>
    /// <param name="sql">DQL指令</param>
    /// <param name="connectionString">ODBC连接字串名</param>
    /// <param name="erpquery">True:Informix资料库，False:其它资料库</param>
    /// <returns>返回查询结果，指令错误返回null</returns>
    public object ExecuteScalar(string sql, string connectionString)
    {
        return ExecuteScalar(sql, connectionString, true);
    }

    /// <summary>
    /// 查询并返回结果(DQL)，需处理执行异常
    /// </summary>
    /// <param name="sql">DQL指令</param>
    /// <param name="connectionString">ODBC连接字串名</param>
    /// <param name="erpquery">False：依sql执行查询，True:在sql前加入"set isolation to dirty read"</param>
    /// <returns>返回查询结果，失败返回null</returns>
    private DataTable GetDataTable(string sql, string connectionString,bool erpquery)
    {
        if (sql == null || sql.Trim().Length == 0) throw new Exception("参数sql为空，放弃操作.");
        DataTable tbl = new DataTable();
        _Command = new OdbcCommand();
        try
        {
            InitConnection(connectionString);
            _Command.Connection = _Connection;
            if (_Connection.State != ConnectionState.Open) _Connection.Open();
            if (erpquery)
            {
                _Command.CommandText = "set isolation to dirty read";
                _Command.ExecuteNonQuery();
            }
            _Command.CommandText = sql;
            _DataAdapter = new OdbcDataAdapter(_Command);
            _DataAdapter.Fill(tbl);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            _DataAdapter.Dispose();
            _DataAdapter = null;
            _Command.Dispose();
            _Command = null;
            _Connection.Close();
        }
        return tbl;
    }

    /// <summary>
    /// 查询Informix资料库并返回结果(DQL)，需处理执行异常
    /// </summary>
    /// <param name="sql">DQL指令</param>
    /// <param name="connectionString">ODBC连接字串名</param>
    /// <returns>返回查询结果，失败返回null</returns>
    public DataTable GetErpDataTable(string sql, string connectionString)
    {
        return GetDataTable(sql, connectionString, true);
    }

    /// <summary>
    /// 查询非ERP资料库并返回结果(DQL)，需处理执行异常
    /// </summary>
    /// <param name="sql">DQL指令</param>
    /// <param name="connectionString">ODBC连接字串名</param>
    /// <returns>返回查询结果，失败返回null</returns>
    public DataTable GetDataTable(string sql, string connectionString)
    {
        return GetDataTable(sql, connectionString, false);
    }
}
