namespace Week05_3_MultiTask_WhenAny
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            try
            {
                await GetSiteLengthTaskAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Main 'Task' Catch: {ex.Message}");
            }

            try
            {
                await GetSiteLengthAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Main 'Task' Catch: {ex.Message}");
            }


            Console.WriteLine("All Done!");

        }


        /// <summary>
        /// Starts multiple asynchronous tasks, to contact multiple websites
        /// </summary>
        /// <remarks>we are not explicitly returning anything from this method</remarks>
        private static async Task GetSiteLengthAsync()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();


            var siteList = new List<string> { "yahoo", "google", "msn", "reddit" };

            //Using LINQ to create and invoke multiple tasks using the sitelist object
            List<Task<string>> taskList = (from site in siteList
                                           select client.GetStringAsync($"http://{site}.com")).ToList();

            int sumLength = 0;
            Console.WriteLine("Starting Loop!!");

            while (taskList.Any()) //Check if there is still any (pending) tasks in the tasklist
            {
                //Retrieve the task object for whichever task that finishes first; there is no guarantee of what order this will be. as a matter of fact, if you run this multiple times; you are more than likely to get different order every few times
                var firstToFinsh = await Task.WhenAny(taskList);
                Console.WriteLine($"content length is {firstToFinsh.Result.Length}");

                sumLength += firstToFinsh.Result.Length;
                //remove completed tasks
                taskList.Remove(firstToFinsh);
            }

            Console.WriteLine($"Total length: {sumLength}");

            var time = watch.ElapsedMilliseconds;
            Console.WriteLine($"ms: {time}");
        }


        /// <summary>
        /// Starts multiple asynchronous tasks, to contact multiple websites
        /// </summary>
        /// <remarks>we are not explicitly returning anything from this method</remarks>
        private static async Task GetSiteLengthTaskAsync()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();


            var siteList = new List<string> { "yahoo", "google", "msn", "reddit" };

            foreach (string site in siteList)
            {
                var task = client.GetStringAsync($"http://{site}.com");

                await task;
                Console.WriteLine($"{site} content length is {task.Result.Length}");
            }

            var time = watch.ElapsedMilliseconds;
            Console.WriteLine($"ms: {time}");
        }

    }

}