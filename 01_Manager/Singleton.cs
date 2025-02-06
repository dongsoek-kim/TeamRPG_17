using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class Singleton<T> where T : class, new()
    {
        private static T instance;
        public static T Instance 
        {             
            get
            {
                if(instance == null)
                    instance = new T();
                return instance;
            }
        }
    }
}
