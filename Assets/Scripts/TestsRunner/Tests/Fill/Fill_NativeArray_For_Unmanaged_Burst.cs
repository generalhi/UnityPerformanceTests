using System.Diagnostics;
using Components.UI.DevConsole;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace TestsRunner.Tests
{
    [BurstCompile]
    public unsafe class Fill_NativeArray_For_Unmanaged_Burst
    {
        public void Start(int count)
        {
            var array = new NativeArray<int>(count, Allocator.Temp);
            var value = 1;

            var ptr = (int*) array.GetUnsafePtr();

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (var i = 0; i < count; i++)
            {
                ptr[i] = value;
            }

            stopwatch.Stop();
            DevConsole.WriteLine($"{GetType().Name,TestRunner.MethodNameSpace} - {stopwatch.ElapsedTicks} ticks");

            array.Dispose();
        }
    }
}
