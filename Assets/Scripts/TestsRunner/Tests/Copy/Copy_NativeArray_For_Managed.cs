using System.Diagnostics;
using Components.UI.DevConsole;
using Unity.Collections;

namespace TestsRunner.Tests.Copy
{
    public class Copy_NativeArray_For_Managed
    {
        public void Start(int count)
        {
            var input = new NativeArray<int>(count, Allocator.Temp);
            var output = new NativeArray<int>(count, Allocator.Temp);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (var i = 0; i < count; i++)
            {
                output[i] = input[i];
            }

            stopwatch.Stop();
            DevConsole.WriteLine($"{GetType().Name,TestRunner.MethodNameSpace} - {stopwatch.ElapsedTicks} ticks");

            input.Dispose();
            output.Dispose();
        }
    }
}
