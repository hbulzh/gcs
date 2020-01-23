using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace schema.Dao
{
    /// <summary>
    /// 检查项目管理
    /// </summary>
    public class ItemsDao
    {
        private Entities db = new Entities();
        /// <summary>
        /// 返回所有检查项信息
        /// </summary>
        /// <returns></returns>
        public List<T_ITEM_PREPOSITION> GetAllitem()
        {
            List<T_ITEM_PREPOSITION> List = new List<T_ITEM_PREPOSITION>();
            List = db.T_ITEM_PREPOSITION.ToList();
            return List;
        }
        /// <summary>
        /// 增加项目
        /// </summary>
        /// <param name="newItem"></param>
        public void Additem(T_ITEM_PREPOSITION   newItem)
        {
            db.T_ITEM_PREPOSITION.Add(newItem);    //直接在数据库上下文的表添加数据
            db.SaveChanges();   //可以同时添加多条数据，直接在最后SaveChanges保存就行,返回值为影响的行数
        }
       /// <summary>
       /// 更改项目
       /// </summary>
       /// <param name="newuser"></param>
        public void Changeitem(T_ITEM_PREPOSITION newitem)
        {
            db.T_ITEM_PREPOSITION.Attach(newitem);
            db.Entry(newitem).State = EntityState.Modified;   //直接修改数据库上下文
            db.SaveChanges();
        }
        /// <summary>
        /// 删除检查项
        /// </summary>
        /// <param name="newitem"></param>
        public void DeleItem(T_ITEM_PREPOSITION newitem)
        {
            T_ITEM_PREPOSITION user = db.T_ITEM_PREPOSITION.Find(newitem.I_ID);
            if (user != null)
            {

                db.T_ITEM_PREPOSITION.Attach(newitem);
                db.Entry(newitem).State = EntityState.Modified;   //直接修改数据库上下文
                db.SaveChanges();
                //删除多条数据参照查找
            }

        }
        /// <summary>
        /// 通过主键查找设置项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T_ITEM_PREPOSITION GetItemByKey(int? key)
        {
            return db.T_ITEM_PREPOSITION.Find(key);
        }

    }
}