using System.Diagnostics;
using Components.UI.DevConsole;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace TestsRunner.Tests
{
    public class ArrayFill_JobParallelForUnmanaged_Test
    {
        [BurstCompile]
        private unsafe struct JobParallelFor : IJobParallelFor
        {
            [NativeDisableUnsafePtrRestriction]
            public int* Ptr;

            public int Value;

            public void Execute(int i)
            {
                Ptr[i] = Value;
            }
        }

        public unsafe void Start(int count)
        {
            var input = new NativeArray<int>(count, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var job = new JobParallelFor
            {
                Ptr = (int*) input.GetUnsafePtr(),
                Value = 1
            };

            var handle = job.Schedule(count, 64);
            handle.Complete();

            stopwatch.Stop();
            DevConsole.WriteLine($"{ToString()} - {stopwatch.ElapsedTicks} ticks");

            input.Dispose();
        }
    }

    public class ArrayFill_JobParallelFor_Test
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

            var job = new JobParallelFor
            {
                Input = input,
                Value = 1
            };

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var handle = job.Schedule(count, 64);
            handle.Complete();

            stopwatch.Stop();
            DevConsole.WriteLine($"{ToString()} - {stopwatch.ElapsedTicks} ticks");

            input.Dispose();
        }
    }
}
