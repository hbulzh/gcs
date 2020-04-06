using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using schema.Dao;
using schema.Service;
using System.Web.Script.Serialization;
using schema.Models;
namespace schema.Controllers
{
    public class AlgorithmController : Controller
    {
        AlgorithmDao adao = new AlgorithmDao();
        AlgorithmService aService = new AlgorithmService();

        public ActionResult Login()
        {
            return View();
        }
        // GET: Algorithm
        public ActionResult Algo(string IP)
        {

            ViewBag.deptName = aService.getDeptName(IP);
            ViewBag.clinicId = adao.getClinicId(IP);
            ViewBag.doctorId = 123;
            return View();
        }

        public JsonResult getNxtDeptInfo(string patientName,string deptName)
        {
            if (patientName == "") return Json("");
            return Json(aService.getNxtDeptInfo(patientName, deptName));
        }

        public JsonResult getFirstUserInfo(string deptName,decimal clinicId,decimal doctorId)
        {


            return Json(aService.getFirstUserInfo(deptName, clinicId, doctorId));   
        }

        public int getDeptNumber(string deptName)
        {
            //status = 0;
            return aService.getDeptNumber(deptName);
            
        }
        /// <summary>
        /// 显示下一个病人信息到页面上
        /// </summary>
        public JsonResult OverNumber(string patientName,string deptName)
        {
            //回滚病人状态Call_time Clinic_id,Doctor_id status (1（就诊) -> 0（等待）)
            rollback(patientName,deptName);
            //当前人过号 并返回下一位病人
            return Json(aService.OverNumber(patientName, deptName));
            
        }
        public JsonResult getFirstDeptInfo(string patientid)
        {
            return Json(adao.GetNxtDeptInfo(patientid));
        }
        public void addQueue(string patientid)
        {
            adao.addQueue(patientid);
        }
        public JsonResult GetUserInfo(string patientid)
        {
            return Json(adao.GetUserInfo(patientid));
        }
        public void rollback(string patientName,string deptName)
        {
            if (patientName == "") return;
            aService.updatePatientStatus(patientName,deptName);
        }
    }
}