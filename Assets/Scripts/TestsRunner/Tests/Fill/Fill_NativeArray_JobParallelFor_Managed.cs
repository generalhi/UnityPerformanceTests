using System.Diagnostics;
using Components.UI.DevConsole;
using Unity.Collections;
using Unity.Jobs;

namespace TestsRunner.Tests.Fill
{
    public class Fill_NativeArray_JobParallelFor_Managed
    {
        private struct JobParallelFor : IJobParallelFor
        {
            public NativeArray<int> Input;
            public int Value;

            public void Execute(int i)
            {
                Input[i] = Value;
            }
        }

        public void Start(int count)
        {
            var type = "NativeArray<int>()";
            var body = "a[i] = n";

            var input = new NativeArray<int>(count, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);

            var job = new JobParallelFor {Input = input, Value = 1};

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var handle = job.Schedule(count, 64);
            handle.Complete();

            stopwatch.Stop();
            DevConsole.WriteLine(
                $"{GetType().Name,TestRunner.MethodNameLength} | " +
                $"{type, TestRunner.TypeLength} | " +
                $"{body, TestRunner.BodyLength} | " +
                $"{stopwatch.ElapsedTicks} ticks");

            input.Dispose();
        }
    }
}
