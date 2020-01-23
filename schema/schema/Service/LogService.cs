using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using schema.Dao;

namespace schema.Service
{
    /// <summary>
    /// 日志相关操作
    /// </summary>
    public class LogService
    {
        private LogsDao logsDao = new LogsDao();
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="log"></param>
        public void AddLogs(T_LOGS log)
        {
            logsDao.AddLogs(log);
        }
        /// <summary>
        /// 返回所有日志信息
        /// </summary>
        /// <returns></returns>
        public List<T_LOGS> GetallLogs()
        {
            return logsDao.GetAllLogs();
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="delitems"></param>
        public void batches(string delitems)
        {
            logsDao.batches(delitems);

        }
        /// <summary>
        /// 指定删除
        /// </summary>
        /// <param name="id"></param>
        public void delete(string id)
        {
            logsDao.delete(id);
        }
        /// <summary>
        ///选择时间导出
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public List<T_LOGS> export(DateTime t1, DateTime t2)
        {

            return logsDao.export(t1, t2);
        }

    }
}