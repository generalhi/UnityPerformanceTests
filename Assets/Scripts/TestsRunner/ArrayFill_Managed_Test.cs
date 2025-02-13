using System.Diagnostics;
using Components.UI.DevConsole;

namespace TestsRunner.Tests
{
    public class ArrayFill_Managed_Test
    {
        public void Start(int count)
        {
            var array = new int[count];
            var value = 1;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (var i = 0; i < count; i++)
            {
                array[i] = value;
            }

            stopwatch.Stop();
            DevConsole.WriteLine($"{ToString()} - {stopwatch.ElapsedTicks} ticks");
        }
    }
}
