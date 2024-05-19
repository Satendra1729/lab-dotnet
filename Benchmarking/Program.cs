using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using System;

namespace Benchmarking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = DefaultConfig.Instance;
            var summary = BenchmarkRunner.Run<Benchmarks>(config, args);
            Console.WriteLine(summary);

            // Use this to select benchmarks from the console:
            // var summaries = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);
        }
    }
}