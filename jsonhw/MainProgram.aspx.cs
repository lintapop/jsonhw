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
            string webContent =
                GetWebContent(
                    "https://data.kcg.gov.tw/dataset/b82b0880-e965-497a-9ba3-73ecd0773b85/resource/208c61e6-5338-474f-9b91-f458681fe489/download/-.json");
            MainProgram discounterInfo = JsonConvert.DeserializeObject<MainProgram>(webContent);
            string table = @" < table style = ""width: 100 % "" >
                        <tr>
                        <th>dataOrg</th>
                        <th>doOrg</th>
                        <th>hlink</th>
                        <th>id</th>
                        <th>informtel</th>
                        <th>lat</th>
                        <th>lng</th>
                        <th>servItem</th>
                        <th>servTime</th>
                        <th>text</th>
                        </tr>";
            for (int i = 0; i < park; i++)
            {
            }

            //            for (int i = 0; i < park.data.Length; i++)
            //            {
            //                table += $@"<tr>
            //<td> { park.data[i].seq}</td>
            //<td> { park.data[i].行政區}</td>
            //<td> { park.data[i].臨時停車處所}</td>
            //<td> { park.data[i].可提供小型車停車位}</td>
            //<td> { park.data[i].地址}</td>
            //</tr>";
            //            }
            //            table += "</table>";
            //            literalMessage.Text = table;

            table += "</table>";
            LiteralMessage.Text = table;
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