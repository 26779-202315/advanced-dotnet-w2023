using System.Diagnostics;
using System.Reflection;

namespace Week02_1_Attributes
{
    /// Represents the main program.
    /// </summary>
    // apply our custom attribute to our program class
    // note the suffix of attribute is missing, because it is implied
    [MyCustom("my custom value")]
    [MyCustom("my other custom value")]
    [MyCustom("my other custom value 3")]
    [MyCustom("my other custom value 4")]
    [MyCustom("my other custom value 5")]
    public class Program
    {

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            Console.ResetColor();
            Console.WriteLine("Printing information about attributes on the program class");

            // retrieve all the attributes of type MyCustomAttribute on the program class
            // and select the values from the retrieved attributes
            var values = typeof(Program).GetCustomAttributes<MyCustomAttribute>().Select(c => c.MyValue);

            // for each value, print the value of the attribute
            foreach (var value in values)
            {
                Console.WriteLine($"Value for custom attribute {value}");
            }

            Console.Write(Environment.NewLine);

            Console.WriteLine("Hello...");
            Console.WriteLine("Enter a command to run, or type help for help, or type exit to exit");

            var exit = false;

            do
            {
                var input = Console.ReadLine();

                if (input == "exit")
                {
                    exit = true;
                }
                else
                {
                    // retrieve all of the static, non public methods from the program class
                    var methods = typeof(Program).GetMethods(BindingFlags.Static | BindingFlags.NonPublic);

                    // retrieve the method name
                    var methodName = input.Contains(" ") ? input.Split(' ')[0] : input;

                    // for each method in the program class, find the methods
                    // that have the attribute CommandAttribute applied
                    // based on the user input command
                    // project the new results into the original method as a list
                    var method = methods.SelectMany(c => c.GetCustomAttributes<CommandAttribute>()
                                                                    .Where(a => a.Name == methodName)
                                                                    .Select(b => c))
                                                                    .FirstOrDefault();
                    if (method == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Method not found for command: {input}");
                        Console.ResetColor();
                        continue;
                    }

                    // skip the first entry in the user input, because the first entry is the command
                    var contents = input.Split(' ').Skip(1).ToArray();

                    // create a single array of length of 1 to handle single method commands
                    var data = new string[1];

                    if (method.GetParameters().Length == 1)
                    {
                        // join all the content of the user input, only if the method to be invoked has 1 parameter
                        data[0] = string.Join(" ", contents);
                    }
                    else
                    {
                        // otherwise set the content of the data array to the value of the contents array
                        data = contents;
                    }

                    // create a list of parameters
                    var parameters = new List<object>();

                    // for each input in the data array
                    for (var i = 0; i < data.Count(); i++)
                    {
                        // add the parameter to the list of parameters
                        // using the convert class, we change the type of parameter to the target type
                        parameters.Add(Convert.ChangeType(data[i], method.GetParameters()[i].ParameterType));
                    }

                    // invoke the appropriate method
                    method.Invoke(null, method.GetParameters().Any() ? parameters.ToArray() : null);
                }
            } while (!exit);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Goodbye");
            Console.WriteLine("Press any key to exit...");

            Console.ReadKey();
        }

        /// <summary>
        /// Invokes the clear command.
        /// </summary>
        [Command("c")]
        [Command("cls")]
        [Command("clear")]
        [Help("Clears the screen and print the help menu. Example usage: 'c' 'cls' 'clear'")]
        private static void Clear()
        {
            Console.Clear();
            Help(null);
        }

        /// <summary>
        /// Provides help based on the given command.
        /// </summary>
        /// <param name="command">The command.</param>
        [Command("h")]
        [Command("help")]
        [Help("Prints the help menu for a single command. Example usage: 'h' 'command' or 'help' 'command', supply no command to display help for all commands")]
        private static void Help(string command)
        {
            // retrieve all the attributes on the methods in the program class with the HelpAttribute applied
            // in our where clause we want to skip this method, otherwise
            // we will encounter a stack overflow exception
            var attributes = typeof(Program).GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
                                            .Where(c => c.GetCustomAttributes<HelpAttribute>().Any() && c.Name != "Help")
                                            .Where(c => string.IsNullOrEmpty(command) || c.GetCustomAttributes<CommandAttribute>().Any(x => x.Name == command))
                                            .SelectMany(c => c.GetCustomAttributes<HelpAttribute>())
                                            .ToList();

            if (!attributes.Any() && !string.IsNullOrEmpty(command))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Command not found or unable to locate help page for command: {command}");
                Console.ResetColor();
            }
            else
            {
                attributes.ForEach((a) =>
                {
                    Console.WriteLine($"Help menu for: {a.Content}");
                });
            }
        }

        /// <summary>
        /// Runs notepad.
        /// </summary>
        [Command("notepad")]
        [Help("Runs notepad.exe. Example usage: 'notepad'")]
        private static void RunNotepad()
        {
            Process.Start("notepad");
        }

        /// <summary>
        /// Says the specified phrase.
        /// </summary>
        /// <param name="phrase">The phrase.</param>
        [Command("say")]
        [Help("Says a given phrase. Example usage: 'say' 'phrase'")]
        private static void Say(string phrase)
        {
            Console.WriteLine(phrase);
        }

        /// <summary>
        /// Says hello.
        /// </summary>
        [Command("sh")]
        [Command("sayhello")]
        [Help("Says hello. Example usage: 'sh' or 'sayhello'")]
        private static void SayHello()
        {
            Console.WriteLine("hello");
        }

        /// <summary>
        /// Walks the specified distance.
        /// </summary>
        /// <param name="distance">The distance.</param>
        [Command("w")]
        [Command("walk")]
        [Help("Walks a given distance. Example usage: 'walk' '10'")]
        private static void Walk(int distance)
        {
            Console.WriteLine($"Walking: {distance} metres");
        }

    }

}