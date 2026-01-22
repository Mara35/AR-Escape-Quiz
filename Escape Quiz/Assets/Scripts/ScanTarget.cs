using UnityEngine;
using UnityEngine.SceneManagement;

public class ScanTarget : MonoBehaviour
{
    // Order of this image (0 = first quiz)
    public int quizID;

    // Quiz scene to load if scan is correct
    public string quizSceneName;

    public void OnImageScanned()
    {
        if (QuizGameManager.Instance == null)
        {
            Debug.LogError("QuizGameManager not found");
            return;
        }

        if (QuizGameManager.Instance.IsCorrectScan(quizID))
        {
            // Correct image → load quiz scene
            SceneManager.LoadScene(quizSceneName);
        }
        else
        {
            // Wrong image → load error scene via SceneLoader
            FindObjectOfType<SceneLoader>().LoadErrorHint();
        }
    }
}
