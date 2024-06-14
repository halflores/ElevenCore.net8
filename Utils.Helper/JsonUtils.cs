using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using System.Xml;

namespace Utils.Helper
{
    public static class JsonUtils
    {
        static readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            Formatting = Newtonsoft.Json.Formatting.Indented,
            NullValueHandling = NullValueHandling.Include,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Error
        };

        public static string ToJson<TTarget>(this TTarget aTarget, ILogger logger)
        {
            try
            {
                return JsonConvert.SerializeObject(aTarget, jsonSettings);
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                logger.LogError(ex, "ToJson");
                throw;
            }
        }

        public static string ToJsonNull<TTarget>(this TTarget aTarget, ILogger logger)
        {
            try
            {
                JsonSerializerSettings jsonSettings = new JsonSerializerSettings
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Error
                };
                return JsonConvert.SerializeObject(aTarget, jsonSettings);
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                logger.LogError(ex, "ToJsonNull");
                throw;

            }
        }

        public static string ToJsonCaseNull<TTarget>(this TTarget aTarget, ILogger logger)
        {
            try
            {
                JsonSerializerSettings jsonSettings = new JsonSerializerSettings
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Error,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                return JsonConvert.SerializeObject(aTarget, jsonSettings);
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                logger.LogError(ex, "ToJsonCaseNull");
                throw;
            }
        }

        public static TTarget ToObject<TTarget>(this string strSource, ILogger logger)
        {
            try
            {
                return JsonConvert.DeserializeObject<TTarget>(strSource, jsonSettings);
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                logger.LogError(ex, "ToObject");
                throw;
            }
        }

        public static TTarget ToJObject<TTarget>(this string strSource, ILogger logger)
        {
            try
            {
                string strMatch = string.Empty;
                Regex regex = new Regex(@"\{(.|\s)*\}");
                Match match = regex.Match(strSource);
                if (match.Success)
                {
                    JObject o = JObject.Parse(match.Value);
                    strMatch = o["value"].ToString();
                }
                return JsonConvert.DeserializeObject<TTarget>(strMatch, jsonSettings);
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                logger.LogError(ex, "ToJObject");
                throw;
            }
        }
        public static TTarget BToObject<TTarget>(this string strSource, ILogger logger)
        {
            try
            {
                string strMatch = string.Empty;
                Regex regex = new Regex(@"\{(.|\s)*\}");
                Match match = regex.Match(strSource);
                if (match.Success)
                {
                    strMatch = match.Value.ToString();
                }
                return JsonConvert.DeserializeObject<TTarget>(strMatch, jsonSettings);
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                logger.LogError(ex, "BToObject");
                throw;
            }
        }

        public static TTarget ToFileObject<TTarget>(this string strSource, ILogger logger)
        {
            try
            {
                using (StreamReader r = new StreamReader(strSource))
                {
                    string json = r.ReadToEnd();
                    return json.ToObject<TTarget>(logger);
                }
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                logger.LogError(ex, "ToFileObject");
                throw;
            }
        }
        public static void ToObjetFile<TTarget>(this TTarget oValue, string PathFileName, ILogger logger)
        {
            try
            {
                JsonSerializerSettings jsonSettings = new JsonSerializerSettings
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Error,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                using (FileStream fs = new FileStream(Path.Combine(PathFileName), FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        JsonSerializer jsonSerializer = JsonSerializer.Create(jsonSettings);
                        jsonSerializer.Serialize(sw, oValue);
                    }
                }
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                logger.LogError(ex, "ToObjetFile");
                throw;
            }
        }

        public static TTarget ToObject<TTarget>(this string strSource, string format, ILogger logger)
        {
            try
            {
                var formato = new IsoDateTimeConverter { DateTimeFormat = format };
                return JsonConvert.DeserializeObject<TTarget>(strSource, formato);
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                logger.LogError(ex, "ToObject");
                throw;
            }
        }

        public static List<TTarget> ResponseToObject<TTarget>(this string strResponse, ILogger logger)
        {
            try
            {
                Regex regex = new Regex(@"\{(.|\s)*\}");
                string[] data = Regex.Split(strResponse, "--changesetresponse_");
                List<TTarget> ListaTTarget = new List<TTarget>();
                for (int i = 1; i < data.Length - 1; i++)
                {
                    string res = regex.Match(data[i]).Value;
                    if (!string.IsNullOrEmpty(res))
                    {
                        ListaTTarget.Add(res.ToObject<TTarget>(logger));
                    }
                }
                return ListaTTarget;
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                logger.LogError(ex, "ToObject");
                throw;
            }
        }

        public static T DeserializeJsonFromStream<T>(this Stream stream, ILogger logger)
        {
            try
            {
                if (stream == null || stream.CanRead == false)
                    return default(T);

                using (var sr = new StreamReader(stream))
                {
                    using (var jtr = new JsonTextReader(sr))
                    {
                        var js = new Newtonsoft.Json.JsonSerializer();
                        var searchResult = js.Deserialize<T>(jtr);
                        return searchResult;
                    }
                }
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                logger.LogError(ex, "ToObject");
                throw;
            }
        }

        public static TTarget ToObject<TTarget>(this JObject o, string property, ILogger logger)
        {
            try
            {
                var e = o.GetValue(property, StringComparison.OrdinalIgnoreCase)?.Value<JToken>();
                return e == null ? default : e.ToObject<TTarget>();
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                logger.LogError(ex, "ToObject");
                throw;
            }
        }
        public static string ConvertXMLtoJSON(this string xml, ILogger logger)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                string json = JsonConvert.SerializeXmlNode(doc);
                return json;
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                logger.LogError(ex, "ConvertXMLtoJSON");
                throw;
            }

        }
    }
}
