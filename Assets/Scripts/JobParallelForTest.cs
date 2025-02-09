using System.Diagnostics;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Debug = UnityEngine.Debug;

[BurstCompile]
public struct JobParallelFor : IJobParallelFor
{
    [ReadOnly]
    public NativeArray<int> Input;

    [WriteOnly]
    public NativeArray<int> Output;

    public void Execute(int i)
    {
        Output[i] = Input[0] + 1;
    }
}

public class JobParallelForTest : MonoBehaviour
{
    private Stopwatch _time = new Stopwatch();
    
    void Start()
    {
        StartJob();
    }

    private void StartJob()
    {
        var length = 10000000;
        var input = new NativeArray<int>(length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
        var output = new NativeArray<int>(length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
        for (var i = 0; i < length; i++)
        {
            input[i] = i;
        }
        var job = new JobParallelFor
        {
            Input = input,
            Output = output
        };
        
        _time.Start();
        var handle = job.Schedule(length, 64);
        handle.Complete();
        _time.Stop();
        
        //Debug.Log($"[{input[0]}] [{output[0]}]");
        Debug.Log($"{_time.ElapsedMilliseconds}ms");
        input.Dispose();
        output.Dispose();
    }
}
