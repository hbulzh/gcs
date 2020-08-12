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
        /// <summary>
        /// 根据ip信息初始化科室信息
        /// </summary>
        /// <param name="IP"></param>
        /// <returns></returns>
        public ActionResult Algo(string IP)
        {

            ViewBag.deptName = aService.getDeptName(IP);
            ViewBag.clinicId = adao.getClinicId(IP);
            ViewBag.doctorId = (decimal)123;
            aService.rollback(ViewBag.deptName, ViewBag.clinicId);
            return View();
        }
        /// <summary>
        /// 获取 当前科室下病人 下一站的地方
        /// </summary>
        /// <param name="patientName"></param>
        /// <param name="deptName"></param>
        /// <returns></returns>
        public JsonResult getNxtDeptInfo(string patientId,string deptName)
        {

            if (patientId == "" || deptName == "") return Json("");
            return Json(aService.getNxtDeptInfo(patientId, deptName));
        }
        /// <summary>
        /// 获取 当前科室 下第一个人信息
        /// clinicid 和  docterid 更新队列状态使用
        /// </summary>
        /// <param name="deptName"></param>
        /// <param name="clinicId"></param>
        /// <param name="doctorId"></param>
        /// <returns></returns>
        public JsonResult getFirstUserInfo(string deptName,decimal clinicId,decimal doctorId)
        {
            if (deptName == "") return Json("");

            return Json(aService.getFirstUserInfo(deptName, clinicId, doctorId));   
        }

        /// <summary>
        /// 获取科室下的人数
        /// </summary>
        /// <param name="deptName"></param>
        /// <returns></returns>
        public int getDeptNumber(string deptName)
        {
            //status = 0;
            if (deptName == "") return 0;
            return aService.getDeptNumber(deptName);
            
        }
        /// <summary>
        /// 过号
        /// </summary>
        public JsonResult OverNumber(string patientId,string deptName,decimal clinicId,decimal doctorId)
        {
            if (patientId == "" || deptName == "") return Json("");
            //回滚病人状态Call_time Clinic_id,Doctor_id status (1（就诊) -> 0（等待）)
            aService.rollback(deptName, clinicId);
            //当前人过号 
            aService.OverNumber(patientId, deptName);
            //并返回下一位病人
            return Json(aService.getFirstUserInfo(deptName, clinicId, doctorId));
            
        }
        /// <summary>
        /// 初始化病人第一个要去的科室信息
        /// </summary>
        /// <param name="patientid"></param>
        /// <returns></returns>
        public JsonResult getFirstDeptInfo(string patientid)
        {
            if (patientid == "") return Json("");
            return Json(adao.GetNxtDeptInfo(patientid));
        }
        /// <summary>
        /// 病人体检项目全部入队
        /// </summary>
        /// <param name="patientid"></param>
        public void addQueue(string patientid)
        {
            if (patientid == "") return;
            adao.addQueue(patientid);
        }
        /// <summary>
        /// 根据病人patientid获取 patient详细信息
        /// </summary>
        /// <param name="patientid"></param>
        /// <returns></returns>
        public JsonResult GetUserInfo(string patientid)
        {
            if (patientid == "") return Json("");
            return Json(adao.GetUserInfo(patientid));
        }
    }
}