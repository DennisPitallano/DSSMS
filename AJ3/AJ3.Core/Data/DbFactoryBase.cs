using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AJ3.Core.Data
{
    public abstract class DbFactoryBase
    {
        private readonly IConfiguration _config;

        internal string DbConnectionString => _config.GetConnectionString("SQLDBConnectionString");

        public DbFactoryBase(IConfiguration config)
        {
            _config = config;
        }

        internal IDbConnection DbConnection => new SqlConnection(DbConnectionString);

        public virtual async Task<IEnumerable<T>> DbQueryAsync<T>(string sql, object parameters = null, CommandType commandType = CommandType.StoredProcedure)
        {
            using var dbCon = DbConnection;
            return parameters == null ? await dbCon.QueryAsync<T>(sql, commandType) : await dbCon.QueryAsync<T>(sql, parameters, commandType: commandType);
        }
        public virtual async Task<T> DbQuerySingleAsync<T>(string sql, object parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            using var dbCon = DbConnection;
            return await dbCon.QueryFirstOrDefaultAsync<T>(sql, parameters, commandType: commandType);
        }

        public virtual async Task<bool> DbExecuteAsync<T>(string sql, object parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            using var dbCon = DbConnection;
            return await dbCon.ExecuteAsync(sql, parameters, commandType: commandType) > 0;
        }

        public virtual async Task<bool> DbExecuteScalarAsync(string sql, object parameters)
        {
            using var dbCon = DbConnection;
            return await dbCon.ExecuteScalarAsync<bool>(sql, parameters);
        }

        public virtual async Task<T> DbExecuteScalarDynamicAsync<T>(string sql, object parameters = null)
        {
            using var dbCon = DbConnection;
            return parameters == null ? await dbCon.ExecuteScalarAsync<T>(sql) : await dbCon.ExecuteScalarAsync<T>(sql, parameters);
        }

        public virtual async Task<(IEnumerable<T> Data, TRecordCount RecordCount)> DbQueryMultipleAsync<T, TRecordCount>(string sql, object parameters = null,CommandType commandType= CommandType.StoredProcedure)
        {
            IEnumerable<T> data;
            TRecordCount totalRecords;

            using (var dbCon = DbConnection)
            {
                using var results = await dbCon.QueryMultipleAsync(sql, parameters,commandType:commandType);
                data = await results.ReadAsync<T>();
                totalRecords = await results.ReadFirstOrDefaultAsync<TRecordCount>();
            }

            return (data, totalRecords);
        }

        public virtual async Task<(TData1 Data1,TData2 Data2 ,TData3 Data3)> DbQueryMultipleAsync<TData1,TData2, TData3>(string sql, object parameters = null,CommandType commandType= CommandType.StoredProcedure)
        {
            TData1 data1;
            TData2 data2;
            TData3 data3;

            using (var dbCon = DbConnection)
            {
                using var results = await dbCon.QueryMultipleAsync(sql, parameters,commandType:commandType);
                data1 = await results.ReadSingleAsync<TData1>();
                data2 = await results.ReadSingleAsync<TData2>();
                data3 = await results.ReadSingleAsync<TData3>();
            }

            return (data1,data2, data3);
        }

        public virtual async Task<(TData1 Data1,TData2 Data2 ,IEnumerable<TData3> Data3,IEnumerable<TData4> Data4)> DbQueryMultipleAsync<TData1,TData2, TData3,TData4>(string sql, object parameters = null,CommandType commandType= CommandType.StoredProcedure)
        {
            TData1 data1;
            TData2 data2;
            IEnumerable<TData3> data3;
            IEnumerable<TData4> data4;

            using (var dbCon = DbConnection)
            {
                using var results = await dbCon.QueryMultipleAsync(sql, parameters,commandType:commandType);
                data1 = await results.ReadSingleAsync<TData1>();
                data2 = await results.ReadSingleAsync<TData2>();
                data3 = await results.ReadAsync<TData3>();
                data4 = await results.ReadAsync<TData4>();
            }
            return (data1,data2, data3,data4);
        }

        public virtual async Task<(TResponse Result, T Data)> DbExecuteSpMultipleAsync<TResponse, T>(string sql, object parameters = null)
        {
            T data;
            TResponse response;

            using (var dbCon = DbConnection)
            {
                using var results = await dbCon.QueryMultipleAsync(sql, parameters, commandType: CommandType.StoredProcedure);
                response = await results.ReadSingleAsync<TResponse>();
                data = await results.ReadSingleAsync<T>();
            }

            return (response, data);
        }
    }
}
