using System.Diagnostics;
using Components.UI.DevConsole;
using Unity.Collections;
using Unity.Jobs;

namespace TestsRunner.Tests
{
    public class ArrayFill_JobSimple_Test
    {
        private struct JobSimple : IJob
        {
            public NativeArray<int> Data;
            public int Value;

            public void Execute()
            {
                for (var i = 0; i < Data.Length; i++)
                {
                    Data[i] = Value;
                }
            }
        }

        public void Start(int count)
        {
            var array = new NativeArray<int>(count, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
            var job = new JobSimple
            {
                Data = array,
                Value = 1
            };

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var handle = job.Schedule();
            handle.Complete();

            stopwatch.Stop();
            DevConsole.WriteLine($"{ToString()} - {stopwatch.ElapsedTicks} ticks");

            array.Dispose();
        }
    }
}
