using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace SincciKC.SsoLogin.SSOMethod
{
    public class GetDataByPlatform
    {
        /// <summary>
        /// 根据地址和参数来从平台中获取数据
        /// </summary>
        /// <param name="address"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public string GetDataByParameterInPost(string address, string parameter)
        {
            byte[] bs = Encoding.UTF8.GetBytes(parameter);
            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(address);
            request1.Method = "POST";
            request1.ContentLength = bs.Length;
            request1.ContentType = "application/json; charset=UTF-8";
            //request1.Headers["Access-Token"] = token;
            request1.Headers["Client-Id"] = ConfigurationManager.AppSettings["Client-Id"];
            request1.Headers["Client-Secret"] = ConfigurationManager.AppSettings["Client-Secret"];
            Stream reqStream = request1.GetRequestStream();
            reqStream.Write(bs, 0, bs.Length);
            reqStream.Close();
            HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
            StreamReader responseStream1 = new StreamReader(response1.GetResponseStream());
            string jsontemp = responseStream1.ReadToEnd();
            return jsontemp;
        }

        public string GetDataByOnlyAddressInGet(string address)
        {
            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(address);
            request1.Method = "GET";
            request1.Headers["Client-Id"] = "1d98bbaa-0507-49f4-a3dc-ddd51f479d86";
            request1.Headers["Client-Secret"] = "f60fb940-d7f2-459f-ab08-dc110f9502a3";
            HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
            StreamReader responseStream1 = new StreamReader(response1.GetResponseStream());
            string jsontemp = responseStream1.ReadToEnd();
            return jsontemp;
        }
    }
}