using Unity.Collections;
using Unity.Jobs;
using UnityEngine;


public class Job : MonoBehaviour
{
    NativeArray<int> intArray;

    void Start()
    {
        intArray = new NativeArray<int>(new int[] { 4, 15, 1, 18 }, Allocator.Persistent);

        MyJob myJob = new MyJob()
        {
            intArr = intArray,
        };

        JobHandle jobHandle = myJob.Schedule();
        jobHandle.Complete();

        for (int i = 0; i < intArray.Length; i++)
        {
            Debug.Log(intArray[i]);
        }
    }

    private void OnDestroy()
    {
        intArray.Dispose();
    }

    public struct MyJob : IJob
    {
        public NativeArray<int> intArr;

        public void Execute()
        {
            for (int i = 0; i < intArr.Length; i++)
            {
                if (intArr[i] > 10)
                    intArr[i] = 0;
            }
        }
    }
}

