﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using schema.Dao;
using Newtonsoft.Json;
using schema.Utills;
using System.Threading;
using System.Speech.Synthesis;

namespace schema.Service
{
    //大屏显示相关业务
    public class ScreenSerice
    {
        ScreenDao screendao = new ScreenDao();
        private ClinicDao clinicDao = new ClinicDao();
        private DepartDao departDao = new DepartDao();
        private PatientDao patientDao = new PatientDao();
        private RoomManageDao roomDao = new RoomManageDao(); 
      
         

        public string GetVoiceURL(string queid, string departcode, string serverpath)
        {
            View_Screen data = screendao.GetView_BigScreemDisplayById(queid, departcode);
            //将眼科一诊室的语言改成眼科衣诊室{解决眼科一（yi 四声）诊室}的问题

            //文字部分模板替换
            string text = "";
            text = "请[num]号，[name]到[pos], [dep]";
            text = text.Replace(Constant.SPEECH_PATIENTNUM, data.PATIENT_ID);
            text = text.Replace(Constant.SPEECH_PATIENTNAME, data.NAME);
            text = text.Replace(Constant.SPEECH_ROOMPOSITION, Convert.ToString(roomDao.GetRoomByKey(Convert.ToString(data.DEPT_CODE)).FLOOR));
            text = text.Replace(Constant.SPEECH_ROOM, data.CLINIC_NAME);
            text = text.Replace("_", "");

            string[] args = new string[] { text, serverpath, "" };  //文本 服务器路径 返回值

            //开启线程执行语音合成
            Thread t = new Thread(GenerateVoice);
            t.Start(args);
            t.Join();

            return args[2];
        }

        public string getPrePatientName(string deptCode, decimal clinicId)
        {
            string prePId = patientDao.getId(deptCode, clinicId, 2);
            string prePName = patientDao.getName(prePId);
            return prePName;
        }
        public string getNxtDeptName(string deptCode, decimal clinicId)
        {
            string prePId = patientDao.getId(deptCode, clinicId, 2);
            string nxtdeptCode = departDao.getId(prePId, 0);
            return departDao.getName(nxtdeptCode);
        }

        public List<String> waittingPatients(string deptCode)
        {
            List<string> ids = patientDao.getIds(deptCode, 0, 0);
            List<string> names = new List<string>();
            foreach(string id in ids)
            {
                names.Add(patientDao.getName(id));
            }
            return names;
        }

        public string getCurPatientName(string deptCode, decimal clinicId)
        {
            string curpId = patientDao.getId(deptCode, clinicId, 1);
            string curpName = patientDao.getName(curpId);
            return curpName;
        }

        internal string getDeptName(string deptCode)
        {
            return departDao.getName(deptCode);
        }

        public string getDeptCode(decimal clinicId)
        {
            return departDao.getId(clinicId);
        }

        public decimal getClinicId(string IP)
        {
            return clinicDao.getId(IP);
        }

        /// <summary>
        /// 语音合成线程
        /// </summary>
        /// <param name="obj"></param>
        private void GenerateVoice(object obj)
        {

            string text;
            text = ((string[])obj)[0];
            //语音合成对象
            SpeechSynthesizer speech = new SpeechSynthesizer();
            //加载语音设置
            // int volum = Convert.ToInt32(setsDao.GetSetByTypeAndItem("Speech", "volum"));    //音量
            // int rate = Convert.ToInt32(setsDao.GetSetByTypeAndItem("Speech", "speed"));     //语速
            speech.Volume = 100;
            speech.Rate = 0;
            //项目物理路径
            string voicefilepath = ((string[])obj)[1];
            string voicefilename = Convert.ToString(Guid.NewGuid().ToString("D"));
            voicefilepath += "Resources\\Voices\\" + voicefilename + ".wav";


            //返回值
            try
            {
                //从服务器生成语音配置文件
                speech.SetOutputToWaveFile(voicefilepath);
                speech.Speak(text);
                speech.SetOutputToNull();
            }
            catch (Exception e)
            {
                ((string[])obj)[2] = "Error:" + e.Message;
                return;

            }

            ((string[])obj)[2] = voicefilepath.Replace(((string[])obj)[1], "");
        }


    }
}