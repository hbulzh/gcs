using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using schema.Dao;

namespace schema.Service
{
    /// <summary>
    /// 前置项相关业务
    /// </summary>
    public class ItemService
    {
        private ItemsDao itemDao = new ItemsDao();
       

        /// <summary>
        /// 查找所有的前置项
        /// </summary>
        /// <returns>所有用户列表</returns>
        public List<T_ITEM_PREPOSITION> GetAllitem()
        {
            return itemDao.GetAllitem();
        }
        /// <summary>
        /// 修改检查项信息
        /// </summary>
        /// <param name="newuser">新用户对象</param>
        public void ChangeDepart(T_ITEM_PREPOSITION newitem)
        {
            itemDao.Changeitem(newitem);
        }
        /// <summary>
        /// 删除检查项
        /// </summary>
        /// <param name="newRoom"></param>
        public void DeleItem(T_ITEM_PREPOSITION newitem)
        {
            itemDao.DeleItem(newitem);
        }
        /// <summary>
        /// 通过主键查检查项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T_ITEM_PREPOSITION GetDepartByKey(int? key)
        {
            return itemDao.GetItemByKey(key);
        }
    }
}