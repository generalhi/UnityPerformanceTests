using System.Diagnostics;
using Components.UI.DevConsole;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace TestsRunner.Tests.Fill
{
    public class Fill_NativeArray_JobParallelFor_Unmanaged_Burst
    {
        [BurstCompile]
        [NativeContainer]
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

            var job = new JobParallelFor {Ptr = (int*) input.GetUnsafePtr(), Value = 1};

            var handle = job.Schedule(count, 64);
            handle.Complete();

            stopwatch.Stop();
            DevConsole.WriteLine($"{GetType().Name,TestRunner.MethodNameSpace} - {stopwatch.ElapsedTicks} ticks");

            input.Dispose();
        }
    }
}
