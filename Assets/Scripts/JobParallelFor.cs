using Unity.Collections;
using Unity.Jobs;
using UnityEngine;


public class JobParallelFor : MonoBehaviour
{
    NativeArray<int> positionsArray;
    NativeArray<int> velocitiesArray;
    NativeArray<int> finalPositionsArray;

    void Start()
    {
        positionsArray = new NativeArray<int>(new int[] { 1, 3, 5 }, Allocator.Persistent);
        velocitiesArray = new NativeArray<int>(new int[] { 1, 3, 5 }, Allocator.Persistent);
        finalPositionsArray = new NativeArray<int>(new int[] { 0, 0, 0 }, Allocator.Persistent);

        MyJob myJob = new MyJob()
        {
            positionsArr = positionsArray,
            velocitiesArr = velocitiesArray,
            finalPositionsArr = finalPositionsArray,
        };

        JobHandle jobHandle = myJob.Schedule(finalPositionsArray.Length, 0);
        jobHandle.Complete();

        for (int i = 0; i < finalPositionsArray.Length; i++)
        {
            Debug.Log(finalPositionsArray[i]);
        }
    }

    private void OnDestroy()
    {
        positionsArray.Dispose();
        velocitiesArray.Dispose();
        finalPositionsArray.Dispose();
    }

    public struct MyJob : IJobParallelFor
    {
        [ReadOnly]
        public NativeArray<int> positionsArr;
        [ReadOnly]
        public NativeArray<int> velocitiesArr;
        //[WriteOnly]
        public NativeArray<int> finalPositionsArr;

        public void Execute(int index)
        {
            for (int i = 0; i < finalPositionsArr.Length; i++)
            {
                finalPositionsArr[i] = positionsArr[i] + velocitiesArr[i];
            }
        }
    }
}

