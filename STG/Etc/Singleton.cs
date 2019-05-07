using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    public sealed  class Singleton
    {
        public int score = 0;

        public int itemhaving = 0;

        public int itemcount = 0;

        public int bomblimit = 1;

        public bool movebomb_flag = false;

        public static  Singleton singleton = new Singleton();
        
       public static Singleton Getsingleton()
        {
            return singleton;
        }

        private Singleton()
        {
            
        }

    }
}
