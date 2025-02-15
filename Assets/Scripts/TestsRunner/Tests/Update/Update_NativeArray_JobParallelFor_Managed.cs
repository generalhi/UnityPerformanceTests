using System.Diagnostics;
using Components.UI.DevConsole;
using Unity.Collections;
using Unity.Jobs;

namespace TestsRunner.Tests.Copy
{
    public class Update_NativeArray_JobParallelFor_Managed
    {
        private struct Data
        {
            public int A;
            public float B;
        }

        private struct JobParallelFor : IJobParallelFor
        {
            public NativeArray<int> Input;
            public NativeArray<Data> Output;

            public void Execute(int i)
            {
                var n = Input[i];
                var d = Output[i];
                d.A = n + n;
                d.B = n - n / 2f;
                Output[i] = d;
            }
        }

        public void Start(int count)
        {
            var type = "NativeArray<Struct>()";
            var body = "Calc";

            var input = new NativeArray<int>(count, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
            var output = new NativeArray<Data>(count, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);

            var job = new JobParallelFor {Input = input, Output = output};

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var handle = job.Schedule(count, 64);
            handle.Complete();

            stopwatch.Stop();
            DevConsole.WriteLine(
                $"{GetType().Name,TestRunner.MethodNameLength} | " +
                $"{type,TestRunner.TypeLength} | " +
                $"{body,TestRunner.BodyLength} | " +
                $"{stopwatch.ElapsedTicks} ticks");

            input.Dispose();
            output.Dispose();
        }
    }
}
