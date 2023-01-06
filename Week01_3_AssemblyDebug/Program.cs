namespace Week01_3_AssemblyDebug
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var assembly = typeof(Program).Assembly;

            // the code base is where the assembly code resides
            Console.WriteLine(assembly.CodeBase);

            // the location is the location on the disk where the assembly resides
            Console.WriteLine(assembly.Location);

            Console.WriteLine("printing defined types");
            // the exported types contain
            // ALL the types defined within an assembly
            foreach (var definedType in assembly.DefinedTypes)
            {
                Console.WriteLine(definedType.AssemblyQualifiedName);
            }

            Console.WriteLine("printing exported types");
            // the exported types contain
            // ALL THE PUBLIC TYPES defined within an assembly
            foreach (var exportedType in assembly.ExportedTypes)
            {
                Console.WriteLine(exportedType.AssemblyQualifiedName);
            }

            Console.WriteLine("printing entry point");

            Console.WriteLine(assembly.EntryPoint.Name);

            // *****Debug Walkthrough*****
            var debugClass = new DebugClass();

            debugClass.MyProperty = "hello world";

            debugClass.ChangeData();

            debugClass.MyMethodOne();
            debugClass.MyMethodTwo();
            debugClass.MyRecursiveMethod(5);

            Console.WriteLine(debugClass.Data);

            Console.WriteLine("program complete");
            Console.ReadKey();
        }
    }

    class Person
    {
        public Person()
        {

        }

        public string FirstName { get; set; }
    }

    public class Circle
    {
        public Circle()
        {

        }

        public Circle(double radius)
        {
            if (radius <= 0)
            {
                throw new ArgumentException("The radius cannot be less than or equal to 0", nameof(radius));
            }
        }

        public double Radius { get; set; }
    }

    class DebugClass
    {
        public DebugClass()
        {
            this.Data = "unchanged";
        }

        public string Data { get; set; }

        public string MyProperty { get; set; }

        public void ChangeData()
        {
            this.Data = "changed";
        }

        public void MyMethodOne()
        {
            Console.WriteLine("my method one");
        }

        public void MyMethodTwo()
        {
            Console.WriteLine("my method two");
        }

        public int MyRecursiveMethod(int value)
        {
            if (value == 0)
            {
                return 0;
            }
            else
            {
                value = value - 1;
                return this.MyRecursiveMethod(value);
            }
        }

    }
}