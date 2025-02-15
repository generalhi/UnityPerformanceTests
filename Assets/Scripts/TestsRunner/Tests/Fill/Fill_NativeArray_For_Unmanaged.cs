using System.Diagnostics;
using Components.UI.DevConsole;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace TestsRunner.Tests.Fill
{
    public unsafe class Fill_NativeArray_For_Unmanaged
    {
        public void Start(int count)
        {
            var type = "NativeArray<int>()";
            var body = "ptr[i] = n";

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
            DevConsole.WriteLine(
                $"{GetType().Name,TestRunner.MethodNameLength} | " +
                $"{type, TestRunner.TypeLength} | " +
                $"{body, TestRunner.BodyLength} | " +
                $"{stopwatch.ElapsedTicks} ticks");

            array.Dispose();
        }
    }
}
