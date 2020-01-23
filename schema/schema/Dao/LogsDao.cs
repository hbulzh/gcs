using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace schema.Dao
{
    /// <summary>
    /// 日志管理
    /// </summary>
    public class LogsDao
    {
        private Entities db = new Entities();
        /// <summary>
        /// 返回日志信息
        /// </summary>
        /// <returns></returns>
        public List<T_LOGS> GetAllLogs()
        {
            List<T_LOGS> List = new List<T_LOGS>();
            List = db.T_LOGS.Where(x => x.IS_EXPORT == 0).OrderByDescending(x => x.CREATE_TIME).ToList();
            return List;
        }
        /// <summary>
        /// 增加日志
        /// </summary>
        /// <param name="newLogs"></param>
        public void AddLogs(T_LOGS  newLogs)
        {
            db.T_LOGS.Add(newLogs);    //直接在数据库上下文的表添加数据
            db.SaveChanges();   //可以同时添加多条数据，直接在最后SaveChanges保存就行,返回值为影响的行数
        }

        /// <summary>
        /// 指定删除
        /// </summary>
        /// <param name="id"></param>
        public void delete(string id)
        {
            T_LOGS item = db.T_LOGS.Find(Convert.ToInt32(id));
            if (item != null)
            {
                db.T_LOGS.Remove(item);
                db.SaveChanges();
                //删除多条数据参照查找
            }
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="delitems"></param>
        public void batches(string delitems)
        {
            string[] ids = delitems.Split(',');
            for (int i = 0; i < ids.Length; i++)
            {
                T_LOGS item = db.T_LOGS.Find(Convert.ToInt32(ids[i]));
                if (item != null)
                {
                    db.T_LOGS.Remove(item);
                    db.SaveChanges();
                    //删除多条数据参照查找
                }
            }
        }
        /// <summary>
        /// 选择时间导出
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public List<T_LOGS> export(DateTime t1, DateTime t2)
        {
            //jiang导出转态改为true
            List<T_LOGS> list = db.T_LOGS.Where(x => x.CREATE_TIME >= t1 && x.CREATE_TIME <= t2).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                T_LOGS logs = db.T_LOGS.Find(list[i].LOG_ID);
                logs.IS_EXPORT = 0;
                db.T_LOGS.Attach(logs);
                db.Entry(logs).State = EntityState.Modified;   //直接修改数据库上下文
                db.SaveChanges();
                //删除多条数据参照查找
            }
            //所需要导出的文件
            List<T_LOGS> List2 = db.T_LOGS.Where(x => x.CREATE_TIME >= t1 && x.CREATE_TIME <= t2).ToList();
            return List2;
        }

        /// <summary>
        /// 通过主键找ID
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T_LOGS GetUserByKey(int? key)
        {
            return db.T_LOGS.Find(key);
        }

    }
}