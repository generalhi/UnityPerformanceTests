using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

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
    void Start()
    {
        StartJob();
    }

    private void StartJob()
    {
        var length = 10;
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
        var handle = job.Schedule(length, 64);
        handle.Complete();
        Debug.Log($"[{input[0]}] [{output[0]}]");
    }
}
