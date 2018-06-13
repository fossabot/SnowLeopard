﻿using Dapper;
using Dapper.Contrib.Extensions;
using Lynx.Extension;
using SnowLeopard.Abstractions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SnowLeopard.Infrastructure
{
    /// <summary>
    /// BaseDAL
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract partial class BaseDAL<T> : IBaseDAL<T>
        where T : class
    {
        private string _connStr = string.Empty;

        /// <summary>
        /// ConnStr
        /// </summary>
        public string ConnStr => _connStr;

        private IDbConnection _dbConnection;

        /// <summary>
        /// BaseDAL
        /// </summary>
        /// <param name="dbConnection"></param>
        public BaseDAL(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        #region Insert

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="model"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Identity of inserted entity</returns>
        public virtual int Insert(T model, int? commandTimeout = null)
        {
            return (int)_dbConnection.Insert(model, null, commandTimeout);
        }

        /// <summary>
        /// InsertAsync
        /// </summary>
        /// <param name="model"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="sqlAdapter"></param>
        /// <returns>Identity of inserted entity</returns>
        public async virtual Task<int> InsertAsync(T model, int? commandTimeout = null, ISqlAdapter sqlAdapter = null)
        {
            return await _dbConnection.InsertAsync(model, null, commandTimeout, sqlAdapter);
        }

        #endregion

        #region Delete

        private const string _deleteObjSql = "DELETE FROM {0} WHERE id=@id";
        private const string _deleteObjsSql = "DELETE FROM {0} WHERE id IN @ids";

        #region Int

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public virtual int Delete(int id, int? commandTimeout = null)
        {
            if (id <= 0)
                throw new ArgumentException(nameof(id) + "必须大于0");

            var sql = string.Format(_deleteObjSql, typeof(T).GetTableName());

            return _dbConnection.Execute(sql, new { id }, null, commandTimeout, CommandType.Text);
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public async virtual Task<int> DeleteAsync(int id, int? commandTimeout = null)
        {
            if (id <= 0)
                throw new ArgumentException(nameof(id) + "必须大于0");

            var sql = string.Format(_deleteObjSql, typeof(T).GetTableName());

            return await _dbConnection.ExecuteAsync(sql, new { id }, null, commandTimeout, CommandType.Text);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public virtual int Delete(IEnumerable<int> ids, int? commandTimeout = null)
        {
            if (ids == null || ids.Count() == 0)
                throw new ArgumentException("不允许为null，且至少包含于个元素", nameof(ids));

            var sql = string.Format(_deleteObjsSql, typeof(T).GetTableName());

            return _dbConnection.Execute(sql, new { ids }, null, commandTimeout, CommandType.Text);
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public async virtual Task<int> DeleteAsync(IEnumerable<int> ids, int? commandTimeout = null)
        {
            if (ids == null || ids.Count() == 0)
                throw new ArgumentException("不允许为null，且至少包含于个元素", nameof(ids));

            var sql = string.Format(_deleteObjsSql, typeof(T).GetTableName());

            return await _dbConnection.ExecuteAsync(sql, new { ids }, null, commandTimeout, CommandType.Text);
        }

        #endregion

        #region String

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public virtual int Delete(string id, int? commandTimeout = null)
        {
            if (id == null)
                throw new ArgumentException("Cannot be null", nameof(id));

            var sql = string.Format(_deleteObjSql, typeof(T).GetTableName());

            return _dbConnection.Execute(sql, new { id }, null, commandTimeout, CommandType.Text);
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public async virtual Task<int> DeleteAsync(string id, int? commandTimeout = null)
        {
            if (id == null)
                throw new ArgumentException("Cannot be null", nameof(id));

            var sql = string.Format(_deleteObjSql, typeof(T).GetTableName());

            return await _dbConnection.ExecuteAsync(sql, new { id }, null, commandTimeout, CommandType.Text);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public virtual int Delete(IEnumerable<string> ids, int? commandTimeout = null)
        {
            if (ids == null || ids.Count() == 0)
                throw new ArgumentException("不允许为null，且至少包含于个元素", nameof(ids));

            var sql = string.Format(_deleteObjsSql, typeof(T).GetTableName());

            return _dbConnection.Execute(sql, new { ids }, null, commandTimeout, CommandType.Text);
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public async virtual Task<int> DeleteAsync(IEnumerable<string> ids, int? commandTimeout = null)
        {
            if (ids == null || ids.Count() == 0)
                throw new ArgumentException("不允许为null，且至少包含于个元素", nameof(ids));

            var sql = string.Format(_deleteObjsSql, typeof(T).GetTableName());

            return await _dbConnection.ExecuteAsync(sql, new { ids }, null, commandTimeout, CommandType.Text);
        }

        #endregion

        #region Guid

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public virtual int Delete(Guid id, int? commandTimeout = null)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(nameof(id) + " 不是标准的Guid");

            var sql = string.Format(_deleteObjSql, typeof(T).GetTableName());

            return _dbConnection.Execute(sql, new { id }, null, commandTimeout, CommandType.Text);
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public async virtual Task<int> DeleteAsync(Guid id, int? commandTimeout = null)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(nameof(id) + " 不是标准的Guid");

            var sql = string.Format(_deleteObjSql, typeof(T).GetTableName());

            return await _dbConnection.ExecuteAsync(sql, new { id }, null, commandTimeout, CommandType.Text);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public virtual int Delete(IEnumerable<Guid> ids, int? commandTimeout = null)
        {
            if (ids == null || ids.Count() == 0)
                throw new ArgumentException("不允许为null，且至少包含于个元素", nameof(ids));

            var sql = string.Format(_deleteObjsSql, typeof(T).GetTableName());

            return _dbConnection.Execute(sql, new { ids }, null, commandTimeout, CommandType.Text);
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public async virtual Task<int> DeleteAsync(IEnumerable<Guid> ids, int? commandTimeout = null)
        {
            if (ids == null || ids.Count() == 0)
                throw new ArgumentException("不允许为null，且至少包含于个元素", nameof(ids));

            var sql = string.Format(_deleteObjsSql, typeof(T).GetTableName());

            return await _dbConnection.ExecuteAsync(sql, new { ids }, null, commandTimeout, CommandType.Text);
        }

        #endregion

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="model"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>true if deleted, false if not found</returns>
        public virtual bool Delete(T model, int? commandTimeout = null)
        {
            return _dbConnection.Delete(model, null, commandTimeout);
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="model"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>true if deleted, false if not found</returns>
        public async virtual Task<bool> DeleteAsync(T model, int? commandTimeout = null)
        {
            return await _dbConnection.DeleteAsync(model, null, commandTimeout);
        }

        /// <summary>
        /// DeleteAll
        /// </summary>
        /// <param name="commandTimeout"></param>
        /// <returns>true if deleted, false if none found</returns>
        public virtual bool DeleteAll(int? commandTimeout = null)
        {
            return _dbConnection.DeleteAll<T>(null, commandTimeout);
        }

        /// <summary>
        /// DeleteAllAsync
        /// </summary>
        /// <param name="commandTimeout"></param>
        /// <returns>true if deleted, false if none found</returns>
        public async virtual Task<bool> DeleteAllAsync(int? commandTimeout = null)
        {
            return await _dbConnection.DeleteAllAsync<T>(null, commandTimeout);
        }

        #endregion

        #region Update

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public virtual bool Update(T model, int? commandTimeout = null)
        {
            return _dbConnection.Update(model, null, commandTimeout);
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="model"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public async virtual Task<bool> UpdateAsync(T model, int? commandTimeout = null)
        {
            return await _dbConnection.UpdateAsync(model, null, commandTimeout);
        }

        #endregion

        #region Get

        #region Int

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        public virtual T Get(int id, int? commandTimeout = null)
        {
            return _dbConnection.Get<T>(id, null, commandTimeout);
        }

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        public async virtual Task<T> GetAsync(int id, int? commandTimeout = null)
        {
            return await _dbConnection.GetAsync<T>(id, null, commandTimeout);
        }

        #endregion

        #region String

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        public virtual T Get(string id, int? commandTimeout = null)
        {
            return _dbConnection.Get<T>(id, null, commandTimeout);
        }

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        public async virtual Task<T> GetAsync(string id, int? commandTimeout = null)
        {
            return await _dbConnection.GetAsync<T>(id, null, commandTimeout);
        }

        #endregion

        #region Guid

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        public virtual T Get(Guid id, int? commandTimeout = null)
        {
            return _dbConnection.Get<T>(id, null, commandTimeout);
        }

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        public async virtual Task<T> GetAsync(Guid id, int? commandTimeout = null)
        {
            return await _dbConnection.GetAsync<T>(id, null, commandTimeout);
        }

        #endregion

        /// <summary>
        /// GetAll
        /// </summary>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        public virtual IEnumerable<T> GetAll(int? commandTimeout = null)
        {
            return _dbConnection.GetAll<T>(null, commandTimeout);
        }

        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        public async virtual Task<IEnumerable<T>> GetAllAsync(int? commandTimeout = null)
        {
            return await _dbConnection.GetAllAsync<T>(null, commandTimeout);
        }

        #endregion

        #region Query

        /// <summary>
        /// Query
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is
        /// queried then the data from the first column in assumed, otherwise an instance
        /// is created per row, and a direct column-name===member-name mapping is assumed
        /// (case insensitive).
        /// </returns>
        public virtual IEnumerable<T> Query(string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _dbConnection.Query<T>(sql, param, null, buffered, commandTimeout, commandType);
        }

        /// <summary>
        /// QueryAsync
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is
        /// queried then the data from the first column in assumed, otherwise an instance
        /// is created per row, and a direct column-name===member-name mapping is assumed
        /// (case insensitive).
        /// </returns>
        public async virtual Task<IEnumerable<T>> QueryAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _dbConnection.QueryAsync<T>(sql, param, null, commandTimeout, commandType);
        }

        /// <summary>
        /// Query
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is
        /// queried then the data from the first column in assumed, otherwise an instance
        /// is created per row, and a direct column-name===member-name mapping is assumed
        /// (case insensitive).
        /// </returns>
        public virtual IEnumerable<Model> Query<Model>(string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _dbConnection.Query<Model>(sql, param, null, buffered, commandTimeout, commandType);
        }

        /// <summary>
        /// QueryAsync
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is
        /// queried then the data from the first column in assumed, otherwise an instance
        /// is created per row, and a direct column-name===member-name mapping is assumed
        /// (case insensitive).
        /// </returns>
        public async virtual Task<IEnumerable<Model>> QueryAsync<Model>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _dbConnection.QueryAsync<Model>(sql, param, null, commandTimeout, commandType);
        }

        #endregion

        #region Execute

        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns>The number of rows affected.</returns>
        public virtual int Execute(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _dbConnection.Execute(sql, param, null, commandTimeout, commandType);
        }

        /// <summary>
        /// ExecuteAsync
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns>The number of rows affected.</returns>
        public async virtual Task<int> ExecuteAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _dbConnection.ExecuteAsync(sql, param, null, commandTimeout, commandType);
        }

        #endregion

        #region ExecuteScalar

        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <typeparam name="Model">The type to return.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The first cell returned, as System.Object.</returns>
        public virtual Model ExecuteScalar<Model>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _dbConnection.ExecuteScalar<Model>(sql, param, null, commandTimeout, commandType);
        }

        /// <summary>
        /// ExecuteScalarAsync
        /// </summary>
        /// <typeparam name="Model">The type to return.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The first cell returned, as System.Object.</returns>
        public async virtual Task<Model> ExecuteScalarAsync<Model>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _dbConnection.ExecuteScalarAsync<Model>(sql, param, null, commandTimeout, commandType);
        }

        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The first cell returned, as System.Object.</returns>
        public virtual object ExecuteScalar(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _dbConnection.ExecuteScalar(sql, param, null, commandTimeout, commandType);
        }

        /// <summary>
        /// ExecuteScalarAsync
        /// </summary>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The first cell returned, as System.Object.</returns>
        public async virtual Task<object> ExecuteScalarAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _dbConnection.ExecuteScalarAsync(sql, param, null, commandTimeout, commandType);
        }

        #endregion

    }
}
