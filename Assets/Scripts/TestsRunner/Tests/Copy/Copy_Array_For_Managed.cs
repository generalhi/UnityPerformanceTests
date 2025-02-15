using System.Diagnostics;
using Components.UI.DevConsole;

namespace TestsRunner.Tests.Copy
{
    public class Copy_Array_For_Managed
    {
        public void Start(int count)
        {
            var type = "int[]";
            var body = "a[i] = b[i]";
            
            var array1 = new int[count];
            var array2 = new int[count];

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (var i = 0; i < count; i++)
            {
                array1[i] = array2[i];
            }

            stopwatch.Stop();
            DevConsole.WriteLine(
                $"{GetType().Name,TestRunner.MethodNameLength} | " +
                $"{type, TestRunner.TypeLength} | " +
                $"{body, TestRunner.BodyLength} | " +
                $"{stopwatch.ElapsedTicks} ticks");
        }
    }
}
