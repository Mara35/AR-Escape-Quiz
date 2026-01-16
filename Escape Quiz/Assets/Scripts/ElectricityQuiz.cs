using UnityEngine;
using TMPro;

public class ElectricityQuiz : MonoBehaviour
{
    [Header("UI")]
    public GameObject quizPopup;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI feedbackText;

    [Header("Correct Answer Index")]
    public int correctAnswerIndex = 1; // 0=A, 1=B, 2=C

    private SceneLoader sceneLoader;
    public bool IsQuizOpen => quizPopup.activeSelf;


    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        quizPopup.SetActive(false);
        feedbackText.text = "";
    }

    public void ShowQuiz()
    {
        quizPopup.SetActive(true);
        questionText.text = "Which unit is used to measure electric current?";
        feedbackText.text = "";
    }

    public void SelectAnswer(int index)
    {
        if (index == correctAnswerIndex)
        {
            feedbackText.text = "Correct! ⚡";
            Invoke(nameof(LoadNextHint), 1.0f);
        }
        else
        {
            feedbackText.text = "Wrong answer. Try again!";
        }
    }

    private void LoadNextHint()
    {
        quizPopup.SetActive(false);
        sceneLoader.LoadThirdHint(); // or LoadThirdHint / LoadFourthHint
    }
}
