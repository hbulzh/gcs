using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace schema.Dao
{
    /// <summary>
    /// 后台科室相关操作
    /// </summary>
    public class DepartManageDao
    {
        private Entities db = new Entities();

        /// <summary>
        /// 返回所有科室信息
        /// </summary>
        /// <returns></returns>
        public  List<T_DEPART_MANAGE>GetAllDepart()
        {
            List<T_DEPART_MANAGE> depart = new List<T_DEPART_MANAGE>();
            depart = db.T_DEPART_MANAGE.ToList();
            return depart;

        }
        /// <summary>
        /// 更改科室信息
        /// </summary>
        /// <param name="newdeparrt"></param>
        public  void ChangeDepart(T_DEPART_MANAGE newdeparrt)
        {
            db.T_DEPART_MANAGE.Attach(newdeparrt);
            db.Entry(newdeparrt).State = EntityState.Modified;   //直接修改数据库上下文
            db.SaveChanges();
        }
        /// <summary>
        /// 通过主键查找科室
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T_DEPART_MANAGE GetDepartByKey(int? key)
        {
            return db.T_DEPART_MANAGE.Find(key);
        }
    }
}