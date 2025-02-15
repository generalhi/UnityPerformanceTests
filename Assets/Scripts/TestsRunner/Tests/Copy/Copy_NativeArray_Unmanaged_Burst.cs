using System.Diagnostics;
using Components.UI.DevConsole;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace TestsRunner.Tests.Copy
{
    [BurstCompile]
    public unsafe class Copy_NativeArray_Unmanaged_Burst
    {
        public void Start(int count)
        {
            var type = "NativeArray<int>()";
            var body = "UnsafeUtility.MemCpy()";

            var input = new NativeArray<int>(count, Allocator.Temp);
            var output = new NativeArray<int>(count, Allocator.Temp);

            var ptrInput = (int*) input.GetUnsafePtr();
            var ptrOutput = (int*) output.GetUnsafePtr();

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            UnsafeUtility.MemCpy(ptrOutput, ptrInput, count * sizeof(int));

            stopwatch.Stop();
            DevConsole.WriteLine(
                $"{GetType().Name,TestRunner.MethodNameLength} | " +
                $"{type, TestRunner.TypeLength} | " +
                $"{body, TestRunner.BodyLength} | " +
                $"{stopwatch.ElapsedTicks} ticks");

            input.Dispose();
            output.Dispose();
        }
    }
}
