﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Data.Abstract
{
	public interface IDapperHelper<T> where T : class
	{
		/// <summary>
		/// Execute raw query not return any values
		/// </summary>
		/// <param name="query"></param>
		/// <param name="parammeters"></param>
		Task ExecuteNotReturnAsync(string query, DynamicParameters parammeters = null, IDbTransaction dbTransaction = null);
		Task<T> ExecuteReturnScalarAsync<T>(string query, DynamicParameters parammeters = null, IDbTransaction dbTransaction = null);
		Task<IEnumerable<T>> ExecuteSqlReturnListAsync<T>(string query, DynamicParameters parammeters = null, IDbTransaction dbTransaction = null);
		Task<IEnumerable<T>> ExecuteStoreProcedureReturnListAsync<T>(string query, DynamicParameters parammeters = null, IDbTransaction dbTransaction = null);
	}
}
