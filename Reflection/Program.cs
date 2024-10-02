using Newtonsoft.Json;
using Reflection.CustomClass;
using Reflection.Serializer;
using System.Runtime.Serialization;

namespace Reflection
{
    class Program
    {
        public static Statictics statSerCsv = new Statictics();
        public static Statictics statSerJson = new Statictics();
        public static Statictics statDeCsv = new Statictics();
        public static Statictics statDeJson = new Statictics();
        public static int iter = 0;

        static void Main(string[] args)
        {
            int countIter = 1;
            if (args.Length > 0)
            {
                int.TryParse(args[0], out countIter );
            }
            for (; iter < countIter; iter++)
            {
                var someClass = new SomeClass()
                {
                    Id = Guid.NewGuid(),
                    Firstname = "Alex",
                    Surname = "Fox",
                    Position = "TeamLead",
                    MainEmail = "Fox@fox.com",
                    MainTelephoneNumber = "1234567890",
                    About = "Super Dev"
                };
                var f = new F();

                Console.WriteLine($"Сериализация csv:");
                var csv = CsvSerialize(f);
                Console.WriteLine($"Сериализация json:");
                var json = JsonSerialize(f);
                Console.WriteLine($"Десериализация csv:");
                var resultCsv = CsvDeserialize<F>(csv, f.GetType());
                if (resultCsv != null)
                    foreach (var obj in resultCsv)
                    {
                        Console.WriteLine(obj);
                    }
                Console.WriteLine($"Десериализация json:");
                var resultJson = JsonDeserialize<F>(json);
                if (resultJson != null) Console.WriteLine(resultJson.ToString());
            }

            using (StreamWriter writer = new StreamWriter("csv.txt", false))
            {
                writer.WriteLine("Csv Serialize \n" + statSerCsv.ToString());
            }

            using (StreamWriter writer = new StreamWriter("csvDe.txt", false))
            {
                writer.WriteLine("Csv Deserialize \n" + statDeCsv.ToString());
            }

            using (StreamWriter writer = new StreamWriter("Json.txt", false))
            {
                writer.WriteLine("Json Serialize \n" + statSerJson.ToString());
            }

            using (StreamWriter writer = new StreamWriter("JsonDe.txt", false))
            {
                writer.WriteLine("Json Deserialize \n" + statDeJson.ToString());
            }

            Console.ReadKey();
        }

        public static string JsonSerialize(ISerializable obj)
        {
            var result = "";
            Timer.Start();
            for (int i = 0; i < 100000; i++)
            {
                result = JsonConvert.SerializeObject(obj);
            }
            var resultTime = Timer.Stop();
            Timer.Start();
            Console.WriteLine(result);
            Console.WriteLine($"Время выполнения {resultTime}");
            statSerJson.addStatictics(iter, resultTime);
            resultTime = Timer.Stop();
            Console.WriteLine($"Время вывода текста {resultTime}");

            return result;
        }

        public static string CsvSerialize(ISerializable obj)
        {
            var serializer = new SerializerCSV(obj.GetType());
            var result = "";
            Timer.Start();
            for (int i = 0; i < 100000; i++)
            {
                result = serializer.Serialize(obj, ",");
            }
            var resultTime = Timer.Stop();

            Timer.Start();
            Console.WriteLine(result);
            Console.WriteLine($"Время выполнения {resultTime}");
            statSerCsv.addStatictics(iter, resultTime);
            resultTime = Timer.Stop();

            Console.WriteLine($"Время вывода текста {resultTime}");

            return result;
        }

        public static List<T> CsvDeserialize<T>(string dataStr, Type type) where T : class, new()
        {
            var serializer = new SerializerCSV(type);
            var result = new List<T>();
            Timer.Start();
            for (int i = 0; i < 100000; i++)
            {
                result = serializer.DeserializeFromString<T>(dataStr, ",");
            }
            var resultTime = Timer.Stop();
            Console.WriteLine($"Время выполнения {resultTime}");
            statDeCsv.addStatictics(iter, resultTime);

            return result;
        }

        public static T JsonDeserialize<T>(string dataStr) where T : class, new()
        {
            T result = null;
            Timer.Start();
            for (int i = 0; i < 100000; i++)
            {
                result = JsonConvert.DeserializeObject<T>(dataStr);
            }
            var resultTime = Timer.Stop();
            Console.WriteLine($"Время выполнения {resultTime}");
            statDeJson.addStatictics(iter, resultTime);
            return result;
        }
    }
}
