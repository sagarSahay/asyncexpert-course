using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Dotnetos.AsyncExpert.Homework.Module01.Benchmark
{
    using System;

    [DisassemblyDiagnoser(exportCombinedDisassemblyReport: true)]
    public class FibonacciCalc
    {
        // HOMEWORK:
        // 1. Write implementations for RecursiveWithMemoization and Iterative solutions
        // 2. Add memory profiler to the benchmark
        // 3. Run with release configuration and compare results
        // 4. Open disassembler report and compare machine code
        // 
        // You can use the discussion panel to compare your results with other students

        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(Data))]
        public ulong Recursive(ulong n)
        {
            if (n == 1 || n == 2) return 1;
            return Recursive(n - 2) + Recursive(n - 1);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public ulong RecursiveWithMemoization(ulong n)
        {
            var fibs = new ulong[n + 1];
            Array.Fill<ulong>(fibs,999999);

            return fib(n, fibs);
        }

        private ulong fib(ulong n, ulong[] fibs)
        {
            if (n == 0 || n == 1)
            {
                return n;
            }

            if (fibs[n] == 999999)
            {
                fibs[n] = fib(n - 1, fibs) + fib(n - 2, fibs);
            }

            return fibs[n];
        }
        
        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public ulong Iterative(ulong n)
        {
            ulong previousPreviousNumber = 0;
            ulong previuousNumber = 0;
            ulong currentNumber = 1;

            for (ulong i = 1; i < n; i++)
            {
                previousPreviousNumber = previuousNumber;
                previuousNumber = currentNumber;
                currentNumber = previuousNumber + previousPreviousNumber;
            }
            return currentNumber;
        }

        public IEnumerable<ulong> Data()
        {
            yield return 15;
            yield return 35;
        }
    }
}
