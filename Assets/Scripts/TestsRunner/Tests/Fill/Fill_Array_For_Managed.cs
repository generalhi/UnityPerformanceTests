using System.ComponentModel;
using System.Diagnostics;
using Components.UI.DevConsole;
using Unity.VisualScripting;

namespace TestsRunner.Tests
{
    public class Fill_Array_For_Managed
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
            DevConsole.WriteLine($"{GetType().Name} - {stopwatch.ElapsedTicks} ticks");
        }
    }
}
