using UnityEngine;

public class TargetObject : MonoBehaviour
{
    public bool isCorrectlyPlaced = false;
    public bool shouldGoIntoHouse = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            isCorrectlyPlaced = shouldGoIntoHouse;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            isCorrectlyPlaced = false;
        }
    }
}
