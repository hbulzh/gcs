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
        private DepartDao departDao = new DepartDao();
        private VoiceDao voicDao = new VoiceDao();
        private PatientDao patientDao = new PatientDao();
        private ClinicDao clinicDao = new ClinicDao();
        /// <summary>
        /// 得到当前科室的所有等待队列
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
      
        internal string getdepartName(string deptCode)
        {
            return departDao.getName(deptCode);
        }
        /// <summary>
        /// 当前科室下status = 0， 1 的病人
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public List<String> getAllPatients(string deptId)
        {
            List<String> pids = patientDao.getIds(deptId, 0, 1);
            List<String> pnames = new List<string>();
            foreach(string pid in pids)
            {
                pnames.Add(patientDao.getName(pid));
            }
            return pnames;
        }

        /// <summary>
        /// 获取当前科室下所有诊室名称
        /// </summary>
        /// <param name="departid"></param>
        /// <returns></returns>
        public List<String> getClinicNames(string departid)
        {
            return clinicDao.getNames(departid);
        }
        /// <summary>
        /// 获取当前科室下所有诊室的上一个病人 下一站
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public List<String[]> getPatientAndDept(string deptCode)
        {
            List<string[]> patientAndDept = new List<string[]>();
            List<decimal> cids = clinicDao.getIds(deptCode);
            foreach(decimal cid in cids)
            {
                string patientid = patientDao.getId(deptCode, cid, 2);
                string patientName = patientDao.getName(patientid);
                string nxtDeptCode = departDao.getId(patientid, 0);
                string nxtDeptName = departDao.getName(nxtDeptCode);
                patientAndDept.Add(new string[] { patientName, nxtDeptName});
            }
            return patientAndDept;
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