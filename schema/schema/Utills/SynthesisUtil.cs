using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Threading;
using System.Web;
using schema.Dao;

namespace schema.Utills
{



    public class SynthesisUtil
    {




       static   ScreenDao screendao = new ScreenDao();
      static   DepartManageDao departdao = new DepartManageDao();
        static RoomManageDao roomDao = new RoomManageDao();
      static   AlgorithmDao algorithmDao = new AlgorithmDao();
        static VoiceDao voiceDao = new VoiceDao();

       /// <summary>
       /// 合成语音信息
       /// </summary>
       /// <param name="patientid">检查人的id</param>
       /// <param name="departcode">检查科室</param>
       /// <param name="clincid"> 检查诊室</param>
        public   static  void  GetVoiceURL(string patientid, string departcode, decimal clincid)
        {
            View_Screen data = screendao.GetView_BigScreemDisplayById(patientid, departcode);
            //将眼科一诊室的语言改成眼科衣诊室{解决眼科一（yi 四声）诊室}的问题

            //文字部分模板替换
            string text = "";
            text = "请[num]号，[name]到[pos], [dep]";
            text = text.Replace(Constant.SPEECH_PATIENTNUM, data.PATIENT_ID);
            text = text.Replace(Constant.SPEECH_PATIENTNAME, data.NAME);
            text = text.Replace(Constant.SPEECH_ROOMPOSITION, Convert.ToString(roomDao.GetRoomByKey(Convert.ToString(data.DEPT_CODE)).FLOOR));
            text = text.Replace(Constant.SPEECH_ROOM, data.CLINIC_NAME);
            text = text.Replace("_", "");


            string[] args = new string[] { text,  "" };  //文本 服务器路径 返回值



            //开启线程执行语音合成
            Thread t = new Thread(GenerateVoice);
            t.Start(args);
            t.Join();


            if(voiceDao.FindVoice( patientid, departcode,  clincid)==0)
            {
                T_VOICE voice = new T_VOICE();
                voice.DEPT_CODE = departcode;
                voice.PATIENT_ID = patientid;
                voice.CLINIC_ID = clincid;
                voice.Voices = args[1];
                //将这个声音实体插入数据库
                voiceDao.addVoice(voice);

            }
            else
            {
                string message = "该声音已经加入数据库";
            }
            




        }
        /// <summary>
        /// 语音合成线程
        /// </summary>
        /// <param name="obj"></param>
        private  static void GenerateVoice(object obj)
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
            voicefilepath +=  voicefilename + ".wav";


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
                ((string[])obj)[1] = "Error:" + e.Message;
                return;

            }

            
        }






        private static List<IDictionary<string, string>> CallingList = new List<IDictionary<string, string>>();

        public static int ERROR_COUNT = 0;

        //public static List<IDictionary<string, string>> CallingList1 { get => CallingList; set => CallingList = value; }

        public static List<IDictionary<string, string>> CallingList1()
        {
            return CallingList;
        }
        /// <summary>
        /// 将这个人的语音信息加进来
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="departcode"></param>
        /// <param name="clincid"></param>
        /// <returns></returns>
       
        public static int AddSinglePatientToCalling(string patientid,string departcode  ,decimal clincid)
        {




            lock (CallingList)
            {
                foreach (IDictionary<string, string> dict in CallingList)
                {
                    if (dict.Keys.Contains("single"))
                    {
                        if (Convert.ToString(dict["single"]) == patientid && dict["depart"]== departcode)
                        {
                            return 0;
                        }
                    }
                }

                IDictionary<string, string> newcall = new Dictionary<string, string>();
                newcall.Add("single", patientid);
                newcall.Add("depart", departcode);
                CallingList.Add(newcall);
            }

            return 1;
        }

        /// <summary>
        /// 从待呼叫队列中删除
        /// </summary>
        /// <param name="queid">队列ID</param>
        /// <returns>1:删除成功 0:要删除的项不存在，删除失败</returns>
        public static int RemovePatientFromCalling(string queid)
        {
            lock (CallingList)
            {
                foreach (IDictionary<string, string> dict in CallingList)
                {
                    if (Convert.ToString(dict["single"]) == queid)
                    {
                        CallingList.Remove(dict);
                        return 1;
                    }
                }
            }
            return 0;
        }

       

        public static void RemoveOneNode(IDictionary<string, string> dict)
        {
            lock (CallingList)
            {
                if (CallingList.Find(x => x == dict) != null)
                {
                    CallingList.Remove(dict);
                }
            }
        }

        public static bool IsNULL()
        {
            return CallingList.Count == 0 ? true : false;
        }

        public static void ClaerAll()
        {
            lock (CallingList)
            {
                CallingList.Clear();
            }
        }

    }
}