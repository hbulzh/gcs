using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using schema.Service;
using schema.Utills;
using Newtonsoft.Json;
using schema.Dao;

namespace schema.Controllers
{
    public class ScreensController : Controller
    {
        private ScreensService screensService = new ScreensService();
   
        private DepartManageService departService = new DepartManageService();
        private string departid;
        private static List<string> calQue = new List<string>();
        // GET: Screens                            
        public ActionResult Screens(string id)
        {
            departid = id;
            ViewBag.departid = id;
            //当前科室的名字
            ViewBag.departName = screensService.getdepartName(id);

            //当前科室的待诊队列 status = 0， 1的病人名称
            ViewBag.pnames = screensService.getAllPatients(departid);

            //当前科室的正在就诊 status = 1的病人名称
            ViewBag.cpnames = screensService.getCurPatients(departid);
            //
            ViewBag.patientAndDept = screensService.getPatientAndDept(departid);
            ViewBag.clinicNames = screensService.getClinicNames(departid);

            //
            ViewBag.callmsg = new List<string>();
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string OnCalling (string name)
        {

            //该诊室该检查人的id
            // string patientid = screensService.GetClinicPatienid(departid, name);

            return "";
           // return screensService.GetVoice(departid, patientid);
        }
        public void AddCallQue(string msg)
        {
            foreach(string m in calQue)
            {
                if (m == msg) return;
            }
            calQue.Add(msg);
        }
        public string GetCallQue()
        {
            string msg = "";
            if (calQue.Count() > 0)
            {
                msg = calQue[0];
                calQue.Remove(calQue[0]);
            }
            return msg;
        }

    }
}