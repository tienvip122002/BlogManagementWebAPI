using BlogManagement.Data.Abstract;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Data
{
	public class DapperHelper<T> : IDapperHelper<T> where T : class
	{
		private readonly string connectString = String.Empty;
		private IDbConnection _dbConnection;
		public DapperHelper(IConfiguration configuration)
		{
			connectString = configuration.GetConnectionString("BlogManagement");
			_dbConnection = new SqlConnection(connectString);
		}
		public async Task ExecuteNotReturnAsync(string query, DynamicParameters parammeters = null, IDbTransaction dbTransaction = null)
		{
			using (var dbConnection = new SqlConnection(connectString))
			{
				await dbConnection.ExecuteAsync(query, parammeters, dbTransaction, commandType: CommandType.Text);
			}
		}

		public async Task<T> ExecuteReturnScalarAsync<T>(string query, DynamicParameters parammeters = null, IDbTransaction dbTransaction = null)
		{
			using (var dbConnection = new SqlConnection(connectString))
			{
				return (T)Convert.ChangeType(await dbConnection.ExecuteScalarAsync<T>(query, parammeters, dbTransaction,
																commandType: System.Data.CommandType.StoredProcedure), typeof(T));
			}
		}

		public async Task<IEnumerable<T>> ExecuteSqlReturnListAsync<T>(string query, DynamicParameters parammeters = null, IDbTransaction dbTransaction = null)
		{
			using (var dbConnection = new SqlConnection(connectString))
			{
				return await dbConnection.QueryAsync<T>(query, parammeters, dbTransaction, commandType: CommandType.Text);
			}
		}

		public async Task<IEnumerable<T>> ExecuteStoreProcedureReturnListAsync<T>(string query, DynamicParameters parammeters = null, IDbTransaction dbTransaction = null)
		{
			using (var dbConnection = new SqlConnection(connectString))
			{
				return await dbConnection.QueryAsync<T>(query, parammeters, dbTransaction, commandType: CommandType.StoredProcedure);
			}
		}
	}
}
