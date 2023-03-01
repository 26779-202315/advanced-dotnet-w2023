using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace Week07_1_HttpListener
{
    class Program
    {
        //The dictionary to hold the content of POST requests.
        private static Dictionary<int, Person> personStore = new Dictionary<int, Person>();

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            HttpListener listener = new HttpListener();

            // add prefixes that the listener object will observe
            listener.Prefixes.Add("http://127.0.0.1:21000/test/");
            listener.Prefixes.Add("http://127.0.0.1:21000/hello/");
            listener.Prefixes.Add("http://127.0.0.1:21000/getperson/");
            listener.Prefixes.Add("http://127.0.0.1:21000/postperson/");


            //[Requires admin access] or Configuring namespace reservations, see here: https://learn.microsoft.com/en-us/dotnet/framework/wcf/feature-details/configuring-http-and-https#configuring-namespace-reservations
            //Because of the wildcard, This will work with things like: http://localhost:21000/demo   OR http://127.0.0.1:21000/demo  OR your actual machine name or IP as long as th eport is 21000
            //listener.Prefixes.Add("http://*:21000/demo/"); 


            //************Multiple listener objects cannot have the same prefix************
            //var listener2 = new HttpListener();
            //listener2.Prefixes.Add("http://127.0.0.1:21000/test/");
            //listener2.Start();


            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("Listener Starting...");
            listener.Start();

            foreach (var item in listener.Prefixes)
            {
                Console.WriteLine($"listening for requests on: {item}");
            }

            // indicate to the HTTP listener that we want to start accepting requests asynchronously
            // the method used to handle the requests is the first parameter 'HandleRequest'
            // the second parameter passes the listener instance to continue the asynchronous pipeline
            listener.BeginGetContext(HandleRequest, listener);
            Console.ResetColor();

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }

        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="result">The result.</param>
        private static void HandleRequest(IAsyncResult result)
        {
            // cast our async state, back to the listener instance
            var listener = (HttpListener)result.AsyncState;

            // indicate to the asynchronous pipeline that this request is being processed
            // and allow other requests to start
            var context = listener.EndGetContext(result);

            Console.WriteLine($"Received a request from : {context.Request.RemoteEndPoint}");

            var serializer = new XmlSerializer(typeof(Person));
            var memoryStream = new MemoryStream();

            byte[] response;

            // set the content type to indicate to the client what type the results are in
            context.Response.ContentType = "text/plain;charset=UTF-8";

            switch (context.Request.Url.LocalPath)
            {
                case "/test":
                    response = SerializeResponse("This is the test endpoint!!");
                    break;
                case "/hello":
                    response = SerializeResponse("Hello There!!");
                    break;
                case "/demo":
                    response = SerializeResponse("this is the demo endpoint");
                    break;
                case "/getperson":
                    context.Response.ContentType = "application/xml";
                    var person = personStore[int.Parse(context.Request.QueryString.GetValues("id").FirstOrDefault())];
                    serializer.Serialize(memoryStream, person);
                    response = memoryStream.ToArray();
                    break;
                case "/postperson":
                    context.Response.ContentType = "application/xml";
                    serializer.Serialize(memoryStream, HandlePost(context));
                    response = memoryStream.ToArray();
                    break;
                default:
                    response = SerializeResponse("I don't know!!");
                    break;
            }

            context.Response.OutputStream.Write(response, 0, response.Length);

            // set the HTTP status code
            context.Response.StatusCode = 200;

            // close response context
            context.Response.Close();

            // start listening again
            listener.BeginGetContext(HandleRequest, listener);
        }

        private static Person HandlePost(HttpListenerContext context)
        {
            // use the XML serializer to deserialize and process our POST request
            var serizlizer = new XmlSerializer(typeof(Person));
            var memoryStram = new MemoryStream();

            // deserialize the request input stream to a Person instance
            var person = (Person)serizlizer.Deserialize(context.Request.InputStream);

            // add the deserialized person to our person store
            personStore.Add(person.Id, person);

            // return the added person
            return person;

        }

        /// <summary>
        /// Serializes the response.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>Returns a byte array representing the response.</returns>
        private static byte[] SerializeResponse(string content)
        {
            Console.WriteLine($"Sending Response: {content}");
            return Encoding.UTF8.GetBytes(content);
        }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}