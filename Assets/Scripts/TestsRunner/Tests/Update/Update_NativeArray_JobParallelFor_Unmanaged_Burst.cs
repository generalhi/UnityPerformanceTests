using System.Diagnostics;
using Components.UI.DevConsole;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace TestsRunner.Tests.Copy
{
    [BurstCompile]
    public class Update_NativeArray_JobParallelFor_Unmanaged_Burst
    {
        private struct Data
        {
            public int A;
            public float B;
        }

        [BurstCompile]
        private unsafe struct JobParallelFor : IJobParallelFor
        {
            [NativeDisableUnsafePtrRestriction]
            public int* PtrInput;

            [NativeDisableUnsafePtrRestriction]
            public Data* PtrOutput;

            public void Execute(int i)
            {
                var n = PtrInput[i];
                PtrOutput[i].A = n + n;
                PtrOutput[i].B = n - n / 2f;
            }
        }

        [BurstCompile]
        public unsafe void Start(int count)
        {
            var type = "NativeArray<Struct>()";
            var body = "Calc ptr";

            var input = new NativeArray<int>(count, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
            var output = new NativeArray<Data>(count, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);

            var job = new JobParallelFor
            {
                PtrInput = (int*) input.GetUnsafePtr(),
                PtrOutput = (Data*) output.GetUnsafePtr()
            };

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var handle = job.Schedule(count, 16);
            handle.Complete();

            stopwatch.Stop();
            DevConsole.WriteLine(
                $"{GetType().Name,TestRunner.MethodNameLength} | " +
                $"{type,TestRunner.TypeLength} | " +
                $"{body,TestRunner.BodyLength} | " +
                $"{stopwatch.ElapsedTicks} ticks");

            input.Dispose();
            output.Dispose();
        }
    }
}
