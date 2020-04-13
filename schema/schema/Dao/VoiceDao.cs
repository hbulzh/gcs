using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace schema.Dao
{
    /// <summary>
    /// 语音叫号信息
    /// </summary>
    public class VoiceDao
    {
        private Entities db = new Entities();

        /// <summary>
        /// 添加声音
        /// </summary>
        /// <param name="user"></param>
        public void addVoice(T_VOICE  user)
        {
            db.T_VOICE.Add(user);    //直接在数据库上下文的表添加数据
            db.SaveChanges();   //可以同时添加多条数据，直接在最后SaveChanges保存就行,返回值为影响的行数
        }
        /// <summary>
        /// 查找这个实体
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="departid"></param>
        /// <param name="clincid"></param>
        /// <returns></returns>
        public int   FindVoice (string  patientid,string departid,decimal clincid)
        {

            T_VOICE voice = db.T_VOICE.Where(x => x.DEPT_CODE == departid && x.PATIENT_ID == patientid&&x.CLINIC_ID== clincid).ToList()[0];
            if (voice==null)
            {
                return 0;
            }
            else
            {
                return 1;
            }

          
       
        }

        /// <summary>
        /// 根据科室id和病人id 查找声音
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="departid"></param>
        /// <returns></returns>
        public   string  GetVoice (string patientid,string departid)
        {
           string  voice = db.T_VOICE.Where(x => x.DEPT_CODE == departid && x.PATIENT_ID == patientid).ToList()[0].Voices;

            return voice;

        }
       

    }
}