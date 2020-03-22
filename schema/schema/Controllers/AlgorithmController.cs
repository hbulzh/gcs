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
        public static decimal clinicId;
        public static decimal doctorId;
        public static string patientId = "";
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
                
                //通过IP初始化诊室信息
                clinicId = adao.getClinicId("192.168.1.1");
                doctorId = 123;
                //通过诊室id 初始化 科室id，科室姓名
                deptCode = adao.getDeptCode(clinicId);
                ViewBag.deptName = deptName = adao.getDeptName(deptCode);
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
            adao.setCompleteTime(patientId, deptCode);
            //更新病人队列信息 1(就诊) -> 2(完成)
            adao.updateStatus(patientId, deptCode, 2);
            //返回当前病人下一站要去的地方
            string NxtDeptInfo = adao.GetNxtDeptInfo(patientId);
            return Json(NxtDeptInfo);
        }

        public JsonResult getFirstUserInfo()
        {
            try
            {
                //获取科室下第一个人的信息
                string UserInfo = adao.GetFirstUserInfo(deptCode);

                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, string> dic = jss.Deserialize<Dictionary<string, string>>(UserInfo);
                patientId = adao.getPatientId(dic["name"]);

                //更新病人Call_time Clinic_id,Doctor_id
                adao.updatePatientCCDStatus(patientId, deptCode, clinicId, doctorId);
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
            adao.overNumber(patientId, deptCode);
            return getFirstUserInfo();
        }
        public JsonResult getFirstDeptInfo(string patientid)
        {
            return Json(adao.GetNxtDeptInfo(patientid));
        }
        public JsonResult GetUserInfo(string patientid)
        {
            string UserInfo = adao.GetUserInfo(patientid);
            //更新病人的队列status 0（等待） - > 1 （就诊）
            adao.updateStatus(patientId, deptCode, 1);
            return Json(UserInfo);
        }
    }
}