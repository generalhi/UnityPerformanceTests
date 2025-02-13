using System.Diagnostics;
using Components.UI.DevConsole;

namespace TestsRunner.Tests
{
    public class ArrayCopy_Managed_Test
    {
        public void Start(int count)
        {
            var array1 = new int[count];
            var array2 = new int[count];

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (var i = 0; i < count; i++)
            {
                array1[i] = array2[i];
            }

            stopwatch.Stop();
            DevConsole.WriteLine($"{ToString()} - {stopwatch.ElapsedTicks} ticks");
        }
    }
}
