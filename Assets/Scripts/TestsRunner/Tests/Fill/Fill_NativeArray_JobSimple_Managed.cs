using System.Diagnostics;
using Components.UI.DevConsole;
using Unity.Collections;
using Unity.Jobs;

namespace TestsRunner.Tests.Fill
{
    public class Fill_NativeArray_JobSimple_Managed
    {
        private struct JobSimple : IJob
        {
            public NativeArray<int> Data;
            public int Value;

            public void Execute()
            {
                for (var i = 0; i < Data.Length; i++)
                {
                    Data[i] = Value;
                }
            }
        }

        public void Start(int count)
        {
            var type = "NativeArray<int>()";
            var body = "a[i] = n";

            var array = new NativeArray<int>(count, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
            var job = new JobSimple {Data = array, Value = 1};

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var handle = job.Schedule();
            handle.Complete();

            stopwatch.Stop();
            DevConsole.WriteLine(
                $"{GetType().Name,TestRunner.MethodNameLength} | " +
                $"{type,TestRunner.TypeLength} | " +
                $"{body,TestRunner.BodyLength} | " +
                $"{stopwatch.ElapsedTicks} ticks");

            array.Dispose();
        }
    }
}
