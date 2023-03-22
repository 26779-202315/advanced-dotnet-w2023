using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week10_1_Singleton
{
    class Singleton
    {
        private static readonly Singleton _instance = new Singleton();

        ////Not Thread Safe
        //private static Singleton _instance;

        // Constructor is 'private'
        private Singleton()
        {

        }

        public static Singleton GetInstance()
        {
            ////Not Thread Safe
            //if(_instance == null)
            //{
            //    _instance = new Singleton();
            //}

            return _instance;
        }
    }
}
