using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Text;

//int count = 5_00_000;
//int counter = 0;

//DateTime now = DateTime.UtcNow;
//List<Task> tasks = new List<Task>();

//for (int i = 0; i < count; i++)
//{
//    //string dt = DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ss.fffffffZ");

//    Console.WriteLine($"{{\"counter\": \"{++counter}\", \"timestamp\": \"{now}\" \"e\": \"trade\", \"E\": 1672515782136, \"s\": \"BNBBTC\", \"q\": \"100\", \"b\": 88, \"a\": 50, \"T\": 1672515782136, \"m\": true, \"M\": true }}");
//}

//Task.WaitAll(tasks.ToArray());


var summary = BenchmarkRunner.Run<MyBenchmarkDemo>();
//Console.WriteLine(count / (DateTime.UtcNow - now).Seconds);
Console.ReadLine();

public class MyBenchmarkDemo
{
    int NumberOfItems = 100000;

    [Benchmark]
    public string ConcatStringsUsingStringBuilder()
    {
        var sb = new StringBuilder();
        for (int i = 0; i < NumberOfItems; i++)
        {
            sb.Append("Hello World!" + i);
        }
        return sb.ToString();
    }
    [Benchmark]
    public string ConcatStringsUsingGenericList()
    {
        var list = new List<string>(NumberOfItems);
        for (int i = 0; i < NumberOfItems; i++)
        {
            list.Add("Hello World!" + i);
        }
        return list.ToString();
    }
}

