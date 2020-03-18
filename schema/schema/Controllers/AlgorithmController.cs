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
        // GET: Algorithm
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Algo()
        {
            
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                ViewBag.deptName = deptName = "彩超";
                deptCode = adao.getDeptCode(deptName);

                //debug
                adao.setCompleteTimeNULL(deptCode);
                //初始化第一个人
                string UserInfo = adao.GetFirstUserInfo(deptCode);

                Dictionary<string, string> dic = jss.Deserialize<Dictionary<string, string>>(UserInfo);
                patientid = adao.getPatientId(dic["name"]);
                ViewBag.name = dic["name"];
                ViewBag.sex = dic["sex"] == "1" ? "男" : "女";
                ViewBag.age = dic["age"];
                ViewBag.deptNum = getDeptNumber();
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
            string UserInfo = adao.GetFirstUserInfo(deptCode);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            if (UserInfo == null) return null;
            Dictionary<string, string> dic = jss.Deserialize<Dictionary<string, string>>(UserInfo);
            patientid = adao.getPatientId(dic["name"]);
            return Json(UserInfo);
        }

        public int getDeptNumber()
        {
            return adao.GetDeptNum(deptCode);
        }
        /// <summary>
        /// 显示下一个病人信息到页面上
        /// </summary>
        public void OverNumber(string username)
        {
            string patientid = adao.getPatientId(username);
            adao.overNumber(patientid, deptCode);
            getFirstUserInfo();
        }
    }
}