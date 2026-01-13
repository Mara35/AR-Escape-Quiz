using UnityEngine;

public class TargetObject : MonoBehaviour
{
    [Header("Target Settings")]
    public bool shouldGoIntoHouse = true;

    private bool hasBeenHandled = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasBeenHandled)
            return;

        // House / target object
        if (other.CompareTag("Target"))
        {
            if (shouldGoIntoHouse)
            {
                hasBeenHandled = true;

                // Notify puzzle manager
                FindObjectOfType<PuzzleProgress>().RegisterCorrectObject();

                // Despawn object
                gameObject.SetActive(false);
            }
            // Forbidden objects do nothing
        }
    }
}
