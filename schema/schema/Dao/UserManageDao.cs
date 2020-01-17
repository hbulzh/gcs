using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace schema.DAO
{
    /// <summary>
    /// 用户后台管理的相关操作
    /// </summary>
    public class UserManageDao
    {
        private Entities db = new Entities();

        /// <summary>
        ///返回所有用户的信息
        /// </summary>
        public List<T_USER> GetAllUsers()
        {
            List<T_USER> List = new List<T_USER>();
             List = db.T_USER.Where(x => x.IS_USE == 0).OrderByDescending(x => x.CREATE_TIME).ToList();
            return List;
        }
        /// <summary>
        ///增加用户
        /// </summary>
        public void AddUser(T_USER user)
        {
            db.T_USER.Add(user);    //直接在数据库上下文的表添加数据
            db.SaveChanges();   //可以同时添加多条数据，直接在最后SaveChanges保存就行,返回值为影响的行数
        }
        ///<summary>
        ///更改用户
        /// </summary>
        public void ChangeUser(T_USER newuser)
        {
            db.T_USER.Attach(newuser);
            db.Entry(newuser).State = EntityState.Modified;   //直接修改数据库上下文
            db.SaveChanges();
        }
        /// <summary>
        /// 判断用户名重复
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Repetiton(string name)
        {
            bool a = db.T_USER.Select(x => x.USER_NAME).Contains(name);
            return a;
        }
        /// <summary>
        /// 修改时判断用户名重名
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Repetiton(int id, string name)
        {
            List<T_USER> list = db.T_USER.Where(x => x.USER_ID != id).ToList();
            string[] b = list.Select(x => x.USER_NAME).ToArray();
            bool a = db.T_USER.Where(x => x.USER_ID != id).Select(x => x.USER_NAME).Contains(name);
            return a;
        }

        /// <summary>
        ///删除用户
        /// </summary>
        public void DeleUser(T_USER newuser)
        {
            T_USER user = db.T_USER.Find(newuser.USER_ID);
            if (user != null)
            {

                db.T_USER.Attach(newuser);
                db.Entry(newuser).State = EntityState.Modified;   //直接修改数据库上下文
                db.SaveChanges();
                //删除多条数据参照查找
            }
        }

        /// <summary>
        /// 通过id来查找用户
        /// </summary>
        /// <param name="key"> id</param>
        /// <returns></returns>
        public T_USER GetUserByKey(int? key)
        {
            return db.T_USER.Find(key);
        }

        /// <summary>
        /// 登录时匹配数据库
        /// </summary>
        /// <param name="name"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public T_USER Login(string name, string key)
        {
            List<T_USER> List = new List<T_USER>();
            List = db.T_USER.Where(x => x.USER_NAME == name && x.PASSWORD == key).ToList();
            return List[0];
        }
        /// <summary>
        /// 查找用户类型通过名字
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public T_USER check(string name)
        {
            List<T_USER> List = new List<T_USER>();
            List = db.T_USER.Where(x => x.USER_NAME == name).ToList();
            return List[0];
        }
        /// <summary>
        /// 通过用户名查找
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public T_USER getUserByName(string name)
        {
            List<T_USER> list = db.T_USER.Where(x => x.USER_NAME == name).ToList();
            return list.ElementAt(0);
        }
    }
}