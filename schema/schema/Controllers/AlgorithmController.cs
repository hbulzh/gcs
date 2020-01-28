﻿using System;
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
            AlgorithmDao algo = new AlgorithmDao();
            string deptcode = algo.GetDeptCode(patientid);
            ViewBag.Message = deptcode;
            return View();
        }
        public JsonResult algo_json(HttpContext context)
        {
            return Json("name:123");
        }
    }
}