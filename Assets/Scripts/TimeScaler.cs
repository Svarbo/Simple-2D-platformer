using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    public void StartTime()
    {
        Time.timeScale = 1;
    }

    public void StopTime()
    {
        Time.timeScale = 0;
    }
}
