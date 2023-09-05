using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

public struct EC : IJob
{
    public NativeArray<float> value;
    public void Execute()
    {
        for (int i = 0; i < 10000000; i++)
        {
            float f = Mathf.Sqrt(Mathf.Pow(10f, 100000f)) / 10000000f;
        }
    }
}
public class jobpet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NativeArray  <float> values = new NativeArray<float>(1,Allocator.TempJob);

        EC job = new EC
        {
            value = values
        };

        JobHandle jobHandle = job.Schedule();
        jobHandle.Complete();
        print(job.value[0] );
        values.Dispose();
    }
}
