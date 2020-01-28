using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using schema.Service;

namespace schema.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult item()
        {
            return View();
        }
    }
}