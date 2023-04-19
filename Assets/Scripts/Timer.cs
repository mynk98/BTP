using UnityEngine;

public class Timer : MonoBehaviour
{
    public static float currentTime = 0;
    public static bool isTimerRunning = false;
    public static int startMin;
    public static int startSec;
    public static int startHr;
    public static int startDay;
    public static int startMonth;
    public static float timeElapsed = 0;

    private void Start()
    {
        Application.runInBackground = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning)
        {
            timeElapsed += Time.unscaledDeltaTime;
            if (timeElapsed >= 1)
            {
                currentTime += timeElapsed;
                timeElapsed = 0;
            }
        }

    }


    public static void StartTimer()
    {
        isTimerRunning = true;
        timeElapsed = 0;
        currentTime = 0;
    }

    public static void StopTimer()
    {
        isTimerRunning = false;
    }

    public static void SkipTime(float time)
    {
        currentTime += time;
    }
}
