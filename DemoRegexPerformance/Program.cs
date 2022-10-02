using BenchmarkDotNet.Running;

namespace DemoRegexPerformance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchmarks>();
        }
    }
}