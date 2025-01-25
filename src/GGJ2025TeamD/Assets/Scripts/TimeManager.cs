using UnityEngine;

public static class TimeManager
{
    public static void TimeStop()
    {
        Time.timeScale = 0f;
    }
    
    public static void TimeResume()
    {
        Time.timeScale = 1f;
    }
}
