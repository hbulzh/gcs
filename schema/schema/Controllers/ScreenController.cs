using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using schema.Service;
using schema.Utills;
using Newtonsoft.Json;

namespace schema.Controllers
{
    public class ScreenController : Controller
    {


        private ScreenSerice screenService = new ScreenSerice();
        // GET: Screen
        public ActionResult Screen(string IP)
        {
            ViewBag.clinicId = screenService.getClinicId(IP);
            ViewBag.deptCode = screenService.getDeptCode(ViewBag.clinicId);
            ViewBag.deptName = screenService.getDeptName(ViewBag.deptCode);
            ViewBag.curPName = screenService.getCurPatientName(ViewBag.deptCode, ViewBag.clinicId);
            ViewBag.waittingPatients = screenService.waittingPatients(ViewBag.deptCode);
            ViewBag.prePName = screenService.getPrePatientName(ViewBag.deptCode, ViewBag.clinicId);
            ViewBag.nxtDeptName = screenService.getNxtDeptName(ViewBag.deptCode,ViewBag.clinicId);
            return View();
        }
        /// <summary>
        /// 大屏显示右边的内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpPost]
        //public ActionResult ScrrenDisplay(string id)
        //{
        //  return Json(screenScrice.BigScreenDisplay(id), JsonRequestBehavior.AllowGet);
        // }
        [HttpGet]
        public ActionResult OnCalling()
        {
            //从静态资源中拷贝所有的待叫号信息
            List<IDictionary<string, string>> list = new List<IDictionary<string, string>>(SynthesisUtil.CallingList1().ToArray());

            string et = "";
            string errpath = Server.MapPath("~\\Resources\\Exception\\screrrlog.txt");  //大屏日志文件目录

            try
            {

                //当前请求的路径和端口
                string serverpath = Server.MapPath("~");
                string host = Request.Url.Host;

                //左边区域数据总容器，包含着每一次叫号的信息
                List<IDictionary<string, object>> leftarea = new List<IDictionary<string, object>>();

                foreach (IDictionary<string, string> idct in list)
                {
                    IDictionary<string, object> result = new Dictionary<string, object>();
                    if (idct.Keys.Contains("single")&& idct.Keys.Contains("depart") )   //单个叫号
                    {

                        string queid = Convert.ToString(idct["single"]);    //得到患者的queid
                        string depart = idct["depart"];
                        //判断这个人是否还在，不在就删掉
                        //  Dao.QueDao queDao = new Dao.QueDao();
                        // if (queDao.GetQueById(queid) == null)
                        //  {
                        //    SynthesisUtil.RemoveOneNode(idct);
                        //   System.IO.File.AppendAllText(errpath, "\r\n因为队列中没有此人，已自动删掉！QueId:" + queid);
                        //   continue;
                        //  }

                        //  View_Screen showcontent = screenScrice.GetLeftContent(queid);     //上方正在叫号信息
                        string voice = screenService.GetVoiceURL(queid, depart, serverpath);     //调用语音合成，返回语音文件的URL                                                      
                        WaveInfo waveInfo = new WaveInfo(serverpath + voice);   //得到语音文件的信息           

                        result.Add("Voice", host + ":" + Request.Url.Port + "\\" + voice);      //语音文件URL
                        result.Add("Type", "Single");   //类型




                    }

                    leftarea.Add(result);
                }

                IDictionary<string, object> json = new Dictionary<string, object>();
                json.Add("LeftArea", leftarea);

                json.Add("PlayCount", 2);   //叫号次数
                return Json(JsonConvert.SerializeObject(json), JsonRequestBehavior.AllowGet);
            }


            catch (Exception ex)
            {
                //出错的时候模拟刷新
                string errstr = et.Replace(':', '|') + ex.ToString().Replace(':', '|');
                string serverpath = errpath;
                System.IO.File.AppendAllText(serverpath, "\r\n" + DateTime.Now + "--其他--" + errstr);
                //return Json(JsonConvert.SerializeObject(errobj), JsonRequestBehavior.AllowGet);

                //防止出错太多
                SynthesisUtil.ERROR_COUNT++;
                if (SynthesisUtil.ERROR_COUNT >= 5)
                {
                    SynthesisUtil.ERROR_COUNT = 0;
                    IDictionary<string, string> errobj = new Dictionary<string, string>();
                    errobj.Add("Exception", ex.ToString());
                    return Json(JsonConvert.SerializeObject(errobj), JsonRequestBehavior.AllowGet);
                }

                return Json("{\"Error\":\"null\"}", JsonRequestBehavior.AllowGet);
            }
        }
    }
}




