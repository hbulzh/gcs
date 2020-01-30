using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using schema.Dao;
namespace schema.Controllers
{
    public class AlgorithmController : Controller
    {
        // GET: Algorithm
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Algo(string patientid)
        {
            
            ViewBag.Message = "获取科室信息";
            return View();
        }
        public JsonResult algo_json(string patientid)
        {
            return Json(new AlgorithmDao().GetDeptInfo(patientid));
        }
        public ActionResult CallNumber()
        {
            return View();
        }
    }
}