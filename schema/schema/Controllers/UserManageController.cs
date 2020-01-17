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

        public ActionResult AddUser(string userName, string nickName, string passWord,  string userType )
        {
           
          T_USER   user = new T_USER();

            // user.userId = 0;
            user.USER_NAME = userName;
            user.LOGIN_NAME = nickName;
            user.PASSWORD = passWord;
            

            user.USER_TYPE = Convert.ToInt32(userType);
            user.IS_USE = 0;
          
            //  string  b =     moduleServices.showModules(Convert.ToInt32(rightValue));
            //user.rightValue = Convert.ToInt32(rightValue);
            user.CREATE_TIME = DateTime.Now;
            userManageService.AddUser(user);
            return RedirectToAction("Index");   //重定向



        }
    }
}