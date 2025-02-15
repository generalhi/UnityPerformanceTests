using System.Diagnostics;
using Components.UI.DevConsole;

namespace TestsRunner.Tests.Copy
{
    public unsafe class Copy_Array_For_Unmanaged
    {
        public void Start(int count)
        {
            var type = "int[]";
            var body = "ptr1[i] = ptr2[i]";

            var array1 = new int[count];
            var array2 = new int[count];

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            fixed (int* ptr1 = &array1[0], ptr2 = &array2[0])
            {
                for (var i = 0; i < count; i++)
                {
                    ptr1[i] = ptr2[i];
                }
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
