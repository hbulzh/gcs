using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using schema.Service;

namespace schema.Controllers
{
    public class RoomController : Controller
    {
        private RoomService roomService = new RoomService();
        // GET: Room
        public ActionResult Room()
        {
            ViewBag.userkist = roomService.GetAllRoom();
            return View();
        }
        /// <summary>
        /// 增加诊室
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="nickName"></param>
        /// <param name="passWord"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddRoom(string userName, string nickName, decimal Floor, string ip,string position, string sex)
        {
            //实例一个文件
           T_CLINIC room = new T_CLINIC();

            room.CLINIC_NAME = userName;
            room.DEPT_CODE = nickName;
            room.FLOOR = Floor;
            room.IP_ADDR = ip;
            room.POSITION = position;
            room.SEX_CODE = sex;
            room.IS_USE = 0;

            room.CREATE_TIME = DateTime.Now;
            roomService.AddRoom(room);
            return RedirectToAction("Index");   //重定向
        }
        public ActionResult ChangeUser(string roomId, string userName, string nickName, decimal Floor, string ip, string position, string sex)
        {
            T_CLINIC room = roomService.GetRoomByKey(Convert.ToInt32(roomId));

            room.CLINIC_NAME = userName;
            room.DEPT_CODE = nickName;
            room.FLOOR = Floor;
            room.IP_ADDR = ip;
            room.POSITION = position;
            room.SEX_CODE = sex;
            room.IS_USE = 0;

            room.CREATE_TIME = DateTime.Now;
            
            roomService.ChangeRoom(room);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isdelete"></param>
        /// <returns></returns>
        public ActionResult DeleUser(int id)
        {
            T_CLINIC user = roomService.GetRoomByKey(Convert.ToInt32(id));
            user.IS_USE = 0;
            user.CREATE_TIME = DateTime.Now;
            roomService.DeleRoom(user);
            /// usersService.DeleUser(key);
            return RedirectToAction("Index");
        }
    }
}