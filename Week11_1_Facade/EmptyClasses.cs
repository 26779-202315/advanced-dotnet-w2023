using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week11_1_Facade
{
    //Facade Class
    class FacadeClass
    {
        private SubSystemA ssA;
        private SubSystemB ssB;
        private SubSystemC ssC;


        public FacadeClass()
        {
            ssA = new SubSystemA();
            ssB = new SubSystemB();
            ssC = new SubSystemC();
        }

        public void Operation1()
        {
            Console.WriteLine("\nMethod1() ----");
            ssB.MethodB();
            ssA.MethodA();
            ssC.MethodC();
        }

        public void Operation2()
        {
            Console.WriteLine("\nMethod2() ----");
            ssC.MethodC();
            ssA.MethodA();
            ssB.MethodB();
        }
    }



    // A 'Subsystem' class
    class SubSystemA
    {
        public void MethodA()
        {
            Console.WriteLine("SubSystemA Method");
        }
    }

    // A 'Subsystem' class
    class SubSystemB
    {
        public void MethodB()
        {
            Console.WriteLine("SubSystemA Method");
        }
    }

    // A 'Subsystem' class
    class SubSystemC
    {
        public void MethodC()
        {
            Console.WriteLine("SubSystemA Method");
        }
    }
}
