using UnityEngine;
using UnityEngine.SceneManagement;

public class ScanTarget : MonoBehaviour
{
    // Set by TrackedImageListener
    public int quizID;

    public void OnImageScanned()
    {
        if (QuizGameManager.Instance == null)
            return;

        // Ignore scans only AFTER correct image was used
        if (QuizGameManager.Instance.scanLocked)
            return;

        if (QuizGameManager.Instance.IsCorrectScan(quizID))
        {
            // Correct image → lock and go to quiz
            QuizGameManager.Instance.LockAfterCorrectScan();
            SceneManager.LoadScene("QuizScene"); 
        }
        else
        {
            // Wrong image → show error
            FindObjectOfType<SceneLoader>().LoadErrorHint();
        }
    }
}
