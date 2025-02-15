using System.Diagnostics;
using Components.UI.DevConsole;
using Unity.Collections;
using Unity.Jobs;

namespace TestsRunner.Tests.Copy
{
    public class Copy_NativeArray_JobParallelFor_Managed
    {
        private struct JobParallelFor : IJobParallelFor
        {
            public NativeArray<int> Input;
            public NativeArray<int> Output;

            public void Execute(int i)
            {
                Output[i] = Input[i];
            }
        }

        public void Start(int count)
        {
            var type = "NativeArray<int>()";
            var body = "a[i] = b[i]";

            var input = new NativeArray<int>(count, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
            var output = new NativeArray<int>(count, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);

            var job = new JobParallelFor {Input = input, Output = output};

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var handle = job.Schedule(count, 64);
            handle.Complete();

            stopwatch.Stop();
            DevConsole.WriteLine(
                $"{GetType().Name,TestRunner.MethodNameLength} | " +
                $"{type,TestRunner.TypeLength} | " +
                $"{body,TestRunner.BodyLength} | " +
                $"{stopwatch.ElapsedTicks} ticks");

            input.Dispose();
        }
    }
}
