using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Trackify.SpotifyApi
{
    public class ApiManager
    {
        public async Task<string> GET(string url)
        {
            //log begin of connection
            try
            {
                using (var client = new HttpClient())
                {
                    string responseString = await client.GetStringAsync(url);
                    //log successfull connection
                    return responseString;
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                return null;
            }
        }
        public static async Task<string> Auth_Get(string url, string AuthenticationToken)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                requestMessage.Headers.Add("Authorization", ("Bearer "+ AuthenticationToken));
                HttpResponseMessage response = await client.SendAsync(requestMessage);
                string responseAsString = await response.Content.ReadAsStringAsync();
                return responseAsString;
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                return null;
            }
        }
    }
}
