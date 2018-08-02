using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jason.common
{
    public class CustomCache
    {
        private static Dictionary<string, object> CustomCacheDictionary;// Static ensure global unique and donot release.

        static CustomCache()
        {
            CustomCacheDictionary = new Dictionary<string, object>();
            Console.WriteLine($"{DateTime.Now.ToString("MM-dd HH:mm:ss fff")} initiate the cache"); //Record when the system restart

        }

        public static void Add (string key, object value)
        {
            CustomCacheDictionary.Add(key, value);
        }

        /// <summary>
        /// If the item exist in dict, then recover it, or add a new one.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SaveOrUpdate(string key, object value)
        {
            CustomCacheDictionary[key] = value;
        }

        public static T Get <T>(string key)
        {
            return (T)CustomCacheDictionary[key];
        }

        public static bool Exist(string key)
        {
            return CustomCacheDictionary.ContainsKey(key);
        }

    }
}
