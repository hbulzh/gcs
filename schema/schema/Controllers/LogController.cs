﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using schema.Service;

namespace schema.Controllers
{
    public class LogController : Controller
    {
        // GET: Log
        public ActionResult log()
        {
            return View();
        }
    }
}