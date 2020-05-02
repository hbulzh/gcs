using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using schema.Dao;
using System.Data.Entity;
using Newtonsoft.Json;

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
        public List<View_Screen_depart> GetScreenQueue(string  departid)
        {

           // using (var context = new DbContext())
          //  {
              //  var posts = context.Posts.SqlQuery("dbo.spGetTopPosts").ToList();

                //还需通过查找大屏显示的初诊人数
                int count = Convert.ToInt32(departdao.getDepartByid(departid).BIGSCREEN_NUM);
            //return db.View_BigScreemDisplay.Where(x => x.departmentName == department && x.ifCall == false && (x.status ==0 || x.status == 1)).OrderBy(x=>x.createTime).Take(count).ToList<View_BigScreemDisplay>();
           
                List<View_Screen_depart> firstlist = db.View_Screen_depart.Where(x => x.DEPT_CODE == departid  && (x.STATUS == 0 )).OrderBy(x=>x.CREAT_TIME).Take(count).ToList();


                return firstlist;
           

        }
        /// <summary>
        /// 得到当前科室正在就诊的人
        /// </summary>
        /// <param name="departid"></param>
        /// <returns></returns>
        public string GetScreenPatientQueue(string departid)
        {
            //如果没有正在检查的就返回空
            if (db.View_Screen.Where(x => x.DEPT_CODE == departid && x.STATUS == 1).ToList().Count == 0)
            {
                return " ";
            }
            //如果有正在检查的就返回这个人的名字
            else
            {
                return db.View_Screen.Where(x => x.DEPT_CODE == departid && x.STATUS == 1).Single().DEPT_NAME;
            }
           
           
        }
        /// <summary>
        /// 得到当前人检查的状态
        /// </summary>
        /// <param name="name"></param>
        /// <param name="departid"></param>
        /// <returns></returns>
        public int  GetStatus(string name, string departid)
        {
            return    Convert.ToInt32(db.View_Screen_depart.Where(x => x.DEPT_CODE == departid && x.NAME == name).Single().STATUS);
        }



        /// <summary>
        ///根据科室得到诊室的数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         public List<View_Screen> GetClincsByDepartCode(string id)
        {

            List<View_Screen> list = db.View_Screen.Where(x => x.DEPT_CODE == id).GroupBy(x => x.CLINIC_ID).Select(r => r.FirstOrDefault()).ToList();

           // List<decimal> list1 = db.View_Screen.Where(x => x.DEPT_CODE == id&&Select(x => x.CLINIC_ID).Distinct()).ToList();

            int S = 1;
            return list;

        }

        /// <summary>
        /// 返回当前科室正在就诊的人名字
        /// </summary>
        /// <param name="departid"></param>
        /// <param name="ClincID"></param>
        /// <returns></returns>
          public   string  GetClincName( string departid,decimal ClincID)
        {

            List<View_Screen> list = db.View_Screen.Where(x => x.DEPT_CODE == departid && x.CLINIC_ID == ClincID && x.STATUS
              ==1).ToList();
            if(list.Count>0)
            {
                return list.Single().NAME;
            }

            else
            {
                return " ";
            }

        }
        /// <summary>
        ///返回该科室这个人的id
        /// </summary>
        /// <param name="departid"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string getPatienID( string departid ,string name)
        {
                   string  patientId=        db.View_Screen.Where(x => x.DEPT_CODE == departid &&x.NAME== name).ToList()[0].PATIENT_ID;
            return patientId;
        }
         
      
        /// <summary>
        /// 得到病人的所在科室诊室
        /// </summary>
        /// <param name="queid"></param>
        /// <param name="departcode"></param>
        /// <returns></returns>
        public View_Screen GetView_BigScreemDisplayById(string  queid ,string departcode  )
        {
            List<View_Screen> list = db.View_Screen.Where(x => x.PATIENT_ID == queid&&x.DEPT_CODE== departcode).ToList();
            if (list.Count == 0)
            {
                return null;
            }
            return list.ElementAt(0);
        }

        /// <summary>
        /// 根据科室得到诊室列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetClincByDepartCode( string  id )
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            List<string>   cliincs= db.T_CLINIC.Where(x => x.DEPT_CODE == id).Select(x=>x.CLINIC_NAME).ToList();
            string   clinc  =  JsonConvert.SerializeObject(cliincs);
            dic.Add("name", clinc);

            return JsonConvert.SerializeObject(dic);
        }

        /// <summary>
        /// 根据科室id 和诊室名返回诊室id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public   decimal  GetClincID(string id ,string name)
        {
            decimal clincid = db.T_CLINIC.Where(x => x.DEPT_CODE == id && x.CLINIC_NAME == name).ToList()[0].CLINIC_ID;
            return clincid;
        }

        
    }


    }
