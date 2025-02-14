using System.Diagnostics;
using Components.UI.DevConsole;

namespace TestsRunner.Tests.Fill
{
    public unsafe class Fill_Array_For_Unmanaged
    {
        public void Start(int count)
        {
            var array = new int[count];
            var value = 1;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            fixed (int* ptr = &array[0])
            {
                for (var i = 0; i < count; i++)
                {
                    ptr[i] = value;
                }
            }

            stopwatch.Stop();
            DevConsole.WriteLine($"{GetType().Name,TestRunner.MethodNameSpace} - {stopwatch.ElapsedTicks} ticks");
        }
    }
}
