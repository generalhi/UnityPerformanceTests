using System.Diagnostics;
using Components.UI.DevConsole;

namespace TestsRunner.Tests.Fill
{
    public class Fill_Array_For_Managed
    {
        public void Start(int count)
        {
            var type = "int[]";
            var body = "a[i] = n";

            var array = new int[count];
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
                $"{type,TestRunner.TypeLength} | " +
                $"{body,TestRunner.BodyLength} | " +
                $"{stopwatch.ElapsedTicks} ticks");
        }
    }
}
