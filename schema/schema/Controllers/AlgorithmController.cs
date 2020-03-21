using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using schema.Dao;
using System.Web.Script.Serialization;

namespace schema.Controllers
{
    public class AlgorithmController : Controller
    {
        AlgorithmDao adao = new AlgorithmDao();
        public static string deptName = "";
        public static string deptCode = "";

        public static string patientid = "";
        public ActionResult Login()
        {
            return View();
        }
        // GET: Algorithm
        public ActionResult Algo(string inputname)
        {
            
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                ViewBag.deptName = deptName = inputname;
                deptCode = adao.getDeptCode(deptName);
                //debug
                //adao.setCompleteTimeNULL(deptCode);
                //初始化第一个人
                string UserInfo = adao.GetFirstUserInfo(deptCode);
                Dictionary<string, string> dic = jss.Deserialize<Dictionary<string, string>>(UserInfo);
                patientid = adao.getPatientId(dic["name"]);
            }
            catch
            {
                ViewBag.deptNum = 0;
            }
            return View();
        }

        public JsonResult getNxtDeptInfo()
        {
            // 对当前病人的检查完成时间赋值
            adao.setCompleteTime(patientid, deptCode);
            //返回当前病人下一站要去的地方
            string NxtDeptInfo = adao.GetNxtDeptInfo(patientid);
            return Json(NxtDeptInfo);
        }
        public JsonResult getFirstUserInfo()
        {
            try
            {
                string UserInfo = adao.GetFirstUserInfo(deptCode);
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, string> dic = jss.Deserialize<Dictionary<string, string>>(UserInfo);
                patientid = adao.getPatientId(dic["name"]);
                return Json(UserInfo);
            }
            catch
            {
                return Json("获取信息失败");
            }
            
        }

        public int getDeptNumber()
        {
            return adao.GetDeptNum(deptCode);
        }
        /// <summary>
        /// 显示下一个病人信息到页面上
        /// </summary>
        public JsonResult OverNumber(string username)
        {
            string patientid = adao.getPatientId(username);
            adao.overNumber(patientid, deptCode);
            return getFirstUserInfo();
        }
        public JsonResult getFirstDeptInfo(string patientid)
        {
            return Json(adao.GetNxtDeptInfo(patientid));
        }
    }
}