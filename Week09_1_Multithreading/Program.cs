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

Console.ReadLine();
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