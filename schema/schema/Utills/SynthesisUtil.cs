using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace schema.Utills
{
    public class SynthesisUtil
    {
        private static List<IDictionary<string, string>> CallingList = new List<IDictionary<string, string>>();

        public static int ERROR_COUNT = 0;

        //public static List<IDictionary<string, string>> CallingList1 { get => CallingList; set => CallingList = value; }

        public static List<IDictionary<string, string>> CallingList1()
        {
            return CallingList;
        }

        /// <summary>
        /// 添加病人到等待呼叫的队列中
        /// </summary>
        /// <param name="queid">队列ID</param>
        /// <returns>1:成功 0:已存在，但不执行覆盖操作</returns>
        public static int AddSinglePatientToCalling(string queid,string departcode)
        {
            lock (CallingList)
            {
                foreach (IDictionary<string, string> dict in CallingList)
                {
                    if (dict.Keys.Contains("single"))
                    {
                        if (Convert.ToString(dict["single"]) == queid&& dict["depart"]== departcode)
                        {
                            return 0;
                        }
                    }
                }

                IDictionary<string, string> newcall = new Dictionary<string, string>();
                newcall.Add("single", queid);
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