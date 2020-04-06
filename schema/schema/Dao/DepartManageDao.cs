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
        /// <summary>
        /// 通过科室id查找对应的科室
        /// </summary>
        /// <param name="departId"></param>
        /// <returns></returns>
        public T_DEPART_MANAGE getDepartByid(string departId)
        {
            T_DEPART_MANAGE depart = db.T_DEPART_MANAGE.Where(x=>x.DEPT_CODE== departId).Single();
            return depart;
        }
        /// <summary>
        /// 通过科室id查找科室名
        /// </summary>
        /// <param name="departid"></param>
        /// <returns></returns>
        public string  GetDepartByDepartId(string  departid)
        {
            return db.DEPT_DICT.Where(x => x.DEPT_CODE == departid).ToList().ElementAt(0).DEPT_NAME;
        }
    }
}