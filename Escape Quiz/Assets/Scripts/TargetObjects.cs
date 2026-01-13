using UnityEngine;

public class TargetObject : MonoBehaviour
{
    public bool shouldGoIntoHouse = true;
    private bool hasBeenHandled = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[TargetObject] Trigger ENTER on {name} with {other.name}");

        if (hasBeenHandled)
        {
            Debug.Log("[TargetObject] Already handled, ignoring");
            return;
        }

        if (other.CompareTag("Target"))
        {
            Debug.Log("[TargetObject] Target tag matched");

            if (shouldGoIntoHouse)
            {
                Debug.Log("[TargetObject] Object is allowed → handling");

                hasBeenHandled = true;

                PuzzleProgress puzzle = FindObjectOfType<PuzzleProgress>();
                if (puzzle == null)
                {
                    Debug.LogError("[TargetObject] PuzzleProgress NOT FOUND");
                }
                else
                {
                    Debug.Log("[TargetObject] PuzzleProgress FOUND → registering object");
                    puzzle.RegisterCorrectObject();
                }

                Debug.Log("[TargetObject] Deactivating object");
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("[TargetObject] Object is FORBIDDEN → doing nothing");
            }
        }
        else
        {
            Debug.Log("[TargetObject] Other collider is NOT Target");
        }
    }
}
