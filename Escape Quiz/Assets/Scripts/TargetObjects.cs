using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetObject : MonoBehaviour
{
    public bool shouldGoIntoHouse = true;

    private static int collectedCount = 0;
    private static int requiredCount = 2;
    private static bool hasReset = false;

    private bool handled = false;

    private void Start()
    {
        if (!hasReset)
        {
            collectedCount = 0;
            hasReset = true;
            Debug.Log("Counter reset");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (handled)
            return;

        if (!other.CompareTag("Target"))
            return;

        if (!shouldGoIntoHouse)
            return;

        handled = true;
        collectedCount++;

        Debug.Log($"Collected {collectedCount}/{requiredCount}");

        gameObject.SetActive(false);

        if (collectedCount >= requiredCount)
        {
            Debug.Log("Quiz complete → loading next scene");
            SceneManager.LoadScene("FourthSceneDoor");
        }
    }
}
