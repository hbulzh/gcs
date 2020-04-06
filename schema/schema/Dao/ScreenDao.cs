using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using schema.Dao;
using System.Data.Entity;

namespace schema.Dao
{
    public class ScreenDao
    {
        private Entities db = new Entities();

        DepartManageDao departdao = new DepartManageDao();
        /// <summary>
        /// 根据科室获得等待队列
        /// </summary>
        /// <param name="department"></param>
        /// <returns>返回视图对象</returns>
        public List<View_Screen> GetScreenQueue(string  departid)
        {

           // using (var context = new DbContext())
          //  {
              //  var posts = context.Posts.SqlQuery("dbo.spGetTopPosts").ToList();

                //还需通过查找大屏显示的初诊人数
                int count = Convert.ToInt32(departdao.getDepartByid(departid).BIGSCREEN_NUM);
            //return db.View_BigScreemDisplay.Where(x => x.departmentName == department && x.ifCall == false && (x.status ==0 || x.status == 1)).OrderBy(x=>x.createTime).Take(count).ToList<View_BigScreemDisplay>();
           
                List<View_Screen> firstlist = db.View_Screen.Where(x => x.DEPT_CODE == departid  && (x.STATUS == 0 || x.STATUS == 1)).OrderBy(x=>x.CREAT_TIME).Take(count).ToList();


                return firstlist;
           

        }

        public View_Screen GetView_BigScreemDisplayById(string  queid ,string departcode  )
        {
            List<View_Screen> list = db.View_Screen.Where(x => x.PATIENT_ID == queid&&x.DEPT_CODE== departcode).ToList();
            if (list.Count == 0)
            {
                return null;
            }
            return list.ElementAt(0);
        }

    }


    }
