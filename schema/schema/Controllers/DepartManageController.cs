using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using schema.Service;

namespace schema.Controllers
{
    public class DepartManageController : Controller
    {
        private DepartManageService depepartManageService = new DepartManageService();
        // GET: DepartManage
        public ActionResult Index()
        {
            //用户的相关信息
            ViewBag.userkist = depepartManageService.GetAllDepart();
            return View();
        }
        /// <summary>
        /// 修改科室信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="departCode"></param>
        /// <param name="bigNum"></param>
        /// <param name="smartNum"></param>
        /// <param name="predictTime"></param>
        /// <param name="synchronTime"></param>
        /// <returns></returns>
        
        [HttpPost]
        public ActionResult Changedepart(string departId, string departCode, decimal bigNum, decimal smartNum,decimal predictTime,decimal dynamicTime, DateTime synchronTime )
        {
            T_DEPART_MANAGE depart = depepartManageService.GetDepartByKey(Convert.ToInt32(departId));

            depart.DEPT_CODE = departCode;
            depart.BIGSCREEN_NUM = bigNum;
            depart.SMALLSCREEN_NUM = smartNum;
            depart.PREDICT_TIME = predictTime;
            depart.DYNAMIC_TIME = dynamicTime;

            depart.SYNCHRON_TIME = synchronTime;
            depepartManageService.ChangeDepart(depart);
            return RedirectToAction("Index");
        }
    }
}