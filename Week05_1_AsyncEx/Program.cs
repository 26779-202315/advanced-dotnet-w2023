namespace Week05_1_AsyncEx
{
    internal class Program
    {
        private static HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {

            // starting our async tasks
            var exampleTask = GetExampleDotComAsync();
            var mohawkCollegeTask = GetMohawkAsync();

            // performing other synchronous work
            Console.WriteLine("Do some other work");

            // await the first async task to complete
            await exampleTask;

            // do more synchronous work
            Console.WriteLine("do work after the example task completion but before the mohawk task completion");

            // await the second async task to complete
            var result = await mohawkCollegeTask;

            // print the result of the second async task
            Console.WriteLine("Mohawk College Response");
            GreenPrint($"mohawkcollege.ca says:\n{result}");

            // using the await keyword directly on a method invocation
            // will force the task to be completed at the time of invocation
            // no 'asynchronous-ness' will happen here
            // the program will wait for this to return before continuing with 
            // subsequent line of codes
            var forcedResult = await ForceTaskCompletionAsync();
            GreenPrint($"microsoft.com says:\n{forcedResult}");

            Console.WriteLine("All done!");
        }

        /// <summary>
        /// Starts an asynchronous task, to contact the example.com website
        /// </summary>
        /// <remarks>we are not explicitly returning anything from this method</remarks>
        static async Task GetExampleDotComAsync()
        {

            Console.WriteLine("IO (Example.com) task starting");

            // this is IO bound code, because we are accessing a network resource
            var result = await client.GetStringAsync("http://example.com");

            //delay the task for 4 seconds to allow for easier observation of control flow
            await Task.Delay(4000);


            //print the first First 50 characters of the response
            Console.WriteLine("IO Task about to be printed");
            Console.WriteLine($"First 50 characters of the response:");//
            GreenPrint($"example.com says:\n{result.Substring(0, 50)}");
        }

        /// <summary>
        /// Starts an asynchronous task, to contact Mohawk College website
        /// </summary>
        /// <returns>The first 50 characters of the response</returns>
        static async Task<string> GetMohawkAsync()
        {

            Console.WriteLine("IO (Mohawk College) task starting");

            // this is IO bound code, because we are accessing a network resource
            var result = await client.GetStringAsync("http://mohawkcollege.ca");

            Console.WriteLine("IO task about to be returned");

            //return the first First 50 characters of the response
            return result.Substring(0, 50);

        }


        /// <summary>
        /// Starts an asynchronous task, to contact Microsoft website
        /// </summary>
        /// <returns>The first 50 characters of the response</returns>
        private static async Task<string> ForceTaskCompletionAsync()
        {
            Console.WriteLine("forced task starting");

            var result = await client.GetStringAsync("http://microsoft.com");

            Console.WriteLine("forced task returning");

            return result.Substring(0, 50);
        }

        /// <summary>
        /// Print text using green font to distinguish it from other print text on the screen
        /// </summary>
        /// <param name="text"></param>
        private static void GreenPrint(string text)
        {
            var currentColor = Console.ForegroundColor; //store the current color value
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ForegroundColor = currentColor;//reset the color to the original value
        }

    }
}