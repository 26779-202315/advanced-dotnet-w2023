using System.Reflection;

namespace Week09_1_Multithreading
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Inside the method: {MethodBase.GetCurrentMethod().Name}");

            Console.WriteLine($"Managed Thread Id: {Thread.CurrentThread.ManagedThreadId}");

            //DoWorK();
            //DoWorkWithParameter(5);

            UseThreads();

            Console.WriteLine("Invoking 'UserThreadPool' method");
            UseThreadPool();
            //Thread.Sleep( 2000 );
            
            Console.WriteLine("Invoking 'UserPLINQ' method");
            UsePLINQ();


            Console.ReadLine();
        }

        private static void UseThreadPool()
        {
            ThreadPool.QueueUserWorkItem(DoWorkThreadPool);
        }

        private static void DoWorkThreadPool(object state)
        {
            Console.WriteLine($"Inside the method {MethodBase.GetCurrentMethod().Name}");

            Console.WriteLine($"Thread Pool Managed Id: {Thread.CurrentThread.ManagedThreadId}");


            int num = 0;

            //for (int i = 0; i < 9; i++)
            for (int i = 0; i < 5000000; i++)
            {
                num = i;
            }

            Console.WriteLine($"Num: {num}"); //4,999,999
        }
        
        private static void UsePLINQ()
        {
            Console.WriteLine($"Inside the method {MethodBase.GetCurrentMethod().Name}");

            Console.WriteLine($"Thread Pool Managed Id: {Thread.CurrentThread.ManagedThreadId}");

            Console.WriteLine("Doing some work with PLINQ...");
            List<int> numbers = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                numbers.Add(i);
            }

            Console.WriteLine("Calling AsParallel without DegreesOfParallelism");

            foreach (var num in numbers.Where(n => n > -1).AsParallel())
            {
                Console.WriteLine(num);
            }


            Console.WriteLine("Calling AsParallel with DegreesOfParallelism");

            foreach (var num in numbers.Where(n => n > -1).OrderBy(n => n).AsParallel().WithDegreeOfParallelism(Environment.ProcessorCount))
            {
                Console.WriteLine(num);
            }


            Console.WriteLine("Calling AsParallel with AND AsOrdered DegreesOfParallelism");
            foreach (var num in numbers.Where(n => n > -1).AsParallel().AsOrdered().WithDegreeOfParallelism(Environment.ProcessorCount))
            {
                Console.WriteLine(num);
            }


        }

        private static void DoWorK()
        {
            Console.WriteLine($"Inside the method: {MethodBase.GetCurrentMethod().Name}");

            Console.WriteLine($"Managed Thread Id: {Thread.CurrentThread.ManagedThreadId}");

            Thread.Sleep(2000);
            Console.WriteLine("Doing some work in DoWork()....");
        }

        private static void DoWorkWithParameter(object param)
        {
            Console.WriteLine($"Inside the method: {MethodBase.GetCurrentMethod().Name}");

            Console.WriteLine($"Managed Thread Id: {Thread.CurrentThread.ManagedThreadId}");

            Console.WriteLine($"Result: {Convert.ToInt32(param) * Convert.ToInt32(param)}.... ");
        }

        private static void UseThreads()
        {
            Console.WriteLine($"Inside the method: {MethodBase.GetCurrentMethod().Name}");

            var thread = new Thread(DoWorK);
            Console.WriteLine($"Thread state BEFORE start: {thread.ThreadState}");

            thread.Start();
            Console.WriteLine($"Thread state AFTER start: {thread.ThreadState}");


            var parameterizedThread = new Thread(DoWorkWithParameter);
            Console.WriteLine($"Parameterized Thread state BEFORE start: {parameterizedThread.ThreadState}");

            parameterizedThread.Start(5);
            Console.WriteLine($"Parameterized Thread state AFTER start: {parameterizedThread.ThreadState}");

        }
    }
}