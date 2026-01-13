using UnityEngine;

public class TargetObject : MonoBehaviour
{
    public bool shouldGoIntoHouse = true;

    private bool hasBeenHandled = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasBeenHandled)
            return;

        if (other.CompareTag("Target"))
        {
            if (shouldGoIntoHouse)
            {
                HandleCorrectPlacement();
            }
            else
            {
                HandleForbiddenPlacement();
            }
        }
    }

    void HandleCorrectPlacement()
    {
        hasBeenHandled = true;

        // Objekt verschwinden lassen
        gameObject.SetActive(false);
    }

    void HandleForbiddenPlacement()
    {
       
        Debug.Log($"{name} not allowed");
    }
}
