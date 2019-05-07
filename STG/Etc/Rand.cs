using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class Rand
    {
        static Random rnd = new Random();
        public int randomNumber = rnd.Next(100);
    }
}
