using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using schema.Dao;
using schema.Utills;
using Newtonsoft.Json;

namespace schema.Service
{

       
    public class ScreensService
    {

        ScreenDao screendao = new ScreenDao();
        DepartManageDao departdao = new DepartManageDao();
        RoomManageDao roomDao = new RoomManageDao();
        AlgorithmDao algorithmDao = new AlgorithmDao();
        VoiceDao voicDao = new VoiceDao();

        /// <summary>
        /// 得到当前科室的所有等待队列
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<View_Screen_depart> ScreenDisplay(string id)
        {
            return screendao.GetScreenQueue(id);
        }
        /// <summary>
        /// 得到所在科室诊室的数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  List<View_Screen> GetClincsByDepartCode(string id)
        {
            return screendao.GetClincsByDepartCode(id);
        }

        /// <summary>
        /// 得到该诊室正在就诊的病人
        /// </summary>
        /// <param name="ClincId"></param>
        /// <param name="departid"></param>
        /// <returns></returns>
        public   string GetClinicPatienName( string  departid, decimal ClincId)
        {
            return screendao.GetClincName(departid, ClincId);
        }
        /// <summary>
        /// 得到该诊室正在就诊的病人
        /// </summary>
        /// <param name="ClincId"></param>
        /// <param name="departid"></param>
        /// <returns></returns>
        public string GetClinicPatienid(string departid, string  name)
        {
            return screendao.getPatienID(departid, name);
        }

        /// <summary>
        /// 根据科室id和诊室名返回诊室id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public decimal GetClincID (string id, string name)
        {
            return screendao.GetClincID(id, name);
        }
        /// <summary>
        /// 返回下一个人的地方
        /// </summary>
        /// <param name="patientid"></param>
        /// <returns></returns>
        public String GetNexDept(string patientid)
        {

            Dictionary<string, string> dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(algorithmDao.GetNxtDeptInfo(patientid));
            string departNamr = dic["deptname"];
            return departNamr;
        }

       /// <summary>
       /// 根据检查人id和检查诊室得出声音
       /// </summary>
       /// <param name="Patienid">检查人id</param>
       /// <param name="departID">诊室id</param>
       /// <returns></returns>
        public   string   GetVoice(string Patienid,string departID)
        {
            IDictionary<string, object> dic = new Dictionary<string, object>();
            string voice = voicDao.GetVoice(Patienid, departID);
            WaveInfo waveinfo = new WaveInfo(voice);
            int playnum = 2;
            dic.Add("Voice", voice);//语音信息
            dic.Add("Time", waveinfo.Second);//语音时长
            dic.Add("playnum", playnum);
            return JsonConvert.SerializeObject(dic);

        }




    }
}