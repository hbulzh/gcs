using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using schema.Service;
using schema.Utills;
using Newtonsoft.Json;

namespace schema.Controllers
{
    public class ScreensController : Controller
    {
        private ScreensService screensScrice = new ScreensService();
        private ScreenSerice screenService = new ScreenSerice();
        string departid ="";
        // GET: Screens                            
        public ActionResult Screens( string id)
        {
            departid = id;
            //当前科室的名字
            ViewBag.departName = screenService.GetdepartNamebyid(id);
            //当前科室的待诊队列
            ViewBag.userkist = screensScrice.ScreenDisplay(id);
            //获得当前科室的所有诊室
            // ViewBag.clincName = screensScrice.GetClincsByDepartCode(id);
            string clinc1 = "诊室1";
            string clinc2 = "诊室3";
            string clinc3 = "诊室4";
            string clinc4 = "诊室7";

            decimal cid1 = screensScrice.GetClincID(id, clinc1);
            decimal cid2 = screensScrice.GetClincID(id, clinc2);
            decimal cid3 = screensScrice.GetClincID(id, clinc3);
            decimal cid4 = screensScrice.GetClincID(id, clinc4);


            //正在就诊的名字
            string name1 = GetPatientName(id, cid1);
            string name2 = GetPatientName(id, cid2);
            string name3 = GetPatientName(id, cid3);
            string name4 = GetPatientName(id, cid4);



            ViewBag.patien1 = name1;
            ViewBag.patien2 = name2;
            ViewBag.patien3 = name3;
            ViewBag.patien4 = name4;

            //第一个科室
            if (name1 != " ")
            {
                if (screenService.getStutes(id, name1) != 2)
                {
                    ViewBag.depart1 = " ";
                }
                else
                {
                    ViewBag.depart1 = GetNexDept(screensScrice.GetClinicPatienid(id, name1));
                }
            }
            else
            {
                ViewBag.depart1 = " ";
            }

          //第二个科室
            if (name2 != " ")
            {
                if (screenService.getStutes(id, name2) != 2)
                {
                    ViewBag.depart2 = " ";
                }
                else
                {
                    ViewBag.depart2 = GetNexDept(screensScrice.GetClinicPatienid(id, name2));
                }
            }
            else
            {
                ViewBag.depart2 = " ";
            }

         //第三个科室
            if (name3 != " ")
            {
                if (screenService.getStutes(id, name3) != 2)
                {
                    ViewBag.depart3 = " ";
                }
                else
                {
                    ViewBag.depart3 = GetNexDept(screensScrice.GetClinicPatienid(id, name3));
                }
            }
            else
            {
                ViewBag.depart3 = " ";
            }

          //第四个科室
            if (name4 != " ")
            {
                if (screenService.getStutes(id, name4) != 2)
                {
                    ViewBag.depart4 = " ";
                }
                else
                {
                    ViewBag.depart4 = GetNexDept(screensScrice.GetClinicPatienid(id, name4));
                }
            }
            else
            {
                ViewBag.depart = " ";
            }



            return View();
        }



        
        /// <summary>
        /// 该科室的诊室数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int  getClincCount( string id)
        {
            return screensScrice.GetClincsByDepartCode(id).Count;
        }
        /// <summary>
        /// 获取该诊室正在就诊名字
        /// </summary>
        /// <param name="clincId"></param>
        /// <param name=""></param>
        /// <returns></returns>
         public string  GetPatientName(string departId,decimal clincId )
        {
            return screensScrice.GetClinicPatienName(departId, clincId);

        }
        /// <summary>
        /// 得到该病人的下一个地方
        /// </summary>
        /// <param name="patientid"></param>
        /// <returns></returns>
        public  String GetNexDept(string patientid)
        {
            return screensScrice.GetNexDept(patientid);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public string OnCalling (string name)
        {

           //该诊室该检查人的id
            string patientid = screensScrice.GetClinicPatienid(departid, name);


            return screensScrice.GetVoice(departid, patientid);
        }

    }
}