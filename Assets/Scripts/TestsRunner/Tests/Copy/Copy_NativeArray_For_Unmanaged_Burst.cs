using System.Diagnostics;
using Components.UI.DevConsole;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace TestsRunner.Tests.Copy
{
    public unsafe class Copy_NativeArray_For_Unmanaged_Burst
    {
        [BurstCompile]
        public void Start(int count)
        {
            var type = "NativeArray<int>()";
            var body = "ptr1[i] = ptr2[i]";

            var input = new NativeArray<int>(count, Allocator.Temp);
            var output = new NativeArray<int>(count, Allocator.Temp);

            var ptrInput = (int*) input.GetUnsafePtr();
            var ptrOutput = (int*) output.GetUnsafePtr();

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (var i = 0; i < count; i++)
            {
                ptrOutput[i] = ptrInput[i];
            }

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
