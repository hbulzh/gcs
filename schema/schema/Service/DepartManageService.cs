using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using schema.Dao;

namespace schema.Service
{
    /// <summary>
    /// 后台科室想关业务
    /// </summary>
    public class DepartManageService
    {
        //实例化Dao包
        private DepartManageDao departDao = new DepartManageDao();

        /// <summary>
        /// 查找所有的科室
        /// </summary>
        /// <returns>所有科室列表</returns>
        public List<T_DEPART_MANAGE> GetAllDepart()
        {
            return departDao.GetAllDepart();
        }
        /// <summary>
        /// 修改科室信息
        /// </summary>
        /// <param name="newuser">新用户对象</param>
        public void ChangeDepart(T_DEPART_MANAGE newdepart)
        {
            departDao.ChangeDepart(newdepart);
        }
        /// <summary>
        /// 通过主键查找科室
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T_DEPART_MANAGE GetDepartByKey(int? key)
        {
            return departDao.GetDepartByKey(key);
        }
    }
}