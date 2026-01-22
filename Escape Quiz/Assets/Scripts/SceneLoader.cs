using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadStart()
    {
        if (GameManager.Instance != null)
            QuizGameManager.Instance.ResetGame(); 
            GameManager.Instance.ResetTimer();
        SceneManager.LoadScene("StartScene");
    }

    public void LoadInstructions()
    {
        SceneManager.LoadScene("InstructionScene");
    }

  public void LoadFirstHint()
    {
        SceneManager.LoadScene("FirstHintRules");
    }

     public void LoadSecondHint()
    {
        SceneManager.LoadScene("SecondHintSocket");
    }


    public void LoadFourthHint()
    {
        SceneManager.LoadScene("FourthSceneDoor");
    }

    public void LoadThirdHint()
    {
        SceneManager.LoadScene("ThirdHintPipe");
    }
    public void LoadSampleScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    public void LoadErrorHint()
    {
        SceneManager.LoadScene("ErrorScene");
    }
    public void LoadSampleScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void LoadGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartTimer();
        }

        SceneManager.LoadScene("SampleScene");
    }

    public void LoadNextHint()
{
    int index = QuizGameManager.Instance.currentQuizIndex;

    switch (index)
    {
        case 1:
            LoadFirstHint();
            break;
        case 2:
            LoadSecondHint();
            break;
        case 3:
            LoadThirdHint();
            break;
        case 4:
            LoadFourthHint();
            break;
        default:
            SceneManager.LoadScene("VictoryScene");
            break;
    }
}

}
