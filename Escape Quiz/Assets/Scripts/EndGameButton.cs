using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameButton : MonoBehaviour
{
    public void EndGame()
    {
        Debug.Log("Spielende über Test-Button");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.StopTimer();
        }

        SceneManager.LoadScene("VictoryScene");
    }
}
