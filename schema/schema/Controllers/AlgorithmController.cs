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
        // GET: Algorithm
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Algo(string patientid)
        {
            
            ViewBag.deptName = "彩超";
            getFirstUserInfo(ViewBag.deptName);
            getDeptNumber(ViewBag.deptname);
            return View();
        }
        public JsonResult nxtPatient(string username)
        {
            string patientid = adao.getPatientId(username);
            getNxtPatient(patientid);
            getNxtDeptInfo(patientid);
            return Json(adao.GetNxtDeptInfo(patientid));
        }
        public void getNxtPatient(string patientid)
        {
            getFirstUserInfo(ViewBag.deptName);
            getNxtDeptInfo(patientid);
            getDeptNumber(ViewBag.deptName);
        }
        public void getNxtDeptInfo(string patientid)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string DeptInfo = adao.GetNxtDeptInfo(patientid);
            Dictionary<string, string> dic = jss.Deserialize<Dictionary<string, string>>(DeptInfo);
            ViewBag.nxtDeptName = dic["deptname"];
            ViewBag.nxtDeptnum = dic["deptnum"];
        }
        public void getFirstUserInfo(string deptName)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string UserInfo = adao.GetFirstUserInfo(adao.getDeptCode(deptName));
            Dictionary<string, string> dic = jss.Deserialize<Dictionary<string, string>>(UserInfo);
            ViewBag.name = dic["name"];
            ViewBag.age = dic["age"];
            ViewBag.sex = dic["sex"] == "1" ? "男":"女" ;
        }
        public ActionResult CallNumber()
        {
            ViewBag.nxtDept = "123";
            ViewBag.num = 100;
            ViewBag.nxtDept = "1231";
            ViewBag.nxtDeptNum = 456;
            return View();
        }
        public void getDeptNumber(string deptname)
        {
            ViewBag.deptNum = adao.GetDeptNum(adao.getDeptCode(deptname));
        }
        /// <summary>
        /// 显示下一个病人信息到页面上
        /// </summary>
        public void OverNumber(string username,string deptname)
        {
            string deptcode = adao.getDeptCode(deptname);
            string patientid = adao.getPatientId(username);
            adao.overNumber(patientid,deptcode);
            getFirstUserInfo(deptcode);
        }
    }
}