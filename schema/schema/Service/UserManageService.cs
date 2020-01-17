using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using schema.DAO;

namespace schema.Service
{
    /// <summary>
    /// 后台用户相关业务
    /// </summary>
    
    public class UserManageService
    {
        //实例化Dao包
        private  UserManageDao usersDao = new UserManageDao();

        /// <summary>
        /// 查找所有的用户
        /// </summary>
        /// <returns>所有用户列表</returns>
        public List<T_USER> GetAllUsers()
        {
            return usersDao.GetAllUsers();
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">新用户对象</param>
        public void AddUser(T_USER user)
        {
            usersDao.AddUser(user);
        }

        /// <summary>
        /// 判断用户名重名
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Repetiton(string name)
        {
            bool a = usersDao.Repetiton(name);
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
            bool a = usersDao.Repetiton(id, name);
            return a;
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="newuser">新用户对象</param>
        public void ChangeUser(T_USER newuser)
        {
            usersDao.ChangeUser(newuser);
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="key">被删除用户的主键</param>
        public void DeleUser(T_USER newusser)
        {
            usersDao.DeleUser(newusser);
        }

        /// <summary>
        /// 通过主键查找用户
        /// </summary>
        /// <param name="key">要查找用户的的主键</param>
        /// <returns></returns>
        public T_USER GetUserByKey(int? key)
        {
            return usersDao.GetUserByKey(key);
        }
    }
}