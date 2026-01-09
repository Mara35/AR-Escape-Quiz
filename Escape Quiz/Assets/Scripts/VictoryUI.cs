using UnityEngine;
using TMPro;

public class VictoryUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    void Start()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager nicht gefunden!");
            return;
        }

        float time = GameManager.Instance.GetFinalTime();
        timeText.text = FormatTime(time);
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return $"{minutes:00}:{seconds:00}";
    }
}
