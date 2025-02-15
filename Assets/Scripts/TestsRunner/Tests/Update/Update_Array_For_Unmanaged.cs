﻿using System.Diagnostics;
using Components.UI.DevConsole;
using Unity.Mathematics;

namespace TestsRunner.Tests.Update
{
    public class Update_Array_For_Unmanaged
    {
        private struct Data
        {
            public int A;
            public float B;
        }

        public unsafe void Start(int count)
        {
            var type = "Struct[]";
            var body = "Calc ptr";

            var input = new int[count];
            var output = new Data[count];
            var r = new Random(85673057);

            for (var i = 0; i < count; i++)
            {
                input[i] = r.NextInt();
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            fixed (int* ptr = &input[0])
            {
                fixed (Data* ptr2 = &output[0])
                {
                    for (var i = 0; i < count; i++)
                    {
                        var n = input[i];
                        output[i].A = n + n;
                        output[i].B = n - n / 2f;
                    }
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
