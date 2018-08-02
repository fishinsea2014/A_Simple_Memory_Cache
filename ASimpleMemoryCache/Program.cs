using Jason.common;
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
                    for (int i = 0; i < 5; i++)
                    {
                        Console.WriteLine($"Obtain {nameof(DBHelper)} for {i} times {DateTime.Now.ToString("yyyyMMdd HHmmss.fff")}" );
                        List<Program> programList = null;
                        string key = $"{nameof(DBHelper)}_Query_{nameof(Program)}_{123}";

                        if (!CustomCache.Exist(key))
                        {
                            programList = DBHelper.Query<Program>(123);
                            CustomCache.Add(key, programList);
                        }
                        else
                        {
                            programList = CustomCache.Get<List<Program>>(key);
                        }

                        //
                        Console.WriteLine($"The {i} time you fetch the data is {programList.GetHashCode()}");
                    }
                }

                Console.Read();
            }catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
