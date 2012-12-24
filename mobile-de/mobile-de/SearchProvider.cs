using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace mobile_de
{
    static class SearchProvider
    {
        private const string API = "http://services.mobile.de/1.0.0/ad/search?";

        public static SearchResult Search(string uName, string password)
        {
            SearchResult result = new SearchResult();
            // testing
            string r = HttpGet(uName, password, API + "exteriorColor=BLACK&modificationTime.min=2012-05-04T18:13:51.0Z");
            Console.WriteLine("response: " + r);

            return result;
        }

        private static string HttpGet(string uName, string passwd, string url)
        {
            HttpWebRequest req = WebRequest.Create(url)
                                 as HttpWebRequest;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            CredentialCache credentialCache = new CredentialCache();
            credentialCache.Add(new System.Uri(url), "Basic", new NetworkCredential(uName, passwd));

            req.Credentials = credentialCache;
            req.PreAuthenticate = true;

            string result = null;
            using (HttpWebResponse resp = req.GetResponse()
                                          as HttpWebResponse)
            {
                StreamReader reader =
                    new StreamReader(resp.GetResponseStream());
                result = reader.ReadToEnd();
            }
            return result;
        }
    }
}
