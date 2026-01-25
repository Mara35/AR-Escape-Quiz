using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private float startTime;
    private float finalTime;
    private bool timerRunning;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartTimer()
    {
        startTime = Time.time;
        timerRunning = true;
        Debug.Log("Timer started");
    }

    public void StopTimer()
    {
        if (!timerRunning) return;

        timerRunning = false;
        finalTime = Time.time - startTime;
        Debug.Log("Timer stopped: " + finalTime);
    }

    public float GetFinalTime()
    {
        return finalTime;
    }

    public void ResetTimer()
    {
        timerRunning = false;
        finalTime = 0f;
    }
}
