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
        /// <summary>
        /// Use delegation to fina an item, if not exist, then add a new one.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T FindOrAdd<T> (string key, Func<T> func)
        {
            T t = default(T);
            if (!Exist(key))
            {
                t = func.Invoke();
                CustomCache.Add(key, t);
            }
            else
            {
                t = Get<T>(key);
            }

            return t;
        }

        public static void RemoveAll()
        {
            CustomCacheDictionary.Clear();
        }

        public static void RemoveCondition(Func<string, bool> func)
        {
            List<string> list = new List<string>();
            foreach (var key in CustomCacheDictionary.Keys)
            {
                if (func.Invoke(key))
                {
                    list.Add(key);
                }                
            }
            list.ForEach(key => CustomCacheDictionary.Remove(key));
        }

    }
}
