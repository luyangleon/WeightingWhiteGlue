using System;
using System.Data;
using System.Data.SqlClient;

class SQLDBHelper
{
    private SqlConnection _Connection = new SqlConnection();
    private SqlCommand _Command = null;
    private SqlDataAdapter _DataAdapter = null;

    /// <summary>
    /// 设定连接字串
    /// </summary>
    /// <param name="connectionString">连接字串</param>
    private void InitConnection(string connectionString)
    {
        if (connectionString == null || (connectionString = connectionString.Trim()).Length == 0)
            throw new Exception("连接字串为空，操作失败.");
        //连接字串不同时才更新
        if (!_Connection.ConnectionString.Equals(connectionString))
        {
            if (_Connection.State != ConnectionState.Closed) _Connection.Close();
            _Connection.ConnectionString = connectionString;
        }
    }

    /// <summary>
    /// 执行增删改指令(带事务)，需处理异常
    /// </summary>
    /// <param name="sql">Insert/Update/Delete指令</param>
    /// <param name="connectionString">SQLDB连接字串</param>
    /// <returns>返回受影响的行数</returns>
    public int ExecuteNonQuery(string sql, string connectionString)
    {
        if (sql == null || (sql = sql.Trim()).Length == 0) throw new Exception("参数sql为空，操作失败.");
        int result = 0;
        SqlTransaction transaction = null;
        try
        {
            InitConnection(connectionString);
            _Command = new SqlCommand();
            if (_Connection.State != ConnectionState.Open) _Connection.Open();
            transaction = _Connection.BeginTransaction();
            _Command.CommandText = sql;
            _Command.Connection = _Connection;
            _Command.Transaction = transaction;
            result = _Command.ExecuteNonQuery();
            transaction.Commit();
        }
        catch (Exception ex)
        {
            try
            {
                transaction.Rollback();
            }
            catch (Exception ex2)
            {
                Utils.AppendToFile(Utils.SystemLogFile, $"SQLDBHelper.ExecuteNonQuery Rollback fail: {ex2.Message}.", true);
            }
            throw ex;
        }
        finally
        {
            _Command.Dispose();
            _Command = null;
            _Connection.Close();
        }
        return result;
    }

    /// <summary>
    /// 查询并返回结果的首行首列值，需处理异常
    /// </summary>
    /// <param name="sql">SQL查询指令</param>
    /// <param name="connectionString">SQLDB连接字串</param>
    /// <returns>返回object查询结果</returns>
    public object ExecuteScalar(string sql, string connectionString)
    {
        if (sql == null || (sql = sql.Trim()).Length == 0) throw new Exception("参数sql为空，操作失败.");
        object result = null;
        try
        {
            InitConnection(connectionString);
            _Command = new SqlCommand(sql, _Connection);
            if (_Connection.State != ConnectionState.Open) _Connection.Open();
            result = _Command.ExecuteScalar();
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
    /// 查询并返回结果，需处理异常
    /// </summary>
    /// <param name="sql">SQL查询指令</param>
    /// <param name="connectionString">SQLDB连接字串</param>
    /// <returns>返回DataTable查询结果</returns>
    public DataTable GetDataTable(string sql, string connectionString)
    {
        if (sql == null || (sql = sql.Trim()).Length == 0) throw new Exception("参数sql为空，操作失败.");
        DataTable tbl = new DataTable();
        try
        {
            InitConnection(connectionString);
            _DataAdapter = new SqlDataAdapter(sql, _Connection);
            _DataAdapter.Fill(tbl);
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            _DataAdapter.Dispose();
            _DataAdapter = null;
            _Connection.Close();
            if (tbl != null) tbl.Dispose();
        }
        return tbl;
    }
}
