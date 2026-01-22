using UnityEngine;

public class QuizGameManager : MonoBehaviour
{
    public static QuizGameManager Instance;

    [Header("Quiz Progress")]
    public int currentQuizIndex = 0;
    public int totalQuizzes = 4;

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

   
    // RESET
  
    public void ResetGame()
    {
        currentQuizIndex = 0;
    }

    
    // QUIZ LOGIC
    
    public bool IsCorrectScan(int quizID)
    {
        return quizID == currentQuizIndex;
    }

    public void QuizSolved()
    {
        currentQuizIndex++;
    }
}
