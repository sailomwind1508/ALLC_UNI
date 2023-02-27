using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace AllCashUFormsApp
{
    public static class BaseDao
    {
        public class IBaseDao
        {
            protected string ClassName { get; set; }

            public List<T> Reverse<T>(Func<T, bool> predicate)
            {
                var result = new T[10];
                int j = 0;
                for (int i = result.Length - 1; i >= 0; i--)
                {
                    result[j] = result[i];

                    j++;
                }
                return result.Where(predicate).ToList();
            }

           
        }
    }
}
