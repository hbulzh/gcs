using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using schema.Dao;

namespace schema.Service
{
    /// <summary>
    /// 设置相关业务
    /// </summary>
    public class SetRoomService
    {
        private SetDao setsDao = new SetDao();
        /// <summary>
        /// 得到语音设置相关设置项
        /// </summary>
        /// <returns>返回语音设置</returns>
        public IDictionary<string, object> GetVoiceSet()
        {
            IDictionary<string, object> set = new Dictionary<string, object>();
            set.Add("speed", setsDao.GetSetByTypeAndItem("Speech", "speed"));   //语速
            set.Add("volum", setsDao.GetSetByTypeAndItem("Speech", "volum"));   //音量
            set.Add("count", setsDao.GetSetByTypeAndItem("Speech", "Count"));   //叫号次数
            set.Add("Template", setsDao.GetSetByTypeAndItem("Speech", "Template"));     //初诊模板
            set.Add("Template_Back", setsDao.GetSetByTypeAndItem("Speech", "Template_Back"));   //回诊模板
            set.Add("batch_template", setsDao.GetSetByTypeAndItem("Speech", "batch_template"));     //批量叫号模板
            set.Add("leftWaitCount", setsDao.GetSetByTypeAndItem("bigScrrenDisplay", "leftWaitCount"));     //大屏左侧显示人数
            return set;
        }
        /// <summary>
        /// 得到楼层时间设置
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, object> GetFloorSet()
        {
            IDictionary<string, object> set = new Dictionary<string, object>();
            set.Add("FloorTime", setsDao.GetSetByTypeAndItem("Floor", "FloorTime"));   //跨楼层时间
            set.Add("FloorEditor", setsDao.GetSetByTypeAndItem("Floor", "FloorEditor"));//确定编辑
            return set;
        }
        /// <summary>
        /// 楼层时间设置
        /// </summary>
        /// <param name="item"></param>
        /// <param name="value"></param>
        public void FloorSetsChange(string item, string value)
        {
            setsDao.ChangeSetsByTypeAndItem("Floor", item, value);
        }
        /// <summary>
        /// 修改语音设置
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="value">值</param>
        public void VoiceSetsChange(string item, string value)
        {
            if (item.Equals("leftWaitCount"))
            {
                setsDao.ChangeSetsByTypeAndItem("bigScrrenDisplay", "leftWaitCount", value);
                return;
            }
            setsDao.ChangeSetsByTypeAndItem("Speech", item, value);

        }

        /// <summary>
        /// 得到语音模板
        /// </summary>
        /// <returns>返回语音模板</returns>
        public string GetSpeechTemplate()
        {
            return setsDao.GetSetByTypeAndItem("Speech", "Template");
        }

        /// <summary>
        /// 修改语音模板
        /// </summary>
        /// <param name="newtemplate">新的语音模板</param>
        public void SpeechTemplateChange(string newtemplate)
        {
            setsDao.ChangeSetsByTypeAndItem("Speech", "Template", newtemplate);
        }

        /// <summary>
        /// 得到设置Log
        /// </summary>
        /// <returns></returns>
        public string GetLogs()
        {

            return setsDao.GetSetByTypeAndItem("HospitalInformation", "log");
        }

        /// <summary>
        /// 得到医院名称
        /// </summary>
        /// <returns></returns>
        public string GetHospitalNmae()
        {

            return setsDao.GetSetByTypeAndItem("HospitalInformation", "Name");
        }

        /// <summary>
        /// 设置医院的logs
        /// </summary>
        /// <param name="logs">log地址</param>
        public void ChangeLogs(string logs)
        {
            setsDao.ChangeSetsByTypeAndItem("HospitalInformation", "log", logs);
        }
        /// <summary>
        /// 设置医院名称
        /// </summary>
        /// <param name="name">医院名称</param>
        public void ChangeName(string name)
        {
            setsDao.ChangeSetsByTypeAndItem("HospitalInformation", "Name", name);
        }
    }
}