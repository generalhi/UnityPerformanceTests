using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public struct JobSimple : IJob
{
    public NativeArray<int> Data;
        
    public void Execute()
    {
        Data[1] = Data[0] + 1;
    }
}

public class JobSimpleTest : MonoBehaviour
{
    void Start()
    {
        StartJob();
    }

    private void StartJob()
    {
        var array = new NativeArray<int>(2, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
        array[0] = 1;
        var job = new JobSimple {Data = array};
        var handle = job.Schedule();
        handle.Complete();
        //Debug.Log($"[{array[0]}] [{array[1]}]");
        array.Dispose();
    }
}
