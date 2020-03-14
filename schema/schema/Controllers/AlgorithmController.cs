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
            
            ViewBag.clinic = "科室信息";
            return View();
        }

        public void algo_json(string patientid)
        {
            
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string DeptInfo = adao.GetDeptInfo(patientid);
            Dictionary<string, string> dic = jss.Deserialize<Dictionary<string, string>>(DeptInfo);
            ViewBag.nxtDeptName = dic["deptname"];
            ViewBag.nxtDeptnum = dic["deptnum"];
            ViewBag.clinicNum = ViewBag.clinicNum - 1;
        }
        public void getUserInfo(string deptcode,int deptnum)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string UserInfo = adao.GetUserInfo(adao.getDeptCodeByName(deptcode), deptnum);
            Dictionary<string, string> dic = jss.Deserialize<Dictionary<string, string>>(UserInfo);
            ViewBag.name = dic["name"];
            ViewBag.age = dic["age"];
            ViewBag.sex = dic["sex"];
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
            ViewBag.clinicNum = adao.GetDeptNum(adao.getDeptCodeByName(deptname));
        }
        /// <summary>
        /// 显示下一个病人信息到页面上
        /// </summary>
        public void OverNumber(string patientid,string deptcode,int deptnum)
        {
            adao.overNumber(patientid,deptcode);
            getUserInfo(deptcode, deptnum);
        }
    }
}