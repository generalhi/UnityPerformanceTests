﻿using System.Diagnostics;
using Components.UI.DevConsole;
using Unity.Burst;

namespace TestsRunner.Tests.Copy
{
    [BurstCompile]
    public unsafe class Copy_Array_For_Unmanaged_Burst
    {
        public void Start(int count)
        {
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
            DevConsole.WriteLine($"{GetType().Name,TestRunner.MethodNameSpace} - {stopwatch.ElapsedTicks} ticks");
        }
    }
}
