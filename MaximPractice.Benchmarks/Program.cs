using BenchmarkDotNet.Running;

namespace MaximPractice.Benchmarks;

internal class Program
{
    static void Main(string[] args)
    {
        var _ = BenchmarkRunner.Run(typeof(Program).Assembly);
    }
}
