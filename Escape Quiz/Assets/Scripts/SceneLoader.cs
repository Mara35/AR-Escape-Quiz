using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadStart()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void LoadInstructions()
    {
        SceneManager.LoadScene("InstructionScene");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
