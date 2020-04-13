using System;
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
        DepartManageDao departdao = new DepartManageDao();
        RoomManageDao roomDao = new RoomManageDao();
        AlgorithmDao algorithmDao = new AlgorithmDao();

        /// <summary>
        /// 大屏显示
        /// </summary>
        /*
        public string BigScreenDisplay(string id)
        {
            IDictionary<string, string> map = new Dictionary<string, string>();
            List<View_Screen> firstlist = screendao.GetScreenQueue(id);
            string queue = JsonConvert.SerializeObject(firstlist);
            map.Add("FirstVist", queue);
            map.Add("room", departdao.GetDepartByDepartId(id).DEPT_NAME);
            return  JsonConvert.SerializeObject(map);
        }
        */
        public  List<View_Screen_depart> ScreenDisplay(string id)
        {
            return screendao.GetScreenQueue(id);
        }
        /// <summary>
        /// 根据科室id得到科室名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       public  string   GetdepartNamebyid(string id)
        {
            return departdao.GetDepartByDepartId(id);
        }
        /// <summary>
        /// 获取当前科室的第一个人
        /// </summary>
        /// <param name="deptcode"></param>
        /// <returns></returns>

        public string GetfistPatient(string deptcode)
        {

                  

           // Dictionary<string, string > dic = JsonConvert.DeserializeObject<Dictionary<string, string >>(algorithmDao.GetFirstUserInfo(deptcode));

           // string name = dic["name"];
            return screendao.GetScreenPatientQueue(deptcode);
        }
        /// <summary>
        /// 得到当前人检查的状态
        /// </summary>
        /// <param name="name"></param>
        /// <param name="departid"></param>
        /// <returns></returns>
         public   int  getStutes(string name ,string departid)
        {
            return screendao.GetStatus(name, departid);
        }
        /// <summary>
        /// 得到当前病人的下一个地方
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
         public   string GetNexdepart(string name)
        {
            string patientId = algorithmDao.getPatientId(name);
            //对json反序列化成字典
            Dictionary<string, string> dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(algorithmDao.GetNxtDeptInfo(patientId));
            string departNamr = dic["deptname"];
            return departNamr;
        }

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