using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace jsonhw
{
    public partial class MainProgram : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) //頁面第一次加載時（或重新整理後），所要執行的事件
            {
                this.Title = "Discounter Info - Open Data";

                string webContent =
                    GetWebContent(
                        "https://data.kcg.gov.tw/dataset/b82b0880-e965-497a-9ba3-73ecd0773b85/resource/208c61e6-5338-474f-9b91-f458681fe489/download/-.json");

                // Deserialization = convert JSON string to custom .Net object
                var discounterInfo = JsonConvert.DeserializeObject<DiscounterInfo>(webContent);
                //Session["discounter"] = discounterInfo;
                //formulate a html message.InnerHtml head
                message.InnerHtml += "<CAPTION><h1>高雄市政府資訊</h1></CAPTION> <table>";

                message.InnerHtml += "\t<tr>\n";
                message.InnerHtml += $"\t\t<th>id</th>\n";
                message.InnerHtml += $"\t\t<th>lat</th>\n";
                message.InnerHtml += $"\t\t<th>lng</th>\n";
                message.InnerHtml += $"\t\t<th>address</th>\n";
                message.InnerHtml += $"\t\t<th>hlink</th>\n";
                message.InnerHtml += $"\t\t<th>tel</th>\n";
                message.InnerHtml += $"\t\t<th>servItem</th>\n";
                message.InnerHtml += $"\t\t<th>servTime</th>\n";
                message.InnerHtml += $"\t\t<th>dataOrg</th>\n";
                message.InnerHtml += $"\t\t<th>doOrg</th> \n";
                message.InnerHtml += $"\t\t<th>text</th>\n";
                message.InnerHtml += $"\t\t</ tr >\n";

                //formulate html table rows using loop

                for (int i = 0; i < discounterInfo.orgs.frg.org.Length; i++)
                {
                    message.InnerHtml += "\t<tr>\n";
                    message.InnerHtml += $"\t\t <td> { discounterInfo.orgs.frg.org[i].id}</td>\n";
                    message.InnerHtml += $"\t\t<td> { discounterInfo.orgs.frg.org[i].lat}</td>\n";
                    message.InnerHtml += $"\t\t<td> { discounterInfo.orgs.frg.org[i].lng}</td>\n";
                    message.InnerHtml += $"\t\t<td> { discounterInfo.orgs.frg.org[i].informaddress}</td>\n";
                    message.InnerHtml += $"\t\t<td> { discounterInfo.orgs.frg.org[i].hlink}</td>\n";
                    message.InnerHtml += $"\t\t<td> { discounterInfo.orgs.frg.org[i].informtel}</td>\n";
                    message.InnerHtml += $"\t\t<td> { discounterInfo.orgs.frg.org[i].servItem}</td>\n";
                    message.InnerHtml += $"\t\t<td> { discounterInfo.orgs.frg.org[i].servTime}</td>\n";
                    message.InnerHtml += $"\t\t<td> { discounterInfo.orgs.frg.org[i].dataOrg}</td>\n";
                    message.InnerHtml += $"\t\t<td> { discounterInfo.orgs.frg.org[i].doOrg}</td>\n";
                    message.InnerHtml += $"\t\t<td> { discounterInfo.orgs.frg.org[i].text}</td>\n";
                    message.InnerHtml += $"\t</tr>\n";
                }
                message.InnerHtml += "</table>";
            }
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        private static string GetWebContent(string Url)
        {
            var uri = new Uri(Url);
            var request = WebRequest.Create(Url) as HttpWebRequest;
            // If required by the server, set the credentials.
            request.UserAgent = "*/*";
            request.Credentials = CredentialCache.DefaultCredentials;
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            // 重點是修改這行
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;// SecurityProtocolType.Tls1.2;
                                                                              // Get the response.
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }
    }
}