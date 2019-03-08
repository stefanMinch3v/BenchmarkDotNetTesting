namespace ExampleTesting
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Running;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// Tests for empty collection using for/foreach loop and linq-any/linq-count/linq-count().
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            var summary = BenchmarkRunner.Run<Program>();

            // project must be runned in release mode
            // [MemoryDiagnoser] for show how much memory does it cost for each operation
            // [ClrJob, CoreJob] clr for running the old dot net (specify the version in the csproj file) and core for running the core version
            // memory, clr and core attributes must be added over the Program class !
        }

        public IList<int> TestList { get; set; } = new List<int>();
        
        [GlobalSetup]
        public void InitializeDependencies()
        {
            // add some initializations here, for instance automapper
        }

        [Benchmark]
        public void TestWithLinqAny()
        {
            var isEmptyCollection = CollectionTester.IsEmptyCollectionWithLinqAny(this.TestList);
            Debug.WriteLine(isEmptyCollection);
        }

        [Benchmark]
        public void TestWithLinqCount()
        {
            var isEmptyCollection = CollectionTester.IsEmptyCollectionWithLinqCount(this.TestList);
            Debug.WriteLine(isEmptyCollection);
        }

        [Benchmark]
        public void TestWithLinqCountBrackets()
        {
            var isEmptyCollection = CollectionTester.IsEmptyCollectionWithLinqCountBrackets(this.TestList);
            Debug.WriteLine(isEmptyCollection);
        }

        [Benchmark]
        public void TestWithForeachLoop()
        {
            var isEmptyCollection = CollectionTester.IsEmptyCollectionWithForeachLoop(this.TestList);
            Debug.WriteLine(isEmptyCollection);
        }

        [Benchmark]
        public void TestWithForLoop()
        {
            var isEmptyCollection = CollectionTester.IsEmptyCollectionWithForLoop(this.TestList);
            Debug.WriteLine(isEmptyCollection);
        }
    }

    public static class CollectionTester
    {
        public static bool IsEmptyCollectionWithLinqAny(IList<int> list)
            => list.Any();

        public static bool IsEmptyCollectionWithLinqCount(IList<int> list)
            => list.Count > 0;

        public static bool IsEmptyCollectionWithLinqCountBrackets(IList<int> list)
            => list.Count() > 0;

        public static bool IsEmptyCollectionWithForeachLoop(IList<int> list)
        {
            foreach (var element in list)
            {
                return true;
            }

            return false;
        }

        public static bool IsEmptyCollectionWithForLoop(IList<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                return true;
            }

            return false;
        }
    }
}