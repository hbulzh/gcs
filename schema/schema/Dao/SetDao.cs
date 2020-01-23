using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace schema.Dao
{
   /// <summary>
   /// 设置相关操作
   /// </summary>
    public class SetDao
    {
        private Entities db = new Entities();
        /// <summary>
        /// 返回所有设置信息
        /// </summary>
        /// <returns></returns>
        public List<T_SETS> GetAllUsers()
        {
            List<T_SETS> List = new List<T_SETS>();
            List = db.T_SETS.ToList();
            return List;
        }
        
        /// <summary>
        /// 修改设置项
        /// </summary>
        /// <param name="type"></param>
        /// <param name="item"></param>
        /// <param name="value"></param>
        public void ChangeSetsByTypeAndItem(string type, string item, string value)
        {
            T_SETS sets = db.T_SETS.Where(x => x.SET_TYPE == type && x.SET_ITEM == item).Single();
            sets.SET_VALUE = value;
            db.T_SETS.Attach(sets);
            db.Entry(sets).State = EntityState.Modified;
            db.SaveChanges();
        }
        /// <summary>
        /// 根据设置项查找设置值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetSetByTypeAndItem(string type, string item)
        {
            return db.T_SETS.Where(x => x.SET_TYPE.Equals(type) && x.SET_ITEM.Equals(item)).Single().SET_VALUE;
        }
        /// <summary>
        ///获得队列编号的数字部分的后缀位数
        /// </summary>
        /// <returns></returns>
        public string getQueNumDigit()
        {
            //查询设置表里的对应的属性值
            List<T_SETS> set = db.T_SETS.Where(x => x.SET_VALUE == "digit" && x.SET_TYPE == "queNumMax").ToList();
            string digit = set[0].SET_VALUE;
            return digit;
        }
    }
}