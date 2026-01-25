using UnityEngine;

public class QuizGameManager : MonoBehaviour
{
    public static QuizGameManager Instance;

    // Current step in the game (0 = first image)
    public int currentQuizIndex = 0;

    // Lock scanning after correct image
    public bool scanLocked = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            ResetGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Called when a new game starts
    public void ResetGame()
    {
        currentQuizIndex = 0;
        scanLocked = false;
    }

    // Check if scanned image is the correct one
    public bool IsCorrectScan(int quizID)
    {
        return quizID == currentQuizIndex;
    }

    // Call this after quiz was solved correctly
    public void QuizSolved()
    {
        currentQuizIndex++;
        scanLocked = false; 
    }

    // Lock scanning after correct image scan
    public void LockAfterCorrectScan()
    {
        scanLocked = true;
    }
}
