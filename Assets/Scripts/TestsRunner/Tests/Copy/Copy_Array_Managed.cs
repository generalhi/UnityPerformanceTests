using System;
using System.Diagnostics;
using Components.UI.DevConsole;

namespace TestsRunner.Tests.Copy
{
    public class Copy_Array_Managed
    {
        public void Start(int count)
        {
            var array1 = new int[count];
            var array2 = new int[count];

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Array.Copy(array1, array2, count);

            stopwatch.Stop();
            DevConsole.WriteLine($"{GetType().Name,TestRunner.MethodNameSpace} - {stopwatch.ElapsedTicks} ticks");
        }
    }
}
