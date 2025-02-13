using System.Diagnostics;
using Components.UI.DevConsole;
using Unity.Collections;

namespace TestsRunner.Tests
{
    public class ArrayFill_NativeArray_Test
    {
        public void Start(int count)
        {
            var array = new NativeArray<int>(count, Allocator.Temp);
            var value = 1;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (var i = 0; i < count; i++)
            {
                array[i] = value;
            }

            stopwatch.Stop();
            DevConsole.WriteLine($"{ToString()} - {stopwatch.ElapsedTicks} ticks");

            array.Dispose();
        }
    }
}
