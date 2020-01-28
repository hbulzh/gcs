using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using schema.Dao;

namespace schema.Service
{

    public class RoomService
    {
        //实例化Dao包
        private  RoomManageDao  roomManageDao= new RoomManageDao();

        /// <summary>
        /// 查找所有的诊室
        /// </summary>
        /// <returns>所有诊室列表</returns>
        public List<T_CLINIC> GetAllRoom()
        {
            return roomManageDao.GetAllRoom();
        }
        /// <summary>
        /// 添加诊室
        /// </summary>
        /// <param name="user">新诊室对象</param>
        public void AddRoom(T_CLINIC newRoom)
        {
            roomManageDao.AddRoom(newRoom);
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="newuser">新用户对象</param>
        public void ChangeRoom(T_CLINIC newRoom)
        {
            roomManageDao.ChangeRoom(newRoom);
        }
        /// <summary>
        /// 删除诊室
        /// </summary>
        /// <param name="key">被删除诊室的主键</param>
        public void DeleRoom(T_CLINIC newRoom)
        {
            roomManageDao.DeleRoom(newRoom);
        }

        /// <summary>
        /// 通过主键查找诊室
        /// </summary>
        /// <param name="key">要查找用户的的主键</param>
        /// <returns></returns>
        public T_CLINIC GetRoomByKey(int? key)
        {
            return roomManageDao.GetRoomByKey(key);
        }
    }
}