using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using schema.Service;

namespace schema.Controllers
{
    public class UserManageController : Controller
    {
        private  UserManageService userManageService = new UserManageService();
        // GET: UserManage
        public ActionResult userManage()
        {
            //用户的相关信息
            ViewBag.userkist = userManageService.GetAllUsers();
            return View();
        }
        [HttpPost]
        public ActionResult AddUser(string userName, string nickName, string passWord,  string userType )
        {
           //实例一个文件
              T_USER user = new T_USER();   
                   
            user.USER_NAME = userName;
            user.LOGIN_NAME = nickName;
            user.PASSWORD = passWord;            
            user.USER_TYPE = Convert.ToInt32(userType);
            user.IS_USE = 0;
              
            user.CREATE_TIME = DateTime.Now;
            userManageService.AddUser(user);
            return RedirectToAction("Index");   //重定向
        }
        /// <summary>
        ///判断是否重名
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>

        [HttpPost]
        public bool Repetitong(string name)
        {
            bool judge = userManageService.Repetiton(name);
            return judge;
        }
        /// <summary>
        /// 修改时判断重名
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public string Repetitong_change(int id, string name)
        {
            bool judge = userManageService.Repetiton(id, name);

            if (judge == false)
            {
                return "False";
            }
            if (judge == true)
            {
                return "True";
            }
            return "Eroor";
        }
        /// <summary>
        /// 通过id  来改变用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       
        public ActionResult ChangeUser(string userId, string userName, string nickname, string passWord , string userType)
        {
            T_USER user = userManageService.GetUserByKey(Convert.ToInt32(userId));

            user.USER_NAME = userName;
            user.LOGIN_NAME = nickname;
            user.PASSWORD = passWord;
            user.USER_TYPE = Convert.ToInt32(userType);
            user.IS_USE = 1;
            user.CREATE_TIME = DateTime.Now;
            userManageService.ChangeUser(user);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isdelete"></param>
        /// <returns></returns>
        public ActionResult DeleUser(int id )
        {
            T_USER user = userManageService.GetUserByKey(Convert.ToInt32(id));                  
                user.IS_USE = 0;
                user.CREATE_TIME = DateTime.Now;
            userManageService.DeleUser(user);
            /// usersService.DeleUser(key);
            return RedirectToAction("Index");
        }
    }
}