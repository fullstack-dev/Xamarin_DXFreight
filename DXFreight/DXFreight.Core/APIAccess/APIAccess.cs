using DXFreight.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DXFreight.Core.APIAccess
{
    public class APIAccess
    {
        public static UserInfo userinfo = null;
        public static LogoutInfo logoutresult = null;
        public static DataInfo datainfo = null;
        public static PingInfo pinginfo = null;
        public static GetNextDataInfo getnextdatainfo = null;
        public static SetDataCompleteInfo setdatacompleteinfo = null;
        public static SetDataFailedInfo setdatafailedinfo = null;

        public async static Task<UserInfo> getSessionId(string base_url)
        {
            try
            {
                string contents;
                string url = String.Format(base_url);
                HttpClient client = new HttpClient();
                contents = await client.GetStringAsync(url);
                userinfo = JsonConvert.DeserializeObject<UserInfo>(contents);
                return userinfo;
            }
            catch(System.Exception sysExc)
            {
                throw;
            }
        }

        public async static Task<LogoutInfo> logOut(string base_url)
        {
            try
            {
                string contents;
                string url = String.Format(base_url);
                HttpClient client = new HttpClient();
                contents = await client.GetStringAsync(url);
                logoutresult = JsonConvert.DeserializeObject<LogoutInfo>(contents);
                return logoutresult;
            }
            catch (System.Exception sysExc)
            {
                throw;
            }
        }

        public async static Task<DataInfo> SendData(string base_url, StringContent content)
        {
            try
            {
                string url = String.Format(base_url);
                HttpClient client = new HttpClient();

                var response = await client.PostAsync(url, content);
                string result = response.Content.ReadAsStringAsync().Result;
                datainfo = JsonConvert.DeserializeObject<DataInfo>(result);
                return datainfo;
            }
            catch (System.Exception sysExc)
            {
                throw;
            }
        }

        public async static Task<PingInfo> Ping(string base_url)
        {
            try
            {
                string contents;
                string url = String.Format(base_url);
                HttpClient client = new HttpClient();
                contents = await client.GetStringAsync(url);
                pinginfo = JsonConvert.DeserializeObject<PingInfo>(contents);
                return pinginfo;
            }
            catch (System.Exception sysExc)
            {
                throw;
            }
        }

        public async static Task<GetNextDataInfo> GetNextData(string base_url)
        {
            try
            {
                string contents;
                string url = String.Format(base_url);
                HttpClient client = new HttpClient();
                contents = await client.GetStringAsync(url);
                getnextdatainfo = JsonConvert.DeserializeObject<GetNextDataInfo>(contents);
                return getnextdatainfo;
            }
            catch (System.Exception sysExc)
            {
                throw;
            }
        }

        public async static Task<SetDataCompleteInfo> SetDataComplete(string base_url)
        {
            try
            {
                string contents;
                //string url = String.Format(base_url);
                HttpClient client = new HttpClient();
                contents = await client.GetStringAsync(base_url);
                setdatacompleteinfo = JsonConvert.DeserializeObject<SetDataCompleteInfo>(contents);
                return setdatacompleteinfo;
            }
            catch (System.Exception sysExc)
            {
                throw;
            }
        }

        public async static Task<SetDataFailedInfo> SetDataFailed(string base_url)
        {
            try
            {
                string contents;
                //string url = String.Format(base_url);
                HttpClient client = new HttpClient();
                contents = await client.GetStringAsync(base_url);
                setdatafailedinfo = JsonConvert.DeserializeObject<SetDataFailedInfo>(contents);
                return setdatafailedinfo;
            }
            catch (System.Exception sysExc)
            {
                throw;
            }
        }
    }
}
