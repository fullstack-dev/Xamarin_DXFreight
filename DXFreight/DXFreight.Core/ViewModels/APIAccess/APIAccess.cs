using DXFreight.Core.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DXFreight.Core.APIAccess
{
    public class APIAccess
    {
        public static UserInfo userinfo = null;
        public static LogoutInfo logoutresult = null;


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
    }
}
