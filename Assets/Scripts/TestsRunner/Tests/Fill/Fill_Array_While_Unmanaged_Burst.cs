using System.Diagnostics;
using Components.UI.DevConsole;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;

namespace TestsRunner.Tests.Fill
{
    public unsafe class Fill_Array_While_Unmanaged_Burst
    {
        [BurstCompile]
        public void Start(int count)
        {
            var array = new int[count];
            var value = 1;
            var i = 0;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            fixed (int* ptr = &array[0])
            {
                do
                {
                    ptr[i] = value;
                }
                while (i++ < count);
            }

            stopwatch.Stop();
            DevConsole.WriteLine($"{GetType().Name,TestRunner.MethodNameSpace} - {stopwatch.ElapsedTicks} ticks");
        }
    }
}
