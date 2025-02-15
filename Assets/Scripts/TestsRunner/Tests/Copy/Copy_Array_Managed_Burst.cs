using System;
using System.Diagnostics;
using Components.UI.DevConsole;
using Unity.Burst;

namespace TestsRunner.Tests.Copy
{
    [BurstCompile]
    public class Copy_Array_Managed_Burst
    {
        public void Start(int count)
        {
            var type = "int[]";
            var body = "Array.Copy()";

            var array1 = new int[count];
            var array2 = new int[count];

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Array.Copy(array1, array2, count);

            stopwatch.Stop();
            DevConsole.WriteLine(
                $"{GetType().Name,TestRunner.MethodNameLength} | " +
                $"{type,TestRunner.TypeLength} | " +
                $"{body,TestRunner.BodyLength} | " +
                $"{stopwatch.ElapsedTicks} ticks");
        }
    }
}
