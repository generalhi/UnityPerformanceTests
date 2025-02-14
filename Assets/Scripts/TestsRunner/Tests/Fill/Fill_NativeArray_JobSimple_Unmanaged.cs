using System.Diagnostics;
using Components.UI.DevConsole;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace TestsRunner.Tests.Fill
{
    public class Fill_NativeArray_JobSimple_Unmanaged
    {
        private unsafe struct JobSimple : IJob
        {
            [NativeDisableUnsafePtrRestriction]
            public int* Ptr;

            public int Count;
            public int Value;

            public void Execute()
            {
                for (var i = 0; i < Count; i++)
                {
                    Ptr[i] = Value;
                }
            }
        }

        public unsafe void Start(int count)
        {
            var array = new NativeArray<int>(count, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
            var job = new JobSimple {Ptr = (int*) array.GetUnsafePtr(), Count = count, Value = 1};

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var handle = job.Schedule();
            handle.Complete();

            stopwatch.Stop();
            DevConsole.WriteLine($"{GetType().Name,TestRunner.MethodNameSpace} - {stopwatch.ElapsedTicks} ticks");

            array.Dispose();
        }
    }
}
