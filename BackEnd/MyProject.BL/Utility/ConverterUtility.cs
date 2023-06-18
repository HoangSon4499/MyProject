using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BL.Utility
{
    public class ConverterUtility
    {
        #region ========== JSON ==========
        public static readonly DateFormatHandling dateFormatHandling = DateFormatHandling.IsoDateFormat;
        public static readonly DateTimeZoneHandling dateTimeZoneHandling = DateTimeZoneHandling.Local;
        public static readonly string dateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ssfffk";
        public static readonly NullValueHandling nullValueHandling = NullValueHandling.Include;
        public static readonly ReferenceLoopHandling referenceLoopHandling = ReferenceLoopHandling.Ignore;

        private static Dictionary<string, JsonSerializerSettings> dicJsonSerializerSettings = new Dictionary<string, JsonSerializerSettings>();

        /// <summary>
        /// Hàm custom lại format
        /// </summary>
        /// <param name="ignoreNullValue"></param>
        /// <returns></returns>
        public static JsonSerializerSettings GetJsonSerializerSettings(bool ignoreNullValue = false)
        {
            var key = $"{ignoreNullValue}";
            if (!dicJsonSerializerSettings.ContainsKey(key))
            {
                var settings = new JsonSerializerSettings()
                {
                    DateFormatHandling = dateFormatHandling,
                    DateTimeZoneHandling = dateTimeZoneHandling,
                    DateFormatString = dateFormatString,
                    NullValueHandling = nullValueHandling,
                    ReferenceLoopHandling = referenceLoopHandling,
                };
                if (ignoreNullValue)
                {
                    settings.NullValueHandling = NullValueHandling.Ignore;
                }
                dicJsonSerializerSettings.TryAdd(key, settings);
            }
            return dicJsonSerializerSettings[key];
        }

        /// <summary>
        /// Hàm convert về string
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="customSettings"></param>
        /// <returns></returns>
        public static string Serialize(object obj, JsonSerializerSettings customSettings)
        {
            return JsonConvert.SerializeObject(obj, customSettings ?? GetJsonSerializerSettings());
        }

        /// <summary>
        /// Hàm convert về string có bỏ qua giá trị null hay không
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="customSettings"></param>
        /// <returns></returns>
        public static string Serialize(object obj, bool ignoreNullValue = false)
        {
            return JsonConvert.SerializeObject(obj, GetJsonSerializerSettings(ignoreNullValue));
        }

        /// <summary>
        /// Hàm convert từ string thành model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, GetJsonSerializerSettings());
        }

        /// <summary>
        /// Hàm convert từ string thành model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static object Deserialize(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type, GetJsonSerializerSettings());
        }
        #endregion
    }
}
