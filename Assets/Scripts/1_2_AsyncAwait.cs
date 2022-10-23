using System.Threading.Tasks;
using UnityEngine;

public class AsyncAwait : MonoBehaviour
{
    void Start()
    {
        TaskDelaySeconds(1);
        TaskDelayFrames(60);
    }

    async void TaskDelaySeconds(int seconds)
    {
        await Task.Delay(seconds * 1000);
        Debug.Log("Task 1 completed!");
    }

    async void TaskDelayFrames(int frames)
    {
        for (int i = 0; i <= frames; i++)
        {
            await Task.Yield();
        }
        Debug.Log("Task 2 completed!");
    }
}
