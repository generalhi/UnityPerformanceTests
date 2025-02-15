using System.Diagnostics;
using Components.UI.DevConsole;
using Unity.Collections;

namespace TestsRunner.Tests.Fill
{
    public class Fill_NativeArray_For_Managed
    {
        public void Start(int count)
        {
            var type = "NativeArray<int>()";
            var body = "a[i] = n";

            var array = new NativeArray<int>(count, Allocator.Temp);
            var value = 1;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (var i = 0; i < count; i++)
            {
                array[i] = value;
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
