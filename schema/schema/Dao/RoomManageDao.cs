using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace schema.Dao
{
    /// <summary>
    /// 诊室相关操作 
    /// </summary>
    public class RoomManageDao
    {
        private Entities db = new Entities();
        /// <summary>
        /// 返回诊室所有信息
        /// </summary>
        /// <returns></returns>
        public List<T_CLINIC> GetAllRoom()
        {
            List<T_CLINIC> List = new List<T_CLINIC>();
            List = db.T_CLINIC.Where(x => x.IS_USE == 1).OrderByDescending(x => x.CREATE_TIME).ToList();
            return List;
        }
        /// <summary>
        /// 增加诊室
        /// </summary>
        /// <param name="user"></param>

        public void AddRoom(T_CLINIC room)
        {
            db.T_CLINIC.Add(room);    //直接在数据库上下文的表添加数据
            db.SaveChanges();   //可以同时添加多条数据，直接在最后SaveChanges保存就行,返回值为影响的行数
        }
        /// <summary>
        /// 更改诊室信息
        /// </summary>
        /// <param name="newuser"></param>
        public void ChangeRoom(T_CLINIC newRoom)
        {
            db.T_CLINIC.Attach(newRoom);
            db.Entry(newRoom).State = EntityState.Modified;   //直接修改数据库上下文
            db.SaveChanges();
        }
        /// <summary>
        /// 删除用户（更改诊室是否停用的状态）
        /// </summary>
        /// <param name="newRoom"></param>
        public void DeleRoom(T_CLINIC newRoom)
        {
            T_CLINIC user = db.T_CLINIC.Find(newRoom.CLINIC_ID);
            if (user != null)
            {

                db.T_CLINIC.Attach(newRoom);
                db.Entry(newRoom).State = EntityState.Modified;   //直接修改数据库上下文
                db.SaveChanges();
                //删除多条数据参照查找
            }

        }
        /// <summary>
        /// 通过科室id查诊室
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T_CLINIC GetRoomByKey(string  key)
        {
            return db.T_CLINIC.Where(x=>x.DEPT_CODE== key).Single();
        }


        /// <summary>
        /// 通过主键查找诊室
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T_CLINIC  GetRoombyKey(int? key)
        {
           return   db.T_CLINIC.Where(x => x.CLINIC_ID == key).Single();
        }
    }
}