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
            
            ViewBag.Message = "科室信息";
            return View();
        }
        public JsonResult algo_json(string patientid)
        {
            ViewBag.num = ViewBag.num - 1;
            return Json(adao.GetDeptInfo(patientid));
        }
        public ActionResult CallNumber()
        {
            ViewBag.nxtDept = "123";
            ViewBag.num = 100;
            ViewBag.nxtDept = "1231";
            ViewBag.nxtDeptNum = 456;
            return View();
        }
        public JsonResult NxtPatient(string deptcode,int deptnum)
        {
            return Json("123:456");
            if (ViewBag.num==0) return Json(adao.GetUserInfo(deptcode, deptnum));
            else return Json("error");
        }
        /// <summary>
        /// 显示下一个病人信息到页面上
        /// </summary>
        public JsonResult OverNumber(string patientid,string deptcode,int deptnum)
        {
            adao.overNumber(patientid,deptcode);
            return NxtPatient(deptcode, deptnum);
        }
    }
}