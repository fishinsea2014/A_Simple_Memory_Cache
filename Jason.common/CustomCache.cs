using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jason.common
{
    public class CustomCache
    {
        //private static Dictionary<string, KeyValuePair<object, DateTime>> CustomCacheDictionary;// Static ensure global unique and donot release.
        private static ConcurrentDictionary<string, KeyValuePair<object, DateTime>> CustomCacheDictionary;

        static CustomCache()
        {
            //CustomCacheDictionary = new Dictionary<string, KeyValuePair<object, DateTime>>();
            CustomCacheDictionary = new ConcurrentDictionary<string, KeyValuePair<object, DateTime>>();
            Console.WriteLine($"{DateTime.Now.ToString("MM-dd HH:mm:ss fff")} initiate the cache"); //Record when the system restart

            //Actively clean up expired items
            while (true)
            {
                Task.Run(() =>
                {
                    List<string> list = new List<string>();
                    foreach (var key in CustomCacheDictionary.Keys)
                    {
                        var valueTime = CustomCacheDictionary[key];
                        if (valueTime.Value > DateTime.Now)
                        {
                            //Not expired, do nothing
                        }
                        else
                        {
                            list.Add(key);
                        }
                    }
                    //list.ForEach(key => CustomCacheDictionary.Remove(key));
                    KeyValuePair<object, DateTime> removedPair ;
                    list.ForEach(key => CustomCacheDictionary.TryRemove(key, out removedPair));

                });
                Thread.Sleep(1000 * 60 * 60); // Do the cleaning every 10 minutes
            }
         }

        

        public static void Add (string key, object value, int second = 1800)
        {
            KeyValuePair<object, DateTime> addedPair;

            CustomCacheDictionary.TryAdd(key,new KeyValuePair<object,DateTime>(value, DateTime.Now.AddSeconds(second)));

            
        }

        /// <summary>
        /// If the item exist in dict, then recover it, or add a new one.
        /// Remove the items which are out of date when they are accessed.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SaveOrUpdate(string key, object value, int second=1800)
        {
            CustomCacheDictionary[key] = new  KeyValuePair<object,DateTime>(value,DateTime.Now.AddSeconds(second));
        }

        public static T Get <T>(string key)
        {
            return (T)CustomCacheDictionary[key].Key; // Why?
        }

        public static bool Exist(string key)
        {
            if (CustomCacheDictionary.ContainsKey(key))
            {
                var valueTime = CustomCacheDictionary[key];
                if (valueTime.Value > DateTime.Now)
                {
                    return true;
                }
                else
                {
                    //CustomCacheDictionary.Remove(key); //If the item is out of date, remove it.
                    KeyValuePair<object, DateTime> removedPair;
                    CustomCacheDictionary.TryRemove(key, out removedPair);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static void Remove(string key)
        {
            //CustomCacheDictionary.Remove(key);
            KeyValuePair<object, DateTime> removedPair;
            CustomCacheDictionary.TryRemove(key, out removedPair);
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
            //list.ForEach(key => CustomCacheDictionary.Remove(key));
            KeyValuePair<object, DateTime> removedPair;
            list.ForEach(key => CustomCacheDictionary.TryRemove(key, out removedPair));
        }

    }
}
