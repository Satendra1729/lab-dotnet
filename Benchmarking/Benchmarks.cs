using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using Newtonsoft.Json.Linq;

namespace Benchmarking
{

  public class Benchmarks
    {

        [Benchmark]
        public void Scenario1()
        {
            getResponseStateParam(new HttpRequest(),"sClientId");
        }
        
        [Benchmark]
        public void Scenario2()
        {
            
        }

          private string getResponseStateParam(HttpRequest request, string sClientId)
        {
            void replaceNullToEmptyString(JObject jObject)
            {
                foreach (var key in jObject)
                {
                    if (jObject[key.Key].GetType() == typeof(JObject))
                        replaceNullToEmptyString((JObject)jObject[key.Key]);
                    jObject[key.Key] = key.Value.ToString();
                }

            }

            string sOrigUri = "getOrigUriFromStateParam()";

            // creating inner object 
            JObject keyValues = new JObject();

            foreach (string name in request.QueryString.Keys)
                keyValues[name] = request.QueryString[name];
            if (!string.IsNullOrEmpty(sOrigUri))
                keyValues["m_csOrigUriFieldName"] = sOrigUri;
            else if (!string.IsNullOrEmpty("RedirectUrlParam"))
                keyValues["m_csOrigUriFieldName"] = "RedirectUrlParam";
            else
                keyValues["m_csOrigUriFieldName"] = "request.Url.AbsoluteUri";

            // end inner jobect creation 

            // create update outer jsong object
            JObject sJson = new JObject();
            sJson["m_csClientQueryFieldName"] = keyValues;
            sJson["ClientIdParamName"] = sClientId;
            sJson["m_csSpecificFieldName"] = "m_csSpecificValue";

            replaceNullToEmptyString(sJson);
            return sJson.ToString( Newtonsoft.Json.Formatting.None);
        }

    }

    internal class HttpRequest
    {
        public Dictionary<string,string> QueryString {get;set;} = new Dictionary<string, string>{
            {"key1","value1"},
            {"key2","value1"}
        };
    }
}
