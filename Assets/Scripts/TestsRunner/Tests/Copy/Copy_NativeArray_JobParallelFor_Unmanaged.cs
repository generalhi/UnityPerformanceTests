using System.Diagnostics;
using Components.UI.DevConsole;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace TestsRunner.Tests.Fill
{
    public class Copy_NativeArray_JobParallelFor_Unmanaged
    {
        private unsafe struct JobParallelFor : IJobParallelFor
        {
            [NativeDisableUnsafePtrRestriction]
            public int* PtrInput;

            [NativeDisableUnsafePtrRestriction]
            public int* PtrOutput;

            public void Execute(int i)
            {
                PtrOutput[i] = PtrInput[i];
            }
        }

        public unsafe void Start(int count)
        {
            var type = "NativeArray<int>()";
            var body = "ptr1[i] = ptr2[i]";

            var input = new NativeArray<int>(count, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
            var output = new NativeArray<int>(count, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var job = new JobParallelFor
            {
                PtrInput = (int*) input.GetUnsafePtr(),
                PtrOutput = (int*) output.GetUnsafePtr(),
            };

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
