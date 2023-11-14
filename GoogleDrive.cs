using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Google.GData.Client;
using Google.GData.Spreadsheets;

namespace HSDeckImporter
{
    public class GoogleDrive
    {
        public static OAuth2Parameters LoadParameters(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(OAuth2Parameters));
            TextReader tr = System.IO.File.OpenText(filename);

            return serializer.Deserialize(tr) as OAuth2Parameters;
        }

        public static void CreateParameters(OAuth2Parameters parameters, string filename, string accessCode)
        {
            parameters.AccessCode = accessCode;
            OAuthUtil.GetAccessToken(parameters);

            XmlSerializer serializer = new XmlSerializer(typeof(OAuth2Parameters));
            System.IO.TextWriter tw = System.IO.File.CreateText(filename);

            serializer.Serialize(tw, parameters);
        }

        public static string GetAuthorizationUrl(OAuth2Parameters parameters, string clientId, string clientSecret, string redirectUrl, string scope)
        {
            parameters.ClientId = clientId;
            parameters.ClientSecret = clientSecret;
            parameters.RedirectUri = redirectUrl;
            parameters.Scope = scope;

            parameters.ApprovalPrompt = "force";

            return OAuthUtil.CreateOAuth2AuthorizationUrl(parameters);
        }

        public static SpreadsheetsService GetSpreadsheetService(OAuth2Parameters parameters, string applicationName)
        {
            GOAuth2RequestFactory requestFactory = new GOAuth2RequestFactory(null, applicationName, parameters);
            SpreadsheetsService service = new SpreadsheetsService(applicationName);
            service.RequestFactory = requestFactory;

            return service;
        }
    }
}
