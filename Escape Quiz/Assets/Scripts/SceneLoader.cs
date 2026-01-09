using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadStart()
    {
        if (GameManager.Instance != null)
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

    public void LoadFourthHint()
    {
        SceneManager.LoadScene("FourthSceneDoor");
    }

    public void LoadGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartTimer();
        }

        SceneManager.LoadScene("SampleScene");
    }
}
