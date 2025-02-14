using System.Diagnostics;
using Components.UI.DevConsole;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace TestsRunner.Tests
{
    public class Fill_NativeArray_JobParallelFor_Managed
    {
        [BurstCompile]
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
            var input = new NativeArray<int>(count, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);

            var job = new JobParallelFor {Input = input, Value = 1};

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var handle = job.Schedule(count, 64);
            handle.Complete();

            stopwatch.Stop();
            DevConsole.WriteLine($"{GetType().Name} - {stopwatch.ElapsedTicks} ticks");

            input.Dispose();
        }
    }
}
