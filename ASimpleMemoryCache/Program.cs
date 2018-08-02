﻿using Jason.common;
using Jason.Dll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASimpleMemoryCache
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                {      
                        //{
                        //    for (int i = 0; i < 5; i++)
                        //    {
                        //    Console.WriteLine($"Obtain {nameof(DBHelper)} for {i} times {DateTime.Now.ToString("yyyyMMdd HHmmss.fff")}");
                        //    List<Program> programList = null;
                        //    string key = $"{nameof(DBHelper)}_Query_{nameof(Program)}_{123}";

                        //        //Encapusulate this part by delegation.
                        //        //if (!CustomCache.Exist(key))
                        //        //{
                        //        //    programList = DBHelper.Query<Program>(123);
                        //        //    CustomCache.Add(key, programList);
                        //        //}
                        //        //else
                        //        //{
                        //        //    programList = CustomCache.Get<List<Program>>(key);
                        //        //}

                        //        programList = CustomCache.FindOrAdd(key, () => DBHelper.Query<Program>(123));
                        //        Console.WriteLine($"The {i} time you fetch the data is {programList.GetHashCode()}");
                        //    }
                        //}
                        //{
                        //    for (int i = 0; i < 5; i++)
                        //    {
                        //    Console.WriteLine($"Obtain {nameof(FileHelper)} for {i} times {DateTime.Now.ToString("yyyyMMdd HHmmss.fff")}");
                        //    List<Program> programList = null;
                        //    Console.WriteLine("==FileHelper==");
                        //        string key = $"{nameof(FileHelper)}_Query_{nameof(Program)}_456";
                        //        programList = CustomCache.FindOrAdd(key, () => (FileHelper.Query<Program>(456)));
                        //        Console.WriteLine($"The {i} time you fetch the data is {programList.GetHashCode()}");
                        //    }
                        //}
                        //{
                        //    for (int i = 0; i < 5; i++)
                        //    {
                        //    Console.WriteLine($"Obtain {nameof(RemoteHelper)} for {i} times {DateTime.Now.ToString("yyyyMMdd HHmmss.fff")}");
                        //    List<Program> programList = null;
                        //    Console.WriteLine("==RemoteHelper==");
                        //        string key = $"{nameof(RemoteHelper)}_Query_{nameof(Program)}_456";
                        //        programList = CustomCache.FindOrAdd(key, () => (RemoteHelper.Query<Program>(456)));
                        //        Console.WriteLine($"The {i} time you fetch the data is {programList.GetHashCode()}");
                        //    }
                        //}                       
                        
                    }

                //Handle the issue of data in chache changed.
                //E.g. User authorities, user->role->menu, generally stable, suit to use chache.
                //1. When a user's authorities changed, just remove it.
                {
                    //2. When a menu is changed, should not go through the whole cache or delete all , just delete the items
                    // which are relavent to the menu
                    string key = "_System_Menu";
                    CustomCache.RemoveCondition(s => s.Contains("_Menu_"));
                }


                

                Console.Read();
            }catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
