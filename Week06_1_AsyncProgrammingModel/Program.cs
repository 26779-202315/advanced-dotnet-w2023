namespace Week06_1_AsyncProgrammingModel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // get the path of the app data folder
            var path = @"C:\TestData";

            // create an DirectoryInfo object instance which contains information about the directory
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            // retrieve all the files ending with file extension .pdf in the folder and all its subfolders
            var files = directoryInfo.GetFiles("*.pdf", SearchOption.AllDirectories);

            Console.WriteLine($"Found {files.Length} files");

            var buffer = new byte[1024];
            foreach (var file in files)
            {
                var stream = file.Open(FileMode.Open,
                    FileAccess.Read, FileShare.Read);

                stream.BeginRead(buffer, 0, buffer.Length, HandleRead, stream);
            }

            Console.ReadLine();
        }

        private static void HandleRead(IAsyncResult result)
        {
            var fileStream = (FileStream)result.AsyncState;
            var bytesRead = fileStream.EndRead(result);
            Console.WriteLine($"Read {bytesRead} bytes from file {fileStream.Name}");
        }

    }
}