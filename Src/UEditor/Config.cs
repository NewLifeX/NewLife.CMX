using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using NewLife.Reflection;
using NewLife.Serialization;

namespace UEditor
{
    /// <summary>
    /// Config 的摘要说明
    /// </summary>
    public static class Config
    {
        private static bool noCache = true;
        private static Object BuildItems()
        {
            var json = File.ReadAllText(HttpContext.Current.Server.MapPath("config.json"));

            var jp = new JsonParser(json);
            return jp.Decode();
        }

        public static IDictionary<String, Object> Items
        {
            get
            {
                if (noCache || _Items == null)
                {
                    _Items = BuildItems() as IDictionary<String, Object>;
                }
                return _Items;
            }
        }
        private static IDictionary<String, Object> _Items;

        public static T GetValue<T>(string key)
        {
            return Items[key].ChangeType<T>();
        }

        public static String[] GetStringList(string key)
        {
            //return Items[key].Select(x => x.Value<String>()).ToArray();
            return Items[key] as String[];
        }

        public static String GetString(string key)
        {
            return GetValue<String>(key);
        }

        public static int GetInt(string key)
        {
            return GetValue<int>(key);
        }
    }
}