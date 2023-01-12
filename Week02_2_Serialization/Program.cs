using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Week02_2_Serialization
{
    class Program
    {
        static void Main(string[] args)
        {
            BinarySerialization();
            JsonSerialization();
            XmlSerialization();
        }

        static void BinarySerialization()
        {
            // declare and initialize our BinaryFormatter class
            // this class is used for binary serialization
            var formatter = new BinaryFormatter();

            // declare and initialize our memory stream
            // the purpose of this memory stream
            // is to hold the serialized content in memory
            var stream = new MemoryStream();

            // create the object to be serialized
            var person = new Person
            {
                Name = "John Doe"
            };

            // serialize the object
            // 'person' is our object we want to serialize
            // 'stream' is the instance of the stream to write the
            // serialized version of the object to
            // formatter indicates the type of serialization
            // we are performing on the object
            // 'serialize' is the action to perform
            formatter.Serialize(stream, person);

            // convert the content of the serialized stream
            // to a byte array
            byte[] serializedContent = stream.ToArray();

            // print the contents of the byte array
            foreach (var content in serializedContent)
            {
                Console.WriteLine(content);
            }

            Console.WriteLine("write our serialized content to a file called 'output.bin'");

            // write our serialized content to a file called 'output.bin'
            File.WriteAllBytes("output.bin", serializedContent);

            Console.WriteLine("read our serialized content from a file called 'output.bin'");
            var bytes = File.ReadAllBytes("output.bin");

            var deserializedPerson = (Person)formatter.Deserialize(new MemoryStream(bytes));

            Console.WriteLine(deserializedPerson.Name);

            Console.ReadKey();
        }


        static void JsonSerialization()
        {
            // declare and initialize our json serializer
            // supply type of person to indicate what type of object
            // is to be serialized
            var serializer = new JsonSerializer();

            var stringWriter = new StringWriter();

            var person = new Person
            {
                Name = "John Doe",
                DateOfBirth = DateTimeOffset.Now,
                Id = Guid.NewGuid()
            };

            serializer.Serialize(stringWriter, person);

            var serializedContent = Encoding.UTF8.GetBytes(stringWriter.ToString());

            Console.WriteLine("write our serialized content to a file called 'output.json'");

            // write our serialized content to a file called 'output.json'
            File.WriteAllBytes("output.json", serializedContent);

            Console.WriteLine("read our serialized content from a file called 'output.json'");
            var bytes = File.ReadAllBytes("output.json");

            var jsonReader = new JsonTextReader(new StringReader(Encoding.UTF8.GetString(bytes)));

            var deserializedPerson = (Person)serializer.Deserialize(jsonReader, typeof(Person));

            Console.WriteLine(deserializedPerson.Name);

            Console.ReadKey();
        }

        static void XmlSerialization()
        {
            // declare and initialize our xml serializer
            // supply type of person to indicate what type of object
            // is to be serialized
            var serializer = new XmlSerializer(typeof(Person));

            var memoryStream = new MemoryStream();

            var person = new Person
            {
                Name = "John Doe",
                DateOfBirth = DateTimeOffset.Now,
                Id = Guid.NewGuid()
            };

            serializer.Serialize(memoryStream, person);

            var serializedContent = memoryStream.ToArray();

            Console.WriteLine("write our serialized content to a file called 'output.xml'");

            // write our serialized content to a file called 'output.xml'
            File.WriteAllBytes("output.xml", serializedContent);

            Console.WriteLine("read our serialized content from a file called 'output.xml'");
            var bytes = File.ReadAllBytes("output.xml");

            var deserializedPerson = (Person)serializer.Deserialize(new MemoryStream(bytes));

            Console.WriteLine(deserializedPerson.Name);

            Console.ReadKey();
        }
    }

}